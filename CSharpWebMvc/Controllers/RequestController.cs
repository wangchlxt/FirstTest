using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}