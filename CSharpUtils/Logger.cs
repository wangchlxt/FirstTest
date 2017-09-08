using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpUtils
{
    /// <summary>
    /// 封装 log4net，提供日志记录功能
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// log4net 日志对象
        /// </summary>
        private ILog logger;

        #region Logger
        /// <summary>
        /// 构造函数
        /// </summary>
        public Logger()
            : this( string.Empty )
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">log4net.config 中配置的日志名称</param>
        public Logger( string name )
        {
            this.logger = LogManager.GetLogger( name );
        }
        #endregion

        #region Debug
        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="message">消息</param>
        public void Debug( string message )
        {
            this.logger.Debug( message );
        }
        #endregion

        #region Info
        /// <summary>
        /// 记录信息日志
        /// </summary>
        /// <param name="message">消息</param>
        public void Info( string message )
        {
            this.logger.Info( message );
        }
        #endregion

        #region Error
        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="message">消息</param>
        public void Error( string message )
        {
            this.logger.Error( message );
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="e">异常</param>
        public void Error( string message, Exception e )
        {
            this.logger.Error( message, e );
        }
        #endregion
    }
}
