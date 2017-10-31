using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpUtils;
using System.Threading;

namespace ToolFairy
{
    public class Manager
    {
        public Thread bingThread { get; set; }
        
        public Manager()
        {
            bingThread = new Thread(Bing.WorkThread);
        }
        

        public void Run()
        {
            LogHelper.Info("Manager Run");

            bingThread.Start();
        }
        
    }
}
