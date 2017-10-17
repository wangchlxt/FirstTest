using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wechat.Util.Messages.WechatApi
{
    public class UserAccessTokenResponseMessage : WechatApiResponseMessage
    {
        [JsonProperty( "access_token" )]
        public string AccessToken { get; set; }

        [JsonProperty( "expires_in" )]
        public int ExpiresIn { get; set; }

        [JsonProperty( "refresh_token" )]
        public string RefreshToken { get; set; }

        [JsonProperty( "openid" )]
        public string OpenId { get; set; }

        [JsonProperty( "scope" )]
        public string Scope { get; set; }

        [JsonProperty( "unionid" )]
        public string UnionId { get; set; }
    }
}
