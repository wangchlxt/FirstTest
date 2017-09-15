using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpConsoleApp
{
    public class StringTest
    {
        public static void Test()
        {
            string one = "110";
            string two = "11010";

            Console.WriteLine(String.Format("{0} - {1} {2}", two, one, two.StartsWith(one)));
            Console.WriteLine(String.Format("{0} - {1} {2}", one, two, one.StartsWith(two)));
            Console.WriteLine(String.Format("{0} - 10 {1}", two, two.StartsWith("10")));
            Console.Read();
        }
    }
}
