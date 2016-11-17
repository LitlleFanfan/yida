using RobotControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using yidascan.DataAccess;

namespace yidascan.scanner
{
    public class RollPosition
    {
        public RollPosition(int locationNo, decimal x, decimal y, decimal z, decimal rz)
        {
            X = x;
            Y = y;
            Z = z;
            Rz = rz;
            ChangeAngle = x + y > 0;
            RobotParam origin = RobotParam.GetOrigin(locationNo);
            Origin = new PostionVar(0, 0, 0, origin.Rx, origin.Ry, origin.Rz + rz);

            int baseindex = CalculateBaseIndex(x, y, rz);
            // 基座
            RobotParam point = RobotParam.GetPoint(locationNo, baseindex);
            Target = new PostionVar(x, y, z, point.Rz + rz, point.Base);
        }

        private int CalculateBaseIndex(decimal x, decimal y, decimal rz)
        {
            int baseindex = 0;
            if (rz > 0)
            {
                if (y < 0)
                {
                    baseindex += 1;
                }
            }
            else
            {
                baseindex = 2;
                if (x < 0)
                {
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
    }

    public class RobotHelper : IDisposable
    {
        RobotControl.RobotControl rCtrl;
        public RobotHelper(string ip)
        {
            rCtrl = new RobotControl.RobotControl(ip);
            rCtrl.Connect();
        }

        public void WritePosition(RollPosition rollPos)
        {
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

        public void RunJob(string jobName)
        {
            rCtrl.ServoPower(true);
            rCtrl.StartJob(jobName);
        }

        public void Dispose()
        {
            rCtrl.Close();
        }
    }
}
