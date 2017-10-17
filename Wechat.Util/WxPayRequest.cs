using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpUtils;

namespace Wechat.Util
{
    public class WxPayRequest
    {
        public string appId { get; set; }
        public string timestamp { get; set; }
        public string nonceStr { get; set; }
        public string package { get; set; }
        public string signType { get; set; }
        public string paySign { get; set; }
        public string prepayId { get; set; }

        public WxPayRequest(string prepay_id)
        {
            appId = ConfigurationManager.AppSettings["AppId"];
            timestamp = DateTimeUtil.GetTimestamp().ToString();
            nonceStr = WxPayJsConfig.GenerateNonceStr();
            package = "prepay_id=" + prepay_id;
            prepayId = prepay_id;
            signType = "MD5";
            paySign = Sign();
        }

        private string Sign()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("appId", appId);
            dic.Add("timeStamp", timestamp);
            dic.Add("nonceStr", nonceStr);
            dic.Add("package", package);
            dic.Add("signType", signType);

            Dictionary<string, string> sortDic = dic.OrderBy(e => e.Key).ToDictionary(k => k.Key, v => v.Value);
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in sortDic)
            {
                sb.Append(pair.Key);
                sb.Append("=");
                sb.Append(pair.Value);
                sb.Append("&");
            }
            sb.Append("key=");
            sb.Append(ConfigurationManager.AppSettings["WechatPayApiSecret"]);

            string sign = StringCoding.EncodeMD5(sb.ToString()).ToUpper();

            LogHelper.Info(String.Format("WxPayRequest Sign ：{0} -> {1}", sb.ToString(), sign));

            return sign;
        }
    }
}
