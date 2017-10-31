using CSharpUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToolFairy
{
    public partial class MainWindow_Form : Form
    {
        public MainWindow_Form()
        {
            InitializeComponent();
        }

        private void MainWindow_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;

                this.WindowState = FormWindowState.Minimized;
                this.mainNotifyIcon.Visible = true;
                this.Hide();

                return;
            }
        }

        private void MC_EXIT_Click(object sender, EventArgs e)
        {
            this.mainNotifyIcon.Visible = false;
            this.Close();
            this.Dispose();
            System.Environment.Exit(System.Environment.ExitCode);
        }

        private void mainNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }
        
        private void MainWindow_Form_Load(object sender, EventArgs e)
        {
            Bing bing = new Bing();
            CKDownBingHomeImage.Checked = bing.Config.IsSwitch;
        }

        private void CKDownBingHomeImage_CheckedChanged(object sender, EventArgs e)
        {
            Bing bing = new Bing();
            bing.Config.IsSwitch = CKDownBingHomeImage.Checked;
            bing.SaveConfig();
        }

        private void buttonSelectDesktopImage_Click(object sender, EventArgs e)
        {
            if(openFileDialogDesktopImage.ShowDialog() == DialogResult.OK)
            {
                textBoxDesktopImage.Text = openFileDialogDesktopImage.FileName;
            }
        }

        private void buttonSetDesktopImage_Click(object sender, EventArgs e)
        {
            string desktopImage = textBoxDesktopImage.Text;
            if(string.IsNullOrEmpty(desktopImage))
            {
                MessageBox.Show("请先选择要设置的图片");
                return;
            }

            DesktopUtil.SetDesktopImg(desktopImage);
        }

        private void buttonOpenBingHomeImageFolder_Click(object sender, EventArgs e)
        {
            Bing bing = new Bing();
            System.Diagnostics.Process.Start(bing.Config.SaveHomeImagePath);
        }

        private void buttonPortScan_Click(object sender, EventArgs e)
        {
            ScanPortForm wnd = new ScanPortForm();
            wnd.Show();
        }

        private void buttonStringDispose_Click(object sender, EventArgs e)
        {
            StringDisposeForm wnd = new StringDisposeForm();
            wnd.Show();
        }

        private void buttonLocalHostNetwork_Click(object sender, EventArgs e)
        {
            LocalHostNetworkForm wnd = new LocalHostNetworkForm();
            wnd.Show();
        }

        private void buttonScanWebSite_Click(object sender, EventArgs e)
        {
            ScanWebSiteForm wnd = new ScanWebSiteForm();
            wnd.Show();
        }
    }
}
