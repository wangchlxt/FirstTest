using Newtonsoft.Json;
using Wechat.Util.Messages.WechatApi.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wechat.Util.Messages.TemplateMessage
{
    public class RemainingSumChangeMessage : CustomMessage
    {
        public RemainingSumChangeMessage()
        {
            this.Data = new RemainingSumChangeData();
        }

        [JsonProperty("touser")]
        public string ToUser { get; set; }

        [JsonProperty("template_id")]
        public string TemplateId { get { return "Gwabhn-GB0Rih_Av1Aaxp-L-qod60lj4rMFYLwHB_ls"; } }

        [JsonProperty("data")]
        public RemainingSumChangeData Data { get; set; }
        
    }

    public class RemainingSumChangeData
    {
        public RemainingSumChangeData()
        {
            first = new CustemTemplateMessageValue();
            accountType = new CustemTemplateMessageValue();
            account = new CustemTemplateMessageValue();
            amount = new CustemTemplateMessageValue();
            result = new CustemTemplateMessageValue();
            remark = new CustemTemplateMessageValue();
        }

        public CustemTemplateMessageValue first { get; set; }
        public CustemTemplateMessageValue accountType { get; set; }
        public CustemTemplateMessageValue account { get; set; }
        public CustemTemplateMessageValue amount { get; set; }
        public CustemTemplateMessageValue result { get; set; }
        public CustemTemplateMessageValue remark { get; set; }
    }
}
