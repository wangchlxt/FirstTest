using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CSharpUtils;

namespace Wechat.Util
{
    public class WechatMessageHandler
    {
        private static IDictionary<string, IWechatMessageHandler> dicMessageHandler;

        static WechatMessageHandler()
        {
            dicMessageHandler = new Dictionary<string, IWechatMessageHandler>();
        }

        #region RegisterMessageHandler
        /// <summary>
        /// 注册微信消息处理器
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="handler"></param>
        public static void RegisterMessageHandler(string messageType, IWechatMessageHandler handler)
        {
            dicMessageHandler.Add(messageType.ToLower(), handler);
        }
        #endregion

        #region GetMessageHandler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageType"></param>
        /// <returns></returns>
        public static IWechatMessageHandler GetMessageHandler(string messageType)
        {
            if (dicMessageHandler.ContainsKey(messageType.ToLower()))
            {
                return dicMessageHandler[messageType.ToLower()];
            }

            return null;
        }
        #endregion

        #region Process
        /// <summary>
        /// 处理微信 POST 过来的消息
        /// </summary>
        /// <param name="xml"></param>
        public static string Process(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return string.Empty;
            }

            string messageType = WechatMessageType.GetMessageType(xml);

            IWechatMessageHandler handler = GetMessageHandler(messageType);
            if (handler == null)
            {
                return string.Empty;
            }

            if (messageType == WechatMessageType.Text)
            {
                return handler.Process(xml);
            }

            Task.Run(() => handler.Process(xml)).ContinueWith(t =>
        {
            if (t.IsFaulted)
            {
                LogHelper.Error(string.Format("[{0}] 消息处理器执行异常。 ", messageType), t.Exception);
            }
            return t.Result;
        });

            return string.Empty;
        }
        #endregion


    }
}
