using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CSharpUtils;


namespace Wechat.Util
{
    public static class WechatUtility
    {
        #region Sign
        /// <summary>
        /// 对时间戳和随机数签名
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public static string Sign( string timestamp, string nonce )
        {
            string token = ConfigurationManager.AppSettings["WechatToken"];

            string[] tmpArr = { token, timestamp, nonce };

            Array.Sort( tmpArr );

            string tmpStr = string.Join( "", tmpArr );

            return StringCoding.EncodeSHA1(tmpStr).ToLower();
        }
        #endregion

        #region CheckSign
        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// <param name="signature">签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <returns></returns>
        public static bool CheckSign( string signature, string timestamp, string nonce )
        {
            return Sign( timestamp, nonce ) == signature;
        }
        #endregion
    }
}
