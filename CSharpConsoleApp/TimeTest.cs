using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpConsoleApp
{
    public class TimeTest
    {
        public static void Test()
        {
            DateTime dt = new DateTime(2017, 9, 19, 9, 30, 0);
            DateTime dt2 = DateTime.Now;
            TimeSpan ts = dt2 - dt;

            Console.WriteLine(dt2);
            Console.WriteLine(dt);
            Console.WriteLine(ts);
            Console.WriteLine("相差总分钟数："+ts.TotalMinutes.ToString());

            Console.Read();
        }

    }
}
