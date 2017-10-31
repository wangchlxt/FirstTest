namespace ToolFairy
{
    partial class StringDisposeForm
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
            this.buttonLower = new System.Windows.Forms.Button();
            this.buttonUpper = new System.Windows.Forms.Button();
            this.buttonUrlDecoding = new System.Windows.Forms.Button();
            this.buttonUrlEncoding = new System.Windows.Forms.Button();
            this.labelOutTip = new System.Windows.Forms.Label();
            this.labelInTip = new System.Windows.Forms.Label();
            this.buttonMD5 = new System.Windows.Forms.Button();
            this.richTextBoxOut = new System.Windows.Forms.RichTextBox();
            this.richTextBoxIn = new System.Windows.Forms.RichTextBox();
            this.buttonDeleteSpecialCharacter = new System.Windows.Forms.Button();
            this.buttonHmacsha1 = new System.Windows.Forms.Button();
            this.buttonBase64Encode = new System.Windows.Forms.Button();
            this.buttonBase64Decode = new System.Windows.Forms.Button();
            this.comboBoxBase64 = new System.Windows.Forms.ComboBox();
            this.textBoxHmacsha1Key = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.comboBoxSHA1 = new System.Windows.Forms.ComboBox();
            this.buttonSHA1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonLower
            // 
            this.buttonLower.Location = new System.Drawing.Point(196, 39);
            this.buttonLower.Name = "buttonLower";
            this.buttonLower.Size = new System.Drawing.Size(75, 23);
            this.buttonLower.TabIndex = 17;
            this.buttonLower.Text = "小写";
            this.buttonLower.UseVisualStyleBackColor = true;
            this.buttonLower.Click += new System.EventHandler(this.buttonLower_Click);
            // 
            // buttonUpper
            // 
            this.buttonUpper.Location = new System.Drawing.Point(94, 39);
            this.buttonUpper.Name = "buttonUpper";
            this.buttonUpper.Size = new System.Drawing.Size(75, 23);
            this.buttonUpper.TabIndex = 16;
            this.buttonUpper.Text = "大写";
            this.buttonUpper.UseVisualStyleBackColor = true;
            this.buttonUpper.Click += new System.EventHandler(this.buttonUpper_Click);
            // 
            // buttonUrlDecoding
            // 
            this.buttonUrlDecoding.Location = new System.Drawing.Point(217, 36);
            this.buttonUrlDecoding.Name = "buttonUrlDecoding";
            this.buttonUrlDecoding.Size = new System.Drawing.Size(75, 23);
            this.buttonUrlDecoding.TabIndex = 15;
            this.buttonUrlDecoding.Text = "URL 解密";
            this.buttonUrlDecoding.UseVisualStyleBackColor = true;
            this.buttonUrlDecoding.Click += new System.EventHandler(this.buttonUrlDecoding_Click);
            // 
            // buttonUrlEncoding
            // 
            this.buttonUrlEncoding.Location = new System.Drawing.Point(94, 36);
            this.buttonUrlEncoding.Name = "buttonUrlEncoding";
            this.buttonUrlEncoding.Size = new System.Drawing.Size(75, 23);
            this.buttonUrlEncoding.TabIndex = 14;
            this.buttonUrlEncoding.Text = "URL 加密";
            this.buttonUrlEncoding.UseVisualStyleBackColor = true;
            this.buttonUrlEncoding.Click += new System.EventHandler(this.buttonUrlEncoding_Click);
            // 
            // labelOutTip
            // 
            this.labelOutTip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelOutTip.AutoSize = true;
            this.labelOutTip.Location = new System.Drawing.Point(28, 601);
            this.labelOutTip.Name = "labelOutTip";
            this.labelOutTip.Size = new System.Drawing.Size(0, 12);
            this.labelOutTip.TabIndex = 13;
            // 
            // labelInTip
            // 
            this.labelInTip.AutoSize = true;
            this.labelInTip.Location = new System.Drawing.Point(28, 190);
            this.labelInTip.Name = "labelInTip";
            this.labelInTip.Size = new System.Drawing.Size(0, 12);
            this.labelInTip.TabIndex = 12;
            // 
            // buttonMD5
            // 
            this.buttonMD5.Location = new System.Drawing.Point(398, 35);
            this.buttonMD5.Name = "buttonMD5";
            this.buttonMD5.Size = new System.Drawing.Size(75, 23);
            this.buttonMD5.TabIndex = 11;
            this.buttonMD5.Text = "MD5 加密";
            this.buttonMD5.UseVisualStyleBackColor = true;
            this.buttonMD5.Click += new System.EventHandler(this.buttonMD5_Click);
            // 
            // richTextBoxOut
            // 
            this.richTextBoxOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxOut.Location = new System.Drawing.Point(26, 358);
            this.richTextBoxOut.Name = "richTextBoxOut";
            this.richTextBoxOut.Size = new System.Drawing.Size(1049, 228);
            this.richTextBoxOut.TabIndex = 10;
            this.richTextBoxOut.Text = "";
            this.richTextBoxOut.TextChanged += new System.EventHandler(this.richTextBoxOut_TextChanged);
            // 
            // richTextBoxIn
            // 
            this.richTextBoxIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxIn.Location = new System.Drawing.Point(26, 12);
            this.richTextBoxIn.Name = "richTextBoxIn";
            this.richTextBoxIn.Size = new System.Drawing.Size(1049, 165);
            this.richTextBoxIn.TabIndex = 9;
            this.richTextBoxIn.Text = "";
            this.richTextBoxIn.TextChanged += new System.EventHandler(this.richTextBoxIn_TextChanged);
            // 
            // buttonDeleteSpecialCharacter
            // 
            this.buttonDeleteSpecialCharacter.Location = new System.Drawing.Point(200, 34);
            this.buttonDeleteSpecialCharacter.Name = "buttonDeleteSpecialCharacter";
            this.buttonDeleteSpecialCharacter.Size = new System.Drawing.Size(75, 23);
            this.buttonDeleteSpecialCharacter.TabIndex = 18;
            this.buttonDeleteSpecialCharacter.Text = "删除特殊字符";
            this.buttonDeleteSpecialCharacter.UseVisualStyleBackColor = true;
            this.buttonDeleteSpecialCharacter.Click += new System.EventHandler(this.buttonDeleteSpecialCharacter_Click);
            // 
            // buttonHmacsha1
            // 
            this.buttonHmacsha1.Location = new System.Drawing.Point(286, 40);
            this.buttonHmacsha1.Name = "buttonHmacsha1";
            this.buttonHmacsha1.Size = new System.Drawing.Size(103, 23);
            this.buttonHmacsha1.TabIndex = 19;
            this.buttonHmacsha1.Text = "Hmacsha1 加密";
            this.buttonHmacsha1.UseVisualStyleBackColor = true;
            this.buttonHmacsha1.Click += new System.EventHandler(this.buttonHmacsha1_Click);
            // 
            // buttonBase64Encode
            // 
            this.buttonBase64Encode.Location = new System.Drawing.Point(297, 36);
            this.buttonBase64Encode.Name = "buttonBase64Encode";
            this.buttonBase64Encode.Size = new System.Drawing.Size(85, 23);
            this.buttonBase64Encode.TabIndex = 20;
            this.buttonBase64Encode.Text = "Base64 加密";
            this.buttonBase64Encode.UseVisualStyleBackColor = true;
            this.buttonBase64Encode.Click += new System.EventHandler(this.buttonBase64Encode_Click);
            // 
            // buttonBase64Decode
            // 
            this.buttonBase64Decode.Location = new System.Drawing.Point(408, 36);
            this.buttonBase64Decode.Name = "buttonBase64Decode";
            this.buttonBase64Decode.Size = new System.Drawing.Size(85, 23);
            this.buttonBase64Decode.TabIndex = 21;
            this.buttonBase64Decode.Text = "Base64 解密";
            this.buttonBase64Decode.UseVisualStyleBackColor = true;
            this.buttonBase64Decode.Click += new System.EventHandler(this.buttonBase64Decode_Click);
            // 
            // comboBoxBase64
            // 
            this.comboBoxBase64.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBase64.FormattingEnabled = true;
            this.comboBoxBase64.Items.AddRange(new object[] {
            "ASCII",
            "BigEndianUnicode",
            "Default",
            "Unicode",
            "UTF32",
            "UTF7",
            "UTF8"});
            this.comboBoxBase64.Location = new System.Drawing.Point(141, 36);
            this.comboBoxBase64.Name = "comboBoxBase64";
            this.comboBoxBase64.Size = new System.Drawing.Size(121, 20);
            this.comboBoxBase64.TabIndex = 22;
            // 
            // textBoxHmacsha1Key
            // 
            this.textBoxHmacsha1Key.Location = new System.Drawing.Point(149, 40);
            this.textBoxHmacsha1Key.Name = "textBoxHmacsha1Key";
            this.textBoxHmacsha1Key.Size = new System.Drawing.Size(103, 21);
            this.textBoxHmacsha1Key.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(108, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 24;
            this.label1.Text = "Key：";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Location = new System.Drawing.Point(26, 211);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1049, 126);
            this.tabControl1.TabIndex = 25;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBoxHmacsha1Key);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.buttonHmacsha1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1041, 100);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Hmacsha1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.comboBoxBase64);
            this.tabPage2.Controls.Add(this.buttonBase64Decode);
            this.tabPage2.Controls.Add(this.buttonBase64Encode);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1041, 100);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Base64";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.buttonMD5);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1041, 100);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "MD5";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.buttonUrlEncoding);
            this.tabPage4.Controls.Add(this.buttonUrlDecoding);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1041, 100);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "URL";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.buttonUpper);
            this.tabPage5.Controls.Add(this.buttonLower);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1041, 100);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "大小写";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.buttonDeleteSpecialCharacter);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(1041, 100);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "删除替换";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.buttonSHA1);
            this.tabPage7.Controls.Add(this.comboBoxSHA1);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(1041, 100);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "SHA1";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // comboBoxSHA1
            // 
            this.comboBoxSHA1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSHA1.FormattingEnabled = true;
            this.comboBoxSHA1.Items.AddRange(new object[] {
            "ASCII",
            "BigEndianUnicode",
            "Default",
            "Unicode",
            "UTF32",
            "UTF7",
            "UTF8"});
            this.comboBoxSHA1.Location = new System.Drawing.Point(193, 38);
            this.comboBoxSHA1.Name = "comboBoxSHA1";
            this.comboBoxSHA1.Size = new System.Drawing.Size(121, 20);
            this.comboBoxSHA1.TabIndex = 23;
            // 
            // buttonSHA1
            // 
            this.buttonSHA1.Location = new System.Drawing.Point(372, 38);
            this.buttonSHA1.Name = "buttonSHA1";
            this.buttonSHA1.Size = new System.Drawing.Size(75, 23);
            this.buttonSHA1.TabIndex = 24;
            this.buttonSHA1.Text = "SHA1 加密";
            this.buttonSHA1.UseVisualStyleBackColor = true;
            this.buttonSHA1.Click += new System.EventHandler(this.buttonSHA1_Click);
            // 
            // StringDisposeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 628);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.labelOutTip);
            this.Controls.Add(this.labelInTip);
            this.Controls.Add(this.richTextBoxOut);
            this.Controls.Add(this.richTextBoxIn);
            this.Name = "StringDisposeForm";
            this.Text = "StringDisposeForm";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLower;
        private System.Windows.Forms.Button buttonUpper;
        private System.Windows.Forms.Button buttonUrlDecoding;
        private System.Windows.Forms.Button buttonUrlEncoding;
        private System.Windows.Forms.Label labelOutTip;
        private System.Windows.Forms.Label labelInTip;
        private System.Windows.Forms.Button buttonMD5;
        private System.Windows.Forms.RichTextBox richTextBoxOut;
        private System.Windows.Forms.RichTextBox richTextBoxIn;
        private System.Windows.Forms.Button buttonDeleteSpecialCharacter;
        private System.Windows.Forms.Button buttonHmacsha1;
        private System.Windows.Forms.Button buttonBase64Encode;
        private System.Windows.Forms.Button buttonBase64Decode;
        private System.Windows.Forms.ComboBox comboBoxBase64;
        private System.Windows.Forms.TextBox textBoxHmacsha1Key;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.Button buttonSHA1;
        private System.Windows.Forms.ComboBox comboBoxSHA1;
    }
}