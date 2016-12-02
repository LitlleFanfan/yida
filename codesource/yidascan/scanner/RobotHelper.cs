using ProduceComm;
using ProduceComm.OPC;
using RobotControl;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using yidascan.DataAccess;

namespace yidascan {
    public enum PanelState {
        LessHalf,
        HalfFull,
        Full
    }
    public class RollPosition {
        public RollPosition(string side, string locationNo, PanelState pnlState, decimal x, decimal y, decimal z, decimal rz) {
            X = x;
            Y = y;
            Z = z;
            Rz = rz;
            ChangeAngle = x > 0 || y < 0;

            ToLocation = locationNo;
            int baseindex = CalculateBaseIndex(x, rz);
            LocationNo = int.Parse(locationNo.Substring(1, 2));
            BaseIndex = 4 * (LocationNo - 1) + baseindex + 1;

            Side = side;
            PnlState = pnlState;

            int tmp = int.Parse(locationNo.Substring(1, 2));
            RobotParam origin = RobotParam.GetOrigin(tmp);
            Origin = new PostionVar(0, 0, 0, origin.Rx, origin.Ry, origin.Rz + rz);

            Target = new PostionVar(x, y, z, origin.Rx, origin.Ry, origin.Rz + rz);
        }


        private int CalculateBaseIndex(decimal x, decimal rz) {
            int baseindex = 0;
            if (x != 0) {
                baseindex = 2;
                if (rz < 0) {
                    baseindex += 1;
                }
            } else {
                if (rz < 0) {
                    baseindex += 1;
                }
            }

            return baseindex;
        }

        public int LocationNo;
        public int BaseIndex;
        public bool ChangeAngle;

        public PostionVar Target;
        public PostionVar Origin;

        public decimal X;
        public decimal Y;
        public decimal Z;
        public decimal Rz;

        public string ToLocation { get; set; }
        // A侧或B侧
        public string Side { get; set; }
        public PanelState PnlState { get; set; }
    }

    public class RobotHelper : IDisposable {
        private string JOB_NAME = "";

        RobotControl.RobotControl rCtrl;
        public static RobotJobQueue robotJobs = new RobotJobQueue();

        public const int DELAY = 5;

        OPCClient client = null;
        OPCParam param = null;
        MessageCenter msg;

        public RobotHelper(string ip, string jobName) {
            try {
                // FrmMain.logOpt.ViewInfo("11");
                rCtrl = new RobotControl.RobotControl(ip);
                // FrmMain.logOpt.ViewInfo("22");
                rCtrl.Connect();
                // FrmMain.logOpt.ViewInfo("33");
                rCtrl.ServoPower(true);
                JOB_NAME = jobName;
            } catch (Exception ex) {
                clsSetting.loger.Error(ex);
            }
        }

        public bool IsConnected() {
            return rCtrl.Connected;
        }

        public void WritePosition(RollPosition rollPos) {
            rCtrl.SetVariables(RobotControl.VariableType.B, 10, 1, rollPos.ChangeAngle ? "1" : "0");
            rCtrl.SetVariables(RobotControl.VariableType.B, 0, 1, rollPos.LocationNo.ToString());

            // 原点高位旋转
            rCtrl.SetPostion(RobotControl.PosVarType.Robot,
                rollPos.Origin, 100, RobotControl.PosType.User, 0, rollPos.LocationNo);

            //基座
            rCtrl.SetPostion(RobotControl.PosVarType.Base,
                new RobotControl.PostionVar(rollPos.Target.axis7, 0, 0, 0, 0),
                0, RobotControl.PosType.Robot, 0, 0);

            // 目标位置
            rCtrl.SetPostion(RobotControl.PosVarType.Robot,
               rollPos.Target, 101, RobotControl.PosType.User, 0, rollPos.LocationNo);
        }

        public void RunJob(string jobName) {
            rCtrl.StartJob(jobName);
        }

        public bool IsBusy() {
            Dictionary<string, bool> status = rCtrl.GetPlayStatus();
            if (status.Count == 0) { return true; } else {
                return (status["Start"] || status["Hold"]);
            }
        }

        private bool IsSafePlace() {
            Dictionary<string, string> b1 = rCtrl.GetVariables(VariableType.B, 1, 1);
            if (b1.Count == 0) { return false; } else {
                return b1["b1"] == "1";
            }
        }

        private void NotifyOpcSafePlace(string side) {
            lock (client) {
                client.Write(side == "A" ? param.RobotCarryA.Signal : param.RobotCarryB.Signal, false);
            }
        }

        private void NotifyOpcJobFinished(PanelState pState, string tolocation) {
            switch (pState) {
                case PanelState.HalfFull:
                    lock (client) {
                        client.Write(param.BAreaFloorFinish[tolocation], true);
                    }
                    break;
                case PanelState.Full:
                    lock (client) {
                        client.Write(param.BAreaPanelFinish[tolocation], true);
                    }
                    break;
                case PanelState.LessHalf:
                default:
                    break;
            }
        }

        public void JobLoop(ref bool isrun) {
            while (isrun) {
                if (robotJobs.Rolls.Count > 0) {
                    while (isrun && IsBusy()) {
                        Thread.Sleep(OPCClient.DELAY);
                    }

                    var roll = robotJobs.GetRoll();

                    while (isrun && !PanelAvailable(roll.ToLocation)) {
                        Thread.Sleep(OPCClient.DELAY * 200);
                    }

                    // 启动机器人动作。
                    WritePosition(roll);

                    lock (client) {
                        // 等待板可放料
                        while (isrun) {
                            lock (client) {
                                var v = client.ReadInt(param.BAreaPanelState[roll.ToLocation]);
                                if (v == 2) { break; }
                            }
                            Thread.Sleep(DELAY * 40); // 200 ms.
                        }

                        RunJob(JOB_NAME);

                        // 等待安全位置信号
                        //while (!IsSafePlace()) { Thread.Sleep(RobotHelper.DELAY * 10); }
                        // 告知OPC
                        //NotifyOpcSafePlace(roll.Side);

                        // 等待完成信号
                        while (IsBusy()) { Thread.Sleep(RobotHelper.DELAY * 10); }
                        // 告知OPC
                        NotifyOpcJobFinished(roll.PnlState, roll.ToLocation);
                    }
                    Thread.Sleep(RobotHelper.DELAY * 400);
                }
            }
        }

        private bool PanelAvailable(string tolocation) {
            lock (client) {
                return client.ReadString(param.BAreaPanelState[tolocation]) == "2";
            }
        }

        public Dictionary<string, string> AlarmTask() {
            Dictionary<string, bool> s = rCtrl.GetAlarmStatus();
            if (s.Count != 0) {
                if (s["Error"] || s["Alarm"]) {
                    return rCtrl.GetAlarmCode();
                }
            }
            return new Dictionary<string, string>();
        }

        public void Dispose() {
            rCtrl.ServoPower(false);
            rCtrl.Close();
        }
    }
}
