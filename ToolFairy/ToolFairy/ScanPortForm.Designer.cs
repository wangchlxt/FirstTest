namespace ToolFairy
{
    partial class ScanPortForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonScan = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxHost = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxBeginPort = new System.Windows.Forms.TextBox();
            this.textBoxEndPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBoxOut = new System.Windows.Forms.RichTextBox();
            this.labelOutTip = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.folderBrowserDialogPort = new System.Windows.Forms.FolderBrowserDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxMinThreadNumber = new System.Windows.Forms.TextBox();
            this.textBoxMaxThreadNumber = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonSetThreadPool = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonScan
            // 
            this.buttonScan.Location = new System.Drawing.Point(232, 12);
            this.buttonScan.Name = "buttonScan";
            this.buttonScan.Size = new System.Drawing.Size(50, 23);
            this.buttonScan.TabIndex = 0;
            this.buttonScan.Text = "扫描";
            this.buttonScan.UseVisualStyleBackColor = true;
            this.buttonScan.Click += new System.EventHandler(this.buttonScan_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Host";
            // 
            // textBoxHost
            // 
            this.textBoxHost.Location = new System.Drawing.Point(70, 46);
            this.textBoxHost.Name = "textBoxHost";
            this.textBoxHost.Size = new System.Drawing.Size(385, 21);
            this.textBoxHost.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port";
            // 
            // textBoxBeginPort
            // 
            this.textBoxBeginPort.Location = new System.Drawing.Point(70, 14);
            this.textBoxBeginPort.Name = "textBoxBeginPort";
            this.textBoxBeginPort.Size = new System.Drawing.Size(54, 21);
            this.textBoxBeginPort.TabIndex = 2;
            this.textBoxBeginPort.Text = "0";
            // 
            // textBoxEndPort
            // 
            this.textBoxEndPort.Location = new System.Drawing.Point(147, 14);
            this.textBoxEndPort.Name = "textBoxEndPort";
            this.textBoxEndPort.Size = new System.Drawing.Size(52, 21);
            this.textBoxEndPort.TabIndex = 2;
            this.textBoxEndPort.Text = "65535";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(130, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "-";
            // 
            // richTextBoxOut
            // 
            this.richTextBoxOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxOut.Location = new System.Drawing.Point(26, 151);
            this.richTextBoxOut.Name = "richTextBoxOut";
            this.richTextBoxOut.Size = new System.Drawing.Size(453, 319);
            this.richTextBoxOut.TabIndex = 3;
            this.richTextBoxOut.Text = "";
            // 
            // labelOutTip
            // 
            this.labelOutTip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelOutTip.AutoSize = true;
            this.labelOutTip.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelOutTip.ForeColor = System.Drawing.Color.Red;
            this.labelOutTip.Location = new System.Drawing.Point(26, 488);
            this.labelOutTip.Name = "labelOutTip";
            this.labelOutTip.Size = new System.Drawing.Size(0, 14);
            this.labelOutTip.TabIndex = 4;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(288, 12);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(50, 23);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "最小线程数";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(179, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "最大线程数";
            // 
            // textBoxMinThreadNumber
            // 
            this.textBoxMinThreadNumber.Location = new System.Drawing.Point(89, 20);
            this.textBoxMinThreadNumber.Name = "textBoxMinThreadNumber";
            this.textBoxMinThreadNumber.Size = new System.Drawing.Size(73, 21);
            this.textBoxMinThreadNumber.TabIndex = 7;
            // 
            // textBoxMaxThreadNumber
            // 
            this.textBoxMaxThreadNumber.Location = new System.Drawing.Point(259, 20);
            this.textBoxMaxThreadNumber.Name = "textBoxMaxThreadNumber";
            this.textBoxMaxThreadNumber.Size = new System.Drawing.Size(73, 21);
            this.textBoxMaxThreadNumber.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSetThreadPool);
            this.groupBox1.Controls.Add(this.textBoxMinThreadNumber);
            this.groupBox1.Controls.Add(this.textBoxMaxThreadNumber);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(29, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 54);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "线程池";
            // 
            // buttonSetThreadPool
            // 
            this.buttonSetThreadPool.Location = new System.Drawing.Point(354, 20);
            this.buttonSetThreadPool.Name = "buttonSetThreadPool";
            this.buttonSetThreadPool.Size = new System.Drawing.Size(51, 23);
            this.buttonSetThreadPool.TabIndex = 8;
            this.buttonSetThreadPool.Text = "设置";
            this.buttonSetThreadPool.UseVisualStyleBackColor = true;
            this.buttonSetThreadPool.Click += new System.EventHandler(this.buttonSetThreadPool_Click);
            // 
            // ScanPortForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 512);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelOutTip);
            this.Controls.Add(this.richTextBoxOut);
            this.Controls.Add(this.textBoxEndPort);
            this.Controls.Add(this.textBoxBeginPort);
            this.Controls.Add(this.textBoxHost);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonScan);
            this.Name = "ScanPortForm";
            this.Text = "ScanPortForm";
            this.Load += new System.EventHandler(this.ScanPortForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonScan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxHost;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxBeginPort;
        private System.Windows.Forms.TextBox textBoxEndPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBoxOut;
        private System.Windows.Forms.Label labelOutTip;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxMinThreadNumber;
        private System.Windows.Forms.TextBox textBoxMaxThreadNumber;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonSetThreadPool;
    }
}