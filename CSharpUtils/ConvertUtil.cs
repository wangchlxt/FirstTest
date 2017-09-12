using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpUtils
{
    public class ConvertUtil
    {
        /// <summary>
        /// 转换编码字符串
        /// </summary>
        /// <param name="codingString"></param>
        /// <returns></returns>
        public static Encoding ConvertEncodingString(string codingString)
        {
            codingString = codingString.ToUpper();

            if (codingString == "ASCII")
            {
                return Encoding.ASCII;
            }
            else if (codingString == "BIGENDIANUNICODE")
            {
                return Encoding.BigEndianUnicode;
            }
            else if (codingString == "DEFAULT")
            {
                return Encoding.Default;
            }
            else if (codingString == "UNICODE")
            {
                return Encoding.Unicode;
            }
            else if (codingString == "UTF32")
            {
                return Encoding.UTF32;
            }
            else if (codingString == "UTF7")
            {
                return Encoding.UTF7;
            }
            else if (codingString == "UTF8")
            {
                return Encoding.UTF8;
            }

            return Encoding.Default;
        }
    }
}
