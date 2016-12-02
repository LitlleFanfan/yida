namespace yidascan
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
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.btnHelp = new System.Windows.Forms.ToolStripButton();
            this.btnQuit = new System.Windows.Forms.ToolStripButton();
            this.grbHandwork = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtLableCode1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Floor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FloorIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Diameter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Coordinates = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PanelNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Finished = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label8 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lsvLog = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblScanner = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblScanner2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblOpcIp = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTimer = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMsgInfo = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.panel3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRun,
            this.btnStop,
            this.btnReset,
            this.btnSet,
            this.btnLog,
            this.toolStripButton1,
            this.btnHelp,
            this.btnQuit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1370, 67);
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
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
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
            this.btnLog.BackColor = System.Drawing.Color.LightPink;
            this.btnLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnLog.Font = new System.Drawing.Font("Microsoft YaHei", 14F);
            this.btnLog.Image = ((System.Drawing.Image)(resources.GetObject("btnLog.Image")));
            this.btnLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(160, 64);
            this.btnLog.Text = "日志(&L)";
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.AutoSize = false;
            this.toolStripButton1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Font = new System.Drawing.Font("Microsoft YaHei", 14F);
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(160, 64);
            this.toolStripButton1.Text = "删除标签(&D)";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.AutoSize = false;
            this.btnHelp.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnHelp.Font = new System.Drawing.Font("Microsoft YaHei", 14F);
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.ImageTransparentColor = System.Drawing.Color.BlueViolet;
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(160, 64);
            this.btnHelp.Text = "帮助(&H)";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
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
            this.grbHandwork.Size = new System.Drawing.Size(982, 136);
            this.grbHandwork.TabIndex = 51;
            this.grbHandwork.TabStop = false;
            this.grbHandwork.Text = "手动操作";
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
            this.panel1.Size = new System.Drawing.Size(951, 102);
            this.panel1.TabIndex = 8;
            // 
            // txtLableCode1
            // 
            this.txtLableCode1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLableCode1.Font = new System.Drawing.Font("SimSun", 24F);
            this.txtLableCode1.ForeColor = System.Drawing.Color.DarkRed;
            this.txtLableCode1.Location = new System.Drawing.Point(134, 27);
            this.txtLableCode1.Margin = new System.Windows.Forms.Padding(4);
            this.txtLableCode1.Name = "txtLableCode1";
            this.txtLableCode1.Size = new System.Drawing.Size(795, 44);
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
            this.label5.Location = new System.Drawing.Point(17, 31);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 36);
            this.label5.TabIndex = 5;
            this.label5.Text = "扫码";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 213);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvData);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Size = new System.Drawing.Size(1593, 410);
            this.splitContainer1.SplitterDistance = 586;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 52;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Code,
            this.Floor,
            this.FloorIndex,
            this.Diameter,
            this.Coordinates,
            this.ToLocation,
            this.PanelNo,
            this.Finished});
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(0, 32);
            this.dgvData.Margin = new System.Windows.Forms.Padding(4);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.ShowEditingIcon = false;
            this.dgvData.Size = new System.Drawing.Size(586, 378);
            this.dgvData.TabIndex = 1;
            // 
            // Code
            // 
            this.Code.DataPropertyName = "Code";
            this.Code.FillWeight = 66.99477F;
            this.Code.HeaderText = "标签";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Floor
            // 
            this.Floor.DataPropertyName = "Floor";
            this.Floor.HeaderText = "层";
            this.Floor.Name = "Floor";
            this.Floor.ReadOnly = true;
            this.Floor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Floor.Visible = false;
            // 
            // FloorIndex
            // 
            this.FloorIndex.DataPropertyName = "FloorIndex";
            this.FloorIndex.HeaderText = "层中序号";
            this.FloorIndex.Name = "FloorIndex";
            this.FloorIndex.ReadOnly = true;
            this.FloorIndex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FloorIndex.Visible = false;
            // 
            // Diameter
            // 
            this.Diameter.DataPropertyName = "Diameter";
            this.Diameter.FillWeight = 32.65995F;
            this.Diameter.HeaderText = "直径mm";
            this.Diameter.Name = "Diameter";
            this.Diameter.ReadOnly = true;
            this.Diameter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Coordinates
            // 
            this.Coordinates.DataPropertyName = "Coordinates";
            this.Coordinates.HeaderText = "坐标";
            this.Coordinates.Name = "Coordinates";
            this.Coordinates.ReadOnly = true;
            this.Coordinates.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Coordinates.Visible = false;
            // 
            // ToLocation
            // 
            this.ToLocation.DataPropertyName = "ToLocation";
            this.ToLocation.FillWeight = 32.65995F;
            this.ToLocation.HeaderText = "交地";
            this.ToLocation.Name = "ToLocation";
            this.ToLocation.ReadOnly = true;
            this.ToLocation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PanelNo
            // 
            this.PanelNo.DataPropertyName = "PanelNo";
            this.PanelNo.HeaderText = "板号";
            this.PanelNo.Name = "PanelNo";
            this.PanelNo.ReadOnly = true;
            this.PanelNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PanelNo.Visible = false;
            // 
            // Finished
            // 
            this.Finished.DataPropertyName = "Finished";
            this.Finished.FillWeight = 32.65995F;
            this.Finished.HeaderText = "状态";
            this.Finished.Name = "Finished";
            this.Finished.ReadOnly = true;
            this.Finished.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.SkyBlue;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(586, 32);
            this.label8.TabIndex = 0;
            this.label8.Text = "数据";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lsvLog);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1002, 410);
            this.panel3.TabIndex = 2;
            // 
            // lsvLog
            // 
            this.lsvLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvLog.ItemHeight = 19;
            this.lsvLog.Location = new System.Drawing.Point(0, 32);
            this.lsvLog.MinimumSize = new System.Drawing.Size(250, 4);
            this.lsvLog.Name = "lsvLog";
            this.lsvLog.Size = new System.Drawing.Size(1002, 378);
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
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblScanner,
            this.lblScanner2,
            this.lblOpcIp,
            this.toolStripStatusLabel2,
            this.lblTimer});
            this.statusStrip1.Location = new System.Drawing.Point(0, 624);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 18, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1370, 26);
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
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(965, 21);
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
            this.groupBox1.Controls.Add(this.lblMsgInfo);
            this.groupBox1.Controls.Add(this.lblCount);
            this.groupBox1.Controls.Add(this.cmbShiftNo);
            this.groupBox1.Controls.Add(this.dtpDate);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Location = new System.Drawing.Point(4, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(599, 136);
            this.groupBox1.TabIndex = 54;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "班次信息";
            // 
            // lblMsgInfo
            // 
            this.lblMsgInfo.BackColor = System.Drawing.Color.Green;
            this.lblMsgInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMsgInfo.Font = new System.Drawing.Font("SimHei", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsgInfo.ForeColor = System.Drawing.Color.White;
            this.lblMsgInfo.Location = new System.Drawing.Point(260, 68);
            this.lblMsgInfo.Name = "lblMsgInfo";
            this.lblMsgInfo.Size = new System.Drawing.Size(333, 59);
            this.lblMsgInfo.TabIndex = 16;
            this.lblMsgInfo.Text = "空闲";
            this.lblMsgInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCount
            // 
            this.lblCount.BackColor = System.Drawing.Color.White;
            this.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCount.Font = new System.Drawing.Font("Microsoft YaHei", 35F, System.Drawing.FontStyle.Bold);
            this.lblCount.ForeColor = System.Drawing.Color.Black;
            this.lblCount.Location = new System.Drawing.Point(3, 68);
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
            this.cmbShiftNo.SelectedIndexChanged += new System.EventHandler(this.cmbShiftNo_SelectedIndexChanged);
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "yyyy-MM-dd";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(63, 29);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(203, 29);
            this.dtpDate.TabIndex = 9;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
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
            this.pictureBox1.Location = new System.Drawing.Point(1292, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(301, 67);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 50;
            this.pictureBox1.TabStop = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 650);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.grbHandwork);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.toolStrip1);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.panel3.ResumeLayout(false);
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
        private System.Windows.Forms.DataGridView dgvData;
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
        private System.Windows.Forms.ListBox lsvLog;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblMsgInfo;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.ToolStripButton btnReset;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Timer timer_message;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripButton btnHelp;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Floor;
        private System.Windows.Forms.DataGridViewTextBoxColumn FloorIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn Diameter;
        private System.Windows.Forms.DataGridViewTextBoxColumn Coordinates;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn PanelNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Finished;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}

