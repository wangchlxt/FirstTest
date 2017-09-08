using CSharpUtils;

namespace CSharpUtils
{
    /// <summary>
    /// 业务操作结果信息类，对操作结果进行封装
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// 获取或设置操作结果类型
        /// </summary>
        public OperationResultType ResultType { get; set; }

        /// <summary>
        /// 获取或设置操作返回信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 获取或设置操作结果附加信息
        /// </summary>
        public dynamic AppendData { get; set; }

        /// <summary>
        /// 获取或设置操作结果附加信息
        /// </summary>
        public dynamic AppendData2 { get; set; }

        /// <summary>
        /// 获取或设置操作结果附加信息
        /// </summary>
        public dynamic AppendData3 { get; set; }

        /// <summary>
        /// 初始化一个业务操作结果信息类的新实例
        /// </summary>
        /// <param name="resultType">业务操作结果类型</param>
        public OperationResult( OperationResultType resultType )
        {
            this.ResultType = resultType;
        }

        /// <summary>
        /// 初始化一个定义返回消息的业务操作结果信息类的新实例
        /// </summary>
        /// <param name="resultType">业务操作结果类型</param>
        /// <param name="message">业务返回消息</param>
        public OperationResult( OperationResultType resultType, string message )
        {
            this.ResultType = resultType;
            this.Message = message;
        }

        /// <summary>
        /// 初始化一个定义返回消息与附加数据的业务操作结果信息类的新实例
        /// </summary>
        /// <param name="resultType">业务操作结果类型</param>
        /// <param name="message">业务返回消息</param>
        /// <param name="appendData">业务返回数据</param>
        public OperationResult( OperationResultType resultType, string message, object appendData )
        {
            this.ResultType = resultType;
            this.Message = message;
            this.AppendData = appendData;
            if( resultType == OperationResultType.Error && appendData.GetType() == typeof( string ) )
            {
                LogHelper.Info( appendData.ToString() );
            }
        }

        /// <summary>
        /// 初始化一个定义返回消息与附加数据的业务操作结果信息类的新实例
        /// </summary>
        /// <param name="resultType">业务操作结果类型</param>
        /// <param name="message">业务返回消息</param>
        /// <param name="appendData">业务返回数据</param>
        public OperationResult( OperationResultType resultType, string message, object appendData, object appendData2 )
        {
            this.ResultType = resultType;
            this.Message = message;
            this.AppendData = appendData;
            this.AppendData2 = appendData2;
        }

        /// <summary>
        /// 初始化一个定义返回消息与附加数据的业务操作结果信息类的新实例
        /// </summary>
        /// <param name="resultType">业务操作结果类型</param>
        /// <param name="message">业务返回消息</param>
        /// <param name="appendData">业务返回数据</param>
        public OperationResult( OperationResultType resultType, object appendData, object appendData2, object appendData3 )
        {
            this.ResultType = resultType;
            this.AppendData = appendData;
            this.AppendData2 = appendData2;
            this.AppendData3 = appendData3;
        }
    }
}
