using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wechat.Util
{
    public class WechatApiInvokeException : Exception
    {
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public string Url { get; set; }

        public WechatApiInvokeException( int errorCode, string errorMessage, string url = null )
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
            this.Url = url;
        }

        public override string Message
        {
            get
            {
                return string.Format( "调用微信接口时返回错误，错误码：{0}，错误信息“{1}，接口地址：{2}", this.ErrorCode, this.ErrorMessage, this.Url );
            }
        }
    }
}
