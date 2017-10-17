using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpConsoleApp
{
    /// <summary>
    /// 线程
    /// </summary>
    public class ThreadTest
    {
        static void Test()
        {
            Thread t = new Thread(T);
            t.Start("abc");

            Thread t2 = new Thread(T);
            t2.Start("def");

            Thread t3 = new Thread(First);
            t3.Start("T - F");

            ConsoleKeyInfo k = Console.ReadKey();
        }

        static void T(object o)
        {
            Console.WriteLine(o);
            for (int i = 1; i <= 5; i++)
            {
                Console.Write(Thread.CurrentThread.ManagedThreadId);
                Console.Write(":");
                Console.WriteLine(i);
                //Thread.Sleep(1000);
            }
        }

        static void First(object o)
        {
            Thread t = new Thread(Second);
            t.Start("T - S");

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(t.ThreadState.ToString());
                Thread.Sleep(1000);
            }
        }

        static void Second(object o)
        {
            Thread.Sleep(5000);
        }
    }
}
