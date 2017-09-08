using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpUtils
{
    /// <summary>
    /// 日志辅助类
    /// </summary>
    public static class LogHelper
    {
        private static Logger logger = new Logger();

        #region Debug
        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="message">消息</param>
        public static void Debug( string message )
        {
            logger.Debug( message );
        }
        #endregion

        #region Info
        /// <summary>
        /// 记录信息日志
        /// </summary>
        /// <param name="message">消息</param>
        public static void Info( string message )
        {
            logger.Info( message );
        }
        #endregion

        #region Error
        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="message">消息</param>
        public static void Error( string message )
        {
            logger.Error( message );
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="e">异常</param>
        public static void Error( string message, Exception e )
        {
            logger.Error( message, e );
        }
        #endregion
    }
}
