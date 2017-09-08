//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

namespace CSharpUtils.DB.DbProvider
{
    using CSharpUtils;
    using System.Data.SQLite;
    using Configuration;

    /// <summary>
    /// SQLite 数据库操作
    /// 有关数据库连接的方法。
    /// 
    /// 修改纪录
    ///
    ///		2010.10.18 版本：1.0 yanzi 创建。
    /// 
    /// 版本：1.0
    /// 
    /// <author>
    ///		<name>yanzi</name>
    ///		<date>2010.10.18</date>
    /// </author> 
    /// </summary>

    public class SqLiteHelper: BaseDbUtil, IDbUtil
    {
        #region 构造函数

        public SqLiteHelper()
        {
            FileName = "SQLite.txt";   // sql查询句日志
        }

        /// <summary>
		/// 数据库连接
		/// </summary>
        /// <param name="connectionString">数据连接</param>
        public SqLiteHelper(string connectionString)
            : this()
		{
            this.ConnectionString = connectionString;
		}
        #endregion

        /// <summary>
        /// 当前数据库实例
        /// </summary>
        /// <returns></returns>
        public override DbProviderFactory GetInstance()
        {
            return SQLiteFactory.Instance;
        }

        /// <summary>
        /// 当前数据库类型
        /// </summary>
        DataBaseType IDbUtil.CurrentDataBaseType
        {
            get
            {
                return DataBaseType.SQLite;
            }
        }

        #region public string GetDBNow()
        /// <summary>
        /// 获得数据库日期时间
        /// </summary>
        /// <returns>日期时间</returns>
        public string GetDBNow()
        {
            return "datetime('now');";
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

        #region public DbParameter MakeInParam(string targetFiled, object targetValue)
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="targetFiled">目标字段</param>
        /// <param name="targetValue">值</param>
        /// <returns>参数</returns>
        public DbParameter MakeInParam(string targetFiled, object targetValue)
        {
            return new SQLiteParameter(targetFiled, targetValue);
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
            DbParameter[] dbParameters = new DbParameter[0];
            if (targetFileds != null && targetValues != null)
            {
                dbParameters = new DbParameter[targetFileds.Length];
                for (int i = 0; i < targetFileds.Length; i++)
                {
                    if (targetFileds[i] != null && targetValues[i] != null)
                    {
                        dbParameters[i] = this.MakeInParam(targetFileds[i], targetValues[i]);
                    }
                }
            }
            return dbParameters;
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
            SQLiteParameter param;

            if (Size > 0)
            {
                param = new SQLiteParameter(DbType,Size,paramName);
            }
            else
            {
                param = new SQLiteParameter(paramName, (DbType)DbType);
            }

            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
                param.Value = Value;

            return param;
        }


        #region public string GetParameter(string parameter) 获得参数Sql表达式
        /// <summary>
        /// 获得参数Sql表达式
        /// </summary>
        /// <param name="parameter">参数名称</param>
        /// <returns>字符串</returns>
        public string GetParameter(string parameter)
        {
            return " ?" + parameter;
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
            returnValue = " CONCAT(";
            for (int i = 0; i < values.Length; i++)
            {
                returnValue += values[i] + " ,";
            }
            returnValue = returnValue.Substring(0, returnValue.Length - 2);
            returnValue += ")";
            return returnValue;
        }
        #endregion

    }
}
