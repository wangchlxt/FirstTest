using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSharpUtils;
using CSharpWebMvc.Filter;

namespace CSharpWebMvc.Controllers
{
    [MyActionFilter]
    public class FileController : Controller
    {
        // GET: File
        public ActionResult Index()
        {
            return View();
        }
        
        
        public ActionResult MP_verify_itgZgT1T3iVkkTxg()
        {
            LogHelper.Info("MP_verify_itgZgT1T3iVkkTxg");
            return Content("itgZgT1T3iVkkTxg");
        }
    }
}