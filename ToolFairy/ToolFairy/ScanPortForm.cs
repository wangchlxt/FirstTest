using CSharpUtils.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using CSharpUtils;
using System.Text.RegularExpressions;

namespace ToolFairy
{
    public partial class ScanPortForm : Form
    {
        public class ScanPortWorkData
        {
            public ScanPortForm Wnd { get; set; }
            public string Host { get; set; }
            public int Port { get; set; }
        }

        public ScanPortForm()
        {
            InitializeComponent();
        }

        #region 控件委托
        private delegate void SetTextCallback(string text);
        
        private void SetText(string text)
        {
            // InvokeRequired需要比较调用线程ID和创建线程ID
            // 如果它们不相同则返回true
            if (richTextBoxOut.InvokeRequired)
            {
                SetTextCallback cellback = new SetTextCallback(SetText);
                this.Invoke(cellback, new object[] { text });
            }
            else
            {
                this.richTextBoxOut.AppendText(text);
            }
        }

        private delegate void SetTipCallback(string text);

        private void SetTip(string text)
        {
            if(labelOutTip.InvokeRequired)
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

        private void buttonScan_Click(object sender, EventArgs e)
        {
            try
            {
                string host = textBoxHost.Text;
                int beginPort = int.Parse(textBoxBeginPort.Text);
                int endPort = int.Parse(textBoxEndPort.Text);
                string ip = "";

                if(RegexUtil.IsIPv4(host))
                {
                    ip = host;
                }
                else
                {
                    ip = new DnsUtil().GetIpByUrl(host);
                }
                
                richTextBoxOut.Text = String.Format("开始扫描 {0}\r\n", ip);

                Thread thread = new Thread(ScanThreadPoolWork);
                thread.Start(this);
                
                for (int i = beginPort; i <= endPort; i++)
                {
                    ScanPortWorkData data = new ScanPortWorkData();
                    data.Wnd = this;
                    data.Host = ip;
                    data.Port = i;

                    ThreadPool.QueueUserWorkItem(new WaitCallback(IsPortOpenWork), data);
                }
            }
            catch (Exception ex)
            {
                richTextBoxOut.Text = ex.Message;
            }
        }

        
        
        public static void ScanThreadPoolWork(Object param)
        {
            try
            {
                ScanPortForm wnd = param as ScanPortForm;
                if (wnd != null)
                {
                    while(true)
                    {
                        Thread.Sleep(500);
                        wnd.LookThreadPoolSize();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("监控线程池任务异常", ex);
            }
        }

        public static void IsPortOpenWork(object param)
        {
            ScanPortWorkData data = param as ScanPortWorkData;
            if(data != null)
            {
                try
                {
                    PortUtil util = new PortUtil();
                    StringBuilder sb = new StringBuilder();
                    
                    if (util.ScanProtIsOpen(data.Host, data.Port, 2000))
                    {
                        data.Wnd.SetText(String.Format("{0}:{1} open\r\n", data.Host, data.Port));
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error("扫描端口异常", ex);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialogPort.ShowDialog() == DialogResult.OK)
            {
                string saveFile = String.Format("{0}\\{1}.txt",
                    folderBrowserDialogPort.SelectedPath,
                    Regex.Replace(textBoxHost.Text, @"\W", "_"));

                System.IO.File.WriteAllText(saveFile, richTextBoxOut.Text.Replace("\n","\r\n"));

                System.Diagnostics.Process.Start("notepad.exe", saveFile);
            }
        }

        private void ScanPortForm_Load(object sender, EventArgs e)
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

        private void buttonSetThreadPool_Click(object sender, EventArgs e)
        {
            try
            {
                int minThreadNumber = int.Parse(textBoxMinThreadNumber.Text);
                int maxThreadNumber = int.Parse(textBoxMaxThreadNumber.Text);

                if(!ThreadPool.SetMinThreads(minThreadNumber, minThreadNumber))
                {
                    richTextBoxOut.AppendText("\r\n线程池最小线程数设置失败\r\n");
                }

                if (!ThreadPool.SetMaxThreads(maxThreadNumber, maxThreadNumber))
                {
                    richTextBoxOut.AppendText("\r\n线程池最大线程数设置失败\r\n");
                }

                LookThreadPoolSize();
            }
            catch (Exception ex)
            {
                richTextBoxOut.Text = ex.Message;
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
    }
}
