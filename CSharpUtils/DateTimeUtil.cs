using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpUtils
{
    public static class DateTimeUtil
    {
        /// <summary>
        /// 获取当前时间的时间戳(秒),10位
        /// </summary>
        /// <returns></returns>
        public static ulong GetTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime( 1970, 1, 1, 0, 0, 0, 0 );

            // return (ulong)ts.TotalMilliseconds;
            return (ulong)ts.TotalSeconds;//.TotalMilliseconds;
        }

        /// <summary>
        /// 获取指定时间的时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long GetTimestamp(DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }

        /// <summary>
        /// 时间戳转时间格式，
        /// </summary>
        /// <param name="timeStamp">秒</param>
        /// <returns></returns>
        public static DateTime TimestampToDateTime(ulong timeStamp)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            DateTime dt = startTime.AddSeconds(timeStamp);
            return dt;
        }

        /// <summary>
        /// 转换为毫秒为0的时间类型
        /// </summary>
        /// <param name="value">时间类型</param>
        /// <returns>毫秒为0的时间类型</returns>
        public static DateTime ToFormatDate(this DateTime value)
        {
            return Convert.ToDateTime(value.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo));
        }

        /// <summary>
        /// 转换为yyyyMMddHHmmssfffffff的日期字符串
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToFormatLongString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMddHHmmssfffffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// 转换为yyyy-MM-dd的日期字符串
        /// </summary>
        /// <param name="value">时间类型</param>
        /// <returns>毫秒为0的时间类型</returns>
        public static string ToFormatShortDateString(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// 转换为yyyy/MM/dd的日期字符串
        /// </summary>
        /// <param name="value">时间类型</param>
        /// <returns>毫秒为0的时间类型</returns>
        public static string ToFormatDateString(this DateTime value)
        {
            return value.ToString("yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// 转换为HH:mm的日期字符串
        /// </summary>
        /// <param name="value">时间类型</param>
        /// <returns>毫秒为0的时间类型</returns>
        public static string ToFormatTimeString(this DateTime value)
        {
            return value.ToString("HH:mm", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// 转换为HH:mm:ss的日期字符串
        /// </summary>
        /// <param name="value">时间类型</param>
        /// <returns>毫秒为0的时间类型</returns>
        public static string ToFormatLongTimeString(this DateTime value)
        {
            return value.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// 转换为yyyy-MM-dd HH:mm:ss的日期字符串
        /// </summary>
        /// <param name="value">时间类型</param>
        /// <returns>毫秒为0的时间类型</returns>
        public static string ToFormatString(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// 转换为yyyy-MM-dd HH:mm的日期字符串
        /// </summary>
        /// <param name="value">时间类型</param>
        /// <returns>毫秒为0的时间类型</returns>
        public static string ToFormatMinString(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd HH:mm", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// 获取当前日期的当天开始时间的日期
        /// </summary>
        /// <param name="value">时间类型</param>
        /// <returns>当前日期的当天开始时间</returns>
        public static DateTime StartTime(this DateTime dateTime)
        {
            return dateTime.Date;
        }

        /// <summary>
        /// 获取当前日期的当天结束时间的日期
        /// </summary>
        /// <param name="value">时间类型</param>
        /// <returns>当前日期的当天结束时间</returns>
        public static DateTime EndTime(this DateTime dateTime)
        {
            return dateTime.Date.AddDays(1).AddSeconds(-1);
        }

        /// <summary>
        /// 获取当前日期的本月开始时间的日期
        /// </summary>
        /// <param name="value">时间类型</param>
        /// <returns>当前日期的当天开始时间</returns>
        public static DateTime ThisMonthStartTime(this DateTime dateTime)
        {
            return dateTime.Date.AddDays(-dateTime.Day + 1);
        }

        /// <summary>
        /// 获取当前日期的本月结束时间的日期
        /// </summary>
        /// <param name="value">时间类型</param>
        /// <returns>当前日期的当天结束时间</returns>
        public static DateTime ThisMonthEndTime(this DateTime dateTime)
        {
            return dateTime.Date.AddDays(-dateTime.Day + 1).AddMonths(1).AddDays(-1);
        }

        public static double ToUTC(this DateTime dateTime)
        {
            double intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (dateTime.ToUniversalTime() - startTime).TotalMilliseconds;
            return intResult;
        }

        public static string GetDateStr(string fmt="yyyyMMddHHmmssfff")
        {
            return DateTime.Now.ToString(fmt);
        }

        public static string GetDateStr(DateTime dateTime)
        {
            string strTime = dateTime.ToString("yyyy-MM-dd");
            return strTime == "0001-01-01" ? "" : strTime;
        }

        public static string GetDateTimeStr(DateTime dateTime)
        {
            string strTime = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            return strTime == "0001-01-01 00:00:00" ? "" : strTime;
        }

    }
}
