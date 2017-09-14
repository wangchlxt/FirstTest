using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpConsoleApp
{
    /// <summary>
    /// 线程池测试
    /// </summary>
    public class ThreadToolTest
    {
        /// <summary>
        /// 加入线程池的任务的执行顺序并不一定是加入的顺序
        /// </summary>
        public static void Test()
        {
            int workerThreads = 0;
            int completionPortThreads = 0;

            ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine(String.Format("线程池默认最小线程数：{0} - {1}", workerThreads, completionPortThreads));

            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine(String.Format("线程池默认最大线程数：{0} - {1}", workerThreads, completionPortThreads));

            ThreadPool.SetMinThreads(5, 5);
            ThreadPool.SetMaxThreads(12, 12);

            Stopwatch watch = new Stopwatch();
            watch.Start();

            WaitCallback callback = index =>
            {
                Console.WriteLine(String.Format("{0}：Task {1} started", watch.Elapsed, index));
                Thread.Sleep(10000);
                Console.WriteLine(String.Format("{0}：Task {1} finished", watch.Elapsed, index));
            };

            for(int i=0;i<20;i++)
            {
                ThreadPool.QueueUserWorkItem(callback, i);
            }

            Thread thread = new Thread(ScanThreadTool);
            thread.Start();

            Console.ReadKey();
        }
        
        public static void ScanThreadTool(object data)
        {
            int workerThreads = 0;
            int completionPortThreads = 0;

            while(true)
            {
                ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
                Console.WriteLine(String.Format("{0} 线程池当前空闲线程数：{1} - {2}", DateTime.Now, workerThreads, completionPortThreads));
                
                Thread.Sleep(3000);
            }
        }
    }
}
