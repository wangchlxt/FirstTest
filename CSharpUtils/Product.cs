using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpUtils
{
   public class Product
    {
        /// <summary>
        /// 运营商编码
        /// </summary>
        public string agent_no;


        /// <summary>
        /// 卡类型编码
        /// </summary>
        public string card_type_no;

        /// <summary>
        /// 套餐编码
        /// </summary>
        public string meal_no;

        /// <summary>
        /// 运营商
        /// </summary>
        public string agent;

        /// <summary>
        /// 卡类型
        /// </summary>
        public string card_type;

        /// <summary>
        /// 套餐
        /// </summary>
        public string meal;

        /// <summary>
        /// 套餐id
        /// </summary>
        public int meal_id;

        /// <summary>
        /// 运营商
        /// </summary>
        public static Dictionary<string, string> g_dic_agent = new Dictionary<string, string>
        {
            {"001","中国移动" },
            {"002","中国联通" },
            {"003","中国电信" }
        };

        /// <summary>
        /// 卡类型
        /// </summary>
        public static Dictionary<string, string> g_dic_card_type = new Dictionary<string, string>
        {
            {"11", "标准卡"},
            {"12", "Micro卡"},
            {"13", "Nano卡"},
            {"14", "STD/Micro双切卡"},
            {"21", "MS0芯片"},
            {"22", "MS1芯片"},
            {"33", "车规卡"},
            {"44", "软卡"}
        };

        /// <summary>
        /// 套餐
        /// </summary>
        public static Dictionary<string, string> g_dic_meal = new Dictionary<string, string>
        {
            {"01","3MB/月" },
            {"21","5MB/月" },
            {"22","10MB/月" },
            {"23","20MB/月" },
            {"24","30MB/月" },
            {"25","50MB/月" },
            {"26","70MB/月" },
            {"27","100MB/月" },
            {"28","150MB/月" },
            {"29","300MB/月" },
            {"30","500MB/月" },
            {"51","1GB/月" },
            {"52","2GB/月" },
            {"53","3GB/月" }
        };

        /// <summary>
        /// 套餐流量，单位 MB
        /// </summary>
        public static Dictionary<string, int> g_dic_meal_flow = new Dictionary<string, int>()
        {
            {"01",3 },
            {"21",5 },
            {"22",10 },
            {"23",20 },
            {"24",30 },
            {"25",50 },
            {"26",70 },
            {"27",100 },
            {"28",150 },
            {"29",300 },
            {"30",500 },
            {"51",1024 },
            {"52",2048 },
            {"53",3072 }
        };

        public static Dictionary<string, int> g_dic_meal_id = new Dictionary<string, int>()
        {
            {"01",1 },
            {"21",2 },
            {"22",3 },
            {"23",4 },
            {"24",5 },
            {"25",6 },
            {"26",7 },
            {"27",8 },
            {"28",9 },
            {"29",10 },
            {"30",11 },
            {"51",12 },
            {"52",13 },
            {"53",14 }
        };

        /// <summary>
        /// 获取运营商
        /// </summary>
        /// <param name="no">运营商编号</param>
        /// <returns></returns>
        public static string GetAgent(string no)
        {
            if (g_dic_agent.Keys.Contains(no))
            {
                return g_dic_agent[no];
            }

            return "";
        }

        /// <summary>
        /// 获取卡类型
        /// </summary>
        /// <param name="no">卡类型编号</param>
        /// <returns></returns>
        public static string GetCardType(string no)
        {
            if (g_dic_card_type.Keys.Contains(no))
            {
                return g_dic_card_type[no];
            }

            return "";
        }

        /// <summary>
        /// 获取套餐
        /// </summary>
        /// <param name="no">套餐编号</param>
        /// <returns></returns>
        public static string GetMeal(string no)
        {
            if (g_dic_meal.Keys.Contains(no))
            {
                return g_dic_meal[no];
            }

            return "";
        }

        /// <summary>
        /// 获取套餐流量
        /// </summary>
        /// <param name="no">套餐编号</param>
        /// <returns></returns>
        public static int GetMealFlow(string no)
        {
            if (g_dic_meal_flow.Keys.Contains(no))
            {
                return g_dic_meal_flow[no];
            }

            return 0;
        }

        /// <summary>
        /// 获取套餐id
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public static int GetMealId(string no)
        {
            if (g_dic_meal_id.Keys.Contains(no))
            {
                return g_dic_meal_id[no];
            }

            return 0;
        }

        /// <summary>
        /// 解析产品编码
        /// </summary>
        /// <param name="product_code">产品编码</param>
        /// <returns>产品信息</returns>
        public static Product Parse(string product_code)
        {
            Product pro = new Product();

            string[] arr = product_code.Split('-');
            if (arr.Length == 4)
            {
                pro.agent_no = arr[1];
                pro.card_type_no = arr[2];
                pro.meal_no = arr[3];

                pro.agent = GetAgent(arr[1]);
                pro.card_type = GetCardType(arr[2]);
                pro.meal = GetMeal(arr[3]);
                pro.meal_id = GetMealId(arr[3]);

            }

            return pro;
        }

    }
}
