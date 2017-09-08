//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace CSharpUtils.Configuration
{
    /// <summary>
    /// ConfigHelper
    /// 访问用户配置文件的类。
    /// 
    /// 修改纪录
    ///
    ///		2008.06.08 版本：1.3 JiRiGaLa 命名修改为 ConfigHelper。
    ///		2008.04.22 版本：1.2 JiRiGaLa 从指定的文件读取配置项。
    ///		2007.07.31 版本：1.1 JiRiGaLa 规范化 FielName 变量。
    ///		2007.04.14 版本：1.0 JiRiGaLa 专门读取注册表的类，主键书写格式改进。
    ///		
    ///	版本：1.2
    /// 
    /// <author>

    ///		<date>2008.04.22</date>
    /// </author> 
    /// </summary>
    public class ConfigUtil
    {
        public static string LoginTo = "Config";

        public static string FileName
        {
            get
            {
                return LoginTo + ".xml";
            }
        }

        public static string SelectPath = "//appSettings/add";

        public static string ConfigFielName
        {
            get
            {
                return FileName;
                // return Application.StartupPath + "\\" + FielName;
            }
        }

        #region public static Dictionary<String, String> GetLoginTo() 获取配置文件选项
        /// <summary>
        /// 获取配置文件选项
        /// </summary>
        /// <returns>配置文件设置</returns>
        public static Dictionary<String, String> GetLoginTo()
        {
            Dictionary<String, String> returnValue = new Dictionary<String, String>();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(ConfigFielName);
            XmlNodeList xmlNodeList = xmlDocument.SelectNodes(SelectPath);
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                if (xmlNode.Attributes["key"].Value.ToUpper().Equals(BaseConfiguration.CURRENT_LOGIN_TO.ToUpper()))
                {
                    returnValue.Add(xmlNode.Attributes["value"].Value, xmlNode.Attributes["dispaly"].Value);
                }
            }
            return returnValue;
        }
        #endregion      

        #region public static string GetValue(string key) 读取配置项
        /// <summary>
        /// 读取配置项
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetValue(string key)
        {
            return GetValue(ConfigFielName, SelectPath, key);
        }
        #endregion

        #region public static string GetValue(string fileName, string key) 读取配置项
        /// <summary>
        /// 读取配置项
        /// </summary>
        /// <param name="fileName">配置文件</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetValue(string fileName, string key)
        {
            return GetValue(fileName, SelectPath, key);
        }
        #endregion

        #region public static string GetValue(string fileName, string selectPath, string key) 设置配置项
        /// <summary>
        /// 读取配置项
        /// </summary>
        /// <param name="fileName">配置文件</param>
        /// <param name="selectPath">查询条件</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetValue(string fileName, string selectPath, string key)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            return GetValue(xmlDocument, selectPath, key);
        }
        #endregion

        #region public static string GetValue(XmlDocument xmlDocument, string key) 读取配置项
        /// <summary>
        /// 读取配置项
        /// </summary>
        /// <param name="xmlDocument">配置文件</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetValue(XmlDocument xmlDocument, string key)
        {
            return GetValue(xmlDocument, SelectPath, key);
        }
        #endregion

        #region public static string GetValue(XmlDocument xmlDocument, string selectPath, string key) 设置配置项
        /// <summary>
        /// 读取配置项
        /// </summary>
        /// <param name="xmlDocument">配置文件</param>
        /// <param name="selectPath">查询条件</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetValue(XmlDocument xmlDocument, string selectPath, string key)
        {
            string returnValue = string.Empty;
            XmlNodeList xmlNodeList = xmlDocument.SelectNodes(selectPath);
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                if (xmlNode.Attributes["key"].Value.ToUpper().Equals(key.ToUpper()))
                {
                    returnValue = xmlNode.Attributes["value"].Value;
                    break;
                }
            }
            return returnValue;
        }
        #endregion

        #region public static void GetConfig() 读取配置文件
        /// <summary>
        /// 读取配置文件
        /// </summary>
        public static void GetConfig()
        {
            GetConfig(ConfigFielName);
        }
        #endregion

        #region public static void GetConfig(string fileName) 从指定的文件读取配置项
        /// <summary>
        /// 从指定的文件读取配置项
        /// </summary>
        /// <param name="fileName">配置文件</param>
        public static void GetConfig(string fileName)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);

            // 客户信息配置
            BaseSystemInfo.CustomerCompanyName = GetValue(xmlDocument, BaseConfiguration.CUSTOMER_COMPANYNAME);
            BaseSystemInfo.ConfigurationFrom = BaseConfiguration.GetConfiguration(GetValue(xmlDocument, BaseConfiguration.CONFIGURATION_FROM));
            BaseSystemInfo.SoftName = GetValue(xmlDocument, BaseConfiguration.SOFT_NAME);
            BaseSystemInfo.SoftFullName = GetValue(xmlDocument, BaseConfiguration.SOFT_FULLNAME);
            BaseSystemInfo.CurrentUsername = GetValue(xmlDocument, BaseConfiguration.CURRENT_USERNAME);
            BaseSystemInfo.CurrentPassword = GetValue(xmlDocument, BaseConfiguration.CURRENT_PASSWORD);
            BaseSystemInfo.RememberPassword = (String.Compare(GetValue(xmlDocument, BaseConfiguration.REMEMBER_PASSWORD), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            BaseSystemInfo.RunMode = BaseConfiguration.GetRunMode(GetValue(xmlDocument, BaseConfiguration.RUN_MODE));
            BaseSystemInfo.RootMenuCode = GetValue(xmlDocument, BaseConfiguration.ROOT_MENU_CODE);
            BaseSystemInfo.CurrentLanguage = GetValue(xmlDocument, BaseConfiguration.CURRENT_LANGUAGE);
            BaseSystemInfo.Version = GetValue(xmlDocument, BaseConfiguration.VERSION);            

            // 这里是服务器设置项
            BaseSystemInfo.WebService = GetValue(xmlDocument, BaseConfiguration.WEBSERVICE);
            BaseSystemInfo.WebServiceUsername = GetValue(xmlDocument, BaseConfiguration.WEBSERVICE_USERNAME);
            BaseSystemInfo.WebServicePassword = GetValue(xmlDocument, BaseConfiguration.WEBSERVICE_PASSWORD);

            BaseSystemInfo.UsePermissionScope = (String.Compare(GetValue(xmlDocument, BaseConfiguration.USE_PERMISSIONS_COPE), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            BaseSystemInfo.UseLicensedPermission = (String.Compare(GetValue(xmlDocument, BaseConfiguration.USE_LICENSED_PERMISSION), "TRUE", true, CultureInfo.CurrentCulture) == 0);

            BaseSystemInfo.ServiceFactory = GetValue(xmlDocument, BaseConfiguration.SERVICE_FACTORY);
            BaseSystemInfo.ServicePath = GetValue(xmlDocument, BaseConfiguration.SERVICE_PATH);
            BaseSystemInfo.DbHelperClass = GetValue(xmlDocument, BaseConfiguration.DBHELPER_CLASSNAME);
            BaseSystemInfo.DbHelperAssmely = GetValue(xmlDocument, BaseConfiguration.DBHELPER_ASSMELY);

            BaseSystemInfo.RecordLog = (String.Compare(GetValue(xmlDocument, BaseConfiguration.RECORD_LOG), "TRUE", true, CultureInfo.CurrentCulture) == 0);

            BaseSystemInfo.LoginAssembly = GetValue(xmlDocument, BaseConfiguration.LOGIN_ASSEMBLY);
            BaseSystemInfo.LoginForm = GetValue(xmlDocument, BaseConfiguration.LOGIN_FORM);
            BaseSystemInfo.MainForm = GetValue(xmlDocument, BaseConfiguration.MAIN_FORM);

            BaseSystemInfo.LoadUser = (String.Compare(GetValue(xmlDocument, BaseConfiguration.LOAD_USER), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            BaseSystemInfo.AllowUserRegister = (String.Compare(GetValue(xmlDocument, BaseConfiguration.ALLOW_USER_REGISTER), "TRUE", true, CultureInfo.CurrentCulture) == 0); 

            // 打开数据库连接
            BaseSystemInfo.BusinessDbConnection = GetValue(xmlDocument, BaseConfiguration.BUSINESS_DBCONNECTION);
            BaseSystemInfo.UserCenterDbConnection = GetValue(xmlDocument, BaseConfiguration.USERCENTER_DBCONNECTION);

            BaseSystemInfo.DataBaseType = BaseConfiguration.GetDataBaseType(GetValue(xmlDocument, BaseConfiguration.DATABASE_TYPE));
            BaseSystemInfo.RegisterKey = GetValue(xmlDocument, BaseConfiguration.REGISTER_KEY);

            // 错误报告相关
            BaseSystemInfo.ErrorReportTo = GetValue(xmlDocument, BaseConfiguration.ERROR_REPORT_TO);
            BaseSystemInfo.ErrorReportMailUsername = GetValue(xmlDocument, BaseConfiguration.ERROR_REPORT_MAIL_USERNAME);
            BaseSystemInfo.ErrorReportMailPassword = GetValue(xmlDocument, BaseConfiguration.ERROR_REPORT_MAIL_PASSWORD);
        }
        #endregion

        public static bool SetValue(XmlDocument xmlDocument, string key, string keyValue)
        {
            return SetValue(xmlDocument, SelectPath, key, keyValue);
        }

        public static bool SetValue(XmlDocument xmlDocument, string selectPath, string key, string keyValue)
        {
            bool returnValue = false;
            XmlNodeList xmlNodeList = xmlDocument.SelectNodes(selectPath);
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                if (xmlNode.Attributes["key"].Value.ToUpper().Equals(key.ToUpper()))
                {
                    xmlNode.Attributes["value"].Value = keyValue;
                    returnValue = true;
                    break;
                }
            }
            return returnValue;
        }

        #region public static void SaveConfig() 保存配置文件
        /// <summary>
        /// 保存配置文件
        /// </summary>
        public static void SaveConfig()
        {
            SaveConfig(ConfigFielName);
        }
        #endregion

        #region public static void SaveConfig(string fileName) 保存到指定的文件
        /// <summary>
        /// 保存到指定的文件
        /// </summary>
        /// <param name="fileName">配置文件</param>
        public static void SaveConfig(string fileName)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);

            // 客户信息配置
            SetValue(xmlDocument, BaseConfiguration.CUSTOMER_COMPANYNAME, BaseSystemInfo.CustomerCompanyName);
            SetValue(xmlDocument, BaseConfiguration.CONFIGURATION_FROM, BaseSystemInfo.ConfigurationFrom.ToString());
            SetValue(xmlDocument, BaseConfiguration.SOFT_NAME, BaseSystemInfo.SoftName);
            SetValue(xmlDocument, BaseConfiguration.SOFT_FULLNAME, BaseSystemInfo.SoftFullName);
            SetValue(xmlDocument, BaseConfiguration.CURRENT_USERNAME, BaseSystemInfo.CurrentUsername);
            SetValue(xmlDocument, BaseConfiguration.CURRENT_PASSWORD, BaseSystemInfo.CurrentPassword);
            SetValue(xmlDocument, BaseConfiguration.REMEMBER_PASSWORD, BaseSystemInfo.RememberPassword.ToString());
            SetValue(xmlDocument, BaseConfiguration.RUN_MODE, BaseSystemInfo.RunMode.ToString());
            SetValue(xmlDocument, BaseConfiguration.ROOT_MENU_CODE, BaseSystemInfo.RootMenuCode);
            SetValue(xmlDocument, BaseConfiguration.CURRENT_LANGUAGE, BaseSystemInfo.CurrentLanguage);
            SetValue(xmlDocument, BaseConfiguration.VERSION, BaseSystemInfo.Version);

            // 这里是服务器设置项
            SetValue(xmlDocument, BaseConfiguration.WEBSERVICE, BaseSystemInfo.WebService);
            SetValue(xmlDocument, BaseConfiguration.WEBSERVICE_USERNAME, BaseSystemInfo.WebServiceUsername);
            SetValue(xmlDocument, BaseConfiguration.WEBSERVICE_PASSWORD, BaseSystemInfo.WebServicePassword);

            SetValue(xmlDocument, BaseConfiguration.USE_PERMISSIONS_COPE, BaseSystemInfo.UsePermissionScope.ToString());
            SetValue(xmlDocument, BaseConfiguration.USE_LICENSED_PERMISSION, BaseSystemInfo.UseLicensedPermission.ToString());

            SetValue(xmlDocument, BaseConfiguration.SERVICE_FACTORY, BaseSystemInfo.ServiceFactory);
            SetValue(xmlDocument, BaseConfiguration.SERVICE_PATH, BaseSystemInfo.ServicePath);
            SetValue(xmlDocument, BaseConfiguration.DBHELPER_CLASSNAME, BaseSystemInfo.DbHelperClass);
            SetValue(xmlDocument, BaseConfiguration.DBHELPER_ASSMELY, BaseSystemInfo.DbHelperAssmely);

            SetValue(xmlDocument, BaseConfiguration.RECORD_LOG, BaseSystemInfo.RecordLog.ToString());

            SetValue(xmlDocument, BaseConfiguration.LOGIN_ASSEMBLY, BaseSystemInfo.LoginAssembly);
            SetValue(xmlDocument, BaseConfiguration.LOGIN_FORM, BaseSystemInfo.LoginForm);
            SetValue(xmlDocument, BaseConfiguration.MAIN_FORM, BaseSystemInfo.MainForm);

            SetValue(xmlDocument, BaseConfiguration.LOAD_USER, BaseSystemInfo.LoadUser.ToString());
            SetValue(xmlDocument, BaseConfiguration.ALLOW_USER_REGISTER, BaseSystemInfo.AllowUserRegister.ToString());

            // 打开数据库连接
            SetValue(xmlDocument, BaseConfiguration.BUSINESS_DBCONNECTION, BaseSystemInfo.BusinessDbConnection);
            SetValue(xmlDocument, BaseConfiguration.USERCENTER_DBCONNECTION, BaseSystemInfo.UserCenterDbConnection);

            SetValue(xmlDocument, BaseConfiguration.DATABASE_TYPE, BaseSystemInfo.DataBaseType.ToString());
            SetValue(xmlDocument, BaseConfiguration.REGISTER_KEY, BaseSystemInfo.RegisterKey);

            xmlDocument.Save(fileName);
        }
        #endregion
    }
}