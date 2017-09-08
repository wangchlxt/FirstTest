using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 将 DateTime 对象转换为 yyyy-MM-dd HH:mm:ss 格式的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToFullString( this DateTime value )
        {
            return value.ToString( "yyyy-MM-dd HH:mm:ss" );
        }

        /// <summary>
        /// 将 DateTime 对象转换为 yyyy-MM 格式的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMonthString( this DateTime value )
        {
            return value.ToString( "yyyy-MM" );
        }

        /// <summary>
        /// 将 DateTime 对象转换为 yyyy-MM-dd 格式的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDateString( this DateTime value )
        {
            return value.ToString( "yyyy-MM-dd" );
        }

        /// <summary>
        /// 将 DateTime 对象转换为 HH:mm:ss 格式的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToTimeString(this DateTime value)
        {
            return value.ToString("HH:mm:ss");
        }
    }
}
