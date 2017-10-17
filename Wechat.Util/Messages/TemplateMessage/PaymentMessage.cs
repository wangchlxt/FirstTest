using Newtonsoft.Json;
using Wechat.Util.Messages.WechatApi.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wechat.Util.Messages.TemplateMessage
{
    public class PaymentMessage: CustomMessage
    {
        public PaymentMessage()
        {
            this.Data = new PaymentData();
        }

        [JsonProperty("touser")]
        public string ToUser { get; set; }

        [JsonProperty("template_id")]
        public string TemplateId { get { return "XEqLsvFuQ3qP9YYDq_uHjZtlNewqPAToOAp-qrEG3wI"; } }

        [JsonProperty("data")]
        public PaymentData Data { get; set; }
    }

    public class PaymentData
    {
        public PaymentData()
        {
            first = new CustemTemplateMessageValue();
            keyword1 = new CustemTemplateMessageValue();
            keyword2 = new CustemTemplateMessageValue();
            keyword3 = new CustemTemplateMessageValue();
            keyword4 = new CustemTemplateMessageValue();
            remark = new CustemTemplateMessageValue();
        }

        public CustemTemplateMessageValue first { get; set; }
        public CustemTemplateMessageValue keyword1 { get; set; }
        public CustemTemplateMessageValue keyword2 { get; set; }
        public CustemTemplateMessageValue keyword3 { get; set; }
        public CustemTemplateMessageValue keyword4 { get; set; }
        public CustemTemplateMessageValue remark { get; set; }
    }
}
