using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSharpUtils;

namespace CSharpWebMvc.Controllers
{
    public class SequenceBaseController : Controller
    {
        public string code
        {
            get
            {
                string strCode = StringUtil.GenerateRandomStr();
                LogHelper.Info(String.Format("SequenceBaseController get code：{0}", strCode));
                return strCode;
            }
        }
    }
}