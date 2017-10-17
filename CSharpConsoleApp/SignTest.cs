using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpUtils;

namespace CSharpConsoleApp
{
    /// <summary>
    /// 签名
    /// </summary>
    public class SignTest
    {
        public static void Test()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("jsapi_ticket", "sM4AOVdWfPE4DxkXGEs8VMCPGGVi4C3VM0P37wVUCFvkVAy_90u5h9nbSlYy3-Sl-HhTdfl2fzFy1AOcHKP7qg");
            dic.Add("timestamp", "1414587457");
            dic.Add("url", "http://mp.weixin.qq.com?params=value");
            dic.Add("noncestr", "Wm3WZYTPz0wzccnW");
            
            Dictionary<string, string> sortDic = dic.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);

            StringBuilder sb = new StringBuilder();
            foreach(KeyValuePair<string,string> pair in sortDic)
            {
                sb.Append(pair.Key);
                sb.Append("=");
                sb.Append(pair.Value);
                sb.Append("&");
            }
            sb.Remove(sb.Length - 1, 1);

            Console.WriteLine(sb.ToString());

            string codeString = StringCoding.EncodeSHA1(sb.ToString());
            Console.WriteLine(codeString);
        }
    }
}
