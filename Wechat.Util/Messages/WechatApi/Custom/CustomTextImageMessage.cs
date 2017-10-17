using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wechat.Util.Messages.WechatApi.Custom
{
    public class Article
    {
        [JsonProperty( "title" )]
        public string Title { get; set; }

        [JsonProperty( "description" )]
        public string Description { get; set; }

        [JsonProperty( "url" )]
        public string Url { get; set; }

        [JsonProperty( "picurl" )]
        public string PicUrl { get; set; }
    }

    public class News
    {
        [JsonProperty( "articles" )]
        public List<Article> Articles { get; private set; }

        public News()
        {
            this.Articles = new List<Article>();
        }
    }

    /// <summary>
    /// 图文消息
    /// </summary>
    public class CustomTextImageMessage : CustomMessage
    {
        [JsonProperty( "touser" )]
        public string ToUser { get; set; }

        [JsonProperty( "msgtype" )]
        public string MsgType { get { return "news"; } }

        [JsonProperty( "news" )]
        public News News { get; set; }

        public CustomTextImageMessage()
        {
            this.News = new News();
        }
    }
}
