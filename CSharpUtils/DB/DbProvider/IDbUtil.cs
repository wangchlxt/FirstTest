﻿//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

using System;
using System.Data;
using System.Data.Common;

namespace CSharpUtils.DB.DbProvider
{
    using Configuration;
    using CSharpUtils;

    /// <summary>
    /// IDbHelper
    /// 数据库访问通用类标准接口。
    /// 
    /// 修改纪录
    /// 
    ///		2008.08.26 版本：2.0 JiRiGaLa 将主键进行精简整理。
    ///		2008.08.25 版本：1.3 JiRiGaLa 将标准数据库接口方法进行分离、分离为标准接口方法与扩展接口方法部分。
    ///		2008.06.03 版本：1.2 JiRiGaLa 增加 DbParameter[] dbParameters 方法。
    ///		2008.05.07 版本：1.1 JiRiGaLa 增加GetWhereString定义。
    ///		2008.03.20 版本：1.0 JiRiGaLa 创建标准接口，这次应该算是一次飞跃。
    /// 
    /// 版本：2.0
    /// 
    /// <author>

    ///		<date>2008.08.26</date>
    /// </author> 
    /// </summary>
    public interface IDbUtil
    {
        /// <summary>
        /// 创建提供程序对数据源类的实现的实例。
        /// </summary>
        /// <returns>数据源类的实现的实例</returns>
        DbProviderFactory GetInstance();

        /// <summary>
        /// 当前数据库类型
        /// </summary>
        DataBaseType CurrentDataBaseType { get; }

        /// <summary>
        /// 默认打开关闭数据库选项（默认为否）
        /// </summary>
        bool AutoOpenClose{ get; set; }
        
        string ConnectionString { get; set; }

        /// <summary>
        /// 获得数据库当前日期
        /// </summary>
        /// <returns>当前日期</returns>
        string GetDBNow();
        
        /// <summary>
        /// 获得数据库当前日期
        /// </summary>
        /// <returns>当前日期</returns>
        string GetDBDateTime();
        
        /// <summary>
        /// 是否已在事务之中
        /// </summary>
        /// <returns></returns>
        bool InTransaction
        {
            get;
            set;
        }
        
        /// <summary>
        /// 检查参数的安全性
        /// </summary>
        /// <param name="value">参数</param>
        /// <returns>安全的参数</returns>
        string SqlSafe(string value);
        
        /// <summary>
        ///  获得Sql字符串相加符号
        /// </summary>
        /// <param name="values">参数值</param>
        /// <returns>字符加</returns>
        string PlusSign(params string[] values);
        

        /// <summary>
        /// 获得Sql参数表达式
        /// </summary>
        /// <param name="parameter">参数名称</param>
        /// <returns>字符串</returns>
        string GetParameter(string parameter);
        
        /// <summary>
        /// 生成参数
        /// </summary>
        /// <param name="targetFiled">目标字段</param>
        /// <param name="targetValue">值</param>
        /// <returns>参数</returns>
        DbParameter MakeInParam(string targetFiled, object targetValue);
        
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="targetFiled">目标字段</param>
        /// <param name="targetValue">值</param>
        /// <returns>参数</returns>
        DbParameter[] MakeParameters(string targetFiled, object targetValue);
        
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="targetFiled">目标字段</param>
        /// <param name="targetValue">值</param>
        /// <returns>参数集</returns>
        DbParameter[] MakeParameters(string[] targetFileds, Object[] targetValues);
        
        /// <summary>
        /// 生成参数
        /// </summary>
        /// <param name="paramName">目标字段</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="size">长度</param>
        /// <param name="direction">输入输出类型</param>
        /// <param name="value">值</param>
        /// <returns>参数</returns>
        DbParameter MakeParam(string paramName, DbType dbType, Int32 size, ParameterDirection direction, object value);
        
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns>数据库连接</returns>
        IDbConnection GetDbConnection();
        
        /// <summary>
        /// 获取数据源上执行的事务
        /// </summary>
        /// <returns>数据源上执行的事务</returns>
        IDbTransaction GetDbTransaction();
        
        /// <summary>
        /// 获取数据源上命令
        /// </summary>
        /// <returns>数据源上命令</returns>
        IDbCommand GetDbCommand();
        
        /// <summary>
		/// 打开数据库连接
		/// </summary>
		/// <returns>数据库连接</returns>
        IDbConnection Open();
		
        /// <summary>
        /// 打开数据库连接
		/// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
		/// <returns>数据库连接</returns>
        IDbConnection Open(string connectionString);
        
        /// <summary>
        /// 事务开始
        /// </summary>
        /// <returns>事务</returns>
        IDbTransaction BeginTransaction();
        
        /// <summary>
        /// 递交事务
        /// </summary>
        void CommitTransaction();
        
        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollbackTransaction();
        
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        void Close();
        
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <returns>结果集流</returns>
        IDataReader ExecuteReader(string commandText);
        
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>结果集流</returns>
        IDataReader ExecuteReader(string commandText, DbParameter[] dbParameters);
        
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>结果集流</returns>
        IDataReader ExecuteReader(CommandType commandType, string commandText, DbParameter[] dbParameters);
        
        /// <summary>
        /// 执行查询, SQL BUILDER 用了这个东西？参数需要保存, 不能丢失.
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <returns>影响行数</returns>
        int ExecuteNonQuery(string commandText);
        
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>影响行数</returns>
        int ExecuteNonQuery(string commandText, DbParameter[] dbParameters);
        
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>影响行数</returns>
        int ExecuteNonQuery(CommandType commandType, string commandText, DbParameter[] dbParameters);

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="commandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>影响行数</returns>
        int ExecuteNonQuery(IDbTransaction dbTransaction, CommandType commandType, string commandText, DbParameter[] dbParameters);

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <returns>Object</returns>
        object ExecuteScalar(string commandText);
        
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>Object</returns>
        object ExecuteScalar(string commandText, DbParameter[] dbParameters);
        
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>Object</returns>
        object ExecuteScalar(CommandType commandType, string commandText, DbParameter[] dbParameters);
        
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="commandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>Object</returns>
        object ExecuteScalar(IDbTransaction dbTransaction, CommandType commandType, string commandText, DbParameter[] dbParameters);

        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="commandText">查询</param>
        /// <returns>数据表</returns>
        DataTable Fill(string commandText);

        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="dataTable">目标数据表</param>
        /// <param name="commandText">查询</param>
        /// <returns>数据表</returns>
        DataTable Fill(DataTable dataTable, string commandText);

        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据表</returns>
        DataTable Fill(string commandText, DbParameter[] dbParameters);

        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="dataTable">目标数据表</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据表</returns>
        DataTable Fill(DataTable dataTable, string commandText, DbParameter[] dbParameters);

        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="CommandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据表</returns>
        DataTable Fill(CommandType commandType, string commandText, DbParameter[] dbParameters);

        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="dataTable">目标数据表</param>
        /// <param name="commandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据表</returns>
        DataTable Fill(DataTable dataTable, CommandType commandType, string commandText, DbParameter[] dbParameters);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="dataSet">目标数据集</param>
        /// <param name="commandText">查询</param>
        /// <param name="tableName">填充表</param>
        /// <returns>数据集</returns>
        DataSet Fill(DataSet dataSet, string commandText, string tableName);
        
        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="dataSet">数据集</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="tableName">填充表</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据集</returns>
        DataSet Fill(DataSet dataSet, string commandText, string tableName, DbParameter[] dbParameters);
        
        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="dataSet">数据集</param>
        /// <param name="commandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="tableName">填充表</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据集</returns>
        DataSet Fill(DataSet dataSet, CommandType commandType, string commandText, string tableName, DbParameter[] dbParameters);
        
        /// <summary>
        /// 执行数据库查询
        /// </summary>
        /// <param name="procedureName">存储过程</param>
        /// <returns>影响行数</returns>
        int ExecuteProcedure(string procedureName);
        
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procedureName">存储过程名</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>影响行数</returns>
        int ExecuteProcedure(string procedureName, DbParameter[] dbParameters);
        
        /// <summary>
        /// 执行数据库脚本
        /// </summary>
        /// <param name="procedureName">存储过程</param>
        /// <param name="tableName">填充表</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据集</returns>
        DataTable ExecuteProcedureForDataTable(string procedureName, string tableName, DbParameter[] dbParameters);
        
        /// <summary>
        /// 写入sql查询日志
        /// </summary>
        /// <param name="commandText">sql查询</param>
        void WriteLog(string commandText);
    }
}