using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSharpUtils;
using CSharpUtils.Net;
using System.Threading;

namespace ToolFairy
{
    public partial class ScanWebSiteForm : Form
    {
        public ScanWebSiteForm()
        {
            InitializeComponent();
        }

        #region 委托
        private delegate void SetTipCallback(string text);

        private void SetTip(string text)
        {
            if (labelOutTip.InvokeRequired)
            {
                SetTipCallback callback = new SetTipCallback(SetTip);
                this.Invoke(callback, new object[] { text });
            }
            else
            {
                this.labelOutTip.Text = text;
            }
        }
        #endregion

        private void buttonSetThreadPool_Click(object sender, EventArgs e)
        {
            try
            {
                int minThreadNumber = int.Parse(textBoxMinThreadNumber.Text);
                int maxThreadNumber = int.Parse(textBoxMaxThreadNumber.Text);

                if (!ThreadPool.SetMinThreads(minThreadNumber, minThreadNumber))
                {
                    SetTip("\r\n线程池最小线程数设置失败");
                }

                if (!ThreadPool.SetMaxThreads(maxThreadNumber, maxThreadNumber))
                {
                    SetTip("\r\n线程池最大线程数设置失败");
                }

                LookThreadPoolSize();
            }
            catch (Exception ex)
            {
                LogHelper.Error("ScanWebSiteForm 设置线程池异常", ex);
            }
        }

        private void LookThreadPoolSize()
        {
            int workerThreadsMin = 0;
            int completionPortThreadsMin = 0;
            ThreadPool.GetMinThreads(out workerThreadsMin, out completionPortThreadsMin);

            int workerThreadsMax = 0;
            int completionPortThreadsMax = 0;
            ThreadPool.GetMaxThreads(out workerThreadsMax, out completionPortThreadsMax);

            int workerThreads = 0;
            int completionPortThreads = 0;
            ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);

            SetTip(String.Format("线程池：{0}({1}-{2}) - {3}({4}-{5})  工作线程数：{6}",
                workerThreads, workerThreadsMin, workerThreadsMax,
                completionPortThreads, completionPortThreadsMin, completionPortThreadsMax,
                workerThreadsMax - workerThreads));
        }

        private void ScanWebSiteForm_Load(object sender, EventArgs e)
        {
            int workerThreadsMin = 0;
            int completionPortThreadsMin = 0;
            ThreadPool.GetMinThreads(out workerThreadsMin, out completionPortThreadsMin);

            int workerThreadsMax = 0;
            int completionPortThreadsMax = 0;
            ThreadPool.GetMaxThreads(out workerThreadsMax, out completionPortThreadsMax);

            textBoxMinThreadNumber.Text = workerThreadsMin.ToString();
            textBoxMaxThreadNumber.Text = workerThreadsMax.ToString();
        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBoxUrl.Text))
            {
                labelOutTip.Text = "请输入 URL";
                return;
            }

            //richTextBoxOut.Text = HttpUtil.GetWebRequest(textBoxUrl.Text, "", ConvertUtil.ConvertEncodingString(comboBoxCoding.Text));

            richTextBoxOut.Text = HttpUtil.HttpGet(textBoxUrl.Text, "");
        }
        
    }
}
