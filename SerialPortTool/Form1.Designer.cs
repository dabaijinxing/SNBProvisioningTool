﻿namespace SerialPortTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PortComboBox = new System.Windows.Forms.ComboBox();
            this.BudComboBox = new System.Windows.Forms.ComboBox();
            this.OpenButton = new System.Windows.Forms.Button();
            this.LogRichTextBox = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClearMac = new System.Windows.Forms.Button();
            this.licensedatatbx = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Order = new System.Windows.Forms.TextBox();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.tbxReadMac = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DecideTextBox = new System.Windows.Forms.TextBox();
            this.ClearButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "串口号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "波特率：";
            // 
            // PortComboBox
            // 
            this.PortComboBox.FormattingEnabled = true;
            this.PortComboBox.Location = new System.Drawing.Point(81, 24);
            this.PortComboBox.Name = "PortComboBox";
            this.PortComboBox.Size = new System.Drawing.Size(110, 23);
            this.PortComboBox.TabIndex = 6;
            this.PortComboBox.SelectedIndexChanged += new System.EventHandler(this.PortComboBox_SelectedIndexChanged);
            this.PortComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PortComboBox_MouseClick);
            // 
            // BudComboBox
            // 
            this.BudComboBox.FormattingEnabled = true;
            this.BudComboBox.Location = new System.Drawing.Point(284, 24);
            this.BudComboBox.Name = "BudComboBox";
            this.BudComboBox.Size = new System.Drawing.Size(110, 23);
            this.BudComboBox.TabIndex = 7;
            // 
            // OpenButton
            // 
            this.OpenButton.Location = new System.Drawing.Point(438, 25);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(90, 31);
            this.OpenButton.TabIndex = 8;
            this.OpenButton.Text = "打开串口";
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // LogRichTextBox
            // 
            this.LogRichTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.LogRichTextBox.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LogRichTextBox.Location = new System.Drawing.Point(438, 131);
            this.LogRichTextBox.Name = "LogRichTextBox";
            this.LogRichTextBox.ReadOnly = true;
            this.LogRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.LogRichTextBox.Size = new System.Drawing.Size(469, 330);
            this.LogRichTextBox.TabIndex = 11;
            this.LogRichTextBox.Text = "";
            this.LogRichTextBox.TextChanged += new System.EventHandler(this.LogRichTextBox_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClearMac);
            this.groupBox2.Controls.Add(this.licensedatatbx);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txt_Order);
            this.groupBox2.Controls.Add(this.DownloadButton);
            this.groupBox2.Controls.Add(this.tbxReadMac);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.DecideTextBox);
            this.groupBox2.Controls.Add(this.ClearButton);
            this.groupBox2.Controls.Add(this.PortComboBox);
            this.groupBox2.Controls.Add(this.CloseButton);
            this.groupBox2.Controls.Add(this.LogRichTextBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.OpenButton);
            this.groupBox2.Controls.Add(this.BudComboBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(9, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(914, 474);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // btnClearMac
            // 
            this.btnClearMac.Location = new System.Drawing.Point(557, 68);
            this.btnClearMac.Name = "btnClearMac";
            this.btnClearMac.Size = new System.Drawing.Size(90, 31);
            this.btnClearMac.TabIndex = 26;
            this.btnClearMac.Text = "删除MAC";
            this.btnClearMac.UseVisualStyleBackColor = true;
            this.btnClearMac.Click += new System.EventHandler(this.btnClearMac_Click);
            // 
            // licensedatatbx
            // 
            this.licensedatatbx.BackColor = System.Drawing.SystemColors.Window;
            this.licensedatatbx.Font = new System.Drawing.Font("等线", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.licensedatatbx.Location = new System.Drawing.Point(8, 131);
            this.licensedatatbx.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.licensedatatbx.Multiline = true;
            this.licensedatatbx.Name = "licensedatatbx";
            this.licensedatatbx.ReadOnly = true;
            this.licensedatatbx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.licensedatatbx.Size = new System.Drawing.Size(385, 330);
            this.licensedatatbx.TabIndex = 24;
            this.licensedatatbx.TextChanged += new System.EventHandler(this.licensedatatbx_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 15);
            this.label4.TabIndex = 23;
            this.label4.Text = "Order：";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // txt_Order
            // 
            this.txt_Order.Location = new System.Drawing.Point(81, 89);
            this.txt_Order.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_Order.Name = "txt_Order";
            this.txt_Order.Size = new System.Drawing.Size(313, 25);
            this.txt_Order.TabIndex = 22;
            this.txt_Order.Text = "87654SCDD2107273210";
            // 
            // DownloadButton
            // 
            this.DownloadButton.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DownloadButton.Location = new System.Drawing.Point(671, 30);
            this.DownloadButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(119, 64);
            this.DownloadButton.TabIndex = 21;
            this.DownloadButton.Text = "烧录";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // tbxReadMac
            // 
            this.tbxReadMac.Location = new System.Drawing.Point(81, 58);
            this.tbxReadMac.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxReadMac.Name = "tbxReadMac";
            this.tbxReadMac.Size = new System.Drawing.Size(313, 25);
            this.tbxReadMac.TabIndex = 19;
            this.tbxReadMac.Text = "001CC2123A12";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 15);
            this.label3.TabIndex = 18;
            this.label3.Text = "MAC：";
            // 
            // DecideTextBox
            // 
            this.DecideTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.DecideTextBox.Font = new System.Drawing.Font("楷体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DecideTextBox.Location = new System.Drawing.Point(814, 45);
            this.DecideTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DecideTextBox.Multiline = true;
            this.DecideTextBox.Name = "DecideTextBox";
            this.DecideTextBox.ReadOnly = true;
            this.DecideTextBox.Size = new System.Drawing.Size(81, 43);
            this.DecideTextBox.TabIndex = 17;
            this.DecideTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(557, 26);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(90, 31);
            this.ClearButton.TabIndex = 10;
            this.ClearButton.Text = "清除数据";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(438, 68);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(90, 31);
            this.CloseButton.TabIndex = 9;
            this.CloseButton.Text = "关闭串口";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 491);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "出厂预置烧录工具 V1.0.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox PortComboBox;
        private System.Windows.Forms.ComboBox BudComboBox;
        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.RichTextBox LogRichTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.TextBox DecideTextBox;
        private System.Windows.Forms.TextBox tbxReadMac;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_Order;
        private System.Windows.Forms.TextBox licensedatatbx;
        private System.Windows.Forms.Button btnClearMac;
    }
}

