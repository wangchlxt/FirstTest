using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wechat.Util
{
    public interface IWechatMessageHandler
    {
        /// <summary>
        /// 处理微信推送到系统的消息
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        string Process( string xml);
    }
}
