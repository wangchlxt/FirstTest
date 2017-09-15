using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpConsoleApp
{
    public class ParallelTest
    {
        public static void Test()
        {
            Console.WriteLine("Parallel For 执行效果：");
            Parallel.For(0, 30, Work);

            Console.WriteLine("\r\nfor 循环执行效果：");
            
            for(int i=0;i<30;i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine(String.Format("ThreadId - {0}  index - {1}", Thread.CurrentThread.ManagedThreadId, i));
            }

            Console.Read();
        }

        public static void Work(int index)
        {
            Thread.Sleep(1000);
            Console.WriteLine(String.Format("TaskId - {0}  ThreadId - {1}  index - {2}", Task.CurrentId, Thread.CurrentThread.ManagedThreadId, index));
        }
    }
}
