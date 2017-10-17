using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wechat.Util
{
    public class WechatUri
    {
        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        /// 随机数
        /// </summary>
        public string Nonce { get; set; }

        /// <summary>
        /// 回显字符串
        /// </summary>
        public string Echostr { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat( "Signature:{0}, ", this.Signature );
            builder.AppendFormat( "Timestamp:{0}, ", this.Timestamp );
            builder.AppendFormat( "Nonce:{0}, ", this.Nonce );
            builder.AppendFormat( "Echostr:{0}", this.Echostr );

            return builder.ToString();
        }
    }
}
