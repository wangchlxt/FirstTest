using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Wechat.Util
{
    public sealed class WechatMessageType
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        public const string Text = "TEXT";

        /// <summary>
        /// 事件消息
        /// </summary>
        internal const string Event = "EVENT";

        /// <summary>
        /// 点击事件
        /// </summary>
        public const string EventClick = "EVENT_CLICK";

        /// <summary>
        /// 注册事件
        /// </summary>
        public const string EventSubscribe = "EVENT_SUBSCRIBE";

        /// <summary>
        /// 自定义事件
        /// </summary>
        public const string Custom = "CUSTOM";

        /// <summary>
        /// 上传地理位置
        /// </summary>
        public const string EventLocation = "EVENT_LOCATION";

        #region GetMessageType
        /// <summary>
        /// 获取微信推送消息的类型
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string GetMessageType( string xml )
        {
            var doc = XDocument.Parse( xml );
            var root = doc.Element( "xml" );
            string messageType = root.Element( "MsgType" ).Value;

            if( messageType.ToUpper() == WechatMessageType.Event )
            {
                string e = root.Element( "Event" ).Value;

                messageType = messageType + "_" + e;
            }

            return messageType.ToUpper();
        }
        #endregion

    
    }
}
