//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CSharpUtils.DB.DbProvider
{
    using CSharpUtils;
    using Configuration;

    /// <summary>
    /// SqlHelper
    /// 有关数据库连接的方法。
    /// 
    /// 修改纪录
    /// 
    ///		2008.08.26 版本：1.2 JiRiGaLa 修改Open时的错误反馈。
    ///		2008.06.01 版本：1.1 JiRiGaLa 数据库连接获得方式进行改进，构造函数获得调通。
    ///		2008.05.07 版本：1.0 JiRiGaLa 创建。
    /// 
    /// 版本：1.2
    /// 
    /// <author>

    ///		<date>2008.08.26</date>
    /// </author> 
    /// </summary>
    public class SqlUtil : BaseDbUtil, IDbUtil, IDbUtilExpand
	{
        public override DbProviderFactory GetInstance()
        {
            return SqlClientFactory.Instance;
        }

        /// <summary>
        /// 当前数据库类型
        /// </summary>
        public DataBaseType CurrentDataBaseType
        {
            get
            {
                return DataBaseType.Sqlserver;
            }
        }
        
        #region public SqlHelper()
        /// <summary>
        /// 构造方法
        /// </summary>
        public SqlUtil()
		{
            FileName = "SqlHelper.txt"; // sql查询句日志
		}
		#endregion

        #region public SqlHelper(string connectionString)
        /// <summary>
        /// 设定软件名称
        /// </summary>
        /// <param name="connectionString">数据连接</param>
        public SqlUtil(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        #endregion


        #region public string GetDBNow()
        /// <summary>
        /// 获得数据库日期时间
        /// </summary>
        /// <returns>日期时间</returns>
        public string GetDBNow()
        {
            return " Getdate() ";
        }
        #endregion

        #region public string GetDBDateTime()
        /// <summary>
        /// 获得数据库日期时间
        /// </summary>
        /// <returns>日期时间</returns>
        public string GetDBDateTime()
        {
            string commandText = " SELECT " + this.GetDBNow();
            this.Open();
            string dateTime = this.ExecuteScalar(CommandType.Text, commandText, new DbParameter[0]).ToString();
            this.Close();
            return dateTime;
        }
        #endregion

        
        #region public string SqlSafe(string value) 检查参数的安全性
        /// <summary>
        /// 检查参数的安全性
        /// </summary>
        /// <param name="value">参数</param>
        /// <returns>安全的参数</returns>
        public string SqlSafe(string value)
        {
            value = value.Replace("'", "''");
            // value = value.Replace("%", "'%");
            return value;
        }
        #endregion

        #region string PlusSign(params string[] values)
        /// <summary>
        ///  获得Sql字符串相加符号
        /// </summary>
        /// <param name="values">参数值</param>
        /// <returns>字符加</returns>
        public string PlusSign(params string[] values)
        {
            string returnValue = string.Empty;
            for (int i = 0; i < values.Length; i++)
            {
                returnValue += values[i] + " + ";
            }
            if (!String.IsNullOrEmpty(returnValue))
            {
                returnValue = returnValue.Substring(0, returnValue.Length - 3);
            }
            else
            {
                returnValue = " + ";
            }
            return returnValue;
        }
        #endregion


        #region public string GetParameter(string parameter) 获得参数Sql表达式
        /// <summary>
        /// 获得参数Sql表达式
        /// </summary>
        /// <param name="parameter">参数名称</param>
        /// <returns>字符串</returns>
        public string GetParameter(string parameter)
        {
            return " @" + parameter + " ";
        }
        #endregion

        #region public DbParameter MakeInParam(string targetFiled, object targetValue)
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="targetFiled">目标字段</param>
        /// <param name="targetValue">值</param>
        /// <returns>参数</returns>
        public DbParameter MakeInParam(string targetFiled, object targetValue)
        {
            return new SqlParameter("@" + targetFiled, targetValue);
        }
        #endregion

        #region public DbParameter[] MakeParameters(string targetFiled, object targetValue)
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="targetFiled">目标字段</param>
        /// <param name="targetValue">值</param>
        /// <returns>参数集</returns>
        public DbParameter[] MakeParameters(string targetFiled, object targetValue)
        {
            DbParameter[] dbParameters = null;
            if (targetFiled != null && targetValue != null)
            {
                dbParameters = new DbParameter[1];
                dbParameters[0] = this.MakeInParam(targetFiled, targetValue);
            }
            return dbParameters;
        }
        #endregion

        #region public DbParameter[] MakeParameters(string[] targetFileds, Object[] targetValues)
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="targetFiled">目标字段</param>
        /// <param name="targetValue">值</param>
        /// <returns>参数集</returns>
        public DbParameter[] MakeParameters(string[] targetFileds, Object[] targetValues)
        {
            // 这里需要用泛型列表，因为有不合法的数组的时候
            List<DbParameter> dbParameters = new List<DbParameter>();
            if (targetFileds != null && targetValues != null)
            {
                for (int i = 0; i < targetFileds.Length; i++)
                {
                    if (targetFileds[i] != null && targetValues[i] != null && (!(targetValues[i] is Array)))
                    {
                        dbParameters.Add(this.MakeInParam(targetFileds[i], targetValues[i]));
                    }
                }
            }
            return dbParameters.ToArray();
        }
        #endregion

        public DbParameter MakeOutParam(string paramName, DbType DbType, int Size)
        {
            return MakeParam(paramName, DbType, Size, ParameterDirection.Output, null);
        }

        public DbParameter MakeInParam(string paramName, DbType DbType, int Size, object Value)
        {
            return MakeParam(paramName, DbType, Size, ParameterDirection.Input, Value);
        }

        public DbParameter MakeParam(string paramName, DbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            SqlParameter param;

            if (Size > 0)
            {
                param = new SqlParameter(paramName, (SqlDbType)DbType, Size);
            }
            else
            {
                param = new SqlParameter(paramName, (SqlDbType)DbType);
            }

            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
                param.Value = Value;

            return param;
        }
        
        #region public void SqlBulkCopyData(DataTable dataTable)
        /// <summary>
        /// 利用Net SqlBulkCopy 批量导入数据库,速度超快
        /// </summary>
        /// <param name="dataTable">源内存数据表</param>
        public void SqlBulkCopyData(DataTable dataTable)
        {
            // SQL 数据连接
            SqlConnection conn = null;
            // 打开数据库
            this.Open();

            // 获取连接
            conn = (SqlConnection)GetDbConnection();

            using (SqlTransaction tran = conn.BeginTransaction())
            {
                // 批量保存数据，只能用于Sql
                SqlBulkCopy sqlbulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, tran);
                // 设置源表名称
                sqlbulkCopy.DestinationTableName = dataTable.TableName;
                // 设置超时限制
                sqlbulkCopy.BulkCopyTimeout = 1000;

                foreach (DataColumn dtColumn in dataTable.Columns)
                {
                    sqlbulkCopy.ColumnMappings.Add(dtColumn.ColumnName, dtColumn.ColumnName);
                }
                try
                {
                    // 写入
                    sqlbulkCopy.WriteToServer(dataTable);
                    // 提交事务
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    sqlbulkCopy.Close();
                }
                finally
                {
                    sqlbulkCopy.Close();
                    this.Close();
                }

            }
        }
        #endregion
    }
}