using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSharpUtils;

namespace ToolFairy
{
    static class Program
    {
        static Manager manager = new Manager();

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            DateTime beginTime = DateTime.Parse("2017/9/5 16:27:43");
            string str = beginTime.ToShortDateString();
            string str2 = DateTime.Now.ToShortDateString();

            manager.Run();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow_Form());
        }
    }
}
