using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSharpWebMvc.Filter;

namespace CSharpWebMvc.Controllers
{
    public class SequenceController : SequenceBaseController
    {
        [MyActionFilter]
        // GET: Sequence
        public ActionResult Index()
        {
            return View();
        }

        [MyActionFilter]
        public ActionResult GetCode()
        {
            ViewBag.code = code;
            return View();
        }
    }
}