using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;

namespace ToolFairy
{
    public partial class LocalHostNetworkForm : Form
    {
        public LocalHostNetworkForm()
        {
            InitializeComponent();
        }
        
        private void LocalHostNetworkForm_Load(object sender, EventArgs e)
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format("主机名：{0}", properties.HostName));
            sb.AppendLine(String.Format("域：{0}", properties.DomainName));
            sb.AppendLine(String.Format("DHCP：{0}", properties.DhcpScopeName));
            sb.AppendLine(String.Format("Windows Internet 名称服务 (WINS) 代理：{0}", properties.IsWinsProxy));
            sb.AppendLine(String.Format("网络基本输入/输出系统 (NetBIOS) 节点类型：{0}", properties.NodeType));

            sb.AppendLine("\r\nActive Tcp Connections：");
            foreach (TcpConnectionInformation conn in properties.GetActiveTcpConnections())
            {
                sb.AppendLine(String.Format("{0}  -  {1}  {2}", conn.LocalEndPoint, conn.RemoteEndPoint, conn.State));
            }

            sb.AppendLine("\r\nActive Tcp Listeners：");
            foreach(IPEndPoint point in properties.GetActiveTcpListeners())
            {
                sb.AppendLine(point.ToString());
            }

            sb.AppendLine("\r\nActive Udp Listeners：");
            foreach (IPEndPoint point in properties.GetActiveUdpListeners())
            {
                sb.AppendLine(point.ToString());
            }

            richTextBoxOut.Text = sb.ToString();
        }
    }
}
