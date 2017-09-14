using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CSharpUtils.Net
{
    public class DnsUtil
    {
        public string GetIpByUrl(string url)
        {
            IPHostEntry host = Dns.GetHostEntry(url);
            return host.AddressList[0].ToString();
        }
    }
}
