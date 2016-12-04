using System;
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

using commonhelper;

namespace yidascan {
    public partial class FrmMain : Form {
        private DataTable dtview = new DataTable();
        NormalScan nscan1;
        NormalScan nscan2;
        private LableCodeBll lcb = new LableCodeBll();
        bool isrun = false;
        OPCParam opcParam = new OPCParam();
        DataTable dtopc = new DataTable();

        RobotHelper robot;
        public static LogOpreate logOpt;

        private DateTime StartTime;

        private decimal zStart = 0;

        // mutex to ensure securely calling opc.
        Mutex OPC_IDLE = new Mutex();
        OPCClient opcClient = new OPCClient();
        RobotJobQueue robotJobQueue;

        public FrmMain() {
            InitializeComponent();

            try {
                // 初始化机器人布卷消息队列。
                robotJobQueue = new RobotJobQueue();

                logOpt = new ProduceComm.LogOpreate();
                timer_message.Enabled = true;

                logOpt.ViewInfo(string.Format("{0} V{1} 启动。",
                                  clsSetting.PRODUCT_NAME,
                                  Application.ProductVersion.ToString()));

                SetupDview();
                StartOpc();

                lblOpcIp.BackColor = Color.LightGreen;
            } catch (Exception ex) {
                logOpt.ViewInfo(string.Format("启动opc失败.\n{0}", ex));
            }
        }

        private void ShowTaskState(bool running) {
            lbTaskState.BackColor = running ? Color.LightGreen : Color.Orange;
            lbTaskState.Text = running ? "任务启动" : "任务停止";            
        }

        private void ShowTitle() {
            this.Text = string.Format("{0} V{1}",
                clsSetting.PRODUCT_NAME,
                Application.ProductVersion.ToString());
        }

        private void FrmMain_Load(object sender, EventArgs e) {
            try {
                ShowTitle();
                ShowTaskState(false);
                cmbShiftNo.SelectedIndex = 0;
                BindDgv();
                SetButtonState(false);
                InitCfgView();
                LableCode.DeleteAllFinished();
            } catch (Exception ex) {
                logOpt.ViewInfo(string.Format("初始化失败。\n{0}", ex));
            }
        }


        private void SetupDview() {
            dtview.Columns.Add(new DataColumn("Code"));
            dtview.Columns.Add(new DataColumn("ToLocation"));
            dtview.Columns.Add(new DataColumn("PanelNo"));
            dtview.Columns.Add(new DataColumn("Floor"));
            dtview.Columns.Add(new DataColumn("FloorIndex"));
            dtview.Columns.Add(new DataColumn("Diameter"));
            dtview.Columns.Add(new DataColumn("Coordinates"));
            dtview.Columns.Add(new DataColumn("Finished"));
        }

        private void StartOpc() {
            dtopc = OPCParam.Query();
            dtopc.Columns.Remove("Class");
            dtopc.Columns.Add(new DataColumn("Value"));
            if (opcClient.Open(clsSetting.OPCServerIP)) {
                logOpt.ViewInfo(string.Format("OPC服务连接成功。"));
                opcClient.AddSubscription(dtopc);
            } else {
                logOpt.ViewInfo(string.Format("OPC服务连接失败。"));
            }
            opcParam.Init();
        }

        /// <summary>
        /// 启动机器人布卷队列等待。
        /// </summary>
        private void StartRobotJobATask() {
            Task.Factory.StartNew(() => {
                while (isrun) {
                    lock (opcClient) {
                        // 等待布卷
                        var r = (bool)opcClient.Read(opcParam.RobotCarryA.Signal);
                        if (r) {
                            logOpt.ViewInfo("收到OPC信号，机器人A处抓料信号：" + r.ToString());

                            // 加入机器人布卷队列。
                            var code1 = opcClient.Read(opcParam.RobotCarryA.LCode1).ToString();
                            var code2 = opcClient.Read(opcParam.RobotCarryA.LCode2).ToString();
                            var fullcode = code1.PadLeft(6, '0') + code2.PadLeft(6, '0');
                            opcClient.Write(opcParam.RobotCarryA.Signal, false);

                            PushInQueue(fullcode, "A");
                            logOpt.ViewInfo("加入机器人队列：" + fullcode);
                        }
                    }

                    Thread.Sleep(100);
                }
            });
            logOpt.ViewInfo("机器人抓料A布卷队列任务启动。");
        }
        /// <summary>
        /// 启动机器人布卷队列等待。
        /// </summary>
        private void StartRobotJobBTask() {
            Task.Factory.StartNew(() => {
                while (isrun) {
                    lock (opcClient) {
                        // 等待布卷
                        var r = (bool)opcClient.Read(opcParam.RobotCarryB.Signal);
                        if (r) {
                            logOpt.ViewInfo("收到OPC信号，机器人B处抓料信号：" + r.ToString());

                            // 加入机器人布卷队列。
                            var code1 = opcClient.Read(opcParam.RobotCarryB.LCode1).ToString();
                            var code2 = opcClient.Read(opcParam.RobotCarryB.LCode2).ToString();
                            var fullcode = code1.PadLeft(6, '0') + code2.PadLeft(6, '0');

                            PushInQueue(fullcode, "B");
                            logOpt.ViewInfo("加入机器人队列：" + fullcode);
                        }
                    }

                    Thread.Sleep(100);
                }
            });
            logOpt.ViewInfo("机器人抓料B布卷队列任务启动。");
        }

        private void PushInQueue(string fullcode, string side) {
            logOpt.ViewInfo(string.Format("机器人抓料{0}处,标签{1}", side, fullcode));
            var label = LableCode.QueryByLCode(fullcode);
            if (label == null) {
                logOpt.ViewInfo("标签找不到。");
                return;
            }
            var state = PanelState.LessHalf;
            if (label.Floor < 6) { state = PanelState.HalfFull; }

            // lc.Coordinates = string.Format("{0},{1},{2}", z, r, xory);
            var ar_s = label.Coordinates.Split(new char[] { ',' });
            var ar = (from item in ar_s select decimal.Parse(item)).ToList();
            decimal x = 0; decimal y = 0;

            ar[2] = RollPosition.GetToolOffSet(ar[2]);

            if (ar[1] == 0) {
                x = 0;
                y = ar[2];
            } else {
                x = ar[2];
                y = 0;
            }

            var z = ar[0] + zStart;
            var rz = ar[1];
            if (ar[2] > 0) {
                if (rz == 0) {
                    rz = -180;
                }
            } else {
                if (rz != 0) {
                    rz = rz * -1;
                }
            }
            var roll = new RollPosition(side, label.ToLocation, state, x, y, z, rz);
            RobotHelper.robotJobs.AddRoll(roll);
        }

        private void StartRobotTask() {
            Task.Factory.StartNew(() => {
                logOpt.ViewInfo("机器人正在启动...");
                robot = new RobotHelper(clsSetting.RobotIP, clsSetting.JobName);
                if (robot.IsConnected()) {
                    lblRobot.BackColor = Color.LightGreen;
                }
                logOpt.ViewInfo("机器人启动完成...");
                //logOpt.ViewInfo("机器人连接状态：" + robot.IsConnected().ToString());  
                robot.JobLoop(ref isrun);
                logOpt.ViewInfo("机器人任务结束。");
            });
        }

        private void StartAreaBPnlStateTask() {
            const int FINISHED_BY_MAN = 3;
            foreach (KeyValuePair<string, string> kv in opcParam.BAreaPanelState) {
                Task.Factory.StartNew(() => {
                    while (isrun) {
                        lock (opcClient) {
                            int signal = int.Parse(OPCRead(kv.Value).ToString());

                            if (signal == FINISHED_BY_MAN && LableCode.SetMaxFloor(kv.Key)) {
                                logOpt.ViewInfo("收到B区OPC板位信号：" + signal.ToString());
                                opcClient.Write(kv.Value, 2);
                            }
                        }
                        Thread.Sleep(OPCClient.DELAY * 200);
                    }
                });

                logOpt.ViewInfo("B区板位信号任务启动。");
            }
        }

        private void BindDgv() {
            dgvData.DataSource = dtview.DefaultView;
            RefreshCounter();

            dtopc.DefaultView.RowFilter = string.Format("IndexNo<15");
        }

        void SetButtonState(bool isRun) {
            this.Invoke((EventHandler)(delegate {
                btnSet.Enabled = !isRun;
                btnRun.Enabled = !isRun;
                btnQuit.Enabled = !isRun;

                dtpDate.Enabled = !isRun;
                cmbShiftNo.Enabled = !isRun;

                grbHandwork.Enabled = isRun;
                btnStop.Enabled = isRun;
            }));
        }

        public void Logger(string s) {
            logOpt.ViewInfo(s);
        }

        private void StartScanner() {
            const string CAMERA_1 = "1#相机";
            const string CAMERA_2 = "2#相机";
            if (OpenPort(ref nscan1, CAMERA_1, FrmSet.pcfgScan1)) {
                nscan1.logger = Logger;
                nscan1.OnDataArrived += new NormalScan.DataArrivedEventHandler(nscan1_OnDataArrived);
                // 启动相机读取线程。
                nscan1._StartJob();
                lblScanner.BackColor = Color.LightGreen;
                logOpt.ViewInfo(nscan1.name + "ok.");
            } else {
                lblScanner2.BackColor = System.Drawing.Color.Gray;
                ShowWarning("启动相机失败。");
                logOpt.ViewInfo(nscan1.name + "启动失败！");
            }
            //if (OpenPort(ref nscan2, SECOND_CAMERA, FrmSet.pcfgScan2))
            //{
            //    nscan2.OnDataArrived += new NormalScan.DataArrivedEventHandler(nscan2_OnDataArrived);
            //    nscan2._StartJob();
            //    lblScanner2.BackColor = System.Drawing.Color.Green;
            //    logOpt.ViewInfo(string.Format("2#采集器启动成功。"));
            //}
            //else
            //{
            //    lblScanner2.BackColor = System.Drawing.Color.Gray;
            //    logOpt.ViewInfo(string.Format("2#采集器启动失败。"));
            //}
        }

        private void btnRun_Click(object sender, EventArgs e) {
            StartTime = DateTime.Now;
            dtpDate.Value = DateTime.Now;

            SetButtonState(true);
            logOpt.ViewInfo(string.Format("运行"));

            PanelGen.Init(dtpDate.Value);

            StartScanner();
            isrun = true;

            if (opcClient.Connected) {
                //logOpt.ViewInfo(JsonConvert.SerializeObject(opcParam));
                WeighTask();
                ACAreaFinishTask();
                BeforCacheTask();

                if (chkUseRobot.Checked) {
                    StartRobotTask();
                    StartRobotJobATask();
                    StartRobotJobBTask();
                    StartAreaBPnlStateTask();
                } else {
                    logOpt.ViewInfo("未使用机器人。", "system");
                }

                // 焦点设在手工输入框。
                txtLableCode1.Focus();

                chkUseRobot.Enabled = false;
                ShowTaskState(isrun);
            } else {
                var msg = "启动设备失败！";
                ShowWarning(msg);
                logOpt.ViewInfo(msg);
            }
        }

        private void WeighTask() {
            logOpt.ViewInfo("称重任务启动。");

            Task.Factory.StartNew(() => {
                while (isrun) {
                    lock (opcClient) {
                        string getWeight = OPCRead(opcParam.ScanParam.GetWeigh).ToString();
                        if (getWeight == "1") {
                            getWeight = NotifyWeigh(false) ? "0" : "2";
                            opcClient.Write(opcParam.ScanParam.GetWeigh, getWeight);
                        }
                    }
                    Thread.Sleep(OPCClient.DELAY * 300);
                }
            });
        }

        private void ACAreaFinishTask() {
            logOpt.ViewInfo("完成信号任务启动。");

            foreach (KeyValuePair<string, LCodeSignal> kv in opcParam.ACAreaPanelFinish) {
                Task.Factory.StartNew(() => {
                    while (isrun) {
                        lock (opcClient) {
                            string signal = OPCRead(kv.Value.Signal).ToString();

                            if (bool.Parse(signal)) {
                                string fullLable = ReadACCompleteLable(kv.Value);

                                logOpt.ViewInfo(string.Format(">>>>>{0} 收到完成信号。标签:{1}", kv.Value.Signal, fullLable), "ACPanelFinish");

                                try {
                                    if (!string.IsNullOrEmpty(fullLable)) {
                                        logOpt.ViewInfo(string.Format("执行状态:{0}",
                                            AreaAAndCFinish(fullLable)), "ACPanelFinish");
                                    }
                                } catch (Exception ex) {
                                    logOpt.ViewInfo(ex.Message);
                                }

                                opcClient.Write(kv.Value.Signal, 0);
                            }
                        }
                        Thread.Sleep(OPCClient.DELAY * 200);
                    }
                });
            }
        }

        /// <summary>
        /// AC区完成信号时，读取完整标签号码。
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
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
            logOpt.ViewInfo("缓存任务启动。");

            Task.Factory.StartNew(() => {
                while (isrun) {
                    lock (opcClient) {
                        if (ReadBeforeCacheStatus(opcClient, opcParam)) {
                            var fullLable = ReadFullBeforeCacheLabel(opcClient, opcParam);
                            logOpt.ViewInfo(string.Format("收到缓存信号。从OPC读到号码: {0}", fullLable), "CaChe");

                            if (!string.IsNullOrEmpty(fullLable)) {
                                LableCode lc = LableCode.QueryByLCode(fullLable);

                                if (lc == null) {
                                    logOpt.ViewInfo(string.Format("{0}标签找不到", fullLable), "CaChe");
                                } else {
                                    // 检查重复计算。
                                    if (string.IsNullOrEmpty(lc.PanelNo)) {
                                        // 板号以前没算过。
                                        string outCacheLable;
                                        CacheState cState = AreaBCalculate(lc, out outCacheLable); //计算位置

                                        opcClient.Write(opcParam.CacheParam.IsCache, cState);
                                        opcClient.Write(opcParam.CacheParam.GetOutLable1, string.IsNullOrEmpty(outCacheLable) ? "0" : outCacheLable.Substring(0, 6));
                                        opcClient.Write(opcParam.CacheParam.GetOutLable2, string.IsNullOrEmpty(outCacheLable) ? "0" : outCacheLable.Substring(6, 6));
                                    } else {
                                        logOpt.ViewInfo(string.Format("{0}标签重复。", fullLable), "CaChe");
                                    }
                                }

                                opcClient.Write(opcParam.CacheParam.BeforCacheStatus, false);
                            }
                        }
                    }
                    Thread.Sleep(OPCClient.DELAY * 200);
                }
            });
        }

        private object OPCRead(string code) {
            object val = opcClient.Read(code);
            if (val == null) {
                logOpt.ViewInfo(string.Format("警告:OPC项目[{0}]质量:坏。", code));
                return string.Empty;
            } else {
                return val;
            }
        }

        void nscan1_OnDataArrived(string type, string code) {
            nscan_OnDataArrived(type, code, 1);
        }

        void nscan2_OnDataArrived(string type, string code) {
            nscan_OnDataArrived(type, code, 2);
        }

        void nscan_OnDataArrived(string type, string code, int scanNo) {
            if (code == "ERROR" || code.Length < 12) { return; }

            // 条码请取前面12位,有些扫描器会扫出13位是因为把后面的识别码也读出来了.
            // 摘自2016年9月10日(星期六) 下午2:37邮件:答复: 答复: 9月9号夜班布卷扫描枪PC连接不上ERP说明
            code = code.Substring(0, 12);

            // wait for opc available.
            // must use try/finally block to release this mutex.
            OPC_IDLE.WaitOne();
            try {
                ScanLableCode(code, scanNo, false);
            } finally {
                OPC_IDLE.ReleaseMutex();
            }
        }

        private bool OpenPort(ref NormalScan nscan, string scannerName, CommunicationCfg cfg) {
            try {
                switch ((CommunicationType)Enum.Parse(typeof(CommunicationType), cfg.CommunicationType, true)) {
                    case CommunicationType.Network:
                        nscan = new NormalScan(scannerName, new TcpIpManage(cfg.IPAddr, int.Parse(cfg.IPPort)));
                        break;
                    case CommunicationType.SerialPort:
                        nscan = new NormalScan(scannerName, new SerialPortManage(cfg.ComPort, int.Parse(cfg.BaudRate)));
                        break;
                }
                return nscan.Open();
            } catch (Exception ex) {
                clsSetting.loger.Error(string.Format("{0}", ex.ToString()));
                return false;
            }
        }

        private void StopScanner(NormalScan scanner) {
            if (scanner == null) { return; }
            try {
                scanner._StopJob();
                Thread.Sleep(500);
                scanner.Close();
                logOpt.ViewInfo("关闭相机：" + scanner.name);
            } catch (Exception ex) {
                var msg = string.Format("{0}关闭失败。\n{1}", scanner.name, ex);
                logOpt.ViewInfo(msg);
            }
        }

        private void StopAllJobs() {
            isrun = false;
            StopScanner(nscan1);
            StopScanner(nscan2);

            SetButtonState(false);
            logOpt.ViewInfo("停止操作完成。");            
            ShowTaskState(isrun);

            try {
                if (chkUseRobot.Checked) {
                    robot.Dispose();
                }
            } catch (Exception ex) {
                FrmMain.logOpt.ViewInfo(ex.ToString());

            }
        }

        private void btnStop_Click(object sender, EventArgs e) {
            if (!CommonHelper.Confirm("确定停止吗?")) { return; }
            chkUseRobot.Enabled = true;
            StopAllJobs();
            ShowTaskState(isrun);
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

            lblOpcIp.Text = string.Format("OPC server ip:{0}", clsSetting.OPCServerIP);
            lblRobot.Text = string.Format("Robot:{0}/{1}", clsSetting.RobotIP, clsSetting.JobName);
        }

        private void btnQuit_Click(object sender, EventArgs e) {
            logOpt.ViewInfo("程序正常退出。");
            Close();
        }

        private void btnToWeigh_Click(object sender, EventArgs e) {
            NotifyWeigh();
        }

        /// <summary>
        /// 手工称重函数。界面按钮调用此函数。
        /// </summary>
        /// <param name="handwork"></param>
        /// <returns></returns>
        private bool NotifyWeigh(bool handwork = true) {
            var re = CallWebApi.Post(clsSetting.ToWeight,
                new Dictionary<string, string>());

            DataTable res = JsonConvert.DeserializeObject<DataTable>(re["Data"].ToString());

            if (re["result"] == "Fail") {
                logOpt.ViewInfo(string.Format("{0}称重{1}", (handwork ? "手工" : "自动"), JsonConvert.SerializeObject(re)));
                return false;
            } else {
                logOpt.ViewInfo(string.Format("{0}称重{1}", (handwork ? "手工" : "自动"), JsonConvert.SerializeObject(re)));
                return true;
            }
        }

        private bool PanelEnd(string panelNo, bool handwork = false) {
            if (!string.IsNullOrEmpty(panelNo)) {
                // 这个从数据库取似更合理。                
                var data = LableCode.QueryLabelcodeByPanelNo(panelNo);

                if (data == null) {
                    logOpt.ViewInfo("板号完成失败，未能查到数据库的标签。");
                    return false;
                }

                Dictionary<string, string> erpParam = new Dictionary<string, string>() {
                        { "Board_No", panelNo },  // first item.
                        { "AllBarCode", string.Join(",", data.ToArray()) } // second item.
                    };
                Dictionary<string, string> re = CallWebApi.Post(clsSetting.PanelFinish, erpParam);

                // show result.
                if (re["State"] == "Fail") {
                    logOpt.ViewInfo(string.Format("{0}板号{1}完成失败。{2}", (handwork ? "手工" : "自动"),
                        JsonConvert.SerializeObject(erpParam), re["ERR"]));
                } else {
                    logOpt.ViewInfo(string.Format("{0}板号{1}完成成功。{2}", (handwork ? "手工" : "自动"),
                        JsonConvert.SerializeObject(erpParam), re["Data"]));
                    return true;
                }
            }
            return false;
        }

        private void timer1_Tick(object sender, EventArgs e) {
            if (isrun) {
                var span = DateTime.Now - StartTime;
                lblTimer.Text = span.ToString(@"dd\.hh\:mm\:ss");
            }
        }

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
            } else if (txtLableCode1.Text.Length > 12) {
                txtLableCode1.Text = txtLableCode1.Text.Substring(0, 12);
            }
        }

        private void ScanLableCode(string code, int scanNo, bool handwork) {
            ShowWarning(code, false);

            string tolocation = string.Empty;

            long t = TimeCount.TimeIt(() => {
                tolocation = GetLocation(code, handwork);
            });
            logOpt.ViewInfo(string.Format("取交地耗时:　{0}ms", t));

            if (string.IsNullOrEmpty(tolocation))
                return;

            var clothsize = new ClothRollSize();
            t = TimeCount.TimeIt(() => {
                lock (opcClient) {
                    clothsize.getFromOPC(opcClient, opcParam);
                }
            });
            const string ROLLSIZE_FMT = "布卷直径:{0};布卷长:{1};耗时:{2}ms;";
            var msg = string.Format(ROLLSIZE_FMT, clothsize.diameter, clothsize.length, t);
            logOpt.ViewInfo(msg);

            LableCode lc = new LableCode();
            lc.LCode = code;
            lc.ToLocation = tolocation;
            lc.Diameter = clothsize.diameter;
            lc.Length = clothsize.length;
            lc.Remark = (handwork ? "handwork" : "automatic");
            lc.Coordinates = "";

            lock (opcClient) {
                t = TimeCount.TimeIt(() => {
                    while (isrun) {
                        var f = OPCRead(opcParam.ScanParam.ScanState);
                        if (!bool.Parse(f.ToString())) { break; }
                        Thread.Sleep(OPCClient.DELAY);
                    }
                });

                logOpt.ViewInfo(string.Format("等OPC ScanState 状态信号耗时:{0}ms", t));

                t = TimeCount.TimeIt(() => {
                    opcClient.Write(opcParam.ScanParam.ToLocationArea, clsSetting.AreaNo[lc.ToLocation.Substring(0, 1)]);
                    opcClient.Write(opcParam.ScanParam.ToLocationNo, lc.ToLocation.Substring(1, 2));

                    // write label back to opc.
                    var lcode1 = lc.LCode.Substring(0, 6);
                    var lcode2 = lc.LCode.Substring(6, 6);
                    opcClient.Write(opcParam.ScanParam.ScanLable1, lcode1);
                    opcClient.Write(opcParam.ScanParam.ScanLable2, lcode2);
                    logOpt.ViewInfo(String.Format("标签写回OPC，{0} {1}", lcode1, lcode2));

                    opcClient.Write(opcParam.ScanParam.CameraNo, scanNo);
                    opcClient.Write(opcParam.ScanParam.ScanState, true);
                });
                logOpt.ViewInfo(string.Format("写OPC耗时:{0}ms", t));
            }

            if (LableCode.Add(lc)) {
                ViewAddLable(lc);
                RefreshCounter();
            } else {
                ShowWarning("程序异常");
            }
        }

        private bool AreaAAndCFinish(string lCode) {
            LableCode lc = LableCode.QueryByLCode(lCode);
            if (lc == null) { return false; }

            lcb.GetPanelNo(lc);

            LableCode.Update(lc);

            LableCode.SetPanelNo(lCode);

            logOpt.ViewInfo("当前板号:" + lc.PanelNo, "ACPanelFinish");
            return PanelEnd(lc.PanelNo);
        }

        private void SetupPanel(LableCode lc, PanelInfo pinfo) {
            lc.PanelNo = pinfo.PanelNo;
            lc.Floor = pinfo.CurrFloor;
            lc.FloorIndex = 0;
            lc.Coordinates = "";
        }

        private CacheState AreaBCalculate(LableCode lc, out string outCacheLable) {
            CacheState cState = CacheState.Error;
            outCacheLable = string.Empty;
            LableCode lc2 = null;
            try {
                // 取当前交地、最后板、最后层所有标签。
                List<LableCode> lcs = LableCode.GetLableCodesOfRecentFloor(lc.ToLocation,
                    string.Format("{0}{1}",
                        dtpDate.Value.ToString(clsSetting.LABEL_CODE_DATE_FORMAT),
                        cmbShiftNo.SelectedIndex.ToString()));

                if (lcs == null || lcs.Count == 0) {
                    // 产生新板号赋予当前标签。
                    lcb.GetPanelNo(lc);
                    LableCode.Update(lc);
                    cState = CacheState.Cache;

                } else {
                    FloorPerformance fp = FloorPerformance.None;
                    PanelInfo pinfo = LableCode.GetPanel(lcs[0].PanelNo);
                    lc.SetupPanelInfo(pinfo);

                    if (pinfo.CurrFloor == lcs[0].Floor) {
                        // 最近一层没满。
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
                            outCacheLable = lc2.LCode;
                        cState = lc.FloorIndex == 0 ? CacheState.GetThenCache : CacheState.GoThenGet;
                    } else {
                        if (LableCode.Update(fp, lc))
                            cState = lc.FloorIndex == 0 ? CacheState.Cache : CacheState.Go;
                    }

                    if (fp == FloorPerformance.BothFinish && lc.Floor == pinfo.MaxFloor) {
                        PanelEnd(lc.PanelNo);
                    }
                }
                logOpt.ViewInfo(string.Format(@"交地:{0};当前标签:{1};直径:{2};长:{3};缓存状态:{4};取出标签:{5};直径:{6};长:{7};",
                    lc.ToLocation, lc.LCode, lc.Diameter, lc.Length, cState, outCacheLable,
                    (string.IsNullOrEmpty(outCacheLable) ? 0 : lc2.Diameter),
                    (string.IsNullOrEmpty(outCacheLable) ? 0 : lc2.Length)), "CaChe");
            } catch (Exception ex) {
                logOpt.ViewInfo(ex.ToString(), "Cache", LogViewType.OnlyFile);
            }
            return cState;
        }

        private void ViewAddLable(LableCode lc, LableCode lc2 = null) {
            lock (dtview) {
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
            }
            txtLableCode1.Text = string.Empty;
        }

        /// <summary>
        /// 根据dtview的行数，来刷新计数器的值。
        /// </summary>
        public void RefreshCounter() {
            lblCount.Text = dtview.DefaultView.Count.ToString();
        }

        private string GetLocation(string code, bool handwork) {
            string re = string.Empty;
            LableCode dt = LableCode.QueryByLCode(code);
            if (dt != null) {
                logOpt.ViewInfo(string.Format("{0}重复扫描{1}", (handwork ? "手工" : "自动"), code));

                ShowWarning("重复扫码");
            } else {
                Dictionary<string, string> str;
                try {
                    str = CallWebApi.Post(clsSetting.GetLocation, new Dictionary<string, string>()
                    { { "Bar_Code", code } });
                    logOpt.ViewInfo(string.Format("{0}{1}获取交地。",
                        code, JsonConvert.SerializeObject(str)), "normal", LogViewType.OnlyFile);
                    DataTable res = JsonConvert.DeserializeObject<DataTable>(str["Data"].ToString());
                    if (str["State"] == "Fail" || res.Rows[0]["LOCATION"].ToString() == "Fail") {
                        ShowWarning("取交地失败");
                        logOpt.ViewInfo(string.Format("{0}{1}获取交地失败。",
                            code, JsonConvert.SerializeObject(str)));
                    } else if (res.Rows.Count > 0) {
                        re = res.Rows[0]["LOCATION"].ToString();
                        logOpt.ViewInfo(string.Format("{0}扫描{1}交地{2}。{3}",
                            (handwork ? "手工" : "自动"), code, re, str["Data"]));
                    } else {
                        ShowWarning("取交地失败");
                        logOpt.ViewInfo(string.Format("{0}{1}获取交地失败。{2}",
                            (handwork ? "手工" : "自动"), code, JsonConvert.SerializeObject(str)));
                    }
                } catch (Exception ex) {
                    logOpt.ViewInfo(string.Format(ex.Message));
                }
            }
            return re;
        }

        private void ShowWarning(string msg, bool isError = true) {
            lbTaskState.Text = msg;
            lbTaskState.BackColor = isError
                ? Color.Red
                : Color.Green;
            lbTaskState.ForeColor = Color.White;
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
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e) {
            if (btnStop.Enabled) {
                MessageBox.Show(string.Format("正在运行无法关闭软件！"));
                e.Cancel = true;
            } else {
                try {
                    opcClient.Close();
                } catch (Exception ex) {
                    logOpt.ViewInfo("关闭OPC异常。\n" + ex);
                }
            }
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e) {
            BindDgv();
        }

        private void cmbShiftNo_SelectedIndexChanged(object sender, EventArgs e) {
            BindDgv();
        }

        private void btnLog_Click(object sender, EventArgs e) {
            var path = System.IO.Path.Combine(Application.StartupPath, "log");
            System.Diagnostics.Process.Start(path);
        }

        /// <summary>
        /// 从窗口的dtview控件中，删除含有code的那一行。
        /// </summary>
        /// <param name="code"></param>
        public void RemoveRowFromView(string code) {
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
            //等PLC做了此功能再放出来
            //string signal = OPCRead(opcParam.DeleteLCode.Signal).ToString();

            //while (isrun && bool.Parse(signal)) {
            //    signal = OPCRead(opcParam.DeleteLCode.Signal).ToString();
            //    Thread.Sleep(OPCClient.DELAY);
            //}
            lock (opcClient) {
                opcClient.Write(opcParam.DeleteLCode.LCode1, fullLabelCode.Substring(0, 6));
                opcClient.Write(opcParam.DeleteLCode.LCode2, fullLabelCode.Substring(6, 6));
                opcClient.Write(opcParam.DeleteLCode.Signal, true);
            }
        }

        private void btnReset_Click(object sender, EventArgs e) {
            lock (opcClient) {
                opcClient.Write(opcParam.ScanParam.ScanState, true);
                logOpt.ViewInfo("传送复位。", "hand");
            }
        }

        private void timer_message_Tick(object sender, EventArgs e) {
            var msgs = logOpt.msgCenter.GetAll();

            if (msgs == null) { return; }

            foreach (var msg in msgs) {
                lsvLog.Items.Insert(0, msg);

                // 显示的总条数超过1000条。
                var len = lsvLog.Items.Count;
                if (len > 1000) {
                    lsvLog.Items.RemoveAt(len - 1);
                }
            }
        }

        private void btnHelp_Click(object sender, EventArgs e) {
            var path = System.IO.Path.Combine(Application.StartupPath, @"help\index.html");
            System.Diagnostics.Process.Start(path);
        }

        private void toolStripButton1_Click(object sender, EventArgs e) {
            var w = new WinDeleteLabel();
            w.mainwin = this;
            w.ShowDialog();
        }

        /// <summary>
        /// 发送报警信号到OPC。
        /// </summary>
        /// <param name="value">报警信号的值。</param>
        private void AlarmToOPC(object value) {
            lock (opcClient) {
                opcClient.Write(opcParam.ALarmSlot, value);
            }
        }

        /// <summary>
        /// 发送机器人报警信号给OPC。
        /// </summary>
        /// <param name="value">报警信号的值。</param>
        private void RobotAlarmToOpc(object value) {
            lock (opcClient) {
                opcClient.Write(opcParam.RobotAlarmSlot, value);
            }
        }
    }
}
