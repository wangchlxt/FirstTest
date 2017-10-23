using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpConsoleApp
{
    public class EFTest
    {
        public class User
        {
            public int id { get; set; }
            public virtual string name { get; set; }
            public virtual string buyer { get; set; }
            public virtual string idArr { get; set; }
        }

        public static void Test()
        {
            GroupCountTest();
        }

        /// <summary>
        /// 分组统计
        /// </summary>
        public static void GroupCountTest()
        {
            List<User> list = new List<User>()
            {
                new User(){id=1001     ,name="语文",buyer="A"},
                new User(){id=1001     ,name="语文",buyer="A"},
                new User(){id=1001     ,name="数学",buyer="A"},
                new User(){id=1001     ,name="数学",buyer="B"},
                new User(){id=1001     ,name="数学",buyer="B"},
                new User(){id=1002     ,name="英语",buyer="C"},
                new User(){id=1005     ,name="英语",buyer="A"},
                new User(){id=1005     ,name="英语",buyer="A"},
                new User(){id=1005     ,name="英语",buyer="B"},
                new User(){id=1004     ,name="计算机",buyer="A"},
                new User(){id=1007     ,name="计算机",buyer="C"}
            };

            var count = list.GroupBy(e => e.name).Select(e => new {
                name=e.Key,
                优秀=e.Count(o=>o.buyer=="A"),
                普通=e.Count(o=>o.buyer=="B"),
                差=e.Count(o=>o.buyer=="C")
            });

            foreach(var item in count)
            {
                Console.WriteLine(String.Format("{0} 优秀：{1} 普通：{2} 差：{3}",
                    item.name, item.优秀, item.普通, item.差));
            }
        }
    }
}
