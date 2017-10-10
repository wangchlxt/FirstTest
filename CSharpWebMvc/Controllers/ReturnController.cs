using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSharpWebMvc.Controllers
{
    public class ReturnController : Controller
    {
        // GET: Return
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ToBaidu()
        {
            return Redirect("http://www.baidu.com");
        }

        public ActionResult ToWechatPay()
        {
            return Redirect("");
        }

        public ActionResult ReturnImage()
        {
            return File("..\\Content\\image\\20170912094052172CastlePointLH_ZH-CN13074557115_1920x1080.jpg", "image/JPEG");
        }
    }
}