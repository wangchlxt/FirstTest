namespace ToolFairy
{
    partial class MainWindow_Form
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow_Form));
            this.mainNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.mainNotifyContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MC_EXIT = new System.Windows.Forms.ToolStripMenuItem();
            this.CKDownBingHomeImage = new System.Windows.Forms.CheckBox();
            this.openFileDialogDesktopImage = new System.Windows.Forms.OpenFileDialog();
            this.textBoxDesktopImage = new System.Windows.Forms.TextBox();
            this.buttonSelectDesktopImage = new System.Windows.Forms.Button();
            this.buttonSetDesktopImage = new System.Windows.Forms.Button();
            this.buttonPortScan = new System.Windows.Forms.Button();
            this.buttonOpenBingHomeImageFolder = new System.Windows.Forms.Button();
            this.buttonStringDispose = new System.Windows.Forms.Button();
            this.buttonLocalHostNetwork = new System.Windows.Forms.Button();
            this.buttonScanWebSite = new System.Windows.Forms.Button();
            this.mainNotifyContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainNotifyIcon
            // 
            this.mainNotifyIcon.ContextMenuStrip = this.mainNotifyContextMenuStrip;
            this.mainNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("mainNotifyIcon.Icon")));
            this.mainNotifyIcon.Text = "工具精灵";
            this.mainNotifyIcon.Visible = true;
            this.mainNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mainNotifyIcon_MouseDoubleClick);
            // 
            // mainNotifyContextMenuStrip
            // 
            this.mainNotifyContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MC_EXIT});
            this.mainNotifyContextMenuStrip.Name = "mainNotifyContextMenuStrip";
            this.mainNotifyContextMenuStrip.Size = new System.Drawing.Size(101, 26);
            // 
            // MC_EXIT
            // 
            this.MC_EXIT.Name = "MC_EXIT";
            this.MC_EXIT.Size = new System.Drawing.Size(100, 22);
            this.MC_EXIT.Text = "退出";
            this.MC_EXIT.Click += new System.EventHandler(this.MC_EXIT_Click);
            // 
            // CKDownBingHomeImage
            // 
            this.CKDownBingHomeImage.AutoSize = true;
            this.CKDownBingHomeImage.Checked = true;
            this.CKDownBingHomeImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CKDownBingHomeImage.Location = new System.Drawing.Point(12, 99);
            this.CKDownBingHomeImage.Name = "CKDownBingHomeImage";
            this.CKDownBingHomeImage.Size = new System.Drawing.Size(126, 16);
            this.CKDownBingHomeImage.TabIndex = 1;
            this.CKDownBingHomeImage.Text = "Bing 首页图片下载";
            this.CKDownBingHomeImage.UseVisualStyleBackColor = true;
            this.CKDownBingHomeImage.CheckedChanged += new System.EventHandler(this.CKDownBingHomeImage_CheckedChanged);
            // 
            // openFileDialogDesktopImage
            // 
            this.openFileDialogDesktopImage.FileName = "请选择图片";
            this.openFileDialogDesktopImage.Filter = "图片文件(*.jpg,*.jpeg,*.gif,*.bmp,*.png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png";
            // 
            // textBoxDesktopImage
            // 
            this.textBoxDesktopImage.Location = new System.Drawing.Point(12, 28);
            this.textBoxDesktopImage.Name = "textBoxDesktopImage";
            this.textBoxDesktopImage.Size = new System.Drawing.Size(429, 21);
            this.textBoxDesktopImage.TabIndex = 2;
            // 
            // buttonSelectDesktopImage
            // 
            this.buttonSelectDesktopImage.Location = new System.Drawing.Point(447, 28);
            this.buttonSelectDesktopImage.Name = "buttonSelectDesktopImage";
            this.buttonSelectDesktopImage.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectDesktopImage.TabIndex = 3;
            this.buttonSelectDesktopImage.Text = "选择图片";
            this.buttonSelectDesktopImage.UseVisualStyleBackColor = true;
            this.buttonSelectDesktopImage.Click += new System.EventHandler(this.buttonSelectDesktopImage_Click);
            // 
            // buttonSetDesktopImage
            // 
            this.buttonSetDesktopImage.Location = new System.Drawing.Point(528, 28);
            this.buttonSetDesktopImage.Name = "buttonSetDesktopImage";
            this.buttonSetDesktopImage.Size = new System.Drawing.Size(75, 23);
            this.buttonSetDesktopImage.TabIndex = 4;
            this.buttonSetDesktopImage.Text = "设置桌面";
            this.buttonSetDesktopImage.UseVisualStyleBackColor = true;
            this.buttonSetDesktopImage.Click += new System.EventHandler(this.buttonSetDesktopImage_Click);
            // 
            // buttonPortScan
            // 
            this.buttonPortScan.Location = new System.Drawing.Point(23, 156);
            this.buttonPortScan.Name = "buttonPortScan";
            this.buttonPortScan.Size = new System.Drawing.Size(75, 23);
            this.buttonPortScan.TabIndex = 5;
            this.buttonPortScan.Text = "端口扫描";
            this.buttonPortScan.UseVisualStyleBackColor = true;
            this.buttonPortScan.Click += new System.EventHandler(this.buttonPortScan_Click);
            // 
            // buttonOpenBingHomeImageFolder
            // 
            this.buttonOpenBingHomeImageFolder.Location = new System.Drawing.Point(156, 95);
            this.buttonOpenBingHomeImageFolder.Name = "buttonOpenBingHomeImageFolder";
            this.buttonOpenBingHomeImageFolder.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenBingHomeImageFolder.TabIndex = 6;
            this.buttonOpenBingHomeImageFolder.Text = "打开文件夹";
            this.buttonOpenBingHomeImageFolder.UseVisualStyleBackColor = true;
            this.buttonOpenBingHomeImageFolder.Click += new System.EventHandler(this.buttonOpenBingHomeImageFolder_Click);
            // 
            // buttonStringDispose
            // 
            this.buttonStringDispose.Location = new System.Drawing.Point(366, 156);
            this.buttonStringDispose.Name = "buttonStringDispose";
            this.buttonStringDispose.Size = new System.Drawing.Size(75, 23);
            this.buttonStringDispose.TabIndex = 7;
            this.buttonStringDispose.Text = "字符串处理";
            this.buttonStringDispose.UseVisualStyleBackColor = true;
            this.buttonStringDispose.Click += new System.EventHandler(this.buttonStringDispose_Click);
            // 
            // buttonLocalHostNetwork
            // 
            this.buttonLocalHostNetwork.Location = new System.Drawing.Point(127, 156);
            this.buttonLocalHostNetwork.Name = "buttonLocalHostNetwork";
            this.buttonLocalHostNetwork.Size = new System.Drawing.Size(75, 23);
            this.buttonLocalHostNetwork.TabIndex = 8;
            this.buttonLocalHostNetwork.Text = "本机网络";
            this.buttonLocalHostNetwork.UseVisualStyleBackColor = true;
            this.buttonLocalHostNetwork.Click += new System.EventHandler(this.buttonLocalHostNetwork_Click);
            // 
            // buttonScanWebSite
            // 
            this.buttonScanWebSite.Location = new System.Drawing.Point(485, 155);
            this.buttonScanWebSite.Name = "buttonScanWebSite";
            this.buttonScanWebSite.Size = new System.Drawing.Size(75, 23);
            this.buttonScanWebSite.TabIndex = 9;
            this.buttonScanWebSite.Text = "网站扫描";
            this.buttonScanWebSite.UseVisualStyleBackColor = true;
            this.buttonScanWebSite.Click += new System.EventHandler(this.buttonScanWebSite_Click);
            // 
            // MainWindow_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 225);
            this.Controls.Add(this.buttonScanWebSite);
            this.Controls.Add(this.buttonLocalHostNetwork);
            this.Controls.Add(this.buttonStringDispose);
            this.Controls.Add(this.buttonOpenBingHomeImageFolder);
            this.Controls.Add(this.buttonPortScan);
            this.Controls.Add(this.buttonSetDesktopImage);
            this.Controls.Add(this.buttonSelectDesktopImage);
            this.Controls.Add(this.textBoxDesktopImage);
            this.Controls.Add(this.CKDownBingHomeImage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow_Form";
            this.Text = "工具精灵";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_Form_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Form_Load);
            this.mainNotifyContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon mainNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip mainNotifyContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem MC_EXIT;
        private System.Windows.Forms.CheckBox CKDownBingHomeImage;
        private System.Windows.Forms.OpenFileDialog openFileDialogDesktopImage;
        private System.Windows.Forms.TextBox textBoxDesktopImage;
        private System.Windows.Forms.Button buttonSelectDesktopImage;
        private System.Windows.Forms.Button buttonSetDesktopImage;
        private System.Windows.Forms.Button buttonPortScan;
        private System.Windows.Forms.Button buttonOpenBingHomeImageFolder;
        private System.Windows.Forms.Button buttonStringDispose;
        private System.Windows.Forms.Button buttonLocalHostNetwork;
        private System.Windows.Forms.Button buttonScanWebSite;
    }
}

