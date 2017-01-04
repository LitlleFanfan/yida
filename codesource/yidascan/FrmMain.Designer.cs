﻿namespace yidascan
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnRun = new System.Windows.Forms.ToolStripButton();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.btnReset = new System.Windows.Forms.ToolStripButton();
            this.btnSet = new System.Windows.Forms.ToolStripButton();
            this.btnLog = new System.Windows.Forms.ToolStripButton();
            this.btnOther = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnStartRobot = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStopRobot = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnWeighReset = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnBrowsePanels = new System.Windows.Forms.ToolStripMenuItem();
            this.btnQuit = new System.Windows.Forms.ToolStripButton();
            this.grbHandwork = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtLableCode1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbxLabelCode = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lsvRobotRollLog = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lsvLog = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.lsvBufferLog = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lsvRobotStackLog = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblScanner = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblScanner2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblOpcIp = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRobot = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTimer = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbRobotState = new System.Windows.Forms.Label();
            this.chkUseRobot = new System.Windows.Forms.CheckBox();
            this.lbTaskState = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.cmbShiftNo = new System.Windows.Forms.ComboBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer_message = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStrip1.SuspendLayout();
            this.grbHandwork.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRun,
            this.btnStop,
            this.btnReset,
            this.btnSet,
            this.btnLog,
            this.btnOther,
            this.btnQuit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1276, 67);
            this.toolStrip1.TabIndex = 48;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnRun
            // 
            this.btnRun.AutoSize = false;
            this.btnRun.BackColor = System.Drawing.Color.LimeGreen;
            this.btnRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRun.Font = new System.Drawing.Font("Microsoft YaHei", 14F);
            this.btnRun.ForeColor = System.Drawing.Color.Honeydew;
            this.btnRun.Image = ((System.Drawing.Image)(resources.GetObject("btnRun.Image")));
            this.btnRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(160, 64);
            this.btnRun.Text = "启动(&R)";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnStop
            // 
            this.btnStop.AutoSize = false;
            this.btnStop.BackColor = System.Drawing.Color.Red;
            this.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStop.Enabled = false;
            this.btnStop.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F);
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(160, 64);
            this.btnStop.Text = "停止(&S)";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnReset
            // 
            this.btnReset.AutoSize = false;
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnReset.Font = new System.Drawing.Font("Microsoft YaHei", 14F);
            this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
            this.btnReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(160, 64);
            this.btnReset.Text = "传送复位";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSet
            // 
            this.btnSet.AutoSize = false;
            this.btnSet.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSet.Font = new System.Drawing.Font("Microsoft YaHei", 14F);
            this.btnSet.Image = ((System.Drawing.Image)(resources.GetObject("btnSet.Image")));
            this.btnSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(160, 64);
            this.btnSet.Text = "设置(&M)";
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnLog
            // 
            this.btnLog.AutoSize = false;
            this.btnLog.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnLog.Font = new System.Drawing.Font("Microsoft YaHei", 14F);
            this.btnLog.Image = ((System.Drawing.Image)(resources.GetObject("btnLog.Image")));
            this.btnLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(160, 64);
            this.btnLog.Text = "日志(&L)";
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // btnOther
            // 
            this.btnOther.AutoSize = false;
            this.btnOther.BackColor = System.Drawing.Color.LightYellow;
            this.btnOther.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnOther.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStartRobot,
            this.btnStopRobot,
            this.toolStripMenuItem3,
            this.btnWeighReset,
            this.toolStripMenuItem2,
            this.btnDelete,
            this.toolStripMenuItem1,
            this.btnHelp,
            this.toolStripMenuItem4,
            this.btnBrowsePanels});
            this.btnOther.Font = new System.Drawing.Font("Microsoft YaHei", 14F);
            this.btnOther.Image = ((System.Drawing.Image)(resources.GetObject("btnOther.Image")));
            this.btnOther.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOther.Name = "btnOther";
            this.btnOther.Size = new System.Drawing.Size(160, 64);
            this.btnOther.Text = "其他";
            // 
            // btnStartRobot
            // 
            this.btnStartRobot.Name = "btnStartRobot";
            this.btnStartRobot.Size = new System.Drawing.Size(217, 30);
            this.btnStartRobot.Text = "启动机器人任务";
            this.btnStartRobot.Click += new System.EventHandler(this.btnStartRobot_Click);
            // 
            // btnStopRobot
            // 
            this.btnStopRobot.Name = "btnStopRobot";
            this.btnStopRobot.Size = new System.Drawing.Size(217, 30);
            this.btnStopRobot.Text = "停止机器人任务";
            this.btnStopRobot.Click += new System.EventHandler(this.btnStopRobot_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(214, 6);
            // 
            // btnWeighReset
            // 
            this.btnWeighReset.Name = "btnWeighReset";
            this.btnWeighReset.Size = new System.Drawing.Size(217, 30);
            this.btnWeighReset.Text = "称重复位";
            this.btnWeighReset.Click += new System.EventHandler(this.btnWeighReset_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(214, 6);
            // 
            // btnDelete
            // 
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(217, 30);
            this.btnDelete.Text = "删除标签(&D)";
            this.btnDelete.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(214, 6);
            // 
            // btnHelp
            // 
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(217, 30);
            this.btnHelp.Text = "帮助(&H)";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(214, 6);
            // 
            // btnBrowsePanels
            // 
            this.btnBrowsePanels.Name = "btnBrowsePanels";
            this.btnBrowsePanels.Size = new System.Drawing.Size(217, 30);
            this.btnBrowsePanels.Text = "板状态浏览";
            this.btnBrowsePanels.Click += new System.EventHandler(this.btnBrowsePanels_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.AutoSize = false;
            this.btnQuit.BackColor = System.Drawing.Color.Orange;
            this.btnQuit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnQuit.Font = new System.Drawing.Font("Microsoft YaHei", 14F);
            this.btnQuit.Image = ((System.Drawing.Image)(resources.GetObject("btnQuit.Image")));
            this.btnQuit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(160, 64);
            this.btnQuit.Text = "退出(&Q)";
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // grbHandwork
            // 
            this.grbHandwork.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbHandwork.Controls.Add(this.panel1);
            this.grbHandwork.Location = new System.Drawing.Point(611, 70);
            this.grbHandwork.Margin = new System.Windows.Forms.Padding(5);
            this.grbHandwork.Name = "grbHandwork";
            this.grbHandwork.Padding = new System.Windows.Forms.Padding(5);
            this.grbHandwork.Size = new System.Drawing.Size(849, 129);
            this.grbHandwork.TabIndex = 51;
            this.grbHandwork.TabStop = false;
            this.grbHandwork.Text = "手动操作";
            this.grbHandwork.Enter += new System.EventHandler(this.grbHandwork_Enter);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtLableCode1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(16, 25);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(824, 95);
            this.panel1.TabIndex = 8;
            // 
            // txtLableCode1
            // 
            this.txtLableCode1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLableCode1.Font = new System.Drawing.Font("SimSun", 24F);
            this.txtLableCode1.ForeColor = System.Drawing.Color.DarkRed;
            this.txtLableCode1.Location = new System.Drawing.Point(183, 18);
            this.txtLableCode1.Margin = new System.Windows.Forms.Padding(4);
            this.txtLableCode1.Multiline = true;
            this.txtLableCode1.Name = "txtLableCode1";
            this.txtLableCode1.Size = new System.Drawing.Size(624, 62);
            this.txtLableCode1.TabIndex = 1;
            this.txtLableCode1.Text = "请将光标放置到这里扫描";
            this.txtLableCode1.Enter += new System.EventHandler(this.txtLableCode1_Enter);
            this.txtLableCode1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLableCode1_KeyPress);
            this.txtLableCode1.Leave += new System.EventHandler(this.txtLableCode1_Leave);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Moccasin;
            this.label5.Font = new System.Drawing.Font("SimSun", 24F);
            this.label5.Location = new System.Drawing.Point(14, 18);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 62);
            this.label5.TabIndex = 5;
            this.label5.Text = "扫码";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 208);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(1460, 529);
            this.splitContainer1.SplitterDistance = 453;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 52;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel2);
            this.splitContainer2.Panel1.Controls.Add(this.label8);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lsvRobotRollLog);
            this.splitContainer2.Panel2.Controls.Add(this.label1);
            this.splitContainer2.Size = new System.Drawing.Size(453, 529);
            this.splitContainer2.SplitterDistance = 264;
            this.splitContainer2.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbxLabelCode);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(453, 232);
            this.panel2.TabIndex = 1;
            // 
            // lbxLabelCode
            // 
            this.lbxLabelCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxLabelCode.FormattingEnabled = true;
            this.lbxLabelCode.ItemHeight = 19;
            this.lbxLabelCode.Location = new System.Drawing.Point(0, 0);
            this.lbxLabelCode.Name = "lbxLabelCode";
            this.lbxLabelCode.Size = new System.Drawing.Size(453, 232);
            this.lbxLabelCode.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.SkyBlue;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(453, 32);
            this.label8.TabIndex = 0;
            this.label8.Text = "数据";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lsvRobotRollLog
            // 
            this.lsvRobotRollLog.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lsvRobotRollLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvRobotRollLog.ItemHeight = 19;
            this.lsvRobotRollLog.Location = new System.Drawing.Point(0, 32);
            this.lsvRobotRollLog.MinimumSize = new System.Drawing.Size(250, 4);
            this.lsvRobotRollLog.Name = "lsvRobotRollLog";
            this.lsvRobotRollLog.Size = new System.Drawing.Size(453, 229);
            this.lsvRobotRollLog.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SkyBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(453, 32);
            this.label1.TabIndex = 3;
            this.label1.Text = "机器人布卷队列";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.panel3);
            this.splitContainer3.Panel1MinSize = 200;
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Panel2MinSize = 200;
            this.splitContainer3.Size = new System.Drawing.Size(1002, 529);
            this.splitContainer3.SplitterDistance = 200;
            this.splitContainer3.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lsvLog);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1002, 200);
            this.panel3.TabIndex = 2;
            // 
            // lsvLog
            // 
            this.lsvLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvLog.Font = new System.Drawing.Font("SimSun", 14F);
            this.lsvLog.FormattingEnabled = true;
            this.lsvLog.ItemHeight = 19;
            this.lsvLog.Location = new System.Drawing.Point(0, 32);
            this.lsvLog.Name = "lsvLog";
            this.lsvLog.Size = new System.Drawing.Size(1002, 168);
            this.lsvLog.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.SkyBlue;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1002, 32);
            this.label9.TabIndex = 1;
            this.label9.Text = "日志";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.lsvBufferLog);
            this.splitContainer4.Panel1.Controls.Add(this.label2);
            this.splitContainer4.Panel1MinSize = 200;
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.lsvRobotStackLog);
            this.splitContainer4.Panel2.Controls.Add(this.label3);
            this.splitContainer4.Panel2MinSize = 100;
            this.splitContainer4.Size = new System.Drawing.Size(1002, 325);
            this.splitContainer4.SplitterDistance = 200;
            this.splitContainer4.TabIndex = 0;
            // 
            // lsvBufferLog
            // 
            this.lsvBufferLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvBufferLog.ItemHeight = 19;
            this.lsvBufferLog.Location = new System.Drawing.Point(0, 32);
            this.lsvBufferLog.MinimumSize = new System.Drawing.Size(250, 4);
            this.lsvBufferLog.Name = "lsvBufferLog";
            this.lsvBufferLog.Size = new System.Drawing.Size(1002, 168);
            this.lsvBufferLog.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.SkyBlue;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1002, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "缓存日志";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lsvRobotStackLog
            // 
            this.lsvRobotStackLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvRobotStackLog.ItemHeight = 19;
            this.lsvRobotStackLog.Location = new System.Drawing.Point(0, 32);
            this.lsvRobotStackLog.MinimumSize = new System.Drawing.Size(250, 4);
            this.lsvRobotStackLog.Name = "lsvRobotStackLog";
            this.lsvRobotStackLog.Size = new System.Drawing.Size(1002, 89);
            this.lsvRobotStackLog.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.SkyBlue;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1002, 32);
            this.label3.TabIndex = 3;
            this.label3.Text = "机器人码垛日志";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblScanner,
            this.lblScanner2,
            this.lblOpcIp,
            this.lblRobot,
            this.toolStripStatusLabel2,
            this.lblTimer});
            this.statusStrip1.Location = new System.Drawing.Point(0, 715);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 18, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1276, 26);
            this.statusStrip1.TabIndex = 53;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Gray;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(306, 21);
            this.toolStripStatusLabel1.Text = "guangzhou golden beaver software.";
            // 
            // lblScanner
            // 
            this.lblScanner.BackColor = System.Drawing.Color.LightGray;
            this.lblScanner.Name = "lblScanner";
            this.lblScanner.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblScanner.Size = new System.Drawing.Size(20, 21);
            // 
            // lblScanner2
            // 
            this.lblScanner2.BackColor = System.Drawing.Color.LightGray;
            this.lblScanner2.Name = "lblScanner2";
            this.lblScanner2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblScanner2.Size = new System.Drawing.Size(20, 21);
            // 
            // lblOpcIp
            // 
            this.lblOpcIp.BackColor = System.Drawing.Color.LightGray;
            this.lblOpcIp.Name = "lblOpcIp";
            this.lblOpcIp.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblOpcIp.Size = new System.Drawing.Size(20, 21);
            // 
            // lblRobot
            // 
            this.lblRobot.BackColor = System.Drawing.Color.LightGray;
            this.lblRobot.Name = "lblRobot";
            this.lblRobot.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblRobot.Size = new System.Drawing.Size(20, 21);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(851, 21);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // lblTimer
            // 
            this.lblTimer.BackColor = System.Drawing.Color.LightPink;
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblTimer.Size = new System.Drawing.Size(20, 21);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbRobotState);
            this.groupBox1.Controls.Add(this.chkUseRobot);
            this.groupBox1.Controls.Add(this.lbTaskState);
            this.groupBox1.Controls.Add(this.lblCount);
            this.groupBox1.Controls.Add(this.cmbShiftNo);
            this.groupBox1.Controls.Add(this.dtpDate);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Location = new System.Drawing.Point(4, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(599, 131);
            this.groupBox1.TabIndex = 54;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "信息";
            // 
            // lbRobotState
            // 
            this.lbRobotState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lbRobotState.Font = new System.Drawing.Font("SimHei", 20F);
            this.lbRobotState.Location = new System.Drawing.Point(260, 89);
            this.lbRobotState.Name = "lbRobotState";
            this.lbRobotState.Size = new System.Drawing.Size(333, 37);
            this.lbRobotState.TabIndex = 18;
            this.lbRobotState.Text = "机器人状态";
            this.lbRobotState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkUseRobot
            // 
            this.chkUseRobot.BackColor = System.Drawing.Color.White;
            this.chkUseRobot.Font = new System.Drawing.Font("SimSun", 20F);
            this.chkUseRobot.Location = new System.Drawing.Point(7, 89);
            this.chkUseRobot.Name = "chkUseRobot";
            this.chkUseRobot.Size = new System.Drawing.Size(253, 37);
            this.chkUseRobot.TabIndex = 17;
            this.chkUseRobot.Text = "启动机器人";
            this.chkUseRobot.UseVisualStyleBackColor = false;
            // 
            // lbTaskState
            // 
            this.lbTaskState.BackColor = System.Drawing.Color.Green;
            this.lbTaskState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTaskState.Font = new System.Drawing.Font("SimHei", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTaskState.ForeColor = System.Drawing.Color.White;
            this.lbTaskState.Location = new System.Drawing.Point(260, 26);
            this.lbTaskState.Name = "lbTaskState";
            this.lbTaskState.Size = new System.Drawing.Size(333, 59);
            this.lbTaskState.TabIndex = 16;
            this.lbTaskState.Text = "空闲";
            this.lbTaskState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCount
            // 
            this.lblCount.BackColor = System.Drawing.Color.White;
            this.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCount.Font = new System.Drawing.Font("Microsoft YaHei", 35F, System.Drawing.FontStyle.Bold);
            this.lblCount.ForeColor = System.Drawing.Color.Black;
            this.lblCount.Location = new System.Drawing.Point(3, 26);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(257, 59);
            this.lblCount.TabIndex = 15;
            this.lblCount.Text = "0";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbShiftNo
            // 
            this.cmbShiftNo.FormattingEnabled = true;
            this.cmbShiftNo.Items.AddRange(new object[] {
            "白班",
            "中班",
            "夜班"});
            this.cmbShiftNo.Location = new System.Drawing.Point(272, 31);
            this.cmbShiftNo.Name = "cmbShiftNo";
            this.cmbShiftNo.Size = new System.Drawing.Size(321, 27);
            this.cmbShiftNo.TabIndex = 10;
            this.cmbShiftNo.Text = "白班";
            this.cmbShiftNo.Visible = false;
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "yyyy-MM-dd";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(63, 29);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(203, 29);
            this.dtpDate.TabIndex = 9;
            this.dtpDate.Visible = false;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(5, 25);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 36);
            this.label10.TabIndex = 12;
            this.label10.Text = "班次";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // timer_message
            // 
            this.timer_message.Interval = 200;
            this.timer_message.Tick += new System.EventHandler(this.timer_message_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(1130, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(331, 67);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 50;
            this.pictureBox1.TabStop = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 741);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.grbHandwork);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("SimSun", 14F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据采集软件";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.grbHandwork.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnRun;
        private System.Windows.Forms.ToolStripButton btnStop;
        private System.Windows.Forms.ToolStripButton btnQuit;
        private System.Windows.Forms.GroupBox grbHandwork;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtLableCode1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblScanner;
        private System.Windows.Forms.ToolStripStatusLabel lblScanner2;
        private System.Windows.Forms.ToolStripButton btnSet;
        private System.Windows.Forms.ToolStripStatusLabel lblTimer;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbShiftNo;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStripButton btnLog;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripStatusLabel lblOpcIp;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbTaskState;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.ToolStripButton btnReset;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Timer timer_message;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripStatusLabel lblRobot;
        private System.Windows.Forms.CheckBox chkUseRobot;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox lsvRobotRollLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.ListBox lsvBufferLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lsvRobotStackLog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripDropDownButton btnOther;
        private System.Windows.Forms.ToolStripMenuItem btnStartRobot;
        private System.Windows.Forms.ToolStripMenuItem btnStopRobot;
        private System.Windows.Forms.ListBox lbxLabelCode;
        private System.Windows.Forms.Label lbRobotState;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem btnHelp;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem btnWeighReset;
        private System.Windows.Forms.ListBox lsvLog;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem btnBrowsePanels;
    }
}

