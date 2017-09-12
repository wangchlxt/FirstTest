using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CSharpUtils.Net
{
    /// <summary>
    /// 端口工具类
    /// </summary>
    public class PortUtil
    {
        /// <summary>
        /// 扫描端口是否打开
        /// </summary>
        /// <param name="host">要扫描的主机地址</param>
        /// <param name="port">要扫描的端口</param>
        /// <param name="timeOut">扫描超时时间</param>
        /// <returns></returns>
        public bool ScanProtIsOpen(string host,int port,int timeOut)
        {
            bool ret = false;

            TcpClient tc = new TcpClient();
            tc.SendTimeout = timeOut;
            tc.ReceiveTimeout = timeOut;
            
            try
            {
                tc.Connect(host, port);
                if(tc.Connected)
                {
                    ret = true;
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error("端口扫描异常（" + host + ":" + port.ToString() + "）：", ex);
            }
            finally
            {
                tc.Close();
                tc = null;
            }

            return ret;
        }
        
    }
}
