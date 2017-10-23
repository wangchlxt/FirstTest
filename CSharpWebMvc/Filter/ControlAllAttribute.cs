using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSharpWebMvc.Filter
{
    /// <summary>
    /// control filter，放在 control 类上
    /// </summary>
    public class ControlAllAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.QueryString["e"] == "400")
            {
                filterContext.Result = new RedirectResult("/Result/Index?msg=error_400");
            }
        }
    }
}
