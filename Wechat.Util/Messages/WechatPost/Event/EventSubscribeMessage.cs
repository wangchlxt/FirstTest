using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Wechat.Util.Messages.WechatPost.Event
{
    public class EventSubscribeMessage
    {
        #region Properties
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 发送方 OpenId
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息创建时间（时间戳）
        /// </summary>
        public int CreateTime { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// 事件 KEY 值
        /// </summary>
        public string EventKey { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xml"></param>
        public EventSubscribeMessage(string xml)
        {
            var doc = XDocument.Parse( xml );

            var root = doc.Element( "xml" );

            this.ToUserName = root.Element( "ToUserName" ).Value;
            this.FromUserName = root.Element( "FromUserName" ).Value;
            this.CreateTime = Convert.ToInt32( root.Element( "CreateTime" ).Value );
            this.MsgType = root.Element( "MsgType" ).Value;
            this.Event = root.Element( "Event" ).Value;
            this.EventKey = root.Element( "EventKey" ).Value;
        }
        #endregion
    }
}
