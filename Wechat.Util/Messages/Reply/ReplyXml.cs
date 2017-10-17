using System;
using System.Configuration;
using System.Text;

namespace Wechat.Util.Messages.Reply
{
    public class ReplyXml
    {
        public static long ConvertDateTimeToInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }

        public static string Text(string openID,string content)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<xml>");

            sb.Append("<ToUserName><![CDATA[");
            sb.Append(openID);
            sb.AppendLine("]]></ToUserName>");

            sb.Append("<FromUserName><![CDATA[");
            sb.Append(ConfigurationManager.AppSettings["WechatUserId"]);
            sb.AppendLine("]]></FromUserName>");

            sb.Append("<CreateTime>");
            sb.Append(ConvertDateTimeToInt(DateTime.Now));
            sb.AppendLine("</CreateTime>");

            sb.AppendLine("<MsgType><![CDATA[text]]></MsgType>");

            sb.Append("<Content><![CDATA[");
            sb.Append(content);
            sb.AppendLine("]]></Content>");

            sb.AppendLine("</xml>");

            return sb.ToString();
        }
    }
}
