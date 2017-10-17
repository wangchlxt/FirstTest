using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wechat.Util.Messages.WechatApi.Custom
{
    public class CustomMessageTextInnerText
    {
        [JsonProperty( "content" )]
        public string Content { get; set; }
    }

    public class CustomTextMessage : CustomMessage
    {
        [JsonProperty( "touser" )]
        public string ToUser { get; set; }

        [JsonProperty( "msgtype" )]
        public string MsgType { get { return "text"; } }

        [JsonProperty( "text" )]
        public CustomMessageTextInnerText Text
        {
            get;
            set;
        }

        public CustomTextMessage()
        {
            this.Text = new CustomMessageTextInnerText();
        }
    }
}
