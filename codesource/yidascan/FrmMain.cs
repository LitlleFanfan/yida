﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using yidascan.DataAccess;
using ProduceComm;
using ProduceComm.Scanner;
using Newtonsoft.Json;
using ProduceComm.OPC;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace yidascan
{
    public partial class FrmMain : Form
    {
        private DataTable dtview = new DataTable();
        NormalScan nscan1 = new NormalScan();
        NormalScan nscan2 = new NormalScan();
        private LableCodeBll lcb = new LableCodeBll();
        bool isrun = false;
        OPCParam opcParam = new OPCParam();
        DataTable dtopc = new DataTable();
#if !DEBUG
        OPCClient opcClient = new OPCClient();
#endif

        public FrmMain()
        {
            InitializeComponent();
            dtview.Columns.Add(new DataColumn("Code"));
            dtview.Columns.Add(new DataColumn("ToLocation"));
            dtview.Columns.Add(new DataColumn("PanelNo"));
            dtview.Columns.Add(new DataColumn("Floor"));
            dtview.Columns.Add(new DataColumn("FloorIndex"));
            dtview.Columns.Add(new DataColumn("Diameter"));
            dtview.Columns.Add(new DataColumn("Coordinates"));
            dtview.Columns.Add(new DataColumn("Finished"));

#if !DEBUG
            dtopc = OPCParam.Query();
            dtopc.Columns.Remove("Class");
            dtopc.Columns.Add(new DataColumn("Value"));
            OPCParam.GetNoneCfg(opcParam);
            if (opcClient.Open(clsSetting.OPCServerIP))
            {
                ViewInfo(string.Format("{0} OPC服务连接成功。加订阅。", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                opcClient.AddSubscription(dtopc);
                opcClient.Write(opcParam.ScanState, false);
            }
            else
            {
                ViewInfo(string.Format("{0} OPC服务连接失败。", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            }
#endif
#if DEBUG
            label11.Visible = true;
            label6.Visible = true;
            numericUpDown1.Visible = true;
            textBox1.Visible = true;
            txtDiameter.Text = ran.Next(diameterMin, diameterMax).ToString();
            txtLength.Text = ran.Next(lengthMin, lengthMax).ToString();
            textBox1.Text = (1).ToString().PadLeft(10, '0');
#endif
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("{0} V{1}", "广州金海狸数据采集软件单机版", Application.ProductVersion.ToString());
            cmbShiftNo.SelectedIndex = 0;
            BindDgv();
            SetButtonState(false);
            InitCfgView();
            LableCode.DeleteAllFinished();
        }

        private void BindDgv()
        {
            dtview = LableCode.Query(string.Format("{0}{1}", dtpDate.Value.ToString("yyyyMMdd"), cmbShiftNo.SelectedIndex));
            dgvData.DataSource = dtview.DefaultView;
            dgvOpcData.DataSource = dtopc;
        }

        void SetButtonState(bool isRun)
        {
            this.Invoke((EventHandler)(delegate
            {
                btnSet.Enabled = !isRun;
                btnRun.Enabled = !isRun;
                btnQuit.Enabled = !isRun;
                groupBox1.Enabled = !isRun;
                grbHandwork.Enabled = isRun;
                btnStop.Enabled = isRun;
            }));
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            dtpDate.Value = DateTime.Now;
            SetButtonState(true);
            ViewInfo(string.Format("{0} 运行", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
#if DEBUG
            timer2.Interval = int.Parse(numericUpDown1.Value.ToString());
            timer2.Start();
#endif
#if !DEBUG
            nscan1.OnDataArrived += new NormalScan.DataArrivedEventHandler(nscan1_OnDataArrived);
            if (OpenPort(FrmSet.pcfgScan1))
            {
                nscan1._StartJob();
                ViewInfo(string.Format("{0} 1#采集器启动成功。", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            }
            else
            {
                ViewInfo(string.Format("{0} 1#采集器启动失败。", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            }
            nscan2.OnDataArrived += new NormalScan.DataArrivedEventHandler(nscan2_OnDataArrived);
            if (OpenPort(FrmSet.pcfgScan2))
            {
                nscan2._StartJob();
                ViewInfo(string.Format("{0} 2#采集器启动成功。", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            }
            else
            {
                ViewInfo(string.Format("{0} 2#采集器启动失败。", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            }
            isrun = true;
            if (opcClient.Connected)
            {
                WeighTask();
                ACAreaFinishTask();
                BeforCacheTask();
            }
#endif
        }

#if !DEBUG
        private void WeighTask()
        {
            Task.Factory.StartNew(() =>
            {
                while (isrun)
                {
                    string getWeight = opcClient.Read(opcParam.GetWeigh).ToString();
                    if (getWeight == "1")
                    {
                        getWeight = ToWeigh(false) ? "0" : "2";
                        opcClient.Write(opcParam.GetWeigh, getWeight);
                    }
                    Thread.Sleep(2);
                }
            });
        }

        private void ACAreaFinishTask()
        {
            OPCParam.GetACAreaFinishCfg(opcParam);
            foreach (KeyValuePair<string, LCodeSignal> kv in opcParam.ACAreaPanelFinish)
            {
                Task.Factory.StartNew(() =>
                {
                    while (isrun)
                    {
                        string signal = opcClient.Read(kv.Value.Signal).ToString();
                        if (signal == "1")
                        {
                            string lable1 = opcClient.Read(kv.Value.LCode1).ToString();
                            string lable2 = opcClient.Read(kv.Value.LCode2).ToString();
                            string fullLable = lable1.PadLeft(6, '0') + lable2.PadLeft(6, '0');
                            if (!string.IsNullOrEmpty(fullLable))
                            {
                                AreaAAndCFinish(fullLable);
                            }
                            opcClient.Write(kv.Value.Signal, "0");
                        }
                        Thread.Sleep(2);
                    }
                });
            }
        }

        private void BeforCacheTask()
        {
            Task.Factory.StartNew(() =>
            {
                while (isrun)
                {
                    string lable1 = opcClient.Read(opcParam.BeforCacheLable1).ToString();
                    string lable2 = opcClient.Read(opcParam.BeforCacheLable2).ToString();
                    string fullLable = lable1.PadLeft(6, '0') + lable2.PadLeft(6, '0');
                    if (!string.IsNullOrEmpty(fullLable))
                    {
                        //计算位置
                        opcClient.Write(opcParam.BeforCacheLable1, "");
                        opcClient.Write(opcParam.BeforCacheLable2, "");
                    }
                    Thread.Sleep(2);
                }
            });
        }

        private void viewopcdata()
        {
            foreach (DataRow dr in dtopc.Rows)
            {
                try
                {
                    dr["Value"] = opcClient.Read(dr["Code"].ToString());
                }
                catch { }
            }
        }
#endif

#if DEBUG
#endif

        void nscan1_OnDataArrived(string type, string code)
        {
            nscan_OnDataArrived(type, code, 1);
        }

        void nscan2_OnDataArrived(string type, string code)
        {
            nscan_OnDataArrived(type, code, 2);
        }

        void nscan_OnDataArrived(string type, string code, int scanNo)
        {
            if (code == "ERROR" || code.Length < 12)
            {
                return;
            }
            code = code.Substring(0, 12);//条码请取前面12位,有些扫描器会扫出13位是因为把后面的识别码也读出来了.摘自2016年9月10日(星期六) 下午2:37邮件：答复: 答复: 9月9号夜班布卷扫描枪PC连接不上ERP说明

            decimal diameter, length;
#if DEBUG
            diameter = decimal.Parse(txtDiameter.Text);
            txtDiameter.Text = ran.Next(diameterMin, diameterMax).ToString();
            length = decimal.Parse(txtLength.Text);
            txtLength.Text = ran.Next(lengthMin, lengthMax).ToString();
#endif
#if !DEBUG
            string tmp = opcClient.Read(opcParam.Diameter).ToString();
            ViewInfo(string.Format("Diameter {0}", tmp));
            diameter = decimal.Parse(tmp);
            tmp = opcClient.Read(opcParam.Length).ToString();
            ViewInfo(string.Format("Length {0}", tmp));
            length = decimal.Parse(tmp);
#endif
            ScanLableCode(diameter, length, code, scanNo, false);
        }

        private bool OpenPort(CommunicationCfg cfg)
        {
            try
            {
                switch ((CommunicationType)Enum.Parse(typeof(CommunicationType), cfg.CommunicationType, true))
                {
                    case CommunicationType.Network:
                        if (!nscan1.Open(CommunicationType.Network, cfg.IPAddr, int.Parse(cfg.IPPort)))
                        {
                            ViewInfo(string.Format("{0}/{1}打开失败。", cfg.IPAddr, cfg.IPPort));
                            return false;
                        }
                        break;
                    case CommunicationType.SerialPort:
                        if (!nscan1.Open(CommunicationType.SerialPort, cfg.ComPort, int.Parse(cfg.BaudRate)))
                        {
                            ViewInfo(string.Format("{0}/{1}打开失败。", cfg.ComPort, cfg.BaudRate));
                            return false;
                        }
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                clsSetting.loger.Error(string.Format("{0}", ex.ToString()));
                return false;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要停止吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            SetButtonState(false);
            ViewInfo(string.Format("{0} 停止", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            isrun = false;
#if DEBUG
            timer2.Stop();
#endif
#if !DEBUG
            try
            {
                nscan1._StopJob();
                nscan1.Close();
            }
            catch { }
            try
            {
                nscan2._StopJob();
                nscan2.Close();
            }
            catch { }
#endif
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            FrmSet fsp = new FrmSet();
            if (fsp.ShowDialog() == DialogResult.OK)
            {
                InitCfgView();
            }
        }

        private void InitCfgView()
        {
            switch (FrmSet.pcfgScan1.CommunicationType)
            {
                case "Network":
                    lblScanner.Text = string.Format("Scanner1:{0}/{1}",
                        FrmSet.pcfgScan1.IPAddr, FrmSet.pcfgScan1.IPPort);
                    break;
                case "SerialPort":
                    lblScanner.Text = string.Format("Scanner1:{0}/{1}",
                        FrmSet.pcfgScan1.ComPort, FrmSet.pcfgScan1.BaudRate);
                    break;
                default:
                    lblScanner.Text = string.Format("Scanner1:Unknown");
                    break;
            }
            switch (FrmSet.pcfgScan2.CommunicationType)
            {
                case "Network":
                    lblScanner2.Text = string.Format("Scanner2:{0}/{1}",
                        FrmSet.pcfgScan2.IPAddr, FrmSet.pcfgScan2.IPPort);
                    break;
                case "SerialPort":
                    lblScanner2.Text = string.Format("Scanner2:{0}/{1}",
                        FrmSet.pcfgScan2.ComPort, FrmSet.pcfgScan2.BaudRate);
                    break;
                default:
                    lblScanner2.Text = string.Format("Scanner2:Unknown");
                    break;
            }
            lblOpcIp.Text = string.Format("OPCIP:{0}", string.IsNullOrEmpty(clsSetting.OPCServerIP) ? "Unknown" : clsSetting.OPCServerIP);
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();//Application.Exit();
        }

        private void btnToWeigh_Click(object sender, EventArgs e)
        {
            ToWeigh();
        }

        private bool ToWeigh(bool handwork = true)
        {
            Dictionary<string, string> re = CallWebApi.Post(clsSetting.ToWeight, new Dictionary<string, string>());

            if (re["State"] == "Fail")
            {
                ViewInfo(string.Format("{0} {1}称重{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), (handwork ? "手工" : "自动"), re["ERR"]));
                return false;
            }
            else
            {
                ViewInfo(string.Format("{0} {1}称重{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), (handwork ? "手工" : "自动"), re["Data"]));
                return true;
            }
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            PanelEnd(txtPanelNo.Text, true);
        }

        private bool PanelEnd(string panelNo, bool handwork = false)
        {
            if (!string.IsNullOrEmpty(panelNo))
            {
                if (!LableCode.PanelNoHas(panelNo))
                {
                    ViewInfo(string.Format("{0} {1}板号{2}不存在，无法完成", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), (handwork ? "手工" : "自动"), panelNo));
                }
                else if (LableCode.PanelNoFinished(panelNo))
                {
                    ViewInfo(string.Format("{0} {1}板号{2}已完成，不能重复完成", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), (handwork ? "手工" : "自动"), panelNo));
                }
                else
                {
                    List<string> data = new List<string>();
                    dtview.DefaultView.RowFilter = string.Format("PanelNo='{0}'", panelNo);
                    for (int i = 0; i < dtview.DefaultView.Count; i++)
                    {
                        DataRow r = dtview.DefaultView[i].Row;//这是一个行对象
                        r["Finished"] = "完成";
                        data.Add(r["Code"].ToString());
                    }
                    dtview.DefaultView.RowFilter = string.Empty;
                    Dictionary<string, string> re = CallWebApi.Post(clsSetting.PanelFinish, new Dictionary<string, string>() { { "Board_No", panelNo }, { "AllBarCode", string.Join(",", data.ToArray()) } });

                    if (re["State"] == "Fail")
                    {
                        ViewInfo(string.Format("{0} {1}板号{2}完成失败。{3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), (handwork ? "手工" : "自动"), panelNo, re["ERR"]));
                    }
                    else
                    {
                        ViewInfo(string.Format("{0} {1}板号{2}完成成功。{3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), (handwork ? "手工" : "自动"), panelNo, re["Data"]));
                        return true;
                    }
                }
            }
            return false;
        }

        void ViewInfo(string msg, bool debug = false)
        {
            if (!debug)
                lsvLog.Items.Insert(0, msg);
            clsSetting.loger.Error(msg);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimer.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        Random ran = new Random();
        int diameterMin = 80;
        int diameterMax = 260;
        int lengthMin = 1100;
        int lengthMax = 1800;
        private void txtLableCode1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                string code = txtLableCode1.Text.Trim();
                if (code.Length < 12)
                {
                    return;
                }
                code = code.Substring(0, 12);
#if DEBUG
                decimal diameter = decimal.Parse(txtDiameter.Text);
                decimal length = decimal.Parse(txtLength.Text);
                txtDiameter.Text = ran.Next(diameterMin, diameterMax).ToString();
                txtLength.Text = ran.Next(lengthMin, lengthMax).ToString();
#endif
#if !DEBUG
                decimal diameter;
                decimal length;
                if (string.IsNullOrEmpty(txtDiameter.Text))
                {
                    string tmp = opcClient.Read(opcParam.Diameter).ToString();
                    ViewInfo(string.Format("Diameter {0}", tmp));
                    diameter = decimal.Parse(tmp);
                }
                else
                {
                    diameter = decimal.Parse(txtDiameter.Text);
                }
                if (string.IsNullOrEmpty(txtLength.Text))
                {
                    string tmp = opcClient.Read(opcParam.Length).ToString();
                    ViewInfo(string.Format("Length {0}", tmp));
                    length = decimal.Parse(tmp);
                }
                else
                {
                    length = decimal.Parse(txtLength.Text);
                }
#endif
                ScanLableCode(diameter, length, code, 1, true);
                txtLableCode1.Text = string.Empty;
            }
        }

        private void ScanLableCode(decimal diameter, decimal length, string code, int scanNo, bool handwork)
        {
            string tolocation = GetLocation(code, handwork);
            if (string.IsNullOrEmpty(tolocation))
                return;

            LableCode lc = new LableCode();
            lc.LCode = code;
            lc.ToLocation = tolocation;
            lc.Diameter = diameter;
            lc.Remark = (handwork ? "handwork" : "automatic");
            lc.Coordinates = "";
            if (LableCode.Add(lc))
                ViewAddLable(false, lc);
#if !DEBUG
            object f = opcClient.Read(opcParam.ScanState);
            ViewInfo(string.Format("scanstate {0}", f));
            while (bool.Parse(f.ToString()))
            {
                Thread.Sleep(2);
                f = opcClient.Read(opcParam.ScanState);
            }
            bool tmp = opcClient.Write(opcParam.ToLocationArea, clsSetting.AreaNo[lc.ToLocation.Substring(0, 1)]);
            tmp = opcClient.Write(opcParam.ToLocationNo, lc.ToLocation.Substring(1, 2));
            tmp = opcClient.Write(opcParam.ScanLable1, lc.LCode.Substring(0, 6));
            tmp = opcClient.Write(opcParam.ScanLable2, lc.LCode.Substring(6, 6));
            tmp = opcClient.Write(opcParam.CameraNo, scanNo);
            tmp = opcClient.Write(opcParam.ScanState, true);
            viewopcdata();
#endif
        }

        private bool AreaAAndCFinish(string lCode)
        {
            LableCode lc = LableCode.QueryByLCode(lCode);
            lcb.GetPanelNo(lc, dtpDate.Value, cmbShiftNo.SelectedIndex);
            LableCode.Update(lc);
            if (LableCode.SetPanelNo(lCode))
            {
                return PanelEnd(lc.PanelNo);
            }
            else
                return false;
        }

        private void AreaBCalculate(string lCode)
        {
            LableCode lc = LableCode.QueryByLCode(lCode);
            List<LableCode> lcs = LableCode.GetLastLableCode(lc.ToLocation, string.Format("{0}{1}", dtpDate.Value.ToString("yyyyMMdd"),
                            cmbShiftNo.SelectedIndex.ToString()));
            if (lcs == null || lcs.Count == 0)
            {
                lcb.GetPanelNo(lc, dtpDate.Value, cmbShiftNo.SelectedIndex);
                LableCode.Update(lc);
            }
            else
            {
                FloorPerformance fp = FloorPerformance.None;
                PanelInfo pinfo = LableCode.GetPanel(lcs[0].PanelNo);
                lc.PanelNo = lcs[0].PanelNo;
                lc.Floor = pinfo.CurrFloor;
                lc.FloorIndex = 0;
                lc.Coordinates = "";
                LableCode lc2 = null;
                if (pinfo.CurrFloor == lcs[0].Floor)
                {
                    lc2 = lcb.CalculateFinish(lcs, lc);
                    if (lc2 != null)//不为NULL，表示满
                    {
                        lcb.CalculatePosition(lcs, lc, lc2);//计算位置坐标

                        if (lc.FloorIndex % 2 == 0)
                        {
                            pinfo.EvenStatus = true;
                            fp = FloorPerformance.EvenFinish;
                        }
                        else
                        {
                            pinfo.OddStatus = true;
                            fp = FloorPerformance.OddFinish;
                        }
                    }
                    else
                    {
                        lc2 = lcb.CalculateCache(pinfo, lc, lcs);//计算缓存，lc2不为NULL需要缓存
                    }
                }
                if (pinfo.EvenStatus && pinfo.OddStatus)
                    fp = FloorPerformance.BothFinish;
                if (lc2 != null)
                {
                    if (LableCode.Update(fp, lc, lc2))
                        ViewAddLable(fp == FloorPerformance.BothFinish && lc.Floor == 7, lc, lc2);
                }
                else
                {
                    if (LableCode.Update(fp, lc))
                        ViewAddLable(fp == FloorPerformance.BothFinish && lc.Floor == 7, lc);
                }
            }
        }

        private void ViewAddLable(bool finish, LableCode lc, LableCode lc2 = null)
        {
            if (lc2 != null)
            {
                dtview.DefaultView.RowFilter = string.Format("Code='{0}'", lc2.LCode);
                for (int i = 0; i < dtview.DefaultView.Count; i++)
                {
                    DataRow r = dtview.DefaultView[i].Row;//这是一个行对象
                    r["FloorIndex"] = lc2.FloorIndex;
                    r["Coordinates"] = lc2.Coordinates;
                }
                dtview.DefaultView.RowFilter = string.Empty;
            }
            DataRow dr = dtview.NewRow();
            dr["Code"] = lc.LCode;
            dr["ToLocation"] = lc.ToLocation;
            dr["PanelNo"] = lc.PanelNo;
            dr["Floor"] = lc.Floor;
            dr["FloorIndex"] = lc.FloorIndex;
            dr["Diameter"] = lc.Diameter;
            dr["Coordinates"] = lc.Coordinates;
            dr["Finished"] = "未完成";
            dtview.Rows.InsertAt(dr, 0);
            dgvData.FirstDisplayedScrollingRowIndex = 0;
            if (finish)
            {
                PanelEnd(lc.PanelNo);
            }
#if DEBUG
#endif
            txtLableCode1.Text = string.Empty;
        }

        private string GetLocation(string code, bool handwork)
        {
            string re = string.Empty;
            LableCode dt = LableCode.QueryByLCode(code);
            if (dt != null)
            {
                ViewInfo(string.Format("{0} {1}重复扫描{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), (handwork ? "手工" : "自动"), code));
            }
            else
            {
                Dictionary<string, string> str;
                try
                {
                    str = CallWebApi.Post(clsSetting.GetLocation, new Dictionary<string, string>() { { "Bar_Code", code } });
                    DataTable res = JsonConvert.DeserializeObject<DataTable>(str["Data"].ToString());
                    if (str["State"] == "Fail" || res.Rows[0]["LOCATION"].ToString() == "Fail")
                    {
                        ViewInfo(string.Format("{0} {1}{2}获取交地失败。", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), (handwork ? "手工" : "自动"), code, JsonConvert.SerializeObject(str)));
                    }
                    else if (res.Rows.Count > 0)
                    {
                        re = res.Rows[0]["LOCATION"].ToString();
                        ViewInfo(string.Format("{0} {1}扫描{2}交地{3}。{4}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), (handwork ? "手工" : "自动"), code, txtToLocation.Text, str["Data"]));
                    }
                    else
                    {
                        ViewInfo(string.Format("{0} {1}{2}获取交地失败。{3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), (handwork ? "手工" : "自动"), code, JsonConvert.SerializeObject(str)));
                    }
                }
                catch (Exception ex)
                {
                    ViewInfo(string.Format(ex.Message));
                }
            }
            return re;
        }

        private void txtLableCode1_Enter(object sender, EventArgs e)
        {
            txtLableCode1.Text = string.Empty;
        }

        private void txtLableCode1_Leave(object sender, EventArgs e)
        {
            txtLableCode1.Text = "请将光标放置到这里扫描";
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    //帮助
                    break;
                case Keys.F2:
                    txtLableCode1.Focus();
                    break;
                case Keys.F3:
                    btnToWeigh_Click(null, null);
                    break;
                case Keys.F4:
                    txtToLocation.Focus();
                    break;
                case Keys.F5:
                    txtPanelNo.Focus();
                    break;
                case Keys.F6:
                    btnComplete_Click(null, null);
                    break;
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnStop.Enabled)
            {
                MessageBox.Show(string.Format("正在运行无法关闭软件！"));
                e.Cancel = true;
            }
#if !DEBUG
            else
            {
                opcClient.Close();
            }
#endif
        }

        private void txtPanelNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnComplete_Click(null, null);
            }
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            BindDgv();
        }

        private void cmbShiftNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDgv();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            textBox1_DoubleClick(null, null);
            textBox1.Text = (double.Parse(textBox1.Text) + 1).ToString().PadLeft(10, '0');
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            decimal diameter = decimal.Parse(txtDiameter.Text);
            decimal length = decimal.Parse(txtLength.Text);
            string code = textBox1.Text.Trim();

            ScanLableCode(diameter, length, code, 1, true);
            txtDiameter.Text = ran.Next(diameterMin, diameterMax).ToString();
            txtLength.Text = ran.Next(lengthMin, lengthMax).ToString();
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = System.IO.Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, @"log");//注意这里写路径时要用c:\\而不是c:\
            openFileDialog1.Filter = "文本文件|*.log";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.Diagnostics.Process.Start("notepad.exe", openFileDialog1.FileName);
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
#if !DEBUG
            timer3.Stop();
            viewopcdata();
            timer3.Start();
#endif
        }
    }
}
