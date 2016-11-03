namespace RobotTest
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtB0 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtB10 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBP0 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtB1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtP0Rx = new System.Windows.Forms.TextBox();
            this.txtP0X = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtP0Ry = new System.Windows.Forms.TextBox();
            this.txtP0Y = new System.Windows.Forms.TextBox();
            this.txtP0Rz = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtP0Z = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtP1Rx = new System.Windows.Forms.TextBox();
            this.txtP1X = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtP1Ry = new System.Windows.Forms.TextBox();
            this.txtP1Y = new System.Windows.Forms.TextBox();
            this.txtP1Rz = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtP1Z = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtLableCode = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(4, 223);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(948, 124);
            this.dataGridView1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(367, 51);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(258, 31);
            this.button1.TabIndex = 1;
            this.button1.Text = "Calculate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "robotIP";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(101, 12);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 26);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "192.168.0.30";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(269, 10);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(135, 31);
            this.button2.TabIndex = 4;
            this.button2.Text = "run";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(434, 10);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(135, 31);
            this.button3.TabIndex = 5;
            this.button3.Text = "stop";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(4, 354);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(948, 433);
            this.textBox2.TabIndex = 6;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(630, 10);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(228, 31);
            this.button4.TabIndex = 7;
            this.button4.Text = "reset";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "B0";
            // 
            // txtB0
            // 
            this.txtB0.Location = new System.Drawing.Point(215, 100);
            this.txtB0.Name = "txtB0";
            this.txtB0.Size = new System.Drawing.Size(100, 26);
            this.txtB0.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(323, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "B10";
            // 
            // txtB10
            // 
            this.txtB10.Location = new System.Drawing.Point(367, 100);
            this.txtB10.Name = "txtB10";
            this.txtB10.Size = new System.Drawing.Size(100, 26);
            this.txtB10.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "BP0";
            // 
            // txtBP0
            // 
            this.txtBP0.Location = new System.Drawing.Point(63, 100);
            this.txtBP0.Name = "txtBP0";
            this.txtBP0.Size = new System.Drawing.Size(100, 26);
            this.txtBP0.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(481, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "B1";
            // 
            // txtB1
            // 
            this.txtB1.Location = new System.Drawing.Point(525, 100);
            this.txtB1.Name = "txtB1";
            this.txtB1.Size = new System.Drawing.Size(100, 26);
            this.txtB1.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtP0Rx);
            this.groupBox1.Controls.Add(this.txtP0X);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtP0Ry);
            this.groupBox1.Controls.Add(this.txtP0Y);
            this.groupBox1.Controls.Add(this.txtP0Rz);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtP0Z);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(19, 130);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(456, 86);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "P0";
            // 
            // txtP0Rx
            // 
            this.txtP0Rx.Location = new System.Drawing.Point(44, 54);
            this.txtP0Rx.Name = "txtP0Rx";
            this.txtP0Rx.Size = new System.Drawing.Size(100, 26);
            this.txtP0Rx.TabIndex = 9;
            // 
            // txtP0X
            // 
            this.txtP0X.Location = new System.Drawing.Point(44, 18);
            this.txtP0X.Name = "txtP0X";
            this.txtP0X.Size = new System.Drawing.Size(100, 26);
            this.txtP0X.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 59);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 16);
            this.label11.TabIndex = 8;
            this.label11.Text = "Rx";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "X";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(314, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 16);
            this.label10.TabIndex = 8;
            this.label10.Text = "Rz";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(314, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 16);
            this.label7.TabIndex = 8;
            this.label7.Text = "Z";
            // 
            // txtP0Ry
            // 
            this.txtP0Ry.Location = new System.Drawing.Point(196, 54);
            this.txtP0Ry.Name = "txtP0Ry";
            this.txtP0Ry.Size = new System.Drawing.Size(100, 26);
            this.txtP0Ry.TabIndex = 9;
            // 
            // txtP0Y
            // 
            this.txtP0Y.Location = new System.Drawing.Point(196, 18);
            this.txtP0Y.Name = "txtP0Y";
            this.txtP0Y.Size = new System.Drawing.Size(100, 26);
            this.txtP0Y.TabIndex = 9;
            // 
            // txtP0Rz
            // 
            this.txtP0Rz.Location = new System.Drawing.Point(348, 54);
            this.txtP0Rz.Name = "txtP0Rz";
            this.txtP0Rz.Size = new System.Drawing.Size(100, 26);
            this.txtP0Rz.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(162, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 16);
            this.label9.TabIndex = 8;
            this.label9.Text = "Ry";
            // 
            // txtP0Z
            // 
            this.txtP0Z.Location = new System.Drawing.Point(348, 18);
            this.txtP0Z.Name = "txtP0Z";
            this.txtP0Z.Size = new System.Drawing.Size(100, 26);
            this.txtP0Z.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(162, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 16);
            this.label8.TabIndex = 8;
            this.label8.Text = "Y";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtP1Rx);
            this.groupBox2.Controls.Add(this.txtP1X);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.txtP1Ry);
            this.groupBox2.Controls.Add(this.txtP1Y);
            this.groupBox2.Controls.Add(this.txtP1Rz);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.txtP1Z);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Location = new System.Drawing.Point(481, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(456, 86);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "P1";
            // 
            // txtP1Rx
            // 
            this.txtP1Rx.Location = new System.Drawing.Point(44, 53);
            this.txtP1Rx.Name = "txtP1Rx";
            this.txtP1Rx.Size = new System.Drawing.Size(100, 26);
            this.txtP1Rx.TabIndex = 9;
            // 
            // txtP1X
            // 
            this.txtP1X.Location = new System.Drawing.Point(44, 18);
            this.txtP1X.Name = "txtP1X";
            this.txtP1X.Size = new System.Drawing.Size(100, 26);
            this.txtP1X.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 58);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 16);
            this.label12.TabIndex = 8;
            this.label12.Text = "Rx";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(16, 16);
            this.label13.TabIndex = 8;
            this.label13.Text = "X";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(314, 58);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(24, 16);
            this.label14.TabIndex = 8;
            this.label14.Text = "Rz";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(314, 23);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(16, 16);
            this.label15.TabIndex = 8;
            this.label15.Text = "Z";
            // 
            // txtP1Ry
            // 
            this.txtP1Ry.Location = new System.Drawing.Point(196, 53);
            this.txtP1Ry.Name = "txtP1Ry";
            this.txtP1Ry.Size = new System.Drawing.Size(100, 26);
            this.txtP1Ry.TabIndex = 9;
            // 
            // txtP1Y
            // 
            this.txtP1Y.Location = new System.Drawing.Point(196, 18);
            this.txtP1Y.Name = "txtP1Y";
            this.txtP1Y.Size = new System.Drawing.Size(100, 26);
            this.txtP1Y.TabIndex = 9;
            // 
            // txtP1Rz
            // 
            this.txtP1Rz.Location = new System.Drawing.Point(348, 53);
            this.txtP1Rz.Name = "txtP1Rz";
            this.txtP1Rz.Size = new System.Drawing.Size(100, 26);
            this.txtP1Rz.TabIndex = 9;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(162, 58);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(24, 16);
            this.label16.TabIndex = 8;
            this.label16.Text = "Ry";
            // 
            // txtP1Z
            // 
            this.txtP1Z.Location = new System.Drawing.Point(348, 18);
            this.txtP1Z.Name = "txtP1Z";
            this.txtP1Z.Size = new System.Drawing.Size(100, 26);
            this.txtP1Z.TabIndex = 9;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(162, 23);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(16, 16);
            this.label17.TabIndex = 8;
            this.label17.Text = "Y";
            // 
            // txtLableCode
            // 
            this.txtLableCode.Location = new System.Drawing.Point(63, 55);
            this.txtLableCode.Name = "txtLableCode";
            this.txtLableCode.Size = new System.Drawing.Size(252, 26);
            this.txtLableCode.TabIndex = 12;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(19, 60);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(40, 16);
            this.label18.TabIndex = 11;
            this.label18.Text = "标签";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 791);
            this.Controls.Add(this.txtLableCode);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtBP0);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtB10);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtB1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtB0);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtB0;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtB10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBP0;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtB1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtP0Rx;
        private System.Windows.Forms.TextBox txtP0X;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtP0Ry;
        private System.Windows.Forms.TextBox txtP0Y;
        private System.Windows.Forms.TextBox txtP0Rz;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtP0Z;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtP1Rx;
        private System.Windows.Forms.TextBox txtP1X;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtP1Ry;
        private System.Windows.Forms.TextBox txtP1Y;
        private System.Windows.Forms.TextBox txtP1Rz;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtP1Z;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtLableCode;
        private System.Windows.Forms.Label label18;
    }
}

