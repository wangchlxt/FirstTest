namespace ToolFairy
{
    partial class LocalHostNetworkForm
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
            this.labelOutTip = new System.Windows.Forms.Label();
            this.richTextBoxOut = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // labelOutTip
            // 
            this.labelOutTip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelOutTip.AutoSize = true;
            this.labelOutTip.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelOutTip.ForeColor = System.Drawing.Color.Red;
            this.labelOutTip.Location = new System.Drawing.Point(22, 503);
            this.labelOutTip.Name = "labelOutTip";
            this.labelOutTip.Size = new System.Drawing.Size(0, 14);
            this.labelOutTip.TabIndex = 12;
            // 
            // richTextBoxOut
            // 
            this.richTextBoxOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxOut.Location = new System.Drawing.Point(22, 12);
            this.richTextBoxOut.Name = "richTextBoxOut";
            this.richTextBoxOut.Size = new System.Drawing.Size(643, 473);
            this.richTextBoxOut.TabIndex = 11;
            this.richTextBoxOut.Text = "";
            // 
            // LocalHostNetworkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 529);
            this.Controls.Add(this.labelOutTip);
            this.Controls.Add(this.richTextBoxOut);
            this.Name = "LocalHostNetworkForm";
            this.Text = "LocalHostNetworkForm";
            this.Load += new System.EventHandler(this.LocalHostNetworkForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelOutTip;
        private System.Windows.Forms.RichTextBox richTextBoxOut;
    }
}