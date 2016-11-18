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
using System.Drawing;

namespace yidascan {
    public partial class FrmMain : Form {
        private DataTable dtview = new DataTable();
        NormalScan nscan1;
        NormalScan nscan2;
        private LableCodeBll lcb = new LableCodeBll();
        bool isrun = false;
        OPCParam opcParam = new OPCParam();
        DataTable dtopc = new DataTable();
        LogOpreate logOpt;

        // mutex to ensure securely calling opc.
        Mutex OPC_IDLE = new Mutex();
#if !DEBUG
        OPCClient opcClient = new OPCClient();
#endif

        public FrmMain() {
            InitializeComponent();
            logOpt = new ProduceComm.LogOpreate(lsvLog);
            dtview.Columns.Add(new DataColumn("Code"));
            dtview.Columns.Add(new DataColumn("ToLocation"));
            dtview.Columns.Add(new DataColumn("PanelNo"));
            dtview.Columns.Add(new DataColumn("Floor"));
            dtview.Columns.Add(new DataColumn("FloorIndex"));
            dtview.Columns.Add(new DataColumn("Diameter"));
            dtview.Columns.Add(new DataColumn("Coordinates"));
            dtview.Columns.Add(new DataColumn("Finished"));

            dtopc = OPCParam.Query();
            dtopc.Columns.Remove("Class");
            dtopc.Columns.Add(new DataColumn("Value"));
#if !DEBUG
            if (opcClient.Open(clsSetting.OPCServerIP)) {
                logOpt.ViewInfo(string.Format("OPC服务连接成功。加订阅。"), LogViewType.Both);
                opcClient.AddSubscription(dtopc);
            } else {
                logOpt.ViewInfo(string.Format("OPC服务连接失败。"), LogViewType.Both);
            }
            opcParam.Init();
#endif
#if DEBUG
            lblCount.Visible = false;
            lblMsgInfo.Visible = false;

            label11.Visible = true;
            label6.Visible = true;
            numericUpDown1.Visible = true;
            textBox1.Visible = true;
            txtDiameter.Text = ran.Next(diameterMin, diameterMax).ToString();
            txtLength.Text = ran.Next(lengthMin, lengthMax).ToString();
            textBox1.Text = (1).ToString().PadLeft(10, '0');
#endif
        }
        private void FrmMain_Load(object sender, EventArgs e) {
            this.Text = string.Format("{0} V{1}", clsSetting.PRODUCT_NAME, Application.ProductVersion.ToString());
            cmbShiftNo.SelectedIndex = 0;
            BindDgv();
            SetButtonState(false);
            InitCfgView();
            LableCode.DeleteAllFinished();
        }

        private void BindDgv() {
            dtview = LableCode.Query(string.Format("{0}{1}", dtpDate.Value.ToString(clsSetting.LABEL_CODE_DATE_FORMAT), cmbShiftNo.SelectedIndex));
            dgvData.DataSource = dtview.DefaultView;
            lblCount.Text = dtview.DefaultView.Count.ToString();

            dtopc.DefaultView.RowFilter = string.Format("IndexNo<15");
            dgvOpcData.DataSource = dtopc.DefaultView;
        }

        void SetButtonState(bool isRun) {
            this.Invoke((EventHandler)(delegate {
                btnSet.Enabled = !isRun;
                btnRun.Enabled = !isRun;
                btnQuit.Enabled = !isRun;
                groupBox1.Enabled = !isRun;
                grbHandwork.Enabled = isRun;
                btnStop.Enabled = isRun;
            }));
        }

        public void logger(string s) {
            logOpt.ViewInfo(s);
        }

        private void StartScanner() {
            if (OpenPort(ref nscan1, FrmSet.pcfgScan1)) {
                nscan1.logger = logger;
                nscan1.OnDataArrived += new NormalScan.DataArrivedEventHandler(nscan1_OnDataArrived);
                nscan1._StartJob();
                lblScanner.BackColor = System.Drawing.Color.Green;
                logOpt.ViewInfo(string.Format("1#采集器启动成功。"), LogViewType.Both);
            } else {
                lblScanner2.BackColor = System.Drawing.Color.Gray;
                logOpt.ViewInfo(string.Format("1#采集器启动失败。"), LogViewType.Both);
            }
            //if (OpenPort(FrmSet.pcfgScan2))
            //{
            //    nscan2.OnDataArrived += new NormalScan.DataArrivedEventHandler(nscan2_OnDataArrived);
            //    nscan2._StartJob();
            //    lblScanner2.BackColor = System.Drawing.Color.Green;
            //    logOpt.ViewInfo(string.Format("2#采集器启动成功。"), LogViewType.Both);
            //}
            //else
            //{
            //    lblScanner2.BackColor = System.Drawing.Color.Gray;
            //    logOpt.ViewInfo(string.Format("2#采集器启动失败。"), LogViewType.Both);
            //}
        }

        private void btnRun_Click(object sender, EventArgs e) {
            dtpDate.Value = DateTime.Now;
            SetButtonState(true);
            logOpt.ViewInfo(string.Format("运行"), LogViewType.Both);

            StartScanner();
            isrun = true;
#if DEBUG
            timer2.Interval = int.Parse(numericUpDown1.Value.ToString());
            timer2.Start();
#endif
#if !DEBUG
            if (opcClient.Connected) {
                lblOpcIp.BackColor = System.Drawing.Color.Green;
                WeighTask();
                ACAreaFinishTask();
                BeforCacheTask();
            } else {
                ShowWarning("启动设备失败！");

            }
#endif
        }

#if !DEBUG
        private void WeighTask() {
            Task.Factory.StartNew(() => {
                while (isrun) {
                    lock (opcClient) {
                        string getWeight = OPCRead(opcParam.ScanParam.GetWeigh).ToString();
                        if (getWeight == "1") {
                            getWeight = ToWeigh(false) ? "0" : "2";
                            opcClient.Write(opcParam.ScanParam.GetWeigh, getWeight);
                        }
                    }
                    Thread.Sleep(OPCClient.DELAY * 300);
                }
            });
        }

        private void ACAreaFinishTask() {
            foreach (KeyValuePair<string, LCodeSignal> kv in opcParam.ACAreaPanelFinish) {
                Task.Factory.StartNew(() => {
                    while (isrun) {
                        lock (opcClient) {
                            string signal = OPCRead(kv.Value.Signal).ToString();

                            if (bool.Parse(signal)) {
                                string fullLable = ReadACCompleteLable(kv.Value);

                                logOpt.ViewInfo(string.Format("{0} 收到完成信号：{1}", kv.Value.Signal, fullLable));

                                try {
                                    if (!string.IsNullOrEmpty(fullLable)) {
                                        logOpt.ViewInfo(string.Format("执行状态：{0}",
                                            AreaAAndCFinish(fullLable)), LogViewType.Both);
                                    }
                                } catch (Exception ex) {
                                    logOpt.ViewInfo(ex.Message, LogViewType.Both);
                                }

                                opcClient.Write(kv.Value.Signal, 0);
                            }
                        }
                        Thread.Sleep(OPCClient.DELAY * 200);
                    }
                });
            }
        }

        private string ReadACCompleteLable(LCodeSignal slot) {
            const int MAX_LEN = 6;
            string lable1 = OPCRead(slot.LCode1).ToString();
            string lable2 = OPCRead(slot.LCode2).ToString();
            return lable1.PadLeft(MAX_LEN, '0') + lable2.PadLeft(MAX_LEN, '0');
        }

        /// <summary>
        /// 读CacheParam.BeforCacheLable1和CacheParam.BeforCacheLable2的值。
        /// 然后拼起来。结果保证12位的长度。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private string ReadFullBeforeCacheLabel(OPCClient client, OPCParam param) {
            const int MAX_LEN = 6;
            string first = client.Read(param.CacheParam.BeforCacheLable1).ToString();
            string second = client.Read(param.CacheParam.BeforCacheLable2).ToString();
            return first.PadLeft(MAX_LEN, '0') + second.PadLeft(MAX_LEN, '0');
        }

        /// <summary>
        /// 读PLC的CacheParam.BeforCacheStatus值。
        /// 如果读到的不是bool值，会弹出异常。
        /// </summary>
        /// <param name="client">OPCClient实例</param>
        /// <param name="param">OPCParam实例</param>
        /// <returns></returns>
        private bool ReadBeforeCacheStatus(OPCClient client, OPCParam param) {
            var r = client.Read(param.CacheParam.BeforCacheStatus);
            return bool.Parse(r.ToString());
        }

        private void BeforCacheTask() {
            Task.Factory.StartNew(() => {
                while (isrun) {
                    lock (opcClient) {
                        if (ReadBeforeCacheStatus(opcClient, opcParam)) {
                            var fullLable = ReadFullBeforeCacheLabel(opcClient, opcParam);
                            if (!string.IsNullOrEmpty(fullLable)) {
                                AreaBCalculate(fullLable); //计算位置
                                opcClient.Write(opcParam.CacheParam.BeforCacheStatus, false);
                            }
                        }
                    }
                    Thread.Sleep(OPCClient.DELAY * 200);
                }
            });
        }

        private void viewopcdata() {
            long t = TimeCount.TimeIt(() => {
                Task.Factory.StartNew(() => {
                    for (int i = 0; i < dtopc.DefaultView.Count; i++) {
                        try {
                            lock (opcClient) {
                                DataRow r = dtopc.DefaultView[i].Row;//这是一个行对象
                                r["Value"] = OPCRead(r["Code"].ToString());
                            }
                            Thread.Sleep(OPCClient.DELAY);
                        } catch { }
                    }
                });
            });
            logOpt.ViewInfo(string.Format("更新ＯＰＣ显示耗时：{0}ms", t), LogViewType.Both);
        }

        private object OPCRead(string code) {
            object val = opcClient.Read(code);
            if (val == null) {
                logOpt.ViewInfo(string.Format("警告：OPC项目[{0}]质量：坏。", code), LogViewType.Both);
                return string.Empty;
            } else {
                return val;
            }
        }
#endif

#if DEBUG
#endif

        void nscan1_OnDataArrived(string type, string code) {
            nscan_OnDataArrived(type, code, 1);
        }

        void nscan2_OnDataArrived(string type, string code) {
            nscan_OnDataArrived(type, code, 2);
        }

        void nscan_OnDataArrived(string type, string code, int scanNo) {

            if (code == "ERROR" || code.Length < 12) {
                return;
            }
            code = code.Substring(0, 12);//条码请取前面12位,有些扫描器会扫出13位是因为把后面的识别码也读出来了.摘自2016年9月10日(星期六) 下午2:37邮件：答复: 答复: 9月9号夜班布卷扫描枪PC连接不上ERP说明

            // wait for opc available.
            // must use try/finally block to release this mutex.
            OPC_IDLE.WaitOne();
            try {
                ScanLableCode(code, scanNo, false);
            } finally {
                OPC_IDLE.ReleaseMutex();
            }
        }

        private bool OpenPort(ref NormalScan nscan, CommunicationCfg cfg) {
            try {
                switch ((CommunicationType)Enum.Parse(typeof(CommunicationType), cfg.CommunicationType, true)) {
                    case CommunicationType.Network:
                        nscan = new NormalScan(new TcpIpManage(cfg.IPAddr, int.Parse(cfg.IPPort)));
                        break;
                    case CommunicationType.SerialPort:
                        nscan = new NormalScan(new SerialPortManage(cfg.ComPort, int.Parse(cfg.BaudRate)));
                        break;
                }
                return nscan.Open();
            } catch (Exception ex) {
                clsSetting.loger.Error(string.Format("{0}", ex.ToString()));
                return false;
            }
        }

        private void StopAllJobs() {
            isrun = false;
#if DEBUG
            timer2.Stop();
#endif
#if !DEBUG
            try {
                nscan1._StopJob();
                nscan1.Close();
            } catch (Exception ex) {
                logOpt.ViewInfo("关闭扫描设备#1失败。\n" + ex);
            }

            try {
                nscan2._StopJob();
                nscan2.Close();
            } catch (Exception ex) {
                logOpt.ViewInfo("关闭扫描设备#2失败。\n" + ex);
            }

#endif

            SetButtonState(false);
            logOpt.ViewInfo("停止操作完成。");
            ShowWarning("空闲", false);
        }

        private void btnStop_Click(object sender, EventArgs e) {
            if (!confirm("确定停止吗?")) { return; }
            StopAllJobs();
        }

        private void btnSet_Click(object sender, EventArgs e) {
            FrmSet fsp = new FrmSet();
            if (fsp.ShowDialog() == DialogResult.OK) {
                InitCfgView();
            }
        }

        private void InitCfgView() {
            switch (FrmSet.pcfgScan1.CommunicationType) {
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
            switch (FrmSet.pcfgScan2.CommunicationType) {
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

        private void btnQuit_Click(object sender, EventArgs e) {
            Close();//Application.Exit();
        }

        private void btnToWeigh_Click(object sender, EventArgs e) {
            ToWeigh();
        }

        private bool ToWeigh(bool handwork = true) {
            Dictionary<string, string> re = CallWebApi.Post(clsSetting.ToWeight, new Dictionary<string, string>());

            if (re["State"] == "Fail") {
                logOpt.ViewInfo(string.Format("{0}称重{1}", (handwork ? "手工" : "自动"), re["ERR"]), LogViewType.Both);
                return false;
            } else {
                logOpt.ViewInfo(string.Format("{0}称重{1}", (handwork ? "手工" : "自动"), re["Data"]), LogViewType.Both);
                return true;
            }
        }

        private void btnComplete_Click(object sender, EventArgs e) {
            PanelEnd(txtToLocation.Text, true);
        }

        private bool PanelEnd(string panelNo, bool handwork = false) {
            if (!string.IsNullOrEmpty(panelNo)) {
                List<string> data = new List<string>();
                dtview.DefaultView.RowFilter = string.Format("PanelNo='{0}'", panelNo);
                for (int i = 0; i < dtview.DefaultView.Count; i++) {
                    DataRow r = dtview.DefaultView[i].Row;//这是一个行对象
                    r["Finished"] = "完成";
                    data.Add(r["Code"].ToString());
                }
                dtview.DefaultView.RowFilter = string.Empty;
                Dictionary<string, string> re = CallWebApi.Post(clsSetting.PanelFinish,
                    new Dictionary<string, string>() { { "Board_No", panelNo },
                        { "AllBarCode", string.Join(",", data.ToArray()) } });

                if (re["State"] == "Fail") {
                    logOpt.ViewInfo(string.Format("{0}板号{1}完成失败。{2}", (handwork ? "手工" : "自动"),
                        JsonConvert.SerializeObject(data), re["ERR"]), LogViewType.Both);
                } else {
                    logOpt.ViewInfo(string.Format("{0}板号{1}完成成功。{2}", (handwork ? "手工" : "自动"),
                        JsonConvert.SerializeObject(data), re["Data"]), LogViewType.Both);
                    return true;
                }
            }
            return false;
        }

        private void timer1_Tick(object sender, EventArgs e) {
            lblTimer.Text = DateTime.Now.ToString(clsSetting.DATE_FORMAT);
        }

        Random ran = new Random();
        int diameterMin = 80;
        int diameterMax = 260;
        int lengthMin = 1100;
        int lengthMax = 1800;

        /// <summary>
        /// read scanned code from input box.
        /// </summary>
        /// <returns></returns>
        private string GetCodeFromInput() {
            string code = txtLableCode1.Text.Trim();
            return code.Length >= 12
                ? code.Substring(0, 12)
                : string.Empty;
        }

        private void txtLableCode1_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '\r') {
                var code = GetCodeFromInput();
                if (string.IsNullOrEmpty(code)) { return; }

                txtLableCode1.Enabled = false;
                // waiting for mutex available.
                OPC_IDLE.WaitOne();
                try {
                    ScanLableCode(code, 0, true);
                } finally {
                    OPC_IDLE.ReleaseMutex();

                    // reset the code input box.
                    txtLableCode1.Text = string.Empty;
                    txtLableCode1.Enabled = true;
                    txtLableCode1.Focus();
                }
            }
        }

        private void ScanLableCode(string code, int scanNo, bool handwork) {
            ShowWarning("OK", false);

            string tolocation = string.Empty;

            long t = TimeCount.TimeIt(() => {
                tolocation = GetLocation(code, handwork);
            });
            logOpt.ViewInfo(string.Format("取交地耗时:　{0}ms", t), LogViewType.Both);

            if (string.IsNullOrEmpty(tolocation))
                return;

            var clothsize = new ClothRollSize();
#if DEBUG
            clothsize.diameter = decimal.Parse(txtDiameter.Text);
            txtDiameter.Text = ran.Next(diameterMin, diameterMax).ToString();
            clothsize.length = decimal.Parse(txtLength.Text);
            txtLength.Text = ran.Next(lengthMin, lengthMax).ToString();
#endif
#if !DEBUG
            t = TimeCount.TimeIt(() => {
                clothsize.getFromOPC(opcClient, opcParam);
            });
            const string ROLLSIZE_FMT = "Diameter: {0} Length: {1} timer:　{2}ms";
            var msg = string.Format(ROLLSIZE_FMT, clothsize.diameter, clothsize.length, t);
            logOpt.ViewInfo(msg, LogViewType.OnlyForm);
#endif
            LableCode lc = new LableCode();
            lc.LCode = code;
            lc.ToLocation = tolocation;
            lc.Diameter = clothsize.diameter;
            lc.Length = clothsize.length;
            lc.Remark = (handwork ? "handwork" : "automatic");
            lc.Coordinates = "";

#if !DEBUG
            logOpt.ViewInfo(string.Format("开始读OPC"), LogViewType.Both);
            lock (opcClient) {
                t = TimeCount.TimeIt(() => {
                    while (isrun) {
                        var f = OPCRead(opcParam.ScanParam.ScanState);
                        if (!bool.Parse(f.ToString())) { break; }
                        Thread.Sleep(OPCClient.DELAY);
                    }
                });

                logOpt.ViewInfo(string.Format("等OPC ScanState 状态信号耗时：{0}ms", t), LogViewType.Both);

                t = TimeCount.TimeIt(() => {
                    opcClient.Write(opcParam.ScanParam.ToLocationArea, clsSetting.AreaNo[lc.ToLocation.Substring(0, 1)]);
                    opcClient.Write(opcParam.ScanParam.ToLocationNo, lc.ToLocation.Substring(1, 2));
                    opcClient.Write(opcParam.ScanParam.ScanLable1, lc.LCode.Substring(0, 6));
                    opcClient.Write(opcParam.ScanParam.ScanLable2, lc.LCode.Substring(6, 6));
                    opcClient.Write(opcParam.ScanParam.CameraNo, scanNo);
                    opcClient.Write(opcParam.ScanParam.ScanState, true);
                });
                logOpt.ViewInfo(string.Format("写ＯＰＣ耗时：{0}ms", t), LogViewType.Both);

                if (LableCode.Add(lc)) {
                    ViewAddLable(false, lc);
                } else {
                    ShowWarning("程序异常");
                }
            }
#endif
        }

        private bool AreaAAndCFinish(string lCode) {
            // debug
            logOpt.ViewInfo(">>>>> area a anc c finished.");

            LableCode lc = LableCode.QueryByLCode(lCode);

            logOpt.ViewInfo(lc == null ? "无效号码: " + lCode : lc.LCode);
            if (lc == null) { return false; }

            lcb.GetPanelNo(lc, dtpDate.Value, cmbShiftNo.SelectedIndex);
            LableCode.Update(lc);

            if (LableCode.SetPanelNo(lCode)) {
                return PanelEnd(lc.PanelNo);
            } else {
                return false;
            }
        }

        private void AreaBCalculate(string lCode) {
            LableCode lc = LableCode.QueryByLCode(lCode);
            List<LableCode> lcs = LableCode.GetLastLableCode(lc.ToLocation, string.Format("{0}{1}", dtpDate.Value.ToString(clsSetting.LABEL_CODE_DATE_FORMAT),
                            cmbShiftNo.SelectedIndex.ToString()));
            if (lcs == null || lcs.Count == 0) {
                lcb.GetPanelNo(lc, dtpDate.Value, cmbShiftNo.SelectedIndex);
                LableCode.Update(lc);
            } else {
                FloorPerformance fp = FloorPerformance.None;
                PanelInfo pinfo = LableCode.GetPanel(lcs[0].PanelNo);
                lc.PanelNo = lcs[0].PanelNo;
                lc.Floor = pinfo.CurrFloor;
                lc.FloorIndex = 0;
                lc.Coordinates = "";
                LableCode lc2 = null;
                if (pinfo.CurrFloor == lcs[0].Floor) {
                    lc2 = lcb.CalculateFinish(lcs, lc);
                    if (lc2 != null)//不为NULL，表示满
                    {
                        lcb.CalculatePosition(lcs, lc, lc2);//计算位置坐标

                        if (lc.FloorIndex % 2 == 0) {
                            pinfo.EvenStatus = true;
                            fp = FloorPerformance.EvenFinish;
                        } else {
                            pinfo.OddStatus = true;
                            fp = FloorPerformance.OddFinish;
                        }
                    } else {
                        lc2 = lcb.CalculateCache(pinfo, lc, lcs);//计算缓存，lc2不为NULL需要缓存
                    }
                }
                if (pinfo.EvenStatus && pinfo.OddStatus)
                    fp = FloorPerformance.BothFinish;
                if (lc2 != null) {
                    if (LableCode.Update(fp, lc, lc2))
                        ViewAddLable(fp == FloorPerformance.BothFinish && lc.Floor == 7, lc, lc2);
                } else {
                    if (LableCode.Update(fp, lc))
                        ViewAddLable(fp == FloorPerformance.BothFinish && lc.Floor == 7, lc);
                }
            }
        }

        private void ViewAddLable(bool finish, LableCode lc, LableCode lc2 = null) {
            if (lc2 != null) {
                dtview.DefaultView.RowFilter = string.Format("Code='{0}'", lc2.LCode);
                for (int i = 0; i < dtview.DefaultView.Count; i++) {
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
            lblCount.Text = dtview.DefaultView.Count.ToString();

            if (finish) {
                PanelEnd(lc.PanelNo);
            }
#if DEBUG
#endif
            txtLableCode1.Text = string.Empty;
        }

        private string GetLocation(string code, bool handwork) {
            string re = string.Empty;
            LableCode dt = LableCode.QueryByLCode(code);
            if (dt != null) {
                logOpt.ViewInfo(string.Format("{0}重复扫描{1}", (handwork ? "手工" : "自动"), code), LogViewType.Both);

                ShowWarning("重复扫码");
            } else {
                Dictionary<string, string> str;
                try {
                    str = CallWebApi.Post(clsSetting.GetLocation, new Dictionary<string, string>()
                    { { "Bar_Code", code } });
                    logOpt.ViewInfo(string.Format("{0}{1}获取交地。",
                        code, JsonConvert.SerializeObject(str)), LogViewType.OnlyFile);
                    DataTable res = JsonConvert.DeserializeObject<DataTable>(str["Data"].ToString());
                    if (str["State"] == "Fail" || res.Rows[0]["LOCATION"].ToString() == "Fail") {
                        ShowWarning("取交地失败");
                        logOpt.ViewInfo(string.Format("{0}{1}获取交地失败。",
                            code, JsonConvert.SerializeObject(str)), LogViewType.Both);
                    } else if (res.Rows.Count > 0) {
                        re = res.Rows[0]["LOCATION"].ToString();
                        logOpt.ViewInfo(string.Format("{0}扫描{1}交地{2}。{3}",
                            (handwork ? "手工" : "自动"), code, txtToLocation.Text, str["Data"]), LogViewType.Both);
                    } else {
                        ShowWarning("取交地失败");
                        logOpt.ViewInfo(string.Format("{0}{1}获取交地失败。{2}",
                            (handwork ? "手工" : "自动"), code, JsonConvert.SerializeObject(str)), LogViewType.Both);
                    }
                } catch (Exception ex) {
                    logOpt.ViewInfo(string.Format(ex.Message), LogViewType.Both);
                }
            }
            return re;
        }

        private void ShowWarning(string msg, bool isError = true) {
            lblMsgInfo.Text = msg;
            lblMsgInfo.BackColor = isError
                ? Color.Red
                : Color.Green;
            lblMsgInfo.ForeColor = Color.White;
        }

        private void txtLableCode1_Enter(object sender, EventArgs e) {
            txtLableCode1.Text = string.Empty;
        }

        private void txtLableCode1_Leave(object sender, EventArgs e) {
            txtLableCode1.Text = "请将光标放置到这里扫描";
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
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

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e) {
            if (btnStop.Enabled) {
                MessageBox.Show(string.Format("正在运行无法关闭软件！"));
                e.Cancel = true;
            }
#if !DEBUG
            else {
                opcClient.Close();
            }
#endif
        }

        private void txtPanelNo_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                btnComplete_Click(null, null);
            }
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e) {
            BindDgv();
        }

        private void cmbShiftNo_SelectedIndexChanged(object sender, EventArgs e) {
            BindDgv();
        }

        private void timer2_Tick(object sender, EventArgs e) {
            textBox1_DoubleClick(null, null);
            textBox1.Text = (double.Parse(textBox1.Text) + 1).ToString().PadLeft(10, '0');
        }

        private void textBox1_DoubleClick(object sender, EventArgs e) {
            string code = textBox1.Text.Trim();

            ScanLableCode(code, 1, true);
            txtDiameter.Text = ran.Next(diameterMin, diameterMax).ToString();
            txtLength.Text = ran.Next(lengthMin, lengthMax).ToString();
        }

        private void btnLog_Click(object sender, EventArgs e) {
            var path = System.IO.Path.Combine(Application.StartupPath, "log");
            System.Diagnostics.Process.Start(path);
        }

        private void timer3_Tick(object sender, EventArgs e) {
#if !DEBUG
            timer3.Stop();
            //viewopcdata();
            timer3.Start();
#endif
        }

        private void txtDelLCode_Enter(object sender, EventArgs e) {
            if (txtDelLCode.Text == "请输入要删除的标签号")
                txtDelLCode.Text = "";
        }

        private void txtDelLCode_Leave(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(txtDelLCode.Text.Trim()))
                txtDelLCode.Text = "请输入要删除的标签号";
        }

        private void txtDelLCode_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '\r') {
                string code = txtDelLCode.Text.Trim();
                if (code.Length < 12) {
                    return;
                }
                btnDelete_Click(null, null);
            }
        }

        /// <summary>
        /// 从窗口的dtview控件中，删除含有code的那一行。
        /// </summary>
        /// <param name="code"></param>
        private void RemoveRowFromView(string code) {
            dtview.DefaultView.RowFilter = string.Format("Code='{0}'", code);
            for (int i = 0; i < dtview.DefaultView.Count; i++) {
                DataRow r = dtview.DefaultView[i].Row;
                dtview.Rows.Remove(r);
            }
            dtview.DefaultView.RowFilter = string.Empty;
        }

        /// <summary>
        /// 等待为OPC允许删除信号，然后删除号码，最后信号复位。
        /// </summary>
        /// <param name="fullLabelCode">长度12位的号码</param>
        public void WriteLabelCodeToOpc(string fullLabelCode) {
            string signal = OPCRead(opcParam.DeleteLCode.Signal).ToString();

            while (isrun && bool.Parse(signal)) {
                signal = OPCRead(opcParam.DeleteLCode.Signal).ToString();
                Thread.Sleep(OPCClient.DELAY);
            }

            opcClient.Write(opcParam.DeleteLCode.LCode1, fullLabelCode.Substring(0, 6));
            opcClient.Write(opcParam.DeleteLCode.LCode2, fullLabelCode.Substring(6, 6));
            opcClient.Write(opcParam.DeleteLCode.Signal, true);
        }

        public bool confirm(string question) {
            var r = MessageBox.Show(question, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return r == DialogResult.Yes;
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if (txtDelLCode.Text.Trim().Length < 12) {
                ShowWarning("删除号码长度不正确");
                return;
            }

            string code = txtDelLCode.Text.Trim().Substring(0, 12);

            //提示用户确认。
            var question = string.Format("您确定要删除标签[{0}]吗？", code);
            if (!confirm(question)) { return; }

            // 删除号码。
            if (LableCode.Delete(code)) {
                RemoveRowFromView(code);
                lblCount.Text = dtview.DefaultView.Count.ToString();

#if !DEBUG
                //WriteLabelCodeToOpc(code);
#endif

                logOpt.ViewInfo(string.Format("删除标签{0}成功", code), LogViewType.Both);
            } else {
                logOpt.ViewInfo(string.Format("删除标签{0}失败", code), LogViewType.Both);
            }

            txtDelLCode.Text = string.Empty;

        }
        private void btnReset_Click(object sender, EventArgs e) {
#if !DEBUG
            lock (opcClient) {
                opcClient.Write(opcParam.ScanParam.ScanState, true);
            }
#endif
        }
    }
}
