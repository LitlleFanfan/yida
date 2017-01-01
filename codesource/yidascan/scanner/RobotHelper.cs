﻿using ProduceComm;
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

        /// <summary>
        /// 返回坐标的字符串表示。
        /// </summary>
        /// <returns></returns>
        public string Pos_s() {
            return $"x: {X}, y: {Y}, z: {Z}, rz: {Rz}";
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

        public Action<string, string, LogViewType> _log;

        private OPCClient client;
        private OPCParam param;

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

        public void setup(Action<string, string, LogViewType> logfunc, OPCClient c, OPCParam p) {
            _log = logfunc;
            client = c;
            param = p;
        }

        private void log(string msg, string group, LogViewType ltype = LogViewType.Both) {
            if (_log == null) { return; }
            _log(msg, group, ltype);
        }

        public bool IsConnected() {
            return rCtrl.IsConnected();
        }

        /// <summary>
        /// 尝试多次机器人写坐标，直到成功或用完次数。
        /// </summary>
        /// <param name="rollPos"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public bool TryWritePositionPro(RollPosition rollPos, int times = 5) {
            const int DELAY = 100;
            while (times > 0) {
                if (WritePositionPro(rollPos)) {
                    log($"机器人写坐标成功, {rollPos.LabelCode}, {rollPos.Pos_s()}。", LogType.ROBOT_STACK);
                    return true;
                }
                times--;
                Thread.Sleep(DELAY);
            }
            log("!机器人写坐标失败", LogType.ROBOT_STACK);
            return false;
        }

        private bool WritePositionPro(RollPosition rollPos) {
            const int DELAY = 100;
            var a = rCtrl.SetVariables(RobotControl.VariableType.B, 10, 1, rollPos.ChangeAngle ? "1" : "0");
            Thread.Sleep(DELAY);
            var b = rCtrl.SetVariables(RobotControl.VariableType.B, 0, 1, rollPos.BaseIndex.ToString());
            Thread.Sleep(DELAY);
            var c = rCtrl.SetVariables(RobotControl.VariableType.B, 5, 1, "1");
            Thread.Sleep(DELAY);

            // 原点高位旋转
            var d = rCtrl.SetPostion(RobotControl.PosVarType.Robot,
                rollPos.Origin, 100, RobotControl.PosType.User, 0, rollPos.LocationNo);
            Thread.Sleep(DELAY);

            //基座
            var e = rCtrl.SetPostion(RobotControl.PosVarType.Base,
                new RobotControl.PostionVar(rollPos.Base2 * 1000, 0, 0, 0, 0),
                0, RobotControl.PosType.Robot, 0, 0);

            Thread.Sleep(DELAY);

            // 目标位置
            var f = rCtrl.SetPostion(RobotControl.PosVarType.Robot,
               rollPos.Target, 101, RobotControl.PosType.User, 0, rollPos.LocationNo);
            Thread.Sleep(DELAY);

            return a && b && c && d && e && f;
        }

        [Obsolete("使用WritePositionPro.")]
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

        public bool TryRunJob(string jobName, int times = 5) {
            const int DELAY = 30;
            while (times > 0) {
                if (rCtrl.StartJob(jobName)) {
                    return true;
                }
                times--;
                Thread.Sleep(DELAY);
            }
            return false;
        }

        public bool IsBusy() {
            // 读机器人的状态可能有错。
            try {
                var status = rCtrl.GetPlayStatus();
                log($"robot status: {JsonConvert.SerializeObject(status)}", LogType.ROBOT_STACK, LogViewType.OnlyFile);

                if (status == null || status.Count == 0) {
                    return true;
                } else {
                    return (status["Start"] || status["Hold"]);
                }
            } catch (Exception ex) {                
                log($"{ex}", LogType.ROBOT_STACK, LogViewType.OnlyFile);
                return true;
            }
        }

        [Obsolete("这个干什么用的不知道")]
        private void NotifyOpcSafePlace(string side) {
            lock (FrmMain.opcClient) {
                // FrmMain.opcClient.Write(side == "A" ? FrmMain.opcParam.RobotCarryA.Signal : FrmMain.opcParam.RobotCarryB.Signal, false);
                client.Write(side == "A" ? param.RobotCarryA.Signal : param.RobotCarryB.Signal, false);
            }
        }

        private void NotifyOpcJobFinished(PanelState pState, string tolocation) {
            try {
                switch (pState) {
                    case PanelState.HalfFull:
                        // FrmMain.opcClient.Write(FrmMain.opcParam.BAreaFloorFinish[tolocation], true);
                        client.Write(param.BAreaFloorFinish[tolocation], true);
                        log($"{tolocation}: 半板信号发出。slot: {param.BAreaFloorFinish[tolocation]}", LogType.ROBOT_STACK);

                        break;
                    case PanelState.Full:
                        // FrmMain.opcClient.Write(FrmMain.opcParam.BAreaPanelFinish[tolocation], true);
                        client.Write(param.BAreaPanelFinish[tolocation], true);
                        log($"{tolocation}: 满板信号发出。slot: {param.BAreaPanelFinish[tolocation]}", LogType.ROBOT_STACK);

                        // FrmMain.opcClient.Write(FrmMain.opcParam.BAreaPanelState[tolocation], 3);
                        const int SIGNAL_3 = 3;
                        client.Write(param.BAreaPanelState[tolocation], SIGNAL_3);
                        log($"{tolocation}: 板状态信号发出，状态值: {SIGNAL_3}。slot: {param.BAreaPanelState[tolocation]}", LogType.ROBOT_STACK);

                        break;
                    case PanelState.LessHalf:
                        // log($"板未半满，不发信号, {pState}", LogType.ROBOT_STACK);
                        break;
                    default:
                        log($"!板状态不明，不发信号, {pState}", LogType.ROBOT_STACK);
                        break;
                }
            } catch (Exception ex) {
                log($"!{ex}", LogType.ROBOT_STACK);
                // log($"!tolocation: {tolocation} state:{pState} opc:{JsonConvert.SerializeObject(param.BAreaFloorFinish)} err:{ex}", LogType.ROBOT_STACK);
            }
        }

        public void JobLoop(ref bool isrun) {
            while (isrun) {
                if (robotJobs.Rolls.Count > 0) {
                    // 机器人正忙，等待。
                    if (IsBusy()) {
                        Thread.Sleep(OPCClient.DELAY * 100);
                        continue;
                    }

                    var roll = robotJobs.GetRoll();
                    if (roll == null) {
                        Thread.Sleep(OPCClient.DELAY);
                        continue;
                    }

                    // WritePosition(roll);
                    if (!TryWritePositionPro(roll)) {
                        break;
                    }

                    if (TryRunJob(JOB_NAME)) {
                        log($"发出机器人示教器动作{JOB_NAME}命令成功。", LogType.ROBOT_STACK);
                    } else {
                        log($"!机器人示教器动作{JOB_NAME}发送失败。", LogType.ROBOT_STACK);
                        break;
                    }

                    Thread.Sleep(RobotHelper.DELAY * 1000);

                    // 等待布卷上垛信号
                    while (isrun) {
                        if (IsRollOnPanel()) {
                            log("布卷已上垛。", LogType.ROBOT_STACK, LogViewType.Both);
                            // 写数据库。
                            LableCode.SetOnPanelState(roll.LabelCode);
                            break;
                        }
                        Thread.Sleep(RobotHelper.DELAY * 200);
                    }

                    // 告知OPC
                    NotifyOpcJobFinished(roll.PnlState, roll.ToLocation);

                    // 等待机器人结束码垛。
                    while (isrun && IsBusy()) {
                        Thread.Sleep(RobotHelper.DELAY * 100);
                    }
                    log($"robot job done: {roll.LabelCode}.", LogType.ROBOT_STACK);
                }
                Thread.Sleep(RobotHelper.DELAY);
            }
        }

        private bool IsRollOnPanel() {
            const string KEY = "5";
            const string V_ON_PANEL = "0";
            try {
                var b5 = rCtrl.GetVariables(VariableType.B, 5, 1);
                return (b5 != null && b5.ContainsKey(KEY) && b5[KEY] == V_ON_PANEL);
            } catch (Exception ex) {
                log($"!IsRollOnPanel异常: {ex}", LogType.ROBOT_STACK, LogViewType.OnlyFile);
                return false;
            }
        }

        private bool PanelAvailable(string tolocation) {
            try {
                var s = client.ReadString(param.BAreaPanelState[tolocation]);
                return s == "2";
            } catch (Exception ex) {
                log($"!读交地状态信号异常 tolocation: {tolocation} opc:{JsonConvert.SerializeObject(param.BAreaFloorFinish)} err:{ex}", LogType.ROBOT_STACK);
                return false;//临时
            }
        }

        [Obsolete]
        public Dictionary<string, string> AlarmTask() {
            try {
                var s = rCtrl.GetAlarmStatus();
                if (s != null && s.Count != 0) {
                    if (s["Error"] || s["Alarm"]) {
                        return rCtrl.GetAlarmCode();
                    }
                }
            } catch (Exception ex) {
                log($"!{ex}", LogType.ROBOT_STACK);
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
