using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CSharpUtils;

namespace CSharpConsoleApp
{
    public class CodingStringTest
    {
        public static void Test()
        {
            string sourceString = "中国";
            Console.WriteLine(StringCoding.EncodeMD5(sourceString));
            Console.WriteLine(StringCoding.EncodeMD5Encrypt16(sourceString));
            Console.WriteLine(StringCoding.EncodeMD5Encrypt32(sourceString));

            Console.Read();
        }



    }
}
