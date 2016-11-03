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

namespace RobotTest
{
    public partial class Form1 : Form
    {
        DataAccess da = new DataAccess();

        DataTable dtview = new DataTable();

        RobotControl.RobotControl rCtrl;
        public Form1()
        {
            InitializeComponent();

            DataBind();
            //initbtn();
        }

        private void DataBind()
        {
            dtview = da.Query("select * from LableCode where Floor is not null order by PanelNo desc,Floor asc,FloorIndex asc");
            dataGridView1.DataSource = dtview.DefaultView;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rCtrl = new RobotControl.RobotControl(textBox1.Text);
            rCtrl.Connect();
            if (rCtrl.Connected)
            {
                initbtn();
            }
        }

        void initbtn()
        {
            button2.Enabled = !(rCtrl != null && rCtrl.Connected);
            textBox1.Enabled = !(rCtrl != null && rCtrl.Connected);
            button1.Enabled = rCtrl != null && rCtrl.Connected;
            button3.Enabled = rCtrl != null && rCtrl.Connected;
        }
        void initbtn2()
        {
            button2.Enabled = !(rCtrl != null);
            textBox1.Enabled = !(rCtrl != null);
            button1.Enabled = rCtrl != null;
            button3.Enabled = rCtrl != null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rCtrl.Close();
            initbtn();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dtview.DefaultView.RowFilter = string.Format("LCode='{0}'", txtLableCode.Text);
            if (dtview.DefaultView.Count <= 0) return;

            DataRow r = dtview.DefaultView[0].Row;//这是一个行对象
            //r["Status"] = ((int)r["Status"]) + 1;
            int panelPoint = int.Parse(r["ToLocation"].ToString().Substring(1, 2));
            string[] coordinates = r["Coordinates"].ToString().Split(',');

            ViewMsg(textBox2, "", string.Format("标签: {0}; 坐标: {1};直径: {2}; 层: {3}; 层序号: {4}", txtLableCode.Text, r["Coordinates"].ToString().PadLeft(18, ' '), r["Diameter"], r["Floor"], r["FloorIndex"]));
            ControlRobot(panelPoint, coordinates);
            dtview.DefaultView.RowFilter = string.Empty;
        }

        private void ControlRobot(int panelPoint, string[] coordinates)
        {
            //原点
            System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@BaseIndex", 0);
            p1.Value = 0;
            DataTable dtOrigin = da.Query("select * from RobotParam where PanelIndexNo=@PanelIndexNo and BaseIndex=@BaseIndex",
                new System.Data.SqlClient.SqlParameter("@PanelIndexNo", panelPoint), p1);

            int baseindex = CalculateBaseIndex(coordinates);
            DataTable dtPoint = da.Query("select * from RobotParam where PanelIndexNo=@PanelIndexNo and BaseIndex=@BaseIndex",
                new System.Data.SqlClient.SqlParameter("@PanelIndexNo", panelPoint),
                new System.Data.SqlClient.SqlParameter("@BaseIndex", baseindex));

            if (dtOrigin == null || dtOrigin.Rows.Count == 0 || dtPoint == null || dtPoint.Rows.Count == 0)
            {
                ViewMsg(textBox2, "", string.Format("{0}原点或对应基座没有设置好", panelPoint));
                return;
            }

            ViewMsg(textBox2, "", string.Format("\r\nOrigin: [PanelIndexNo: {0};BaseIndex: {1};Base: {2};Rx: {3};Ry: {4};Rz: {5};];\r\n Point: [PanelIndexNo: {6};BaseIndex: {7};Base: {8};Rx: {9};Ry: {10};Rz: {11};]",
               dtOrigin.Rows[0]["PanelIndexNo"].ToString().PadLeft(2, ' '), dtOrigin.Rows[0]["BaseIndex"], dtOrigin.Rows[0]["Base"],
               dtOrigin.Rows[0]["Rx"].ToString().PadLeft(8, ' '), dtOrigin.Rows[0]["Ry"].ToString().PadLeft(8, ' '), dtOrigin.Rows[0]["Rz"].ToString().PadLeft(8, ' '),
               dtPoint.Rows[0]["PanelIndexNo"].ToString().PadLeft(2, ' '), dtPoint.Rows[0]["BaseIndex"], dtPoint.Rows[0]["Base"],
               dtPoint.Rows[0]["Rx"].ToString().PadLeft(8, ' '), dtPoint.Rows[0]["Ry"].ToString().PadLeft(8, ' '), dtPoint.Rows[0]["Rz"].ToString().PadLeft(8, ' ')
               ));

            decimal rx = (decimal)dtPoint.Rows[0]["Rx"];
            decimal ry = (decimal)dtPoint.Rows[0]["Ry"];
            decimal rz = (decimal.Parse(coordinates[2]) > 800 ? -10000 : 10000) * decimal.Parse(coordinates[1]) + (decimal)dtPoint.Rows[0]["Rz"];
            decimal x = 0, y = 0, z = -1830000 - decimal.Parse(coordinates[0]) * -1000;
            x = baseindex == 0 ? 0 : ((decimal)(dtOrigin.Rows[0]["Base"]) - (decimal)(dtPoint.Rows[0]["Base"]));
            if (decimal.Parse(coordinates[1]) > 0)
            {
                x = x + decimal.Parse(coordinates[2]) * 1000;
            }
            else
            {
                y = decimal.Parse(coordinates[2]) * 1000;
            }
            ViewMsg(textBox2, "", string.Format("B10: {0};B0: {1};BP0: {2}; \r\nP0: [X:        0;Y:        0;Z:        0;Rx: {3};Ry: {4};Rz: {5};]; \r\nP1: [X: {6};Y: {7};Z: {8};Rx: {3};Ry: {4};Rz: {5};];基座: {9}",
               decimal.Parse(coordinates[2]) > 800 ? "1" : "0", panelPoint.ToString().PadLeft(2, ' '), dtPoint.Rows[0]["Base"].ToString().PadLeft(8, ' '),
               rx.ToString().PadLeft(8, ' '), ry.ToString().PadLeft(8, ' '), rz.ToString().PadLeft(8, ' '), x.ToString().PadLeft(8, ' '), y.ToString().PadLeft(8, ' '), z.ToString().PadLeft(8, ' '), baseindex));
            //if (rCtrl.SetVariables(RobotControl.VariableType.B, 10, 1, decimal.Parse(coordinates[2]) > 800 ? "1" : "0") &&
            //    rCtrl.SetVariables(RobotControl.VariableType.B, 0, 1, panelPoint.ToString()))
            //{
            //    txtB10.Text = decimal.Parse(coordinates[2]) > 800 ? "1" : "0";
            //    txtB0.Text = panelPoint.ToString();

            //    if (rCtrl.SetPostion(RobotControl.PosVarType.Base,
            //        new RobotControl.PostionVar((decimal)(dtPoint.Rows[0]["Base"]), 0, 0, 0, 0),
            //        0, RobotControl.PosType.Robot, 0, 0))//write base point
            //    {
            //        ViewMsg(textBox2, "", "BPVar");

            //        if (rCtrl.SetPostion(RobotControl.PosVarType.Robot, new RobotControl.PostionVar(0, 0, 0,
            //             rx, ry, rz, 0, false), 0, RobotControl.PosType.User, 0, panelPoint))//write robot angle
            //        {
            //            ViewMsg(textBox2, "", "P0Var");
            //            if (rCtrl.SetPostion(RobotControl.PosVarType.Robot, new RobotControl.PostionVar(x, y, z, rx, ry, rz, 0, false),
            //                0, RobotControl.PosType.User, 0, panelPoint))//write robot angle
            //            {
            //                ViewMsg(textBox2, "", "P1Var");
            //            }
            //        }
            //    }
            //}
        }

        private static int CalculateBaseIndex(string[] coordinates)
        {
            int baseindex = 0;
            if (decimal.Parse(coordinates[1]) == 90)
            {
                if (decimal.Parse(coordinates[2]) < 800)
                {
                    baseindex += 1;
                }
            }
            else
            {
                baseindex = 2;
                if (decimal.Parse(coordinates[2]) < 800)
                {
                    baseindex += 1;
                }
            }

            return baseindex;
        }

        public void ViewMsg(TextBox t, string msgType, string msg)
        {
            if (string.IsNullOrEmpty(msg))
                return;
            this.Invoke((EventHandler)(delegate
            {
                t.Text = string.Format("{3}{0} {1}|  {2}\r\n", DateTime.Now.ToString(), msgType, msg, t.Text);
            }));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataBind();
        }
    }
}
