//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Globalization;
using CSharpUtils.Configuration;
using CSharpUtils.DB.DbProvider;

namespace CSharpUtils.DB
{
    /// <summary>
    /// DbHelper
    /// 有关数据库连接的方法。
    /// 
    /// 修改纪录
    /// 
    ///		2008.09.03 版本：1.0 JiRiGaLa 创建。
    /// 
    /// 版本：1.2
    /// 
    /// <author>

    ///		<date>2008.08.26</date>
    /// </author> 
    /// </summary>
    public class DbUtil
    {
        /// <summary>
        /// 数据库连接串
        /// </summary>
        public static string BusinessDbConnection = BaseSystemInfo.BusinessDbConnection;

        private static readonly IDbUtil dbHelper = DbUtilFactory.GetHelper();

        /// <summary>
        /// DbProviderFactory实例
        /// </summary>
        private static DbProviderFactory factory = null;

        /// <summary>
        /// DbFactory实例
        /// </summary>
        public static DbProviderFactory Factory
        {
            get
            {
                if (factory == null)
                {
                    factory = dbHelper.GetInstance();
                }
                return factory;
            }
        }

        #region private DbHelper()
        /// <summary>
        /// 构造方法
        /// </summary>
        private DbUtil()
        {
        }
        #endregion

        /// <summary>
        /// 当前数据库类型
        /// </summary>
        public DataBaseType CurrentDataBaseType
        {
            get
            {
                return dbHelper.CurrentDataBaseType;
            }
        }

        #region public static string GetDBNow()
        /// <summary>
        /// 获得数据库日期时间
        /// </summary>
        /// <returns>日期时间</returns>
        public static string GetDBNow()
        {
            return dbHelper.GetDBNow();
        }
        #endregion

        #region public static string SqlSafe(string value) 检查参数的安全性
        /// <summary>
        /// 检查参数的安全性
        /// </summary>
        /// <param name="value">参数</param>
        /// <returns>安全的参数</returns>
        public static string SqlSafe(string value)
        {
            return dbHelper.SqlSafe(value);
        }
        #endregion

        #region string PlusSign(params string[] values)
        /// <summary>
        ///  获得Sql字符串相加符号
        /// </summary>
        /// <param name="values">参数值</param>
        /// <returns>字符加</returns>
        public static string PlusSign(params string[] values)
        {
            return dbHelper.PlusSign(values);
        }
        #endregion

        #region public static string GetParameter(string parameter) 获得参数Sql表达式
        /// <summary>
        /// 获得参数Sql表达式
        /// </summary>
        /// <param name="parameter">参数名称</param>
        /// <returns>字符串</returns>
        public static string GetParameter(string parameter)
        {
            return dbHelper.GetParameter(parameter);
        }
        #endregion

        #region public static DbParameter MakeInParam(string targetFiled, object targetValue)
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="targetFiled">目标字段</param>
        /// <param name="targetValue">值</param>
        /// <returns>参数</returns>
        public static DbParameter MakeInParam(string targetFiled, object targetValue)
        {
            return dbHelper.MakeInParam(targetFiled, targetValue);
        }
        #endregion

        #region public static DbParameter[] MakeParameters(string targetFiled, object targetValue)
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="targetFiled">目标字段</param>
        /// <param name="targetValue">值</param>
        /// <returns>参数集</returns>
        public static DbParameter[] MakeParameters(string targetFiled, object targetValue)
        {
            return dbHelper.MakeParameters(targetFiled, targetValue);
        }
        #endregion

        #region public static DbParameter[] MakeParameters(string[] targetFileds, Object[] targetValues)
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="targetFiled">目标字段</param>
        /// <param name="targetValue">值</param>
        /// <returns>参数集</returns>
        public static DbParameter[] MakeParameters(string[] targetFileds, Object[] targetValues)
        {
            return dbHelper.MakeParameters(targetFileds, targetValues);
        }
        #endregion

        public static DbParameter MakeParam(string paramName, DbType DbType, Int32 size, ParameterDirection Direction, object Value)
        {
            return dbHelper.MakeParam(paramName, DbType, size, Direction, Value);
        }

        #region public static int ExecuteNonQuery(string commandText)
        /// <summary>
        /// 执行查询, SQL BUILDER 用了这个东西？参数需要保存, 不能丢失.
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(CommandType.Text, commandText, null);
        }
        #endregion

        #region public static int ExecuteNonQuery(string commandText, DbParameter[] dbParameters);
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQuery(string commandText, DbParameter[] dbParameters)
        {
            return ExecuteNonQuery(CommandType.Text, commandText, dbParameters);
        }
        #endregion

        #region public static int ExecuteNonQuery(CommandType commandType, string commandText, DbParameter[] dbParameters)
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="CommandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQuery(CommandType commandType, string commandText, DbParameter[] dbParameters)
        {
            int returnValue = 0;
            dbHelper.Open(BusinessDbConnection);
            returnValue = dbHelper.ExecuteNonQuery(commandType, commandText, dbParameters);
            dbHelper.Close();
            return returnValue;
        }
        #endregion


        #region static public object ExecuteScalar(string commandText)
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <returns>object</returns>
        public static object ExecuteScalar(string commandText)
        {
            return ExecuteScalar(CommandType.Text, commandText, null);
        }
        #endregion

        #region static public object ExecuteScalar(string commandText, DbParameter[] dbParameters)
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>Object</returns>
        public static object ExecuteScalar(string commandText, DbParameter[] dbParameters)
        {
            return ExecuteScalar(CommandType.Text, commandText, dbParameters);
        }
        #endregion

        #region public static object ExecuteScalar(CommandType commandType, string commandText, DbParameter[] dbParameters)
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="CommandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>Object</returns>
        public static object ExecuteScalar(CommandType commandType, string commandText, DbParameter[] dbParameters)
        {
            object returnValue = null;
            dbHelper.Open(BusinessDbConnection);
            returnValue = dbHelper.ExecuteScalar(commandType, commandText, dbParameters);
            dbHelper.Close();
            return returnValue;
        }
        #endregion


        #region public static IDataReader ExecuteReader(string commandText)
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <returns>结果集流</returns>
        public static IDataReader ExecuteReader(string commandText)
        {
            dbHelper.Open(BusinessDbConnection);
            dbHelper.AutoOpenClose = true;
            return dbHelper.ExecuteReader(commandText);
        }
        #endregion

        #region public static IDataReader ExecuteReader(string commandText, DbParameter[] dbParameters);
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>结果集流</returns>
        public static IDataReader ExecuteReader(string commandText, DbParameter[] dbParameters)
        {
            return ExecuteReader(CommandType.Text, commandText, dbParameters);
        }
        #endregion

        #region public IDataReader ExecuteReader(string commandText, List<DbParameter> dbParameters);
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>结果集流</returns>
        public IDataReader ExecuteReader(string commandText, List<DbParameter> dbParameters)
        {
            return ExecuteReader(CommandType.Text, commandText, dbParameters.ToArray());
        }
        #endregion

        #region public static IDataReader ExecuteReader(CommandType commandType, string commandText, DbParameter[] dbParameters)
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>结果集流</returns>
        public static IDataReader ExecuteReader(CommandType commandType, string commandText, DbParameter[] dbParameters)
        {
            dbHelper.Open(BusinessDbConnection);
            dbHelper.AutoOpenClose = true;
            return dbHelper.ExecuteReader(commandType, commandText, dbParameters);
        }
        #endregion

        #region public static IDataReader ExecuteReader(CommandType commandType, string commandText, DbParameter[] dbParameters)
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>结果集流</returns>
        public static IDataReader ExecuteReader(CommandType commandType, string commandText, List<DbParameter> dbParameters)
        {
            return ExecuteReader(commandType, commandText, dbParameters.ToArray());
        }
        #endregion


        #region public static DataTable Fill(string commandText)
        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="commandText">查询</param>
        /// <returns>数据表</returns>
        public static DataTable Fill(string commandText)
        {
            DataTable dataTable = new DataTable();
            Fill(dataTable, CommandType.Text, commandText, null);
            return dataTable;
        }
        #endregion

        #region public static DataTable Fill(DataTable dataTable, string commandText)
        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="dataTable">目标数据表</param>
        /// <param name="commandText">查询</param>
        /// <returns>数据表</returns>
        public static DataTable Fill(DataTable dataTable, string commandText)
        {
            return Fill(dataTable, CommandType.Text, commandText, null);
        }
        #endregion

        #region public static DataTable Fill(string commandText, DbParameter[] dbParameters)
        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据表</returns>
        public static DataTable Fill(string commandText, DbParameter[] dbParameters)
        {
            return dbHelper.Fill(CommandType.Text, commandText, dbParameters);
        }
        #endregion

        #region public static DataTable Fill(DataTable dataTable, string commandText, DbParameter[] dbParameters)
        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="dataTable">目标数据表</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据表</returns>
        public static DataTable Fill(DataTable dataTable, string commandText, DbParameter[] dbParameters)
        {
            return dbHelper.Fill(dataTable, CommandType.Text, commandText, dbParameters);
        }
        #endregion

        #region public static DataTable Fill(DataTable dataTable, CommandType commandType, string commandText, DbParameter[] dbParameters)
        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="dataSet">目标数据表</param>
        /// <param name="CommandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据表</returns>
        public static DataTable Fill(DataTable dataTable, CommandType commandType, string commandText, DbParameter[] dbParameters)
        {
            dbHelper.Open(BusinessDbConnection);
            dbHelper.Fill(dataTable, commandType, commandText, dbParameters);
            dbHelper.Close();
            return dataTable;
        }
        #endregion


        #region public static DataSet Fill(DataSet dataSet, string commandText, string tableName)
        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="dataSet">目标数据集</param>
        /// <param name="commandText">查询</param>
        /// <param name="tableName">填充表</param>
        /// <returns>数据集</returns>
        public static DataSet Fill(DataSet dataSet, string commandText, string tableName)
        {
            dbHelper.Open(BusinessDbConnection);
            dbHelper.Fill(dataSet, commandText, tableName);
            dbHelper.Close();
            return dataSet;
        }
        #endregion

        #region static public DataSet Fill(DataSet dataSet, string commandText, string tableName, DbParameter[] dbParameters)
        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="dataSet">数据集</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="tableName">填充表</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据集</returns>
        public static DataSet Fill(DataSet dataSet, string commandText, string tableName, DbParameter[] dbParameters)
        {
            return Fill(dataSet, CommandType.Text, commandText, tableName, dbParameters);
        }
        #endregion

        #region public static DataSet Fill(DataSet dataSet, CommandType commandType, string commandText, string tableName, DbParameter[] dbParameters)
        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="dataSet">数据集</param>
        /// <param name="CommandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="tableName">填充表</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据集</returns>
        public static DataSet Fill(DataSet dataSet, CommandType commandType, string commandText, string tableName, DbParameter[] dbParameters)
        {
            dbHelper.Open(BusinessDbConnection);
            dbHelper.Fill(dataSet, commandType, commandText, tableName, dbParameters);
            dbHelper.Close();
            return dataSet;
        }
        #endregion


        #region public static int ExecuteProcedure(string procedureName)
        /// <summary>
        /// 执行数据库查询
        /// </summary>
        /// <param name="procedureName">存储过程</param>
        /// <returns>int</returns>
        public static int ExecuteProcedure(string procedureName)
        {
            return ExecuteProcedure(procedureName, null);
        }
        #endregion

        #region public static int ExecuteProcedure(string procedureName, DbParameter[] dbParameters)
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procedureName">存储过程名</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>影响行数</returns>
        public static int ExecuteProcedure(string procedureName, DbParameter[] dbParameters)
        {
            int returnValue = 0;
            dbHelper.Open(BusinessDbConnection);
            returnValue = dbHelper.ExecuteProcedure(procedureName, dbParameters);
            dbHelper.Close();
            return returnValue;
        }
        #endregion

        #region public static DataSet ExecuteProcedureForDataTable(string procedureName, string tableName, DbParameter[] dbParameters)
        /// <summary>
        /// 执行数据库脚本
        /// </summary>
        /// <param name="dataSet">数据集</param>
        /// <param name="procedureName">存储过程</param>
        /// <param name="tableName">填充表</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据集</returns>
        public static DataTable ExecuteProcedureForDataTable(string procedureName, string tableName, DbParameter[] dbParameters)
        {
            dbHelper.Open(BusinessDbConnection);
            DataTable dataTable = dbHelper.ExecuteProcedureForDataTable(procedureName, tableName, dbParameters);
            dbHelper.Close();
            return dataTable;
        }
        #endregion

        #region public static bool TestConn(string dataBaseType, string dataBase, string userName, string password, string workstation, bool trustLink)
        /// <summary>
        /// 测试数据库连接是否成功，这里抛出异常进行加强，充分使用系统的异常功能。
        /// </summary>
        /// <param name="dataBaseType">数据库类别</param>
        /// <param name="dataBase">数据库名称</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="workstation">服务器名称</param>
        /// <param name="trustLink">是否信任的连接</param>
        /// <returns>是否连接成功</returns>
        public static bool TestConn(DataBaseType dataBaseType, string dataBase, string userName, string password, string workstation, bool trustLink)
        {
            bool returnValue = false;	// 连接是否成功
            IDbUtil dbHelper = null;	// 数据库连接
            string connectionString = GetOleDbConnection(dataBaseType, dataBase, userName, password, workstation, trustLink);
            dbHelper = new OleDbUtil(connectionString);
            try
            {
                if (dbHelper.GetDbConnection().State == ConnectionState.Closed)
                {
                    dbHelper.Open();
                }
                dbHelper.Close();
                returnValue = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbHelper = null;
            }
            return returnValue;
        }
        #endregion

        #region public static string GetOleDbConnection(string dataBaseType, string dataBase, string userName, string password, string workstation, bool trustLink)
        /// <summary>
        /// 获得数据库连接
        /// </summary>
        /// <param name="dataBaseType">数据库类别</param>
        /// <param name="dataBase">数据库名</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="workstation">主机</param>
        /// <param name="trustLink">信任的连接</param>
        /// <returns>是否连接成功</returns>
        public static string GetOleDbConnection(DataBaseType dataBaseType, string dataBase, string userName, string password, string workstation, bool trustLink)
        {
            string returnValue = string.Empty;
            switch (dataBaseType)
            {
                case DataBaseType.Access:
                    if (dataBase.IndexOf(":") < 0)
                    {
                        dataBase = BaseSystemInfo.StartupPath + dataBase;
                    }
                    returnValue = String.Format(CultureInfo.CurrentCulture, "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"{0}\";User Id={1};jet OleDB:Database Password={2}", dataBase, userName, password);
                    break;
                case DataBaseType.Sqlserver:
                    // 信任的连接与不信任的连接的差距
                    if (trustLink)
                    {
                        returnValue = String.Format(CultureInfo.CurrentCulture, "Provider=SQLOLEDB;Data Source={0};Initial Catalog={1};Integrated Security=SSPI", workstation, dataBase);
                    }
                    else
                    {
                        returnValue = String.Format(CultureInfo.CurrentCulture, "Provider=SQLOLEDB;Data Source={0};Initial Catalog={1};User ID={2};Password={3}", workstation, dataBase, userName, password);
                    }
                    break;
                case DataBaseType.Oracle:
                    if (!trustLink)
                    {
                        // 需要上传文件时必须用 Provider=OraOLEDB.Oracle.1
                        returnValue = String.Format(CultureInfo.CurrentCulture, "Provider=OraOLEDB.Oracle.1;Data Source={0};User Id={1};Password={2};", dataBase, userName, password);
                        // returnValue = String.Format(CultureInfo.CurrentCulture, "Provider=msdaora;Data Source={0};User Id={1};Password={2};", dataBase, userName, password);
                    }
                    break;
            }
            return returnValue;
        }
        #endregion
    }
}