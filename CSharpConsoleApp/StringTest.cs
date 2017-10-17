using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpUtils;

namespace CSharpConsoleApp
{
    /// <summary>
    /// 字符串
    /// </summary>
    public class StringTest
    {
        public static void Test()
        {
            string one = "110";
            string two = "11010";

            Console.WriteLine(String.Format("{0} - {1} {2}", two, one, two.StartsWith(one)));
            Console.WriteLine(String.Format("{0} - {1} {2}", one, two, one.StartsWith(two)));
            Console.WriteLine(String.Format("{0} - 10 {1}", two, two.StartsWith("10")));

            Console.WriteLine("随机字符串：");
            Console.WriteLine(StringUtil.GenerateRandomStr32());
            Console.WriteLine(StringUtil.GenerateRandomStr(DateTime.Now.ToString()));
        }

        public static void GenerateStringTest()
        {
            for(int i=0;i<10;i++)
            {
                Console.WriteLine(StringUtil.GenerateRandomStr32());
            }

            Console.WriteLine();

            for(int i=0;i<10;i++)
            {
                Console.WriteLine(StringUtil.GenerateRandomStr24());
            }
        }
    }
}
