﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSharpWebMvc.Filter
{
    public class FirstAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(filterContext.HttpContext.Request.QueryString["a"] == "abc")
            {
                filterContext.Result = new RedirectResult("/Result/Index?msg=abc");
            }
        }
    }
}