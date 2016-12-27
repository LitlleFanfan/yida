using ProduceComm;
using ProduceComm.OPC;
using RobotControl;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using yidascan.DataAccess;

namespace yidascan {
    public enum PanelState {
        LessHalf,
        HalfFull,
        Full
    }
    public class RollPosition {
        public RollPosition(string label, string side, string locationNo, PanelState pnlState, decimal x, decimal y, decimal z, decimal rz) {
            this.LabelCode = label;

            X = x;
            Y = y;
            Z = z;
            Rz = rz;
            ChangeAngle = x > 0 || y < 0;

            ToLocation = locationNo;
            Index = CalculateBaseIndex(locationNo, x, y);

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
            Origin = new PostionVar(XOffSet, 0, 1700, origin.Rx, origin.Ry, origin.Rz + rz);
            Target = new PostionVar(X, Y, Z, origin.Rx, origin.Ry, origin.Rz + rz);
        }

        private decimal GetXOffSet(decimal originBase, decimal targetBase) {
            return ((targetBase - originBase) * 2 * -1);
        }

        public static decimal GetToolOffSet(decimal xory) {
            decimal toolLen = 250;
            if (xory >= 0) {
                return xory + toolLen;
            } else {
                return xory - toolLen;
            }
        }


        public static List<string> robotRSidePanel = new List<string>() { "B03", "B04", "B05", "B06", "B07", "B08" };
        private int CalculateBaseIndex(string tolocation, decimal x, decimal y) {
            int baseindex = 0;
            if (x != 0) {
                baseindex = 2;
                if (x > 0) {
                    baseindex += 1;
                }
            } else {
                if (robotRSidePanel.Contains(tolocation) && y > 0) {
                    baseindex += 1;
                } else if (!robotRSidePanel.Contains(tolocation) && y < 0) {
                    baseindex += 1;
                }
            }

            return baseindex;
        }

        public bool IsSameLabel(RollPosition roll) {
            return this.LabelCode == roll.LabelCode;
        }

        public string LabelCode;

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
            rCtrl.SetVariables(RobotControl.VariableType.B, 5, 1, "1");

            // 原点高位旋转
            if (rCtrl.SetPostion(RobotControl.PosVarType.Robot,
                rollPos.Origin, 100, RobotControl.PosType.User, 0, rollPos.LocationNo)) {
                rCtrl.SetPostion(RobotControl.PosVarType.Robot,
                    rollPos.Origin, 100, RobotControl.PosType.User, 0, rollPos.LocationNo);
            }

            //基座
            if (rCtrl.SetPostion(RobotControl.PosVarType.Base,
                new RobotControl.PostionVar(rollPos.Base2 * 1000, 0, 0, 0, 0),
                0, RobotControl.PosType.Robot, 0, 0)) {
                rCtrl.SetPostion(RobotControl.PosVarType.Base,
                    new RobotControl.PostionVar(rollPos.Base2 * 1000, 0, 0, 0, 0),
                    0, RobotControl.PosType.Robot, 0, 0);
            }

            // 目标位置
            if (rCtrl.SetPostion(RobotControl.PosVarType.Robot,
               rollPos.Target, 101, RobotControl.PosType.User, 0, rollPos.LocationNo)) {
                rCtrl.SetPostion(RobotControl.PosVarType.Robot,
                    rollPos.Target, 101, RobotControl.PosType.User, 0, rollPos.LocationNo);
            }
        }

        public void RunJob(string jobName) {
            rCtrl.StartJob(jobName);
        }

        public bool IsBusy() {
            try {
                Dictionary<string, bool> status = rCtrl.GetPlayStatus();
                FrmMain.logOpt.Write(string.Format("{0}", Newtonsoft.Json.JsonConvert.SerializeObject(status)), LogType.ROBOT_STACK, LogViewType.OnlyFile);
                if (status == null || status.Count == 0) { return true; } else {
                    return (status["Start"] || status["Hold"]);
                }
            } catch (Exception ex) {
                FrmMain.logOpt.Write(string.Format("{0}", ex), LogType.ROBOT_STACK, LogViewType.OnlyFile);
                return true;
            }
        }

        private void NotifyOpcSafePlace(string side) {
            lock (FrmMain.opcClient) {
                FrmMain.opcClient.Write(side == "A" ? FrmMain.opcParam.RobotCarryA.Signal : FrmMain.opcParam.RobotCarryB.Signal, false);
            }
        }

        private void NotifyOpcJobFinished(PanelState pState, string tolocation) {
            try {
                switch (pState) {
                    case PanelState.HalfFull:
                        lock (FrmMain.opcClient) {
                            FrmMain.opcClient.Write(FrmMain.opcParam.BAreaFloorFinish[tolocation], true);
                            FrmMain.logOpt.Write(string.Format("{0} HalfFull", tolocation), LogType.ROBOT_STACK);
                        }
                        break;
                    case PanelState.Full:
                        lock (FrmMain.opcClient) {
                            FrmMain.opcClient.Write(FrmMain.opcParam.BAreaPanelFinish[tolocation], true);
                            FrmMain.opcClient.Write(FrmMain.opcParam.BAreaPanelState[tolocation], 3);
                            FrmMain.logOpt.Write(string.Format("{0} Full", tolocation), LogType.ROBOT_STACK);
                        }
                        break;
                    case PanelState.LessHalf:
                    default:
                        break;
                }
            } catch (Exception ex) {
                FrmMain.logOpt.Write(string.Format("tolocation: {0} state:{1} opc:{2} err:{3}", tolocation, pState, Newtonsoft.Json.JsonConvert.SerializeObject(FrmMain.opcParam.BAreaFloorFinish), ex), LogType.ROBOT_STACK);
            }
        }

        public void JobLoop(ref bool isrun) {
            while (isrun) {
                if (robotJobs.Rolls.Count > 0) {
                    FrmMain.logOpt.Write("robotJobs.Rolls: " + JsonConvert.SerializeObject(robotJobs.Rolls), LogType.ROBOT_STACK, LogViewType.OnlyFile);
                    while (IsBusy()) {
                        Thread.Sleep(OPCClient.DELAY);
                    }
                    var roll = robotJobs.GetRoll();

                    FrmMain.logOpt.Write(string.Format("roll:{0} {1}\r\n{2}", roll.LabelCode, roll.ToLocation, JsonConvert.SerializeObject(roll)), LogType.ROBOT_STACK);

                    // 启动机器人动作。
                    // FrmMain.logOpt.Write("机器人动作准备。", LogType.ROBOT_STACK);
                    WritePosition(roll);

                    FrmMain.logOpt.Write($"完成机器人写位置{roll.LabelCode}操作。");

                    RunJob(JOB_NAME);
                    FrmMain.logOpt.Write($"发出机器人示教器动作{JOB_NAME}命令。");

                    Thread.Sleep(RobotHelper.DELAY * 1000);

                    // 等待布卷上垛信号
                    while (isrun) {
                        if (IsRollOnPanel()) {
                            LableCode.SetOnPanelState(roll.LabelCode);
                            break;
                        }
                        Thread.Sleep(RobotHelper.DELAY * 200);
                    }

                    // 告知OPC
                    Task.Factory.StartNew(() => {
                        NotifyOpcJobFinished(roll.PnlState, roll.ToLocation);
                    });

                    // 等待机器人结束码垛。
                    while (isrun && IsBusy()) {
                        FrmMain.logOpt.Write("Working", LogType.ROBOT_STACK, LogViewType.OnlyFile);
                        Thread.Sleep(RobotHelper.DELAY * 100);
                    }
                    FrmMain.logOpt.Write("Work ok", LogType.ROBOT_STACK);
                }
                Thread.Sleep(RobotHelper.DELAY);
            }
        }

        private bool IsRollOnPanel() {
            try {
                Dictionary<string, string> b5 = rCtrl.GetVariables(VariableType.B, 5, 1);
                FrmMain.logOpt.Write(JsonConvert.SerializeObject(b5), LogType.ROBOT_STACK, LogViewType.OnlyFile);
                if (b5 != null && b5.Count != 0 && b5.ContainsKey("5")) {
                    return b5["5"] == "0";
                }
            } catch (Exception ex) {
                FrmMain.logOpt.Write(string.Format("{0}", ex), LogType.ROBOT_STACK, LogViewType.OnlyFile);
            }
            return false;
        }

        private bool PanelAvailable(string tolocation) {
            try {
                lock (FrmMain.opcClient) {
                    string s = FrmMain.opcClient.ReadString(FrmMain.opcParam.BAreaPanelState[tolocation]);
                    return s == "2";
                }
            } catch (Exception ex) {
                FrmMain.logOpt.Write(string.Format("读交地状态信号异常 tolocation: {0} opc:{1} err:{2}", tolocation, Newtonsoft.Json.JsonConvert.SerializeObject(FrmMain.opcParam.BAreaFloorFinish), ex), LogType.ROBOT_STACK);
                return true;//临时
            }
        }

        public Dictionary<string, string> AlarmTask() {
            try {
                Dictionary<string, bool> s = rCtrl.GetAlarmStatus();
                if (s != null && s.Count != 0) {
                    if (s["Error"] || s["Alarm"]) {
                        return rCtrl.GetAlarmCode();
                    }
                }
            } catch (Exception ex) {
                FrmMain.logOpt.Write(string.Format("{0}", ex), LogType.ROBOT_STACK);
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
