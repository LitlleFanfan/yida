using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using yidascan.DataAccess;
using yidascan;

namespace RobotTest {
    public partial class Form1 : Form {
        List<LableCode> rolls;

        RobotHelper robot;

        string jobname = "LJE2"; // 存于机器人本身的程序名。

        int index = 0;

        bool isrun = false;

        bool hold = false;

        MessageCenter msgCtr = new MessageCenter();

        public Form1() {
            InitializeComponent();

            DataBind();
            //initbtn();
        }

        private void DataBind() {
            rolls = new List<LableCode>() {
            new LableCode() { LCode = "122363112003", ToLocation = "B10", PanelNo = "16112140008", Status = 0, Floor = 5, FloorIndex = 1, Diameter = 120, Length = 1515, Coordinates = "780,90,0" },
            new LableCode() { LCode = "122363315003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 5, FloorIndex = 2, Diameter = 70, Length = 1504, Coordinates = "780,90,-70" },
            new LableCode() { LCode = "122345184003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 3, FloorIndex = 7, Diameter = 200, Length = 1524, Coordinates = "375,90,660" },
            new LableCode() { LCode = "122361104003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 4, FloorIndex = 9, Diameter = 195, Length = 1521, Coordinates = "575,0,445" },
            new LableCode() { LCode = "122361103003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 4, FloorIndex = 2, Diameter = 190, Length = 1511, Coordinates = "575,0,-190" },
            new LableCode() { LCode = "122361102003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 4, FloorIndex = 3, Diameter = 75, Length = 2027, Coordinates = "575,0,160" },
            new LableCode() { LCode = "122361101003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 4, FloorIndex = 4, Diameter = 165, Length = 1568, Coordinates = "575,0,-355" },
            new LableCode() { LCode = "122361100003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 4, FloorIndex = 5, Diameter = 165, Length = 1514, Coordinates = "575,0,235" },
            new LableCode() { LCode = "122357519003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 4, FloorIndex = 7, Diameter = 150, Length = 1496, Coordinates = "575,0,295" },
            new LableCode() { LCode = "122347962003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 4, FloorIndex = 1, Diameter = 160, Length = 1506, Coordinates = "575,0,0" },
            new LableCode() { LCode = "122361083003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 1, FloorIndex = 7, Diameter = 185, Length = 1570, Coordinates = "30,90,495" },
            new LableCode() { LCode = "122361082003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 1, FloorIndex = 8, Diameter = 180, Length = 1553, Coordinates = "30,90,-585" },
            new LableCode() { LCode = "122361081003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 1, FloorIndex = 1, Diameter = 180, Length = 1566, Coordinates = "30,90,0" },
            new LableCode() { LCode = "122361080003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 1, FloorIndex = 2, Diameter = 130, Length = 1539, Coordinates = "30,90,-130" },
            new LableCode() { LCode = "122362060003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 1, FloorIndex = 3, Diameter = 160, Length = 1513, Coordinates = "30,90,180" },
            new LableCode() { LCode = "122362059003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 1, FloorIndex = 4, Diameter = 160, Length = 1574, Coordinates = "30,90,-290" },
            new LableCode() { LCode = "122362058003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 1, FloorIndex = 5, Diameter = 155, Length = 1584, Coordinates = "30,90,340" },
            new LableCode() { LCode = "122355585003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 1, FloorIndex = 6, Diameter = 115, Length = 1506, Coordinates = "30,90,-405" },
            new LableCode() { LCode = "122361079003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 2, FloorIndex = 7, Diameter = 190, Length = 1518, Coordinates = "185,0,410" },
            new LableCode() { LCode = "122356531003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 2, FloorIndex = 1, Diameter = 105, Length = 1529, Coordinates = "185,0,0" },
            new LableCode() { LCode = "122362056003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 2, FloorIndex = 8, Diameter = 190, Length = 115, Coordinates = "185,0,-715" },
            new LableCode() { LCode = "122362055003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 2, FloorIndex = 2, Diameter = 175, Length = 1521, Coordinates = "185,0,-175" },
            new LableCode() { LCode = "122362054003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 2, FloorIndex = 3, Diameter = 190, Length = 1525, Coordinates = "185,0,105" },
            new LableCode() { LCode = "122362053003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 2, FloorIndex = 4, Diameter = 185, Length = 1517, Coordinates = "185,0,-360" },
            new LableCode() { LCode = "122362324003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 2, FloorIndex = 5, Diameter = 115, Length = 231, Coordinates = "185,0,295" },
            new LableCode() { LCode = "122362317003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 2, FloorIndex = 6, Diameter = 165, Length = 1517, Coordinates = "185,0,-525" },
            new LableCode() { LCode = "122362316003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 3, FloorIndex = 5, Diameter = 195, Length = 1520, Coordinates = "375,90,465" },
            new LableCode() { LCode = "122362315003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 3, FloorIndex = 6, Diameter = 195, Length = 1520, Coordinates = "375,90,-665" },
            new LableCode() { LCode = "122362323003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 3, FloorIndex = 1, Diameter = 110, Length = 1507, Coordinates = "375,90,0" },
            new LableCode() { LCode = "122362314003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 3, FloorIndex = 2, Diameter = 100, Length = 1511, Coordinates = "375,90,-100" },
            new LableCode() { LCode = "122362313003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 3, FloorIndex = 3, Diameter = 160, Length = 1518, Coordinates = "375,90,110" },
            new LableCode() { LCode = "122348048003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 3, FloorIndex = 4, Diameter = 175, Length = 1515, Coordinates = "375,90,-275" },
            new LableCode() { LCode = "122354748003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 4, FloorIndex = 6, Diameter = 205, Length = 1530, Coordinates = "575,0,-560" }
        };
            dataGridView1.DataSource = rolls;
        }

        private void btnStart_Click(object sender, EventArgs e) {
            isrun = true;
            initbtn();
            StartRobotTask();
        }

        void initbtn() {
            btnStart.Enabled = !isrun;
            txtIP.Enabled = !isrun;
            btnAddRoll.Enabled = isrun;
            btnStop.Enabled = isrun;
            btnRun.Enabled = isrun;
            btnHold.Enabled = isrun;
        }

        private void btnStop_Click(object sender, EventArgs e) {
            isrun = false;
            initbtn();
            robot.Dispose();
        }

        private void btnAddRoll_Click(object sender, EventArgs e) {
            if (index < rolls.Count) {
                PushInQueue(rolls[index], "A");
                msgCtr.Push(string.Format("Add roll {0}", rolls[index].LCode));
                index++;
            } else {
                msgCtr.Push("Havn't othar roll");
            }
        }

        private void StartRobotTask() {
            Task.Factory.StartNew(() => {
                robot = new RobotHelper(txtIP.Text, jobname, msgCtr);
                robot.JobLoop(ref isrun);
            });
        }

        private void PushInQueue(LableCode label, string side) {
            var state = PanelState.LessHalf;
            if (label.Floor < 6) { state = PanelState.HalfFull; }

            // lc.Coordinates = string.Format("{0},{1},{2}", z, r, xory);
            var ar_s = label.Coordinates.Split(new char[] { ',' });
            var ar = (from item in ar_s select decimal.Parse(item)).ToList();
            decimal x = 0; decimal y = 0;

            if (ar[1] > 0) {
                x = ar[2];
            } else {
                y = ar[2];
            }

            var z = ar[0];
            var rz = ar[1];
            var roll = new RollPosition(side, label.ToLocation, state, x, y, z, rz);
            lock (RobotHelper.robotJobs) {
                RobotHelper.robotJobs.AddRoll(roll);
            }
        }

        public void ViewMsg() {
            foreach (string s in msgCtr.GetAll()) {
                txtLog.Text = string.Format("{1}{0}\r\n", s, txtLog.Text);
            }
        }

        private void btnReset_Click(object sender, EventArgs e) {
            DataBind();
        }

        private void btnHold_Click(object sender, EventArgs e) {
            robot.rCtrl.Hold(hold);
            hold = !hold;
            btnRun.Enabled = hold;
        }

        private void btnRun_Click(object sender, EventArgs e) {
            robot.rCtrl.StartJob(jobname);
        }

        private void timer1_Tick(object sender, EventArgs e) {
            ViewMsg();
        }
    }
}
