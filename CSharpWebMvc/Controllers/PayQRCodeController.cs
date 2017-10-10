using CSharpUtils;
using CSharpWebMvc.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CSharpWebMvc.Controllers
{
    public class PayQRCodeController : Controller
    {
        // GET: PayQRCode
        public ActionResult Index()
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> pair in Request.QueryString)
            {
                sb.Append(pair.Key);
                sb.Append("=");
                sb.Append(pair.Value);
                sb.Append("&");
            }

            LogHelper.Info(String.Format("Pay ->\r\n url:{0}\r\n UserAgent:{1}\r\n Param:{2}",
                Request.Url,
                Request.UserAgent,
                sb.ToString()));
            
            if (Request.UserAgent.IndexOf("AlipayClient") > 0)
            {
                LogHelper.Info("支付宝支付");
                return Redirect("HTTPS://QR.ALIPAY.COM/FKX00658OYYACI96HEXGC8");
            }
            else if (Request.UserAgent.IndexOf("MicroMessenger") > 0)
            {
                LogHelper.Info("微信支付");
                ViewBag.type = "wechat";
            }

            return View();
        }
    }
}