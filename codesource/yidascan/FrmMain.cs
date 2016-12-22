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
using System.IO;
using System.Linq;
using System.Drawing;

using commonhelper;
using ListBoxHelper.ext;

namespace yidascan {
    public partial class FrmMain : Form {
        private DataTable dtview = new DataTable();
        NormalScan nscan1;
        NormalScan nscan2;
        private LableCodeBll lcb = new LableCodeBll();
        bool isrun = false;
        public static OPCParam opcParam = new OPCParam();
        public static OPCClient opcClient = new OPCClient();
        DataTable dtopc = new DataTable();

        RobotHelper robot;
        private bool robotRun = false;

        public static LogOpreate logOpt;

        private DateTime StartTime;

        private decimal zStart = 0;

        // mutex to ensure securely calling opc.
        Mutex OPC_IDLE = new Mutex();
        RobotJobQueue robotJobQueue;

        private int counter = 0;

        public FrmMain() {
            InitializeComponent();

            try {
                // 显示效果不对，以后再说。
                InitListBoxes();

                // 初始化机器人布卷消息队列。
                robotJobQueue = new RobotJobQueue();

                logOpt = new ProduceComm.LogOpreate();
                timer_message.Enabled = true;

                var msgStart = string.Format("{0} V{1} 启动。",
                                  clsSetting.PRODUCT_NAME,
                                  Application.ProductVersion.ToString());
                logOpt.Write(msgStart, LogType.NORMAL);

                StartOpc();

                lblOpcIp.BackColor = Color.LightGreen;
            } catch (Exception ex) {
                logOpt.Write(string.Format("!启动opc失败.\n{0}", ex), LogType.NORMAL);
            }
        }

        // 运行效果不正确。无红字显示。
        private void InitListBoxes() {
            lbxLabelCode.Initstyle();
            lsvBufferLog.Initstyle();
            lsvLog.Initstyle();
            lsvRobotRollLog.Initstyle();
            lsvRobotStackLog.Initstyle();
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
                RefreshRobotMenuState();
                cmbShiftNo.SelectedIndex = 0;

                SetRobotTip(false);
                SetButtonState(false);
                InitCfgView();
                LableCode.DeleteAllFinished();
            } catch (Exception ex) {
                logOpt.Write(string.Format("!初始化失败。\n{0}", ex), LogType.NORMAL);
            }
        }

        private void StartOpc() {
            dtopc = OPCParam.Query();
            dtopc.Columns.Remove("Class");
            dtopc.Columns.Add(new DataColumn("Value"));
            if (opcClient.Open(clsSetting.OPCServerIP)) {
                logOpt.Write(string.Format("OPC服务连接成功。"), LogType.NORMAL);
                opcClient.AddSubscription(dtopc);
            } else {
                logOpt.Write(string.Format("!OPC服务连接失败。"), LogType.NORMAL);
            }
            opcParam.Init();

            logOpt.Write(JsonConvert.SerializeObject(opcParam), LogType.NORMAL, LogViewType.OnlyFile);
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
                            // 加入机器人布卷队列。
                            var code1 = opcClient.Read(opcParam.RobotCarryA.LCode1).ToString();
                            var code2 = opcClient.Read(opcParam.RobotCarryA.LCode2).ToString();
                            var fullcode = code1.PadLeft(6, '0') + code2.PadLeft(6, '0');
                            opcClient.Write(opcParam.RobotCarryA.Signal, false);

                            PushInQueue(fullcode, "A");

                            opcClient.Write(opcParam.RobotCarryA.Signal, false);
                        }
                    }
                    Thread.Sleep(1000);
                }
            });
            logOpt.Write("机器人抓料A布卷队列任务启动。", LogType.NORMAL);
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
                            // 加入机器人布卷队列。
                            var code1 = opcClient.Read(opcParam.RobotCarryB.LCode1).ToString();
                            var code2 = opcClient.Read(opcParam.RobotCarryB.LCode2).ToString();
                            var fullcode = code1.PadLeft(6, '0') + code2.PadLeft(6, '0');

                            PushInQueue(fullcode, "B");

                            opcClient.Write(opcParam.RobotCarryB.Signal, false);
                        }
                    }
                    Thread.Sleep(1000);
                }
            });
            logOpt.Write("机器人抓料B布卷队列任务启动。", LogType.NORMAL);
        }

        private void PushInQueue(string fullcode, string side) {

            var label = LableCode.QueryByLCode(fullcode);
            if (label == null) {
                logOpt.Write(string.Format("!{0} {1}找不到", side, fullcode), LogType.ROLL_QUEUE);
                return;
            }
            if (label.Status >= (int)LableState.OnPanel) {
                logOpt.Write(string.Format("!{0} {1}已在板上,未加入队列,交地{2}.",
                    side, label.LCode, label.ToLocation), LogType.ROLL_QUEUE);
                return;
            }
            if (label.CoordinatesIsEmpty()) {
                logOpt.Write(string.Format("!{0} {1}未算位置，未加入队列,交地{2}.",
                    side, label.LCode, label.ToLocation), LogType.ROLL_QUEUE);
                return;
            }

            PanelInfo pinfo = LableCode.GetPanel(label.PanelNo);
            PanelState state = GetPanelState(label, pinfo);
            logOpt.Write(string.Format("{0} {1} {2}", label.LCode, label.ToLocation, Enum.GetName(typeof(PanelState), state)), LogType.ROLL_QUEUE, LogViewType.OnlyFile);

            decimal x = label.Cx;
            decimal y = label.Cy;
            decimal z = label.Cz + zStart;
            decimal rz = label.Crz;

            if (rz == 0) {
                y = RollPosition.GetToolOffSet(y);
            } else {
                x = RollPosition.GetToolOffSet(x);
            }

            if (x + y > 0) {
                if (rz == 0) {
                    rz = -180;
                }
            } else {
                if (rz != 0) {
                    rz = rz * -1;
                }
            }
            if (RollPosition.robotRSidePanel.Contains(label.ToLocation)) {
                rz = rz + 180;
            }
            var roll = new RollPosition(fullcode, side, label.ToLocation, state, x, y, z, rz);
            var success = RobotHelper.robotJobs.AddRoll(roll);

            var msg = success
                ? string.Format("布卷:{0}。", roll.LabelCode)
                : string.Format("重复:{0}", roll.LabelCode);

            logOpt.Write(string.Format((success ? "" : "!") + "{0} {1} {2}", side, msg, label.ToLocation), LogType.ROLL_QUEUE);
        }

        private static PanelState GetPanelState(LableCode label, PanelInfo pinfo) {
            var state = PanelState.LessHalf;
            if (label.Floor == pinfo.MaxFloor - 1 && (pinfo.OddStatus || pinfo.EvenStatus)) {
                state = PanelState.HalfFull;
            }
            if (label.Floor == pinfo.MaxFloor && pinfo.OddStatus && pinfo.EvenStatus) {
                state = PanelState.Full;
            }
            return state;
        }

        private void StartRobotTask() {
            Task.Factory.StartNew(() => {
                logOpt.Write("机器人正在启动...", LogType.NORMAL);
                robot = new RobotHelper(clsSetting.RobotIP, clsSetting.JobName);
                if (robot.IsConnected()) {
                    lblRobot.BackColor = Color.LightGreen;
                }
                logOpt.Write("机器人启动完成...", LogType.NORMAL);
                SetRobotTip(true);
                robot.JobLoop(ref robotRun);
                logOpt.Write("机器人启动任务结束。", LogType.NORMAL);
            });
        }

        private void BAreaUserFinalLayerTask() {
            foreach (KeyValuePair<string, string> kv in opcParam.BAreaUserFinalLayer) {
                Task.Factory.StartNew(() => {
                    while (isrun) {
                        lock (opcClient) {
                            string signal = OPCRead(kv.Value).ToString();

                            if (signal == "1") {
                                LableCode.SetMaxFloor(kv.Key);
                                logOpt.Write(string.Format("{0} 收到人工完成信号。", kv.Key), LogType.NORMAL, LogViewType.OnlyFile);
                                opcClient.Write(kv.Value, "0");
                            }
                        }
                        Thread.Sleep(OPCClient.DELAY * 200);
                    }
                });
            }

            logOpt.Write("B区人工完成信号任务启动。", LogType.NORMAL);
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
            logOpt.Write(s);
        }

        private void StartScanner() {
            const string CAMERA_1 = "1#相机";
            const string CAMERA_2 = "2#相机";
            if (OpenPort(ref nscan1, CAMERA_1, FrmSet.pcfgScan1)) {
                nscan1.logger = Logger;
                nscan1.OnDataArrived = nscan1_OnDataArrived;
                // nscan1.OnDataArrived += new NormalScan.DataArrivedEventHandler(nscan1_OnDataArrived);
                // 启动相机读取线程。
                nscan1._StartJob();
                lblScanner.BackColor = Color.LightGreen;
                logOpt.Write(nscan1.name + "ok.");
            } else {
                lblScanner2.BackColor = System.Drawing.Color.Gray;
                ShowWarning("启动相机失败。");
                logOpt.Write("!" + nscan1.name + "启动失败！", LogType.NORMAL);
            }
            //if (OpenPort(ref nscan2, SECOND_CAMERA, FrmSet.pcfgScan2))
            //{   
            //    nscan1.OnDataArrived = nscan1_OnDataArrived;
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
            logOpt.Write(string.Format("!系统流程开始运行"), LogType.NORMAL);

            PanelGen.Init(dtpDate.Value);

            StartScanner();
            isrun = true;

            if (opcClient.Connected) {
                WeighTask();
                ACAreaFinishTask();
                BeforCacheTask();

                StartRobotJobATask();
                StartRobotJobBTask();
                BAreaUserFinalLayerTask();

                if (chkUseRobot.Checked) {
                    StartAllRobotTasks();
                } else {
                    logOpt.Write("未使用机器人。", LogType.NORMAL);
                }

                // 焦点设在手工输入框。
                txtLableCode1.Focus();

                chkUseRobot.Enabled = false;
                ShowTaskState(isrun);
                RefreshRobotMenuState();
            } else {
                var msg = "连接OPC设备失败！";
                ShowWarning(msg);
                logOpt.Write("!" + msg, LogType.NORMAL);
            }
        }

        private void WeighTask() {
            const string TO_WEIGH = "1";
            const string SUCCESS = "0";
            const string FAIL = "2";

            Task.Factory.StartNew(() => {
                while (isrun) {
                    try {
                        lock (opcClient) {
                            string getWeight = OPCRead(opcParam.ScanParam.GetWeigh).ToString();
                            if (getWeight == TO_WEIGH) {
                                string code = "";

                                lock (lablecodes) {
                                    if (lablecodes.Count > 0) {
                                        code = lablecodes.Dequeue();
                                    }
                                }

                                getWeight = NotifyWeigh(code, false) ? SUCCESS : FAIL;
                                logOpt.Write(code + "称重API状态: " + getWeight);
                                opcClient.Write(opcParam.ScanParam.GetWeigh, getWeight);
                            }
                        }
                    } catch (Exception ex) {
                        logOpt.Write("!" + ex.ToString(), LogType.NORMAL);
                    }

                    Thread.Sleep(OPCClient.DELAY * 100);
                }
            });

            logOpt.Write("称重任务启动。", LogType.NORMAL);
        }

        private void ACAreaFinishTask() {
            foreach (KeyValuePair<string, LCodeSignal> kv in opcParam.ACAreaPanelFinish) {
                Task.Factory.StartNew(() => {
                    while (isrun) {
                        lock (opcClient) {
                            string signal = OPCRead(kv.Value.Signal).ToString();

                            if (bool.Parse(signal)) {
                                string fullLable = ReadCompleteLable(kv.Value);

                                logOpt.Write(string.Format("{0} 收到完成信号。标签:{1}", kv.Value.Signal, fullLable), LogType.NORMAL);

                                try {
                                    if (!string.IsNullOrEmpty(fullLable)) {
                                        logOpt.Write(string.Format("执行状态:{0}",
                                            AreaAAndCFinish(fullLable)), LogType.NORMAL);
                                    }
                                } catch (Exception ex) {
                                    logOpt.Write("!" + ex.Message);
                                }

                                opcClient.Write(kv.Value.Signal, 0);
                            }
                        }
                        Thread.Sleep(OPCClient.DELAY * 200);
                    }
                });
            }

            logOpt.Write("AC区完成信号任务启动。", LogType.NORMAL);
        }

        /// <summary>
        /// 读取完整标签号码。
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        private string ReadCompleteLable(LCodeSignal slot) {
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
            logOpt.Write("缓存任务启动。", LogType.NORMAL);

            Task.Factory.StartNew(() => {
                while (isrun) {
                    lock (opcClient) {
                        try {
                            if (ReadBeforeCacheStatus(opcClient, opcParam)) {
                                var fullLable = ReadFullBeforeCacheLabel(opcClient, opcParam);
                                logOpt.Write(string.Format("收到缓存信号。从OPC读到号码: {0}", fullLable), LogType.BUFFER);

                                if (!string.IsNullOrEmpty(fullLable)) {
                                    LableCode lc = LableCode.QueryByLCode(fullLable);

                                    if (lc == null) {
                                        logOpt.Write(string.Format("!{0}标签找不到", fullLable), LogType.BUFFER);
                                    } else {
                                        // 检查重复计算。
                                        if (string.IsNullOrEmpty(lc.PanelNo)) {
                                            // 板号以前没算过。                                            

                                            // 计算位置
                                            string outCacheLable, msg;
                                            CacheState cState = lcb.AreaBCalculate(lc,
                                                string.Format("{0}{1}",
                                                        dtpDate.Value.ToString(clsSetting.LABEL_CODE_DATE_FORMAT),
                                                        cmbShiftNo.SelectedIndex.ToString()),
                                                out outCacheLable, out msg); //计算位置

                                            logOpt.Write(msg, LogType.BUFFER);

                                            // 写标签码到OPC
                                            opcClient.Write(opcParam.CacheParam.IsCache, cState);
                                            opcClient.Write(opcParam.CacheParam.GetOutLable1, string.IsNullOrEmpty(outCacheLable) ? "0" : outCacheLable.Substring(0, 6));
                                            opcClient.Write(opcParam.CacheParam.GetOutLable2, string.IsNullOrEmpty(outCacheLable) ? "0" : outCacheLable.Substring(6, 6));
                                        } else {
                                            logOpt.Write(string.Format("!{0}标签重复。", fullLable), LogType.BUFFER);
                                        }
                                    }

                                    opcClient.Write(opcParam.CacheParam.BeforCacheStatus, false);
                                }
                            }
                        } catch (Exception ex) {
                            logOpt.Write("!" + ex.ToString(), LogType.BUFFER);
                        }
                    }
                    Thread.Sleep(OPCClient.DELAY * 200);
                }
            });
        }

        private object OPCRead(string code) {
            object val = opcClient.Read(code);
            if (val == null) {
                logOpt.Write(string.Format("!警告:OPC项目[{0}]质量:坏。", code), LogType.NORMAL);
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
                clsSetting.loger.Error(string.Format("!{0}", ex.ToString()));
                return false;
            }
        }

        private void StopScanner(NormalScan scanner) {
            if (scanner == null) { return; }
            try {
                scanner._StopJob();
                Thread.Sleep(500);
                scanner.Close();
                logOpt.Write("关闭相机：" + scanner.name);
            } catch (Exception ex) {
                var msg = string.Format("!{0}关闭失败。\n{1}", scanner.name, ex);
                logOpt.Write(msg, LogType.NORMAL);
            }
        }

        private void StartAllRobotTasks() {
            robotRun = true;

            StartRobotTask();
        }

        private void StopAllRobotTasks() {
            robotRun = false;

            Thread.Sleep(1000);

            try {
                if (chkUseRobot.Checked) {
                    robot.Dispose();
                }
            } catch (Exception ex) {
                FrmMain.logOpt.Write("!" + ex.ToString());
            }

            logOpt.Write("机器人任务停止。", LogType.NORMAL);
            SetRobotTip(false, "机器人停止");
        }

        private void StopAllJobs() {
            isrun = false;

            Thread.Sleep(500);

            StopScanner(nscan1);
            StopScanner(nscan2);
            StopAllRobotTasks();
        }

        private void btnStop_Click(object sender, EventArgs e) {
            if (!CommonHelper.Confirm("确定停止吗?")) { return; }

            StopAllJobs();

            chkUseRobot.Enabled = true;
            SetButtonState(false);
            ShowTaskState(isrun);
            RefreshRobotMenuState();

            logOpt.Write("停止操作完成。", LogType.NORMAL);
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

        private void SetRobotTip(bool run, string msg = "") {
            if (run) {
                lbRobotState.ForeColor = Color.Green;
                lbRobotState.Text = string.IsNullOrEmpty(msg) ? "机器人已启动" : msg;
                lblRobot.BackColor = Color.LightGreen;
            } else {
                lbRobotState.ForeColor = Color.Red;
                lbRobotState.Text = string.IsNullOrEmpty(msg) ? "机器人未启动" : msg;
                lblRobot.BackColor = Color.LightGray;
            }
        }

        private void btnQuit_Click(object sender, EventArgs e) {
            logOpt.Write("程序正常退出。", LogType.NORMAL);
            Close();
        }

        private void btnToWeigh_Click(object sender, EventArgs e) {
            NotifyWeigh("");
        }

        /// <summary>
        /// 手工称重函数。界面按钮调用此函数。
        /// 成功返回true, 失败返回false.
        /// </summary>
        /// <param name="handwork"></param>
        /// <returns></returns>
        private bool NotifyWeigh(string code, bool handwork = true) {
            try {
                var re = CallWebApi.Post(clsSetting.ToWeight,
                    new Dictionary<string, string>() { { "Fabric_Code", code } });

                var msg = string.Format("{0} {1}称重{2}", code, (handwork ? "手工" : "自动"), JsonConvert.SerializeObject(re));
                logOpt.Write(msg, LogType.NORMAL);

                if (re["ERPState"] == "OK") {
                    var re1 = JsonConvert.DeserializeObject<DataTable>(re["Data"]);
                    return (re1.Rows[0]["result"].ToString().ToUpper() != "FAIL");
                } else {
                    FrmMain.ERPAlarm(FrmMain.opcClient, FrmMain.opcParam, ERPAlarmNo.COMMUNICATION_ERROR);
                    return false;
                }
            } catch (Exception ex) {
                logOpt.Write(string.Format("!称重调用webapi异常: {0}", ex));
                return false;
            }
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

        Queue<string> lablecodes = new Queue<string>();

        static List<string> errorPanel = new List<string>() { };//{ "B08", "B07", "B03", "B02", "B01", "B04", "B05", "B06" };

        private void ScanLableCode(string code, int scanNo, bool handwork) {
            ShowWarning(code, false);

            string tolocation = string.Empty;

            long t = TimeCount.TimeIt(() => {
                tolocation = GetLocation(code, handwork);
            });
            logOpt.Write(string.Format("取交地耗时:　{0}ms", t));

            if (string.IsNullOrEmpty(tolocation))
                return;

            var clothsize = new ClothRollSize();
            lock (opcClient) {
                t = TimeCount.TimeIt(() => {
                    while (isrun) {
                        var f = OPCRead(opcParam.ScanParam.SizeState);
                        if (bool.Parse(f.ToString())) { break; }
                        Thread.Sleep(OPCClient.DELAY);
                    }
                });
                logOpt.Write(string.Format("等尺寸信号耗时:{0}ms", t), LogType.NORMAL);

                t = TimeCount.TimeIt(() => {
                    clothsize.getFromOPC(opcClient, opcParam);
                    opcClient.Write(opcParam.ScanParam.SizeState, false);
                });
                const string ROLLSIZE_FMT = "布卷直径:{0};布卷长:{1};耗时:{2}ms;";
                var msg = string.Format(ROLLSIZE_FMT, clothsize.diameter, clothsize.length, t);
                logOpt.Write(msg, LogType.NORMAL);
            }

            LableCode lc = new LableCode();
            lc.LCode = code;
            lc.ToLocation = tolocation;
            lc.Diameter = clothsize.diameter;
            lc.Length = clothsize.length;
            lc.Remark = (handwork ? "handwork" : "automatic");
            lc.Coordinates = "";

            ////开始临时用
            //if (errorPanel.Contains(lc.ToLocation)) {
            //    lc.Remark = string.Format("{0} {1}", lc.Remark, lc.ToLocation);
            //    lc.ToLocation = "B06";
            //}
            ////结束临时用

            lock (opcClient) {
                t = TimeCount.TimeIt(() => {
                    while (isrun) {
                        var f = OPCRead(opcParam.ScanParam.ScanState);
                        if (!bool.Parse(f.ToString())) { break; }
                        Thread.Sleep(OPCClient.DELAY);
                    }
                });

                logOpt.Write(string.Format("等OPC ScanState 状态信号耗时:{0}ms", t), LogType.NORMAL);

                t = TimeCount.TimeIt(() => {
                    opcClient.Write(opcParam.ScanParam.ToLocationArea, clsSetting.AreaNo[lc.ToLocation.Substring(0, 1)]);
                    opcClient.Write(opcParam.ScanParam.ToLocationNo, lc.ToLocation.Substring(1, 2));

                    // write label back to opc.
                    var lcode1 = lc.LCode.Substring(0, 6);
                    var lcode2 = lc.LCode.Substring(6, 6);
                    opcClient.Write(opcParam.ScanParam.ScanLable1, lcode1);
                    opcClient.Write(opcParam.ScanParam.ScanLable2, lcode2);
                    //logOpt.ViewInfo(String.Format("标签写回OPC，{0} {1}", lcode1, lcode2), LogType.NORMAL);

                    opcClient.Write(opcParam.ScanParam.CameraNo, scanNo);
                    opcClient.Write(opcParam.ScanParam.ScanState, true);
                });
                logOpt.Write(string.Format("写OPC耗时:{0}ms", t), LogType.NORMAL);

                lock (lablecodes) {
                    lablecodes.Enqueue(lc.LCode);
                }
            }

            if (LableCode.Add(lc)) {
                ViewAddLable(lc);
                counter += 1;
                RefreshCounter();
            } else {
                ShowWarning("程序异常");
            }
        }

        private bool AreaAAndCFinish(string lCode) {
            LableCode lc = LableCode.QueryByLCode(lCode);
            if (lc == null) { return false; }

            lcb.GetPanelNo(lc, "");
            LableCode.Update(lc);
            LableCode.SetPanelNo(lCode);

            string msg;
            bool re = lcb.NotifyPanelEnd(lc.PanelNo, out msg);
            logOpt.Write(string.Format("{0} {1}", lc.ToLocation, msg), LogType.NORMAL);

            return re;
        }

        private void SetupPanel(LableCode lc, PanelInfo pinfo) {
            lc.PanelNo = pinfo.PanelNo;
            lc.Floor = pinfo.CurrFloor;
            lc.FloorIndex = 0;
            lc.Coordinates = "";
        }

        private void ViewAddLable(LableCode lc) {
            lbxLabelCode.Invoke((Action)(() => {
                var msg = string.Format("[{0}] 标签: {1} 交地: {2}", DateTime.Now, lc.LCode, lc.ToLocation);
                lbxLabelCode.Items.Insert(0, msg);

                var count = lbxLabelCode.Items.Count;
                if (count > 1000) {
                    lbxLabelCode.Items.RemoveAt(count - 1);
                }
            }));
        }

        /// <summary>
        /// 根据dtview的行数，来刷新计数器的值。
        /// </summary>
        public void RefreshCounter() {
            this.Invoke((Action)(() => {
                lblCount.Text = counter.ToString();
            }));
        }

        private string GetLocation(string code, bool handwork) {
            string re = string.Empty;
            LableCode dt = LableCode.QueryByLCode(code);
            if (dt != null) {
                var msg = string.Format("{0}重复扫描{1}", (handwork ? "手工" : "自动"), code);
                logOpt.Write("!" + msg, LogType.NORMAL);
                ShowWarning("重复扫码");
            } else {
                Dictionary<string, string> str;
                try {
                    str = CallWebApi.Post(clsSetting.GetLocation, new Dictionary<string, string>()
                    { { "Bar_Code", code } });
                    DataTable res = JsonConvert.DeserializeObject<DataTable>(str["Data"].ToString());
                    if (str["ERPState"] == "OK") {
                        if (res.Rows.Count > 0 && res.Rows[0]["LOCATION"].ToString() != "Fail") {
                            re = res.Rows[0]["LOCATION"].ToString();
                            logOpt.Write(string.Format("{0}扫描{1}交地{2}。{3}",
                                (handwork ? "手工" : "自动"), code, re, str["Data"]), LogType.NORMAL);
                        } else {
                            ShowWarning("取交地失败");
                            ERPAlarm(opcClient, opcParam, ERPAlarmNo.TO_LOCATION_ERROR);
                            logOpt.Write(string.Format("!{0}{1}获取交地失败。{2}",
                                (handwork ? "手工" : "自动"), code, JsonConvert.SerializeObject(str)), LogType.NORMAL);
                        }
                    } else {
                        ShowWarning("取交地失败");
                        ERPAlarm(opcClient, opcParam, ERPAlarmNo.COMMUNICATION_ERROR);
                        logOpt.Write(string.Format("!{0}{1}获取交地失败。{2}",
                            (handwork ? "手工" : "自动"), code, JsonConvert.SerializeObject(str)), LogType.NORMAL);
                    }
                } catch (Exception ex) {
                    logOpt.Write("!" + ex.Message, LogType.NORMAL);
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
                    logOpt.Write("!关闭OPC异常。\n" + ex, LogType.NORMAL);
                }
            }
        }

        private void btnLog_Click(object sender, EventArgs e) {
            var path = System.IO.Path.Combine(Application.StartupPath, "log");
            System.Diagnostics.Process.Start(path);
        }

        /// <summary>
        /// 等待为OPC允许删除信号，然后删除号码，最后信号复位。
        /// </summary>
        /// <param name="fullLabelCode">长度12位的号码</param>
        public void WriteDeletedLabelToOpc(string fullLabelCode) {
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
                opcClient.Write(opcParam.ScanParam.ScanLable1, "");
                opcClient.Write(opcParam.ScanParam.ScanLable2, "");
                logOpt.Write("传送复位。", LogType.NORMAL);
                opcClient.Write(opcParam.ScanParam.ScanState, true);
            }
        }

        private void timer_message_Tick(object sender, EventArgs e) {
            var msgs = logOpt.msgCenter.GetAll();

            if (msgs == null) { return; }

            foreach (var msg in msgs) {
                ListBox box = lsvLog;

                if (msg.Group == LogType.BUFFER) {
                    box = lsvBufferLog;
                } else if (msg.Group == LogType.ROBOT_STACK) {
                    box = lsvRobotStackLog;
                } else if (msg.Group == LogType.ROLL_QUEUE) {
                    box = lsvRobotRollLog;
                }

                box.Items.Insert(0, msg);

                // 显示的总条数超过1000条。
                var len = box.Items.Count;
                if (len > 1000) {
                    box.Items.RemoveAt(len - 1);
                }
            }
        }

        private void btnHelp_Click(object sender, EventArgs e) {
            var path = Path.Combine(Application.StartupPath, @"help\index.html");
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
        /// ERP故障
        /// </summary>
        /// <param name="erpAlarm"></param>
        public static void ERPAlarm(OPCClient opcClient, OPCParam opcParam, ERPAlarmNo erpAlarm) {
            lock (opcClient) {
                opcClient.Write(opcParam.ALarmSlot, (int)erpAlarm);
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

        private void RefreshRobotMenuState() {
            btnStartRobot.Enabled = !robotRun;
            btnStopRobot.Enabled = robotRun;
        }

        private void btnStartRobot_Click(object sender, EventArgs e) {
            chkUseRobot.Checked = true;

            StartAllRobotTasks();
            RefreshRobotMenuState();
        }

        private void btnStopRobot_Click(object sender, EventArgs e) {
            StopAllRobotTasks();
            RefreshRobotMenuState();
        }

        private void btnWeighReset_Click(object sender, EventArgs e) {
            opcClient.Write(opcParam.ScanParam.GetWeigh, 0);
        }
    }
}
