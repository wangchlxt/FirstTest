using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Wechat.Util.Messages.WechatApi.Custom
{
    public class CustemTemplateMessage:CustomMessage
    {

        [JsonProperty("touser")]
        public string ToUser { get; set; }

        [JsonProperty("template_id")]
        public string TemplateId { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("topcolor")]
        public string TopColor { get; set; }

        [JsonProperty("data")]
        public TemplateMessageData Data { get; set; }

        public CustemTemplateMessage()
        {
            this.Data = new TemplateMessageData();
        }
    }

    public class TemplateMessageData
    {
        public TemplateMessageData() {
            this.First = new CustemTemplateMessageValue();
            this.Keyword1 = new CustemTemplateMessageValue();
            this.Keyword2 = new CustemTemplateMessageValue();
            this.Keyword3 = new CustemTemplateMessageValue();
        }
        /// <summary>
        /// 标题
        /// </summary>
        [JsonProperty("first")]
        public CustemTemplateMessageValue First { get; set; }

        /// <summary>
        /// 待办内容
        /// </summary>
        [JsonProperty("keyword1")]
        public CustemTemplateMessageValue Keyword1 { get; set; }

        /// <summary>
        /// 待办事件
        /// </summary>
        [JsonProperty("keyword2")]
        public CustemTemplateMessageValue Keyword2 { get; set; }

        /// <summary>
        /// 待办事件
        /// </summary>
        [JsonProperty("keyword3")]
        public CustemTemplateMessageValue Keyword3 { get; set; }

        /// <summary>
        /// 详情
        /// </summary>
        [JsonProperty("remark")]
        public CustemTemplateMessageValue Remark { get; set; }


    }

    public class CustemTemplateMessageValue
    {
        public CustemTemplateMessageValue()
        {
        }

        public CustemTemplateMessageValue(string value,string color=null) {
            this.Value = value;
            if (!string.IsNullOrEmpty(color))
            {
                this.Color = color;
            }
        }

          [JsonProperty("value")]
        public string Value { get; set; }
          [JsonProperty("color")]
        public string Color { get; set; }
    }


}