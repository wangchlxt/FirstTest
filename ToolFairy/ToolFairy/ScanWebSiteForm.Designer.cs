namespace ToolFairy
{
    partial class ScanWebSiteForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonSetThreadPool = new System.Windows.Forms.Button();
            this.textBoxMinThreadNumber = new System.Windows.Forms.TextBox();
            this.textBoxMaxThreadNumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelOutTip = new System.Windows.Forms.Label();
            this.richTextBoxOut = new System.Windows.Forms.RichTextBox();
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.buttonGet = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxCoding = new System.Windows.Forms.ComboBox();
            this.textBoxCode = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSetThreadPool);
            this.groupBox1.Controls.Add(this.textBoxMinThreadNumber);
            this.groupBox1.Controls.Add(this.textBoxMaxThreadNumber);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(26, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 54);
            this.groupBox1.TabIndex = 9;
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
            // labelOutTip
            // 
            this.labelOutTip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelOutTip.AutoSize = true;
            this.labelOutTip.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelOutTip.ForeColor = System.Drawing.Color.Red;
            this.labelOutTip.Location = new System.Drawing.Point(23, 9);
            this.labelOutTip.Name = "labelOutTip";
            this.labelOutTip.Size = new System.Drawing.Size(0, 14);
            this.labelOutTip.TabIndex = 10;
            // 
            // richTextBoxOut
            // 
            this.richTextBoxOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxOut.Location = new System.Drawing.Point(26, 153);
            this.richTextBoxOut.Name = "richTextBoxOut";
            this.richTextBoxOut.Size = new System.Drawing.Size(532, 360);
            this.richTextBoxOut.TabIndex = 11;
            this.richTextBoxOut.Text = "";
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.Location = new System.Drawing.Point(71, 115);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(254, 21);
            this.textBoxUrl.TabIndex = 12;
            // 
            // buttonGet
            // 
            this.buttonGet.Location = new System.Drawing.Point(512, 114);
            this.buttonGet.Name = "buttonGet";
            this.buttonGet.Size = new System.Drawing.Size(46, 23);
            this.buttonGet.TabIndex = 13;
            this.buttonGet.Text = "GET";
            this.buttonGet.UseVisualStyleBackColor = true;
            this.buttonGet.Click += new System.EventHandler(this.buttonGet_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "URL：";
            // 
            // comboBoxCoding
            // 
            this.comboBoxCoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCoding.FormattingEnabled = true;
            this.comboBoxCoding.Items.AddRange(new object[] {
            "",
            "Unicode",
            "UTF-7",
            "UTF-8",
            "UTF-32",
            "gb2312",
            "gbk"});
            this.comboBoxCoding.Location = new System.Drawing.Point(331, 115);
            this.comboBoxCoding.Name = "comboBoxCoding";
            this.comboBoxCoding.Size = new System.Drawing.Size(77, 20);
            this.comboBoxCoding.TabIndex = 23;
            // 
            // textBoxCode
            // 
            this.textBoxCode.Location = new System.Drawing.Point(414, 115);
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.Size = new System.Drawing.Size(92, 21);
            this.textBoxCode.TabIndex = 24;
            // 
            // ScanWebSiteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 525);
            this.Controls.Add(this.textBoxCode);
            this.Controls.Add(this.comboBoxCoding);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonGet);
            this.Controls.Add(this.textBoxUrl);
            this.Controls.Add(this.richTextBoxOut);
            this.Controls.Add(this.labelOutTip);
            this.Controls.Add(this.groupBox1);
            this.Name = "ScanWebSiteForm";
            this.Text = "ScanWebSiteForm";
            this.Load += new System.EventHandler(this.ScanWebSiteForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonSetThreadPool;
        private System.Windows.Forms.TextBox textBoxMinThreadNumber;
        private System.Windows.Forms.TextBox textBoxMaxThreadNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelOutTip;
        private System.Windows.Forms.RichTextBox richTextBoxOut;
        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.Button buttonGet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxCoding;
        private System.Windows.Forms.TextBox textBoxCode;
    }
}