using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpUtils;

namespace Wechat.Util
{
    public class WxPayJsConfig
    {
        public bool debug { get; set; }
        public string appId { get; set; }
        public string timestamp { get; set; }
        public string nonceStr { get; set; }

        public WxPayJsConfig()
        {
            debug = bool.Parse(ConfigurationManager.AppSettings["JsDebug"]);
            appId = ConfigurationManager.AppSettings["AppId"];
            timestamp = DateTimeUtil.GetTimestamp().ToString();
            nonceStr = GenerateNonceStr();
        }

        public static string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        public string GetSignature(string url)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("noncestr",nonceStr);
            dic.Add("jsapi_ticket",WechatApi.GetAccessTokenAndRefresh());
            dic.Add("timestamp", timestamp);
            dic.Add("url", url);

            Dictionary<string, string> sortDic = dic.OrderBy(e => e.Key).ToDictionary(k => k.Key, v => v.Value);
            StringBuilder sb = new StringBuilder();
            foreach(KeyValuePair<string,string> pair in sortDic)
            {
                sb.Append(pair.Key);
                sb.Append("=");
                sb.Append(pair.Value);
                sb.Append("&");
            }
            sb.Remove(sb.Length - 1, 1);

            string sign = StringCoding.EncodeSHA1(sb.ToString()).ToLower();

            LogHelper.Info(String.Format("WxPayJsConfig GetSignature：{0} -> {1}", sb.ToString(), sign));

            return sign;
        }
    }
}
