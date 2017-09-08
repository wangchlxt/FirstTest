using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CSharpUtils
{
    /// <summary>
    /// 枚举值类型
    /// </summary>
    /// <remarks>
    /// tryc(netps@126.com) 2012.01.27
    /// </remarks>
    public partial class EnumItem
    {
        /// <summary>
        /// 枚举常数的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 枚举常数值
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 枚举属性的描述
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 枚举辅助类
    /// </summary>
    /// <remarks>
    /// BiaoYong 2012.01.01
    /// tryc(netps@126.com) 2012.01.27
    /// </remarks>
    public static partial class EnumUtil
    {
        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <param name="enumValue">枚举值</param>
        /// <returns>枚举值指定枚举中的常数值</returns>
        /// <exception cref="System.ArgumentNullException">value 为 null。</exception>
        /// <exception cref="System.ArgumentException"> value 既不是 enumType 类型，也没有与 enumType 相同的基础类型。</exception>
        public static string GetValue(this Enum enumValue)
        {
            if (enumValue == null)
            {
                throw new ArgumentNullException("enumValue");
            }

            if (enumValue.GetType().IsEnum)
            {
                return Convert.ToString(Convert.ToInt32(enumValue));
            }
            throw new ArgumentException("enumValue不是枚举值");
        }

        /// <summary>
        /// 在指定枚举中检索具有指定值的常数的名称
        /// </summary>
        /// <param name="enumValue">枚举值</param>
        /// <returns>具有指定值的常数的名称</returns>
        /// <exception cref="System.ArgumentNullException">value 为 null。</exception>
        /// <exception cref="System.ArgumentException"> value 既不是 enumType 类型，也没有与 enumType 相同的基础类型。</exception>
        public static string GetName(this Enum enumValue)
        {
            if (enumValue == null)
            {
                throw new ArgumentNullException("enumValue");
            }

            if (enumValue.GetType().IsEnum)
            {
                return Enum.GetName(enumValue.GetType(), enumValue);
            }
            throw new ArgumentException("enumValue不是枚举值");
        }

        /// <summary>
        /// 转换字符串到枚举值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="enumValue">字符串值</param>
        /// <returns>枚举类型值</returns>
        /// <exception cref="ArgumentNullException">参数enumValue, 字符串值不能为空.</exception>
        /// <exception cref="InvalidCastException">enumValue不是枚举值</exception>
        public static T GetEnum<T>(this string enumValue)
        {
            if (string.IsNullOrEmpty(enumValue))
            {
                throw new ArgumentNullException("enumValue");
            }

            Type type = typeof(T);
            if (type.IsEnum)
            {
                return (T)Enum.Parse(typeof(T), enumValue, true);
            }

            throw new InvalidCastException("enumValue不是枚举值");
        }

        /// <summary>
        /// 根据值, 返回值对应的列, 用于处理[Flags]public enum EnumOrderType标记的枚举
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static List<int> GetValues<T>(string enumValue)
        {
            if (string.IsNullOrEmpty(enumValue))
            {
                throw new ArgumentNullException("enumValue");
            }
            var list = GetNames<T>(enumValue);
            if (list != null && list.Count > 0)
            {
                var result = list.Select(p =>
                {
                    var t = (T)Enum.Parse(typeof(T), p, true);
                    return Convert.ToInt32(t);
                })
                .ToList();

                return result;
            }
            throw new InvalidCastException("list");
        }

        /// <summary>
        /// 根据值, 返回值对应的名称, 用于处理[Flags]public enum EnumOrderType标记的枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        /// <example>
        /// <pre>
        /// 
        /// </pre>
        /// </example>
        public static List<string> GetNames<T>(string enumValue)
        {
            if (string.IsNullOrEmpty(enumValue))
            {
                throw new ArgumentNullException("enumValue");
            }

            Type type = typeof(T);
            if (type.IsEnum)
            { // 验证是否枚举
                var tt = (T)Enum.Parse(typeof(T), enumValue, true);
                var result = tt.ToString()
                    .Replace(" ", string.Empty)
                    .Split(',')
                    .ToList();

                return result;
            }

            throw new InvalidCastException("enumValue不是枚举值");
        }

        /// <summary>
        /// 获取字段描述
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        private static string GetFieldDesc(FieldInfo fieldInfo)
        {
            var attrs = fieldInfo.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if ((attrs.Length > 0))
            {
                var attribute = attrs[0] as System.ComponentModel.DescriptionAttribute;
                if (attribute != null)
                    return attribute.Description;
            }
            return fieldInfo.Name;
        }


        /// <summary>
        /// 获取一个枚举类型的列表，用于在dropdownList中显示
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <returns></returns>
        public static List<EnumItem> GetEnumItems(Type type)
        {
            //System.Type type = typeof(T);
            FieldInfo[] fields = type.GetFields();

            var itemList = new List<EnumItem>(fields.Length);
            foreach (FieldInfo fi in fields)
            {
                if (fi.FieldType == type)
                {
                    var item = new EnumItem
                                    {
                                        Name = fi.Name,
                                        Value = Convert.ToInt32(fi.GetRawConstantValue()),
                                        Description = GetFieldDesc(fi)
                                    };

                    itemList.Add(item);
                }
            }

            return itemList;
        }

        /// <summary>
        /// 获取枚举值的描述
        /// </summary>
        /// <param name="enumType">指定的枚举类型</param>
        /// <param name="enumValue">枚举类型的值</param>
        /// <returns>枚举值的描述,需用DescriptionAttribute进行描述</returns>
        public static string GetDescription(Type enumType, int enumValue)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException("enumType");
            }
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("enumType不是枚举类型");
            }
            var items = GetEnumItems(enumType);
            var item = items.Find(p => p.Value == enumValue);
            if (item == null)
            {
                throw new ArgumentException("enumType不包括此枚举值");
            }
            return item.Description;
        }


        /// <summary>
        /// 获取枚举值的描述
        /// </summary>
        /// <param name="enumValue">指定的枚举值: 如SystemParameterEnum.ServerLevel</param>
        /// <returns>枚举值的描述,需用DescriptionAttribute进行描述</returns>
        public static string GetDescription(this object enumValue)
        {
            if (enumValue == null)
            {
                throw new ArgumentNullException("enumValue");
            }

            if (enumValue.GetType().IsEnum)
            {
                return GetDescription(enumValue.GetType(),
                    Convert.ToInt32(enumValue));
            }
            else
            {
                throw new ArgumentException("enumValue不是枚举值");
            }
        }

    }
}
