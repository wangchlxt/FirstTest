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
    public class WechatApiResponseMessage
    {
        [JsonProperty( "errcode" )]
        public int? ErrorCode { get; set; }

        [JsonProperty( "errmsg" )]
        public string ErrorMessage { get; set; }
    }
}
