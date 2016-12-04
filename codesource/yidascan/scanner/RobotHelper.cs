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
            Index = CalculateBaseIndex(x, y);

            LocationNo = int.Parse(locationNo.Substring(1, 2));
            BaseIndex = 4 * (LocationNo - 1) + Index + 1;

            Side = side;
            PnlState = pnlState;

            int tmp = int.Parse(locationNo.Substring(1, 2));
            RobotParam origin = RobotParam.GetOrigin(tmp);
            RobotParam point = RobotParam.GetPoint(tmp, Index);

            Base1 = origin.Base;
            Base2 = point.Base;
            XOffSet = GetXOffSet(origin.Base, point.Base);
            X = X + XOffSet;
            Origin = new PostionVar(XOffSet, 0, 1750, origin.Rx, origin.Ry, origin.Rz + rz);
            Target = new PostionVar(X, Y, Z, origin.Rx, origin.Ry, origin.Rz + rz);
        }

        private decimal GetXOffSet(decimal originBase, decimal targetBase) {
            return ((targetBase - originBase) * 2 * -1);
        }

        public static decimal GetToolOffSet(decimal xory) {
            decimal toolLen = 250;
            if (xory > 0) {
                return xory + toolLen;
            } else {
                return xory - toolLen;
            }
        }

        private int CalculateBaseIndex(decimal x, decimal y) {
            int baseindex = 0;
            if (x != 0) {
                baseindex = 2;
                if (x > 0) {
                    baseindex += 1;
                }
            } else {
                if (y < 0) {
                    baseindex += 1;
                }
            }

            return baseindex;
        }

        public int LocationNo;
        public int Index;
        public int BaseIndex;
        public bool ChangeAngle;

        public PostionVar Target;
        public PostionVar Origin;

        public decimal X;
        public decimal Y;
        public decimal Z;
        public decimal Rz;
        public decimal XOffSet;
        public decimal Base1;
        public decimal Base2;

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

        public RobotHelper(string ip, string jobName) {
            try {
                rCtrl = new RobotControl.RobotControl(ip);
                rCtrl.Connect();
                rCtrl.ServoPower(true);
                JOB_NAME = jobName;
            } catch (Exception ex) {
                clsSetting.loger.Error(ex);
            }
        }

        public bool IsConnected() {
            return rCtrl.IsConnected();
        }

        public void WritePosition(RollPosition rollPos) {
            rCtrl.SetVariables(RobotControl.VariableType.B, 10, 1, rollPos.ChangeAngle ? "1" : "0");
            rCtrl.SetVariables(RobotControl.VariableType.B, 0, 1, rollPos.BaseIndex.ToString());

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
            //FrmMain.logOpt.ViewInfo(string.Format("{0}", Newtonsoft.Json.JsonConvert.SerializeObject(status)));
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
                FrmMain.logOpt.ViewInfo("JobLoop Head");
                if (robotJobs.Rolls.Count > 0) {
                    while (isrun && IsBusy()) {
                        Thread.Sleep(OPCClient.DELAY);
                    }

                    var roll = robotJobs.GetRoll();

                    FrmMain.logOpt.ViewInfo(string.Format("roll:{0}", Newtonsoft.Json.JsonConvert.SerializeObject(roll)));
                    // 等待板可放料
                    //while (isrun && !PanelAvailable(roll.ToLocation)) {
                    //    FrmMain.logOpt.ViewInfo("1111。");
                    //    Thread.Sleep(OPCClient.DELAY * 400);
                    //}

                    FrmMain.logOpt.ViewInfo("启动机器人动作。");
                    // 启动机器人动作。
                    WritePosition(roll);

                    RunJob(JOB_NAME);

                    Thread.Sleep(RobotHelper.DELAY * 1000);

                    // 等待安全位置信号
                    //while (!IsSafePlace()) { Thread.Sleep(RobotHelper.DELAY * 10); }

                    // 告知OPC
                    //NotifyOpcSafePlace(roll.Side);

                    // 等待完成信号
                    while (isrun && IsBusy()) {
                        FrmMain.logOpt.ViewInfo("Working");
                        Thread.Sleep(RobotHelper.DELAY * 200);
                    }
                    // 告知OPC
                    NotifyOpcJobFinished(roll.PnlState, roll.ToLocation);
                }
                FrmMain.logOpt.ViewInfo("JobLoop End");
                Thread.Sleep(RobotHelper.DELAY * 400);
            }
        }

        private bool PanelAvailable(string tolocation) {
            lock (client) {
                string s = client.ReadString(param.BAreaPanelState[tolocation]);
                FrmMain.logOpt.ViewInfo(string.Format("{0}:{1}", param.BAreaPanelState[tolocation], s));
                return s == "2";
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
            Thread.Sleep(1000);
            rCtrl.Close();
        }
    }
}
