//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;

namespace CSharpUtils.DB.DbProvider
{
    using Configuration;
    using CSharpUtils;
    using System.Diagnostics;
    using System.Reflection;

    /// <summary>
    /// OleDbHelper
    /// 有关数据库连接的方法。
    /// 
    /// 修改纪录
    ///
    ///     2010.06.13 版本：3.0 JiRiGaLa 改进为支持静态方法，不用数据库Open、Close的方式，AutoOpenClose开关。
    ///		2010.03.14 版本：2.0 JiRiGaLa 无法彻底释放、并发时出现异常问题解决。
    ///		2009.11.25 版本：1.0 JiRiGaLa 改进ConnectionString。
    /// 
    /// 版本：1.0
    /// 
    /// <author>

    ///		<date>2009.11.25</date>
    /// </author> 
    /// </summary>
    public abstract class BaseDbUtil // : IDbHelper
    {
        private DbConnection dbConnection = null;

        #region public DbConnection DbConnection 数据库适配器
        /// <summary>
        /// 数据库连接
        /// </summary>
        public DbConnection DbConnection
        {
            get
            {
                if (this.dbConnection == null)
                {
                    // 若没打开，就变成自动打开关闭的
                    this.Open();
                    this.AutoOpenClose = true;
                }
                return this.dbConnection;
            }
            set
            {
                this.dbConnection = value;
            }
        }
        #endregion


        private DbCommand dbCommand = null;

        #region public DbCommand DbCommand 命令
        /// <summary>
        /// 命令
        /// </summary>
        public DbCommand DbCommand
        {
            get
            {
                return this.dbCommand;
            }

            set
            {
                this.dbCommand = value;
            }
        }
        #endregion

        private DbTransaction dbTransaction = null;

        private DbDataAdapter dbDataAdapter = null;

        #region public DbDataAdapter DbDataAdapter 数据库适配器
        /// <summary>
        /// 数据库适配器
        /// </summary>
        public DbDataAdapter DbDataAdapter
        {
            get
            {
                return this.dbDataAdapter;
            }

            set
            {
                this.dbDataAdapter = value;
            }
        }
        #endregion

        private string connectionString = string.Empty;

        #region public string ConnectionString 数据库连接
        /// <summary>
        /// 数据库连接
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }

            set
            {
                this.connectionString = value;
            }
        }
        #endregion

        // 是否已在事务之中
        private bool inTransaction = false;
        
        #region public bool InTransaction 是否已采用事务
        /// <summary>
        /// 是否已采用事务
        /// </summary>
        public bool InTransaction
        {
            get
            {
                return this.inTransaction;
            }

            set
            {
                this.inTransaction = value;
            }
        }
        #endregion
        
        public string FileName = "BaseDbHelper.txt";    // sql查询句日志

        private bool autoOpenClose = false;
        /// <summary>
        /// 默认打开关闭数据库选项（默认为否）
        /// </summary>
        public bool AutoOpenClose
        {
            get
            {
                return autoOpenClose;
            }
            set
            {
                autoOpenClose = value;
            }
        }

        private DbProviderFactory _dbProviderFactory = null;
        /// <summary>
        /// DbProviderFactory实例
        /// </summary>
        public virtual DbProviderFactory GetInstance()
        {
            if (_dbProviderFactory == null)
            {
                _dbProviderFactory = DbUtilFactory.GetHelper().GetInstance();
            }

            return _dbProviderFactory;
        }

        #region public IDbConnection Open()
        /// <summary>
		/// 这时主要的获取数据库连接的方法
		/// </summary>
		/// <returns>数据库连接</returns>
        public IDbConnection Open()
		{
            #if (DEBUG)
                int milliStart = Environment.TickCount;
			#endif
            // 这里是获取一个连接的详细方法
            if (String.IsNullOrEmpty(this.ConnectionString))
            {
                // 是否静态数据库里已经设置了连接？

                /*
                if (!string.IsNullOrEmpty(DbHelper.ConnectionString))
                {
                    this.ConnectionString = DbHelper.ConnectionString;
                }
                else
                {
                     读取配置文件？
                }
                */

                // 默认打开业务数据库，而不是用户中心的数据库
                if (String.IsNullOrEmpty(BaseSystemInfo.BusinessDbConnection))
                {
                    BaseConfiguration.GetSetting();
                }
                if (String.IsNullOrEmpty(BaseSystemInfo.BusinessDbConnection))
                {
                    this.ConnectionString = BaseSystemInfo.UserCenterDbConnection;
                }
                else
                {
                    this.ConnectionString = BaseSystemInfo.BusinessDbConnection;
                }
            }
            this.Open(this.ConnectionString);
            // 写入调试信息
            #if (DEBUG)
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif
            return this.dbConnection;
		}
		#endregion

        #region public IDbConnection Open(string connectionString)
        /// <summary>
		/// 获得新的数据库连接
		/// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
		/// <returns>数据库连接</returns>
        public IDbConnection Open(string connectionString)
		{
            // 写入调试信息
            #if (DEBUG)
                int milliStart = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif			
            // 这里数据库连接打开的时候，就判断注册属性的有效性
            if (!SecretUtil.CheckRegister())
            {
                // 若没有进行注册，让程序无法打开数据库比较好。
                connectionString = string.Empty;

                // 抛出异常信息显示给客户
                throw new Exception(BaseSystemInfo.RegisterException);
            }
            // 若是空的话才打开
            if (this.dbConnection == null || this.dbConnection.State == ConnectionState.Closed)
            {
                this.ConnectionString = connectionString;
                this.dbConnection = this.GetInstance().CreateConnection();
                this.dbConnection.ConnectionString = this.ConnectionString;
                this.dbConnection.Open();

                // 创建对象
                // this.dbCommand = this.DbConnection.CreateCommand();
                // this.dbCommand.Connection = this.dbConnection;
                // this.dbDataAdapter = this.dbProviderFactory.CreateDataAdapter();
                // this.dbDataAdapter.SelectCommand = this.dbCommand;

                // 写入调试信息
                #if (DEBUG)
                    int milliEnd = Environment.TickCount;
                    Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds(milliEnd - milliStart).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
                #endif
            }
            return this.dbConnection;
		}
		#endregion
        
        #region public IDbConnection GetDbConnection() 获取数据库连接
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns>数据库连接</returns>
        public IDbConnection GetDbConnection()
        {
            return this.dbConnection;
        }
        #endregion

        #region public IDbTransaction GetDbTransaction() 获取数据源上执行的事务
        /// <summary>
        /// 获取数据源上执行的事务
        /// </summary>
        /// <returns>数据源上执行的事务</returns>
        public IDbTransaction GetDbTransaction()
        {
            return this.dbTransaction;
        }
        #endregion

        #region public IDbCommand GetDbCommand() 获取数据源上命令
        /// <summary>
        /// 获取数据源上命令
        /// </summary>
        /// <returns>数据源上命令</returns>
        public IDbCommand GetDbCommand()
        {
            return this.DbConnection.CreateCommand();
        }
        #endregion
        
        #region public IDataReader ExecuteReader(string commandText)
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <returns>结果集流</returns>
        public IDataReader ExecuteReader(string commandText)
        {
            // 写入调试信息
            #if (DEBUG)
                int milliStart = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif

            // 自动打开
            // 自动打开
            if (this.AutoOpenClose)
            {
                this.Open();
            }
            else
            {
                if (this.DbConnection == null)
                {
                    this.AutoOpenClose = true;
                    this.Open();
                }
            }

            this.dbCommand = this.DbConnection.CreateCommand();
            this.dbCommand.CommandType = CommandType.Text;
            this.dbCommand.CommandText = commandText;
            if (this.InTransaction)
            {
                this.dbCommand.Transaction = this.dbTransaction;
            }

            // DbDataReader dbDataReader = this.dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
            // 这里要关闭数据库才可以的
            DbDataReader dbDataReader = null;
            if (this.AutoOpenClose)
            {
                dbDataReader = this.dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            else
            {
                dbDataReader = this.dbCommand.ExecuteReader();
            }

            // 写入调试信息
            #if (DEBUG)
                int milliEnd = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds(milliEnd - milliStart).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif
            // 写入日志
            this.WriteLog(commandText);

            return dbDataReader;
        }
        #endregion

        #region public IDataReader ExecuteReader(string commandText, DbParameter[] dbParameters);
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>结果集流</returns>
        public IDataReader ExecuteReader(string commandText, DbParameter[] dbParameters)
        {
            return this.ExecuteReader(CommandType.Text, commandText, dbParameters);
        }
        #endregion

        #region public IDataReader ExecuteReader(CommandType commandType, string commandText, DbParameter[] dbParameters)
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>结果集流</returns>
        public IDataReader ExecuteReader(CommandType commandType, string commandText, DbParameter[] dbParameters)
        {
            // 写入调试信息
            #if (DEBUG)
                int milliStart = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif

            // 自动打开
            // 自动打开
            if (this.AutoOpenClose)
            {
                this.Open();
            }
            else
            {
                if (this.DbConnection == null)
                {
                    this.AutoOpenClose = true;
                    this.Open();
                }
            }

            this.dbCommand = this.DbConnection.CreateCommand();
            this.dbCommand.CommandText = commandText;
            this.dbCommand.CommandType = commandType;
            if (this.dbTransaction != null)
            {
                this.dbCommand.Transaction = this.dbTransaction;
            }

            if (dbParameters != null)
            {
                this.dbCommand.Parameters.Clear();
                for (int i = 0; i < dbParameters.Length; i++)
                {
                    if (dbParameters[i] != null)
                    {
                        this.dbCommand.Parameters.Add(dbParameters[i]);
                    }
                }
            }

            // 这里要关闭数据库才可以的
            DbDataReader dbDataReader = null;
            if (this.AutoOpenClose)
            {
                dbDataReader = this.dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            else
            {
                dbDataReader = this.dbCommand.ExecuteReader();
                this.dbCommand.Parameters.Clear();
            }

            // 写入调试信息
            #if (DEBUG)
                int milliEnd = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds(milliEnd - milliStart).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif
            // 写入日志
            this.WriteLog(commandText);

            return dbDataReader;
        }
        #endregion
        
        #region public int ExecuteNonQuery(string commandText)
        /// <summary>
        /// 执行查询, SQL BUILDER 用了这个东西？参数需要保存, 不能丢失.
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <returns>影响行数</returns>
        public int ExecuteNonQuery(string commandText)
        {
            // 写入调试信息
            #if (DEBUG)
                int milliStart = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif
            
            // 自动打开
            // 自动打开
            if (this.AutoOpenClose)
            {
                this.Open();
            }
            else
            {
                if (this.DbConnection == null)
                {
                    this.AutoOpenClose = true;
                    this.Open();
                }
            }

            this.dbCommand = this.DbConnection.CreateCommand();
            this.dbCommand.CommandType = CommandType.Text;
            this.dbCommand.CommandText = commandText;
            if (this.InTransaction)
            {
                this.dbCommand.Transaction = this.dbTransaction;
            }
          
            int returnValue = this.dbCommand.ExecuteNonQuery();

            // 自动打开
            if (this.AutoOpenClose)
            {
                this.Close();
            }

            // 写入调试信息
            #if (DEBUG)
                int milliEnd = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds(milliEnd - milliStart).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif
            // 写入日志
            this.WriteLog(commandText);
            return returnValue;
		}
        #endregion

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>影响行数</returns>
        public int ExecuteNonQuery(string commandText, DbParameter[] dbParameters)
        {
            return this.ExecuteNonQuery(CommandType.Text, commandText, dbParameters);
        }
        
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>影响行数</returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText, DbParameter[] dbParameters)
        {
            return this.ExecuteNonQuery(this.dbTransaction, commandType, commandText, dbParameters);
        }
        
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="commandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>影响行数</returns>
        public int ExecuteNonQuery(IDbTransaction transaction, CommandType commandType, string commandText, DbParameter[] dbParameters)
        {
            // 写入调试信息
            #if (DEBUG)
                int milliStart = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif

            // 自动打开
            if (this.AutoOpenClose)
            {
                this.Open();
            }
            else
            {
                if (this.DbConnection == null)
                {
                    this.AutoOpenClose = true;
                    this.Open();
                }
            }

            this.dbCommand = this.DbConnection.CreateCommand();
            this.dbCommand.CommandText = commandText;
            this.dbCommand.CommandType = commandType;
            if (this.dbTransaction != null)
            {
                this.dbCommand.Transaction = this.dbTransaction;
            }
            if (dbParameters != null)
            {
                this.dbCommand.Parameters.Clear();
                for (int i = 0; i < dbParameters.Length; i++)
                {
                    // if (dbParameters[i] != null)
                    //{
                    this.dbCommand.Parameters.Add(dbParameters[i]);
                    //}
                }
            }
            int returnValue = this.dbCommand.ExecuteNonQuery();

            // 自动关闭
            if (this.AutoOpenClose)
            {
                this.Close();
            }
            else
            {
                this.dbCommand.Parameters.Clear();
            }

            // 写入调试信息
            #if (DEBUG)
                int milliEnd = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds(milliEnd - milliStart).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif
            // 写入日志
            this.WriteLog(commandText);

            return returnValue;
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <returns>object</returns>
        public object ExecuteScalar(string commandText)
        {
			return this.ExecuteScalar(CommandType.Text, commandText, new DbParameter[0]);
        }
        
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>Object</returns>
        public object ExecuteScalar(string commandText, DbParameter[] dbParameters)
        {
            return this.ExecuteScalar(CommandType.Text, commandText, dbParameters);
        }
        
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>Object</returns>
        public object ExecuteScalar(CommandType commandType, string commandText, DbParameter[] dbParameters)
        {
            return this.ExecuteScalar(this.dbTransaction, commandType, commandText, dbParameters);
        }
        
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="commandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>Object</returns>
        public object ExecuteScalar(IDbTransaction transaction, CommandType commandType, string commandText, DbParameter[] dbParameters)
        {
            // 写入调试信息
            #if (DEBUG)
                int milliStart = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif
            
            // 自动打开
            // 自动打开
            if (this.AutoOpenClose)
            {
                this.Open();
            }
            else
            {
                if (this.DbConnection == null)
                {
                    this.AutoOpenClose = true;
                    this.Open();
                }
            }

            this.dbCommand = this.DbConnection.CreateCommand();
            this.dbCommand.CommandText = commandText;
            this.dbCommand.CommandType = commandType;
            if (this.dbTransaction != null)
            {
                this.dbCommand.Transaction = this.dbTransaction;
            }
            if (dbParameters != null)
            {
                this.dbCommand.Parameters.Clear();
                for (int i = 0; i < dbParameters.Length; i++)
                {
                    if (dbParameters[i] != null)
                    {
                        this.dbCommand.Parameters.Add(dbParameters[i]);
                    }
                }
            }
            object returnValue = this.dbCommand.ExecuteScalar();
            
            // 自动关闭
            if (this.AutoOpenClose)
            {
                this.Close();
            }
            else
            {
                this.dbCommand.Parameters.Clear();
            }

            // 写入调试信息
            #if (DEBUG)
                int milliEnd = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds(milliEnd - milliStart).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif
            // 写入日志
            this.WriteLog(commandText);
            return returnValue;
        }


        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="commandText">查询</param>
        /// <returns>数据表</returns>
        public DataTable Fill(string commandText)
        {
            DataTable dataTable = new DataTable();
            return this.Fill(dataTable, CommandType.Text, commandText, new DbParameter[0]);
        }

        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="dataTable">目标数据表</param>
        /// <param name="commandText">查询</param>
        /// <returns>数据表</returns>
        public DataTable Fill(DataTable dataTable, string commandText)
        {
            return this.Fill(dataTable, CommandType.Text, commandText, new DbParameter[0]);
        }

        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据表</returns>
        public DataTable Fill(string commandText, DbParameter[] dbParameters)
        {
            DataTable dataTable = new DataTable();
            return this.Fill(dataTable, CommandType.Text, commandText, dbParameters);
        }

        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="dataTable">目标数据表</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据表</returns>
        public DataTable Fill(DataTable dataTable, string commandText, DbParameter[] dbParameters)
        {
            return this.Fill(dataTable, CommandType.Text, commandText, dbParameters);
        }

        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="commandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据表</returns>
        public DataTable Fill(CommandType commandType, string commandText, DbParameter[] dbParameters)
        {
            DataTable dataTable = new DataTable();
            return this.Fill(dataTable, commandType, commandText, dbParameters);
        }
        
        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="dataTable">目标数据表</param>
        /// <param name="commandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据表</returns>
        public DataTable Fill(DataTable dataTable, CommandType commandType, string commandText, DbParameter[] dbParameters)
        {
            // 写入调试信息
            #if (DEBUG)
                int milliStart = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif

            // 自动打开
            if (this.AutoOpenClose)
            {
                this.Open();
            }
            else
            {
                if (this.DbConnection == null)
                {
                    this.AutoOpenClose = true;
                    this.Open();
                }
            }

            using (this.dbCommand = this.DbConnection.CreateCommand())
            {   
                //this.dbCommand.Parameters.Clear();
                //if ((dbParameters != null) && (dbParameters.Length > 0))
                //{
                //    for (int i = 0; i < dbParameters.Length; i++)
                //    {
                //        if (dbParameters[i] != null)
                //        {
                //            this.dbDataAdapter.SelectCommand.Parameters.Add(dbParameters[i]);
                //        }
                //    }
                //}
                this.dbCommand.CommandText = commandText;
                this.dbCommand.CommandType = commandType;
                if (this.InTransaction)
                {
                    // 这个不这么写，也不行，否则运行不能通过的
                    this.dbCommand.Transaction = this.dbTransaction;
                }
                this.dbDataAdapter = this.GetInstance().CreateDataAdapter();
                this.dbDataAdapter.SelectCommand = this.dbCommand;
                if ((dbParameters != null) && (dbParameters.Length > 0))
                {
                    this.dbCommand.Parameters.AddRange(dbParameters);
                }
                this.dbDataAdapter.Fill(dataTable);
                this.dbDataAdapter.SelectCommand.Parameters.Clear();
            }

            // 自动关闭
            if (this.AutoOpenClose)
            {
                this.Close();
            }

            // 写入调试信息
            #if (DEBUG)
                int milliEnd = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds(milliEnd - milliStart).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif
            // 写入日志
            this.WriteLog(commandText);
            return dataTable;
        }

        #region public DataSet Fill(DataSet dataSet, string commandText, string tableName)
        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="dataSet">目标数据集</param>
        /// <param name="commandText">查询</param>
        /// <param name="tableName">填充表</param>
        /// <returns>数据集</returns>
        public DataSet Fill(DataSet dataSet, string commandText, string tableName)
        {
            return this.Fill(dataSet, CommandType.Text, commandText, tableName, null);
        }
        #endregion

        #region public DataSet Fill(DataSet dataSet, string commandText, string tableName, DbParameter[] dbParameters)
        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="dataSet">数据集</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="tableName">填充表</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据集</returns>
        public DataSet Fill(DataSet dataSet, string commandText, string tableName, DbParameter[] dbParameters)
        {
            return this.Fill(dataSet, CommandType.Text, commandText, tableName, dbParameters);
        }
        #endregion

        #region public DataSet Fill(DataSet dataSet, CommandType commandType, string commandText, string tableName, DbParameter[] dbParameters)
        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="dataSet">数据集</param>
        /// <param name="commandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="tableName">填充表</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据集</returns>
        public DataSet Fill(DataSet dataSet, CommandType commandType, string commandText, string tableName, DbParameter[] dbParameters)
        {
            // 写入调试信息
            #if (DEBUG)
                int milliStart = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif

            // 自动打开
            if (this.AutoOpenClose)
            {
                this.Open();
            }
            else
            {
                if (this.DbConnection == null)
                {
                    this.AutoOpenClose = true;
                    this.Open();
                }
            }

            using (this.dbCommand = this.DbConnection.CreateCommand())
            {
                //this.dbCommand.Parameters.Clear();
                //if ((dbParameters != null) && (dbParameters.Length > 0))
                //{
                //    for (int i = 0; i < dbParameters.Length; i++)
                //    {
                //        if (dbParameters[i] != null)
                //        {
                //            this.dbDataAdapter.SelectCommand.Parameters.Add(dbParameters[i]);
                //        }
                //    }
                //}
                this.dbCommand.CommandText = commandText;
                this.dbCommand.CommandType = commandType;
                if ((dbParameters != null) && (dbParameters.Length > 0))
                {
                    this.dbCommand.Parameters.AddRange(dbParameters);
                }

                this.dbDataAdapter = this.GetInstance().CreateDataAdapter();
                this.dbDataAdapter.SelectCommand = this.dbCommand;
                this.dbDataAdapter.Fill(dataSet, tableName);

                if (this.AutoOpenClose)
                {
                    this.Close();
                }
                else
                {
                    this.dbDataAdapter.SelectCommand.Parameters.Clear();
                }
            }

            // 写入调试信息
            #if (DEBUG)
                int milliEnd = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds(milliEnd - milliStart).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif
            // 写入日志
            this.WriteLog(commandText);
            return dataSet;
        }
        #endregion

        #region public int ExecuteProcedure(string procedureName)
        /// <summary>
        /// 执行数据库查询
        /// </summary>
        /// <param name="procedureName">存储过程</param>
        /// <returns>int</returns>
        public int ExecuteProcedure(string procedureName)
        {
            return this.ExecuteNonQuery(CommandType.StoredProcedure, procedureName, new DbParameter[0]);
        }
        #endregion

        #region public int ExecuteProcedure(string procedureName, DbParameter[] dbParameters)
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procedureName">存储过程名</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>影响行数</returns>
        public int ExecuteProcedure(string procedureName, DbParameter[] dbParameters)
        {
            return this.ExecuteNonQuery(CommandType.StoredProcedure, procedureName, dbParameters);
        }
        #endregion

        #region public DataTable ExecuteProcedureForDataTable(string procedureName, string tableName, DbParameter[] dbParameters)
        /// <summary>
        /// 执行数据库脚本
        /// </summary>
        /// <param name="procedureName">存储过程</param>
        /// <param name="tableName">填充表</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据集</returns>
        public DataTable ExecuteProcedureForDataTable(string procedureName, string tableName, DbParameter[] dbParameters)
        {
            DataTable dataTable = new DataTable(tableName);
            this.Fill(dataTable, CommandType.StoredProcedure, procedureName, dbParameters);
            return dataTable;
        }
        #endregion

        #region public IDbTransaction BeginTransaction()
        /// <summary>
        /// 事务开始
        /// </summary>
        /// <returns>事务</returns>
        public IDbTransaction BeginTransaction()
        {
            // 写入调试信息
            #if (DEBUG)
                int milliStart = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif

            if (!this.InTransaction)
            {
                this.InTransaction = true;
                this.dbTransaction = this.DbConnection.BeginTransaction();
                // this.dbCommand.Transaction = this.dbTransaction;
            }

            // 写入调试信息
            #if (DEBUG)
                int milliEnd = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds(milliEnd - milliStart).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif

            return this.dbTransaction;
        }
        #endregion

        #region public void CommitTransaction()
        /// <summary>
        /// 递交事务
        /// </summary>
        public void CommitTransaction()
        {
            // 写入调试信息
            #if (DEBUG)
                int milliStart = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif

            if (this.InTransaction)
            {
                // 事务已经完成了，一定要更新标志信息
                this.InTransaction = false;
                this.dbTransaction.Commit();
            }

            // 写入调试信息
            #if (DEBUG)
                int milliEnd = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds(milliEnd - milliStart).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif
        }
        #endregion

        #region public void RollbackTransaction()
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTransaction()
        {
            // 写入调试信息
            #if (DEBUG)
                int milliStart = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif

            if (this.InTransaction)
            {
                this.InTransaction = false;
                this.dbTransaction.Rollback();
            }

            // 写入调试信息
            #if (DEBUG)
                int milliEnd = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds(milliEnd - milliStart).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif
        }
        #endregion

        #region public void Close()
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            // 写入调试信息
            #if (DEBUG)
                int milliStart = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif

            if (this.dbConnection != null)
            {
                this.dbConnection.Close();
                this.dbConnection.Dispose();
            }
            this.dbConnection = null;
            this.dbCommand = null;
            this.dbDataAdapter = null;
            this.dbTransaction = null;
            // 写入调试信息
            #if (DEBUG)
                int milliEnd = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds(milliEnd - milliStart).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            #endif
        }
        #endregion

        #region public void WriteLog(string commandText) 写入sql查询句日志
        /// <summary>
        /// 写入sql查询日志
        /// </summary>
        /// <param name="commandText">sql查询</param>
        public void WriteLog(string commandText)
        {
            this.WriteLog(DateTime.Now.ToString(BaseSystemInfo.DateFormat) + " _ " + this.FileName, commandText);
            
            // 将调试信息输出到屏幕上
            #if DEBUG
                System.Console.WriteLine(commandText);
            #endif
        }
        #endregion

        #region public void WriteLog(string fileName, string commandText) 写入sql查询句日志
        /// <summary>
        /// 写入sql查询句
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="commandText">异常</param>
        public void WriteLog(string fileName, string commandText)
        {
            string returnValue = string.Empty;
            // 系统里应该可以配置是否记录异常现象
            if (!BaseSystemInfo.LogSQL)
            {
                return;
            }
            // 将异常信息写入本地文件中
            string logDirectory = BaseSystemInfo.StartupPath + @"\\Log\\Query";
            if (!System.IO.Directory.Exists(logDirectory))
            {
                System.IO.Directory.CreateDirectory(logDirectory);
            }
            string writerFileName = logDirectory + "\\" + fileName;
            if (!File.Exists(writerFileName))
            {
                FileStream FileStream = new FileStream(writerFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                FileStream.Close();
            }
            StreamWriter streamWriter = new StreamWriter(writerFileName, true, Encoding.Default);
            streamWriter.WriteLine(DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + " " + commandText);
            streamWriter.Close();
        }
        #endregion
    }
}