using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wechat.Util.Messages.WechatApi
{
    public class CallbackIpResponseMessage : WechatApiResponseMessage
    {
        [JsonProperty( "ip_list" )]
        public List<string> IpList { get; set; }
    }
}
