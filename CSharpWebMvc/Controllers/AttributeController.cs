using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSharpWebMvc.Filter;
using System.Text;

namespace CSharpWebMvc.Controllers
{
    [ControlAll]
    public class AttributeController : Controller
    {
        [First]
        [Second]
        public ActionResult Index()
        {
            StringBuilder sb = new StringBuilder();
            foreach(string key in Request.QueryString.AllKeys)
            {
                sb.Append(String.Format("<div>{0}={1}</div>", key, Request.QueryString[key]));
            }

            ViewBag.QueryString = sb.ToString();

            return View();
        }

        [First]
        public ActionResult First()
        {
            return View();
        }

        [Second]
        public ActionResult Second()
        {
            return View();
        }
    }
}