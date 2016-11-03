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
            this.btnSet = new System.Windows.Forms.ToolStripButton();
            this.btnLog = new System.Windows.Forms.ToolStripButton();
            this.btnQuit = new System.Windows.Forms.ToolStripButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.grbHandwork = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtToLocation = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPanelNo = new System.Windows.Forms.TextBox();
            this.btnComplete = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtDiameter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLableCode1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnToWeigh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvOpcData = new System.Windows.Forms.DataGridView();
            this.Name1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lsvLog = new System.Windows.Forms.ListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblScanner = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblScanner2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblOpcIp = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTimer = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbShiftNo = new System.Windows.Forms.ComboBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.txtLength = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grbHandwork.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpcData)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRun,
            this.btnStop,
            this.btnSet,
            this.btnLog,
            this.btnQuit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1115, 67);
            this.toolStrip1.TabIndex = 48;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnRun
            // 
            this.btnRun.AutoSize = false;
            this.btnRun.BackColor = System.Drawing.Color.LimeGreen;
            this.btnRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRun.Font = new System.Drawing.Font("Microsoft YaHei", 14F);
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
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(160, 64);
            this.btnStop.Text = "停止(&S)";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
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
            // btnQuit
            // 
            this.btnQuit.AutoSize = false;
            this.btnQuit.BackColor = System.Drawing.Color.Gold;
            this.btnQuit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnQuit.Font = new System.Drawing.Font("Microsoft YaHei", 14F);
            this.btnQuit.Image = ((System.Drawing.Image)(resources.GetObject("btnQuit.Image")));
            this.btnQuit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(160, 64);
            this.btnQuit.Text = "退出(&Q)";
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(809, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(306, 67);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 50;
            this.pictureBox1.TabStop = false;
            // 
            // grbHandwork
            // 
            this.grbHandwork.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbHandwork.Controls.Add(this.panel3);
            this.grbHandwork.Controls.Add(this.panel1);
            this.grbHandwork.Location = new System.Drawing.Point(0, 130);
            this.grbHandwork.Margin = new System.Windows.Forms.Padding(5);
            this.grbHandwork.Name = "grbHandwork";
            this.grbHandwork.Padding = new System.Windows.Forms.Padding(5);
            this.grbHandwork.Size = new System.Drawing.Size(1115, 131);
            this.grbHandwork.TabIndex = 51;
            this.grbHandwork.TabStop = false;
            this.grbHandwork.Text = "手动操作";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtToLocation);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.txtPanelNo);
            this.panel3.Controls.Add(this.btnComplete);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(16, 78);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1084, 48);
            this.panel3.TabIndex = 10;
            // 
            // txtToLocation
            // 
            this.txtToLocation.Location = new System.Drawing.Point(172, 9);
            this.txtToLocation.Margin = new System.Windows.Forms.Padding(4);
            this.txtToLocation.Name = "txtToLocation";
            this.txtToLocation.Size = new System.Drawing.Size(345, 29);
            this.txtToLocation.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Moccasin;
            this.label7.Location = new System.Drawing.Point(9, 5);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 36);
            this.label7.TabIndex = 7;
            this.label7.Text = "码垛完成";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPanelNo
            // 
            this.txtPanelNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPanelNo.Location = new System.Drawing.Point(606, 9);
            this.txtPanelNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtPanelNo.Name = "txtPanelNo";
            this.txtPanelNo.Size = new System.Drawing.Size(334, 29);
            this.txtPanelNo.TabIndex = 5;
            this.txtPanelNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPanelNo_KeyDown);
            // 
            // btnComplete
            // 
            this.btnComplete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnComplete.BackColor = System.Drawing.Color.LimeGreen;
            this.btnComplete.Location = new System.Drawing.Point(948, 1);
            this.btnComplete.Margin = new System.Windows.Forms.Padding(4);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(125, 44);
            this.btnComplete.TabIndex = 6;
            this.btnComplete.Text = "完成";
            this.btnComplete.UseVisualStyleBackColor = false;
            this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(556, 5);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 36);
            this.label4.TabIndex = 2;
            this.label4.Text = "板号";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(121, 5);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 36);
            this.label3.TabIndex = 0;
            this.label3.Text = "板位";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtLength);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txtDiameter);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtLableCode1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnToWeigh);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(16, 25);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1084, 48);
            this.panel1.TabIndex = 8;
            // 
            // txtDiameter
            // 
            this.txtDiameter.Location = new System.Drawing.Point(172, 9);
            this.txtDiameter.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiameter.Name = "txtDiameter";
            this.txtDiameter.Size = new System.Drawing.Size(144, 29);
            this.txtDiameter.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(121, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 36);
            this.label2.TabIndex = 6;
            this.label2.Text = "直径";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLableCode1
            // 
            this.txtLableCode1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLableCode1.ForeColor = System.Drawing.Color.DarkRed;
            this.txtLableCode1.Location = new System.Drawing.Point(606, 9);
            this.txtLableCode1.Margin = new System.Windows.Forms.Padding(4);
            this.txtLableCode1.Name = "txtLableCode1";
            this.txtLableCode1.Size = new System.Drawing.Size(334, 29);
            this.txtLableCode1.TabIndex = 1;
            this.txtLableCode1.Text = "请将光标放置到这里扫描";
            this.txtLableCode1.Enter += new System.EventHandler(this.txtLableCode1_Enter);
            this.txtLableCode1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLableCode1_KeyPress);
            this.txtLableCode1.Leave += new System.EventHandler(this.txtLableCode1_Leave);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Moccasin;
            this.label5.Location = new System.Drawing.Point(9, 5);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 36);
            this.label5.TabIndex = 5;
            this.label5.Text = "扫码";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnToWeigh
            // 
            this.btnToWeigh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToWeigh.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnToWeigh.Location = new System.Drawing.Point(948, 1);
            this.btnToWeigh.Margin = new System.Windows.Forms.Padding(4);
            this.btnToWeigh.Name = "btnToWeigh";
            this.btnToWeigh.Size = new System.Drawing.Size(125, 44);
            this.btnToWeigh.TabIndex = 3;
            this.btnToWeigh.Text = "称重";
            this.btnToWeigh.UseVisualStyleBackColor = false;
            this.btnToWeigh.Click += new System.EventHandler(this.btnToWeigh_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(518, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "扫描标签";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 263);
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
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1115, 360);
            this.splitContainer1.SplitterDistance = 619;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 52;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.dgvData.Location = new System.Drawing.Point(0, 33);
            this.dgvData.Margin = new System.Windows.Forms.Padding(4);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.ShowEditingIcon = false;
            this.dgvData.Size = new System.Drawing.Size(619, 327);
            this.dgvData.TabIndex = 1;
            // 
            // Code
            // 
            this.Code.DataPropertyName = "Code";
            this.Code.HeaderText = "标签";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            // 
            // Floor
            // 
            this.Floor.DataPropertyName = "Floor";
            this.Floor.HeaderText = "层";
            this.Floor.Name = "Floor";
            this.Floor.ReadOnly = true;
            // 
            // FloorIndex
            // 
            this.FloorIndex.DataPropertyName = "FloorIndex";
            this.FloorIndex.HeaderText = "层中序号";
            this.FloorIndex.Name = "FloorIndex";
            this.FloorIndex.ReadOnly = true;
            // 
            // Diameter
            // 
            this.Diameter.DataPropertyName = "Diameter";
            this.Diameter.HeaderText = "直径mm";
            this.Diameter.Name = "Diameter";
            this.Diameter.ReadOnly = true;
            // 
            // Coordinates
            // 
            this.Coordinates.DataPropertyName = "Coordinates";
            this.Coordinates.HeaderText = "坐标";
            this.Coordinates.Name = "Coordinates";
            this.Coordinates.ReadOnly = true;
            // 
            // ToLocation
            // 
            this.ToLocation.DataPropertyName = "ToLocation";
            this.ToLocation.HeaderText = "交地";
            this.ToLocation.Name = "ToLocation";
            this.ToLocation.ReadOnly = true;
            // 
            // PanelNo
            // 
            this.PanelNo.DataPropertyName = "PanelNo";
            this.PanelNo.HeaderText = "板号";
            this.PanelNo.Name = "PanelNo";
            this.PanelNo.ReadOnly = true;
            // 
            // Finished
            // 
            this.Finished.DataPropertyName = "Finished";
            this.Finished.HeaderText = "状态";
            this.Finished.Name = "Finished";
            this.Finished.ReadOnly = true;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.BackColor = System.Drawing.Color.SkyBlue;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(619, 32);
            this.label8.TabIndex = 0;
            this.label8.Text = "数据";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.splitContainer2.Panel1.Controls.Add(this.dgvOpcData);
            this.splitContainer2.Panel1.Controls.Add(this.label12);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.label9);
            this.splitContainer2.Panel2.Controls.Add(this.lsvLog);
            this.splitContainer2.Size = new System.Drawing.Size(491, 360);
            this.splitContainer2.SplitterDistance = 223;
            this.splitContainer2.TabIndex = 3;
            // 
            // dgvOpcData
            // 
            this.dgvOpcData.AllowUserToAddRows = false;
            this.dgvOpcData.AllowUserToDeleteRows = false;
            this.dgvOpcData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOpcData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOpcData.BackgroundColor = System.Drawing.Color.White;
            this.dgvOpcData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOpcData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Name1,
            this.Value1,
            this.Remark1,
            this.Code1});
            this.dgvOpcData.Location = new System.Drawing.Point(3, 33);
            this.dgvOpcData.Name = "dgvOpcData";
            this.dgvOpcData.RowHeadersVisible = false;
            this.dgvOpcData.RowTemplate.Height = 23;
            this.dgvOpcData.ShowEditingIcon = false;
            this.dgvOpcData.Size = new System.Drawing.Size(485, 187);
            this.dgvOpcData.TabIndex = 3;
            // 
            // Name1
            // 
            this.Name1.DataPropertyName = "Name";
            this.Name1.HeaderText = "项名称";
            this.Name1.Name = "Name1";
            this.Name1.ReadOnly = true;
            // 
            // Value1
            // 
            this.Value1.DataPropertyName = "Value";
            this.Value1.HeaderText = "值";
            this.Value1.Name = "Value1";
            this.Value1.ReadOnly = true;
            // 
            // Remark1
            // 
            this.Remark1.DataPropertyName = "Remark";
            this.Remark1.HeaderText = "描述";
            this.Remark1.Name = "Remark1";
            this.Remark1.ReadOnly = true;
            // 
            // Code1
            // 
            this.Code1.DataPropertyName = "Code";
            this.Code1.HeaderText = "项代码";
            this.Code1.Name = "Code1";
            this.Code1.ReadOnly = true;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.BackColor = System.Drawing.Color.SkyBlue;
            this.label12.Location = new System.Drawing.Point(-1, 0);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(492, 32);
            this.label12.TabIndex = 2;
            this.label12.Text = "OPC数据监控";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.BackColor = System.Drawing.Color.SkyBlue;
            this.label9.Location = new System.Drawing.Point(-1, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(492, 32);
            this.label9.TabIndex = 1;
            this.label9.Text = "日志";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lsvLog
            // 
            this.lsvLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvLog.ItemHeight = 19;
            this.lsvLog.Location = new System.Drawing.Point(-1, 33);
            this.lsvLog.MinimumSize = new System.Drawing.Size(250, 4);
            this.lsvLog.Name = "lsvLog";
            this.lsvLog.Size = new System.Drawing.Size(492, 80);
            this.lsvLog.TabIndex = 2;
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
            this.statusStrip1.Size = new System.Drawing.Size(1115, 26);
            this.statusStrip1.TabIndex = 53;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.SkyBlue;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(222, 21);
            this.toolStripStatusLabel1.Text = "广州溯创信息科技有限公司";
            // 
            // lblScanner
            // 
            this.lblScanner.BackColor = System.Drawing.Color.LightGreen;
            this.lblScanner.Name = "lblScanner";
            this.lblScanner.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblScanner.Size = new System.Drawing.Size(20, 21);
            // 
            // lblScanner2
            // 
            this.lblScanner2.BackColor = System.Drawing.Color.Aquamarine;
            this.lblScanner2.Name = "lblScanner2";
            this.lblScanner2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblScanner2.Size = new System.Drawing.Size(20, 21);
            // 
            // lblOpcIp
            // 
            this.lblOpcIp.BackColor = System.Drawing.Color.LightBlue;
            this.lblOpcIp.Name = "lblOpcIp";
            this.lblOpcIp.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblOpcIp.Size = new System.Drawing.Size(20, 21);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(794, 21);
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
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.cmbShiftNo);
            this.groupBox1.Controls.Add(this.dtpDate);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(0, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1115, 52);
            this.groupBox1.TabIndex = 54;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "班次信息";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(484, 18);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 29);
            this.numericUpDown1.TabIndex = 13;
            this.numericUpDown1.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Visible = false;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(429, 14);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 36);
            this.label11.TabIndex = 14;
            this.label11.Text = "频率";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label11.Visible = false;
            // 
            // cmbShiftNo
            // 
            this.cmbShiftNo.FormattingEnabled = true;
            this.cmbShiftNo.Items.AddRange(new object[] {
            "白班",
            "中班",
            "夜班"});
            this.cmbShiftNo.Location = new System.Drawing.Point(300, 19);
            this.cmbShiftNo.Name = "cmbShiftNo";
            this.cmbShiftNo.Size = new System.Drawing.Size(92, 27);
            this.cmbShiftNo.TabIndex = 10;
            this.cmbShiftNo.SelectedIndexChanged += new System.EventHandler(this.cmbShiftNo_SelectedIndexChanged);
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "yyyy-MM-dd";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(153, 18);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(141, 29);
            this.dtpDate.TabIndex = 9;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.ForeColor = System.Drawing.Color.DarkRed;
            this.textBox1.Location = new System.Drawing.Point(765, 18);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(335, 29);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "请将光标放置到这里扫描";
            this.textBox1.Visible = false;
            this.textBox1.DoubleClick += new System.EventHandler(this.textBox1_DoubleClick);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(91, 14);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 36);
            this.label10.TabIndex = 12;
            this.label10.Text = "班次";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(632, 14);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 36);
            this.label6.TabIndex = 0;
            this.label6.Text = "扫描标签TEST";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Visible = false;
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // timer3
            // 
            this.timer3.Interval = 1000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(373, 9);
            this.txtLength.Margin = new System.Windows.Forms.Padding(4);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(144, 29);
            this.txtLength.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Location = new System.Drawing.Point(322, 5);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 36);
            this.label13.TabIndex = 8;
            this.label13.Text = "长度";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 650);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.grbHandwork);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("SimSun", 14F);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grbHandwork.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpcData)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnRun;
        private System.Windows.Forms.ToolStripButton btnStop;
        private System.Windows.Forms.ToolStripButton btnQuit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox grbHandwork;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtToLocation;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPanelNo;
        private System.Windows.Forms.Button btnComplete;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnToWeigh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtLableCode1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.TextBox txtDiameter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbShiftNo;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Floor;
        private System.Windows.Forms.DataGridViewTextBoxColumn FloorIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn Diameter;
        private System.Windows.Forms.DataGridViewTextBoxColumn Coordinates;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn PanelNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Finished;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolStripButton btnLog;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripStatusLabel lblOpcIp;
        private System.Windows.Forms.ListBox lsvLog;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgvOpcData;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code1;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.Label label13;
    }
}

