using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wechat.Util;
using CSharpWebMvc.Models;

namespace CSharpWebMvc.Controllers
{
    public class RequestController : Controller
    {
        // GET: Request
        public ActionResult Index()
        {
            ViewBag.data = Request.Params.ToString();
            return View();
        }

        public ActionResult Param()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult DoParamByGet()
        {
            Dictionary<string, string> param = new Dictionary<string, string>();

            NameValueCollection nv = Request.QueryString;

            foreach (string key in nv.AllKeys)
            {
                param.Add(key, nv[key]);
            }

            param.Remove("sex");

            ViewBag.param = param;
            return View();
        }

        [HttpPost]
        public ActionResult DoParamByPost()
        {
            Dictionary<string, string> param = new Dictionary<string, string>();

            NameValueCollection nv = Request.Form;

            foreach (string key in nv.AllKeys)
            {
                param.Add(key, nv[key]);
            }

            param.Remove("sex");

            ViewBag.param = param;
            return View();
        }

        public ActionResult DoXml()
        {
            WxPayData data = new WxPayData();
            data.SetValue("return_code", "SUCCESS");
            data.SetValue("result_code", "SUCCESS");

            string txt = data.ToXml();

            // 无效
            //Response.Write(data.ToXml());
            //Response.End();

            return File(System.Text.Encoding.Default.GetBytes(txt), "text/xml");
        }

        public ActionResult ShowQueryString()
        {
            return View();
        }

        public ActionResult To(int? id)
        {
            return View("ToId");
        }
        
        [HttpPost]
        public ActionResult To(UserModels user)
        {
            return View("ToName");
        }

    }
}