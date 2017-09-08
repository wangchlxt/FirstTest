using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CSharpUtils
{
    /// <summary>
    /// 常量
    /// </summary>
    public class Constant
    {
        public static string GetCarrierName(string carrierCode)
        {
            switch (carrierCode)
            { 
                case "01": return "中国移动";
                case "02": return "中国联通";
                case "03": return "中国电信";
                default: return string.Empty;
            }
        }

        public static string GetCarrierCode(string carrierName)
        {
            switch (carrierName)
            {
                case "中国移动": return "01";
                case "中国联通": return "02";
                case "中国电信": return "03";
                default: return string.Empty;
            }
        }


    }
}
