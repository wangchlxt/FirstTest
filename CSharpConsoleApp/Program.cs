using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CSharpConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //EFTest.Test();
            int oid = 102;

            string str = $"<a href='http://cm.quectel-service.com/Admin/OrderDetail/{oid}'>点击查看订单详情</a>";
            str += $"<a href='http://cm.quectel-service.com/Admin/OrderDetail/{oid}'>点击查看订单详情</a>";

            Console.Write(str);

            Console.Read();
        }
    }
}
