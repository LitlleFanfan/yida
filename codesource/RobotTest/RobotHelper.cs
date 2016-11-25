﻿using RobotControl;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using yidascan.DataAccess;

namespace yidascan.scanner {
    public enum PanelState {
        LessHalf,
        HalfFull,
        Full
    }

    public class RobotJobQueue {
        public Queue<RollPosition> Rolls { get; set; }

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
            ChangeAngle = x + y > 0;

            ToLocation = locationNo;
            Side = side;
            PnlState = pnlState;

            int tmp = int.Parse(locationNo.Substring(1, 2));
            RobotParam origin = RobotParam.GetOrigin(tmp);
            Origin = new PostionVar(0, 0, 0, origin.Rx, origin.Ry, origin.Rz + rz);

            int baseindex = CalculateBaseIndex(x, y, rz);
            //x = baseindex == 0 ? 0 : ((decimal)(dtOrigin.Rows[0]["Base"]) - (decimal)(dtPoint.Rows[0]["Base"]));
            // 基座
            RobotParam point = RobotParam.GetPoint(tmp, baseindex);
            Target = new PostionVar(x, y, z, point.Rz + rz, point.Base);
        }

        private int CalculateBaseIndex(decimal x, decimal y, decimal rz) {
            int baseindex = 0;
            if (rz > 0) {
                if (y < 0) {
                    baseindex += 1;
                }
            } else {
                baseindex = 2;
                if (x < 0) {
                    baseindex += 1;
                }
            }

            return baseindex;
        }

        public int LocationNo;
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

        public RobotHelper(string ip, string jobName) {
            rCtrl = new RobotControl.RobotControl(ip);
            rCtrl.Connect();
            rCtrl.ServoPower(true);
            JOB_NAME = jobName;
        }

        public void WritePosition(RollPosition rollPos) {
            rCtrl.SetVariables(RobotControl.VariableType.B, 10, 1, rollPos.ChangeAngle ? "1" : "0");
            rCtrl.SetVariables(RobotControl.VariableType.B, 0, 1, rollPos.LocationNo.ToString());

            // 原点高位旋转
            rCtrl.SetPostion(RobotControl.PosVarType.Robot,
                rollPos.Origin,
                0, RobotControl.PosType.User, 0, rollPos.LocationNo);

            //基座
            rCtrl.SetPostion(RobotControl.PosVarType.Base,
                new RobotControl.PostionVar(rollPos.Target.axis7, 0, 0, 0, 0),
                0, RobotControl.PosType.Robot, 0, 0);

            // 目标位置
            rCtrl.SetPostion(RobotControl.PosVarType.Robot,
               rollPos.Target, 0, RobotControl.PosType.User, 0, rollPos.LocationNo);
        }

        public void RunJob(string jobName) {
            rCtrl.StartJob(jobName);
        }

        public bool IsBusy() {
            Dictionary<string, bool> status = rCtrl.GetPlayStatus();
            return (status["Start"] || status["Hold"]);
        }

        private bool IsSafePlace() {
            Dictionary<string, string> b1 = rCtrl.GetVariables(VariableType.B, 1, 1);
            return b1["b1"] == "1";
        }

        public void JobLoop(ref bool isrun) {
            while (isrun) {
                if (robotJobs.Rolls.Count > 0) {
                    while (isrun && IsBusy()) {
                        Thread.Sleep(5);
                    }

                    var roll = robotJobs.GetRoll();
                    // 启动机器人动作。
                    WritePosition(roll);
                    
                    RunJob(JOB_NAME);

                    // 等待安全位置信号
                    while (!IsSafePlace()) { Thread.Sleep(RobotHelper.DELAY * 10); }
                    

                    // 等待完成信号
                    while (IsBusy()) { Thread.Sleep(RobotHelper.DELAY * 10); }
                    
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
