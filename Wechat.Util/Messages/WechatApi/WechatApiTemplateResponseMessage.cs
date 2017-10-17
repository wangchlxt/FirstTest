using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wechat.Util.Messages.WechatApi
{
    /// <summary>
    /// 微信 API 相应消息消息
    /// </summary>
    public class WechatApiTemplateResponseMessage : WechatApiResponseMessage
    {
        [JsonProperty( "msgid" )]
        public int? MsgId { get; set; }

    }
}
