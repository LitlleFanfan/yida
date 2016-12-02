﻿using RobotControl;

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

    public class RobotJobQueue {
        public Queue<RollPosition> Rolls = new Queue<RollPosition>();

        public void AddRoll(RollPosition roll) {
            lock (Rolls) {
                Rolls.Enqueue(roll);
            }
        }

        public RollPosition GetRoll() {
            lock (Rolls) {
                return Rolls.Count == 0 ?
                    null : Rolls.Dequeue();
            }
        }

        public void ResetRolls() {
            lock (Rolls) {
                while (Rolls.Count > 0) {
                    Rolls.Dequeue();
                }
            }
        }

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
        MessageCenter msg = null;
        public RobotControl.RobotControl rCtrl;
        public static RobotJobQueue robotJobs = new RobotJobQueue();

        public const int DELAY = 5;

        public bool IsConnected() {
            return rCtrl.Connected;
        }

        public RobotHelper(string ip, string jobName, MessageCenter _msg) {
            rCtrl = new RobotControl.RobotControl(ip);
            rCtrl.Connect();
            rCtrl.ServoPower(true);
            JOB_NAME = jobName;
            msg = _msg;
        }

        public void WritePosition(RollPosition rollPos) {
            rCtrl.SetVariables(RobotControl.VariableType.B, 10, 1, rollPos.ChangeAngle ? "1" : "0");
            rCtrl.SetVariables(RobotControl.VariableType.B, 0, 1, rollPos.BaseIndex.ToString());

            //msg.Push(string.Format("Origin: {0}", Newtonsoft.Json.JsonConvert.SerializeObject(rollPos.Origin)));
            //msg.Push(string.Format("Target: {0}", Newtonsoft.Json.JsonConvert.SerializeObject(rollPos.Target)));

            // 原点高位旋转
            rCtrl.SetPostion(RobotControl.PosVarType.Robot,
                rollPos.Origin, 100, RobotControl.PosType.User, 0, rollPos.LocationNo);

            ////基座
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
            msg.Push(string.Format("{0}", Newtonsoft.Json.JsonConvert.SerializeObject(status)));
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

        public void JobLoop(ref bool isrun, ref bool hold) {
            while (isrun && !hold) {
                msg.Push("JobLoop Head");
                if (robotJobs.Rolls.Count > 0) {
                    RollPosition roll;
                    lock (robotJobs) {
                        roll = robotJobs.GetRoll();
                    }
                    msg.Push(string.Format("roll:{0}", Newtonsoft.Json.JsonConvert.SerializeObject(roll)));

                    msg.Push(string.Format("Jobs list count:{0} x:{1};y:{2};z:{3};rz:{4};base:{5};", robotJobs.Rolls.Count, roll.X, roll.Y, roll.Z, roll.Rz, roll.Target.Axis7));

                    while (isrun && !hold && IsBusy()) {
                        Thread.Sleep(RobotHelper.DELAY);
                    }

                    // 启动机器人动作。
                    WritePosition(roll);

                    RunJob(JOB_NAME);

                    // 等待安全位置信号
                    //while (!IsSafePlace() && !hold) { Thread.Sleep(RobotHelper.DELAY * 10); }

                    Thread.Sleep(RobotHelper.DELAY * 1000);

                    // 等待完成信号
                    while (IsBusy() && !hold) {
                        msg.Push("Working");
                        Thread.Sleep(RobotHelper.DELAY * 100);
                    }

                }
                msg.Push("JobLoop End");
                Thread.Sleep(RobotHelper.DELAY * 1000);
            }
        }

        public void AlarmTask(ref bool isrun, ref bool hold) {
            while (isrun && !hold) {
                Dictionary<string, bool> s = rCtrl.GetAlarmStatus();
                if (s.Count != 0) {
                    if (s["Error"] || s["Alarm"]) {
                        msg.Push(string.Format("{0}", Newtonsoft.Json.JsonConvert.SerializeObject(rCtrl.GetAlarmCode())));
                    }
                }
                Thread.Sleep(RobotHelper.DELAY * 1000);
            }
        }

        public void Dispose() {
            rCtrl.ServoPower(false);
            rCtrl.Close();
        }
    }
}