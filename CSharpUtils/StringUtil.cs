using System;
using System.Reflection;
using System.Text;

namespace CSharpUtils
{
    public class StringUtil
    {
        /// <summary>
        /// 删除不可见字符
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public static string DeleteUnVisibleChar(string sourceString)
        {
            var sBuilder = new StringBuilder(131);
            for (int i = 0; i < sourceString.Length; i++)
            {
                int Unicode = sourceString[i];
                if (Unicode >= 16)
                {
                    sBuilder.Append(sourceString[i].ToString());
                }
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// 将实体类属性和值转为字符串
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public static string ParseString<T>(T model)
        {
            StringBuilder sb = new StringBuilder();

            Type t = model.GetType();
            PropertyInfo[] PropertyList = t.GetProperties();
            foreach (PropertyInfo item in PropertyList)
            {
                sb.Append(item.Name);
                sb.Append("：");

                object value = item.GetValue(model, null);
                if(value == null)
                {
                    sb.AppendLine("null");
                }
                else
                {
                    sb.AppendLine(value.ToString());
                }
            }

            return sb.ToString();
        }
    }
}