//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

using System;
using System.Globalization;

namespace CSharpUtils.Configuration
{
    /// <summary>
    /// BaseConfiguration
    /// 连接配置。
    /// 
    /// 修改纪录
    /// 
    ///		2008.06.08 版本：3.5 JiRiGaLa 将读取配置文件进行分离。
    ///		2008.05.08 版本：3.4 JiRiGaLa 获得不同的数据库连接字符串 OracleConnection、SqlConnection、OleDbConnection。
    ///		2007.11.28 版本：3.2 JiRiGaLa 获得数据连接字符串，减少配置文件的读的次序，提高性能。
    ///		2007.05.23 版本：3.1 JiRiGaLa 增加 public const string 定义部分。
    ///		2007.04.14 版本：3.0 JiRiGaLa 检查程序格式通过，不再进行修改主键操作。
    ///		2006.11.17 版本：2.4 JiRiGaLa GetFromRegistryKey() 方法主键进行整理，删除多余的引用。
    ///		2006.05.02 版本：2.3 JiRiGaLa GetFromConfig 增加从配置文件读取数据库联接的方法。
    ///		2006.04.18 版本：2.2 JiRiGaLa 重新调整主键的规范化。
    ///		2006.02.02 版本：2.0 JiRiGaLa 删除数据库连接池的想法，修改了命名，更规范化，贴切了。 
    ///		2005.12.29 版本：1.0 JiRiGaLa 从配置文件读取数据库连接。
    /// 
    /// 版本：3.5
    /// 
    /// <author>

    ///		<date>2008.06.08</date>
    /// </author> 
    /// </summary>
    public class BaseConfiguration
    {
        public const string CURRENT_LOGIN_TO    = "LoginTo";
        public const string COMPANY_NAME        = "Jirisoft";

        public const string REMEMBER_PASSWORD   = "RememberPassword";
        public const string CURRENT_USERNAME    = "CurrentUsername";
        public const string CURRENT_PASSWORD    = "CurrentPassword";

        // 客户信息配置
        public const string CUSTOMER_COMPANYNAME    = "CustomerCompanyName";
        public const string CONFIGURATION_FROM      = "ConfigurationFrom";
        public const string SOFT_NAME               = "SoftName";
        public const string SOFT_FULLNAME           = "SoftFullName";
        public const string RUN_MODE                = "RunMode";
        public const string ROOT_MENU_CODE            = "RootMenuCode";
        public const string CURRENT_LANGUAGE        = "CurrentLanguage";
        public const string VERSION                 = "Version";
        
        
        // 这里是服务器设置项
        public const string WEBSERVICE              = "WebService";
        public const string WEBSERVICE_USERNAME     = "WebServiceUsername";
        public const string WEBSERVICE_PASSWORD     = "WebServicePassword";

        public const string USE_PERMISSIONS_COPE     = "UsePermissionScope";          // 启用分级管理范围权限设置
        public const string USE_LICENSED_PERMISSION = "UseLicensedPermission";  // 启用逐级授权

        public const string SERVICE_FACTORY         = "ServiceFactory";
        public const string SERVICE_PATH            = "ServicePath";
        public const string DBHELPER_CLASSNAME      = "DbHelperClass";
        public const string DBHELPER_ASSMELY        = "DbHelperAssmely";
        public const string RECORD_LOG              = "RecordLog";        

        // 登录窗体
        public const string LOGIN_ASSEMBLY      = "LoginAssembly";
        public const string LOGIN_FORM          = "LoginForm";
        public const string MAIN_FORM           = "MainForm";

        public const string LOAD_USER           = "LoadUser";
        public const string ALLOW_USER_REGISTER = "AllowUserRegister";
        
        // 数据库连接
        public const string USERCENTER_DBCONNECTION = "UserCenterDbConnection";
        public const string BUSINESS_DBCONNECTION = "BusinessDbConnection";
        
        public const string DATABASE_TYPE       = "DataBaseType";
        public const string REGISTER_KEY        = "RegisterKey";

        // 错误报告
        public const string ERROR_REPORT_TO = "ErrorReportTo";
        public const string ERROR_REPORT_MAIL_USERNAME = "ErrorReportMailUsername";
        public const string ERROR_REPORT_MAIL_PASSWORD = "ErrorReportMailPassword";

        #region public BaseConfiguration()
        /// <summary>
        /// 构造方法
        /// </summary>
        public BaseConfiguration()
        {
        }
        #endregion

        #region public BaseConfiguration(string softName)
        /// <summary>
        /// 设定当前软件ID
        /// </summary>
        /// <param name="softName">当前软件ID</param>
        public BaseConfiguration(string softName)
        {
            BaseSystemInfo.SoftName = softName;
        }
        #endregion

        #region public static DataBaseType GetDataBaseType(string dataBase)
        /// <summary>
        /// 数据库连接的类型判断
        /// </summary>
        /// <param name="dataBase">数据库类型</param>
        /// <returns>数据库类型</returns>
        public static DataBaseType GetDataBaseType(string dataBase)
        {
            DataBaseType returnValue = DataBaseType.Sqlserver;
            foreach (DataBaseType dataBaseType in Enum.GetValues(typeof(DataBaseType)))
            {
                if (dataBaseType.ToString().Equals(dataBase))
                {
                    returnValue = dataBaseType;
                    break;
                }
            }
            return returnValue;
        }
        #endregion

        #region public static ConfigurationCategory GetConfiguration(string configuration)
        /// <summary>
        /// 配置信息的类型判断
        /// </summary>
        /// <param name="configuration">配置信息类型</param>
        /// <returns>配置信息类型</returns>
        public static ConfigurationCategory GetConfiguration(string configuration)
        {
            ConfigurationCategory returnValue = ConfigurationCategory.Configuration;
            foreach (ConfigurationCategory configurationCategory in Enum.GetValues(typeof(ConfigurationCategory)))
            {
                if (configurationCategory.ToString().Equals(configuration))
                {
                    returnValue = configurationCategory;
                    break;
                }
            }
            return returnValue;
        }
        #endregion

        #region public static GetRunMode(string runMode)
        /// <summary>
        /// 运行模式配置
        /// </summary>
        /// <param name="runMode">运行模式</param>
        /// <returns>配置信息类型</returns>
        public static RunMode GetRunMode(string runMode)
        {
            RunMode returnValue = RunMode.Local;
            foreach (RunMode mode in Enum.GetValues(typeof(RunMode)))
            {
                if (mode.ToString().Equals(runMode))
                {
                    returnValue = mode;
                    break;
                }
            }
            return returnValue;
        }
        #endregion

        #region public static void GetSetting()
        /// <summary>
        /// 读取配置信息
        /// </summary>
        public static void GetSetting()
        {
            if (BaseSystemInfo.ConfigurationFrom == ConfigurationCategory.Configuration)
            {
                ConfigurationUtil.GetConfig();
            }
            else if (BaseSystemInfo.ConfigurationFrom == ConfigurationCategory.UserConfig)
            {
                ConfigUtil.GetConfig();
            }
            else if (BaseSystemInfo.ConfigurationFrom == ConfigurationCategory.RegistryKey)
            {
                RegistryUtil.GetConfig();
            }
        }
        #endregion
    }
}