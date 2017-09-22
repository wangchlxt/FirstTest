using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSharpUtils;

namespace CSharpWebMvc.Filter
{
    public class MyActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            LogHelper.Info(String.Format("ActionFilter:OnActionExecuted {0}", filterContext.HttpContext.Request.Url));
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LogHelper.Info(String.Format("ActionFilter:OnActionExecuting {0}", filterContext.HttpContext.Request.Url));
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            LogHelper.Info(String.Format("ActionFilter:OnResultExecuted {0}", filterContext.HttpContext.Request.Url));
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            LogHelper.Info(String.Format("ActionFilter:OnResultExecuting {0}", filterContext.HttpContext.Request.Url));
        }
    }
}