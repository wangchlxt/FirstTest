using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CSharpUtils
{
    public class RequestUtil
    {
        public static string GetUserHostString(HttpRequestBase request)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("RequestType：");
            sb.AppendLine(request.RequestType);
            sb.Append("UserHostName：");
            sb.AppendLine(request.UserHostName);
            sb.Append("UserHostAddress：");
            sb.AppendLine(request.UserHostAddress);

            sb.Append("UserLanguages：");
            string[] languages = request.UserLanguages;
            foreach(string lang in languages)
            {
                sb.Append(lang);
            }
            sb.AppendLine("");

            return sb.ToString();
        }
    }
}
