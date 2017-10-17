using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wechat.Util.Messages.WechatApi
{
    public class AccessTokenResponseMessage : WechatApiResponseMessage
    {
        [JsonProperty( "access_token" )]
        public string AccessToken { get; set; }

        [JsonProperty( "expires_in" )]
        public int ExpiresIn { get; set; }
    }
}
