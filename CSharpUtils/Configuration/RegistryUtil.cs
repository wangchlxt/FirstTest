//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

using System;
using Microsoft.Win32;
using System.Globalization;

namespace CSharpUtils.Configuration
{
    /// <summary>
    /// RegistryHelper
    /// 访问注册表的类。
    /// 
    /// 修改纪录
    ///
    ///		2008.06.08 版本：3.2 JiRiGaLa 命名修改为 RegistryHelper。 
    ///		2007.07.30 版本：3.1 JiRiGaLa Exists 函数名规范化。 
    ///		2007.04.14 版本：3.0 JiRiGaLa 检查程序格式通过，不再进行修改主键操作。 
    ///     2006.11.17 版本：2.2 JiRiGaLa 添加方法CheckExistSubKey()。
    ///     2006.09.08 版本：2.1 JiRiGaLa 变量命名规范化。
    ///     2006.04.18 版本：2.0 JiRiGaLa 重新调整主键的规范化。
    ///		2005.08.08 版本：1.0 JiRiGaLa 专门读取注册表的类，主键书写格式改进。
    ///		
    ///	版本：3.0
    /// 
    /// <author>

    ///		<date>2007.04.14</date>
    /// </author> 
    /// </summary>
    public class RegistryUtil
    {
        /// <summary>
        /// 注册表中的位置
        /// </summary>
        public static string SubKey = "Software\\" + BaseConfiguration.COMPANY_NAME;

        #region public static string GetValue(string key) 读取注册表
        /// <summary>
		/// 读取注册表
		/// </summary>
        /// <param name="subKey">注册表子项</param>
		/// <param name="registryKey">键</param>
		/// <returns>值</returns>
        public static string GetValue(string key)
		{
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(SubKey, false);
            return (string)registryKey.GetValue(key);
		}
		#endregion

        #region public static void SetValue(string key, string keyValue) 设置注册表
        /// <summary>
        /// 设置注册表
        /// </summary>
        /// <param name="subKey">注册表子项</param>
        /// <param name="registryKey">键</param>
        /// <param name="keyValue">值</param>
        public static void SetValue(string key, string keyValue)
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(SubKey, true);
            registryKey.SetValue(key, keyValue);
        }
        #endregion

        #region public static bool Exists(string key) 判断一个注册表项是否存在
        /// <summary>
        /// 判断一个注册表项是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>是否存在</returns>
        public static bool Exists(string key)
        {
            return Exists(SubKey, key);
        }
        #endregion

        #region public static bool Exists(string subKey, string key)
        /// <summary>
        /// 判断一个注册表项是否存在
        /// </summary>
        /// <param name="subKey">注册表子项</param>
        /// <param name="key">键</param>
        /// <returns>是否存在</returns>
        public static bool Exists(string subKey, string key)
        {
            bool returnValue = false;
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(subKey, false);
            string[] SubKeyNames = registryKey.GetSubKeyNames();
            for (int i = 0; i < SubKeyNames.Length; i++)
            {
                if (key.Equals(SubKeyNames[i]))
                {
                    returnValue = true;
                    break;
                }
            }
            return returnValue;
        }
        #endregion

        #region private static void GetValues() 获取注册表的值
        /// <summary>
        /// 获取注册表的值
        /// </summary>
        private static void GetValues()
        {
            SubKey = "Software\\" + BaseConfiguration.COMPANY_NAME + "\\" + BaseSystemInfo.SoftName;
            // 客户信息配置
            BaseSystemInfo.CustomerCompanyName = GetValue(BaseConfiguration.CUSTOMER_COMPANYNAME);
            BaseSystemInfo.ConfigurationFrom = BaseConfiguration.GetConfiguration(GetValue(BaseConfiguration.CONFIGURATION_FROM));
            BaseSystemInfo.SoftName = GetValue(BaseConfiguration.SOFT_NAME);
            BaseSystemInfo.SoftFullName = GetValue(BaseConfiguration.SOFT_FULLNAME);
            BaseSystemInfo.RunMode = BaseConfiguration.GetRunMode(GetValue(BaseConfiguration.RUN_MODE));
            BaseSystemInfo.RootMenuCode = GetValue(BaseConfiguration.ROOT_MENU_CODE);
            BaseSystemInfo.CurrentLanguage = GetValue(BaseConfiguration.CURRENT_LANGUAGE);
            BaseSystemInfo.Version = GetValue(BaseConfiguration.VERSION);            

            // 这里是服务器设置项
            BaseSystemInfo.WebService = GetValue(BaseConfiguration.WEBSERVICE);
            BaseSystemInfo.WebServiceUsername = GetValue(BaseConfiguration.WEBSERVICE_USERNAME);
            BaseSystemInfo.WebServicePassword = GetValue(BaseConfiguration.WEBSERVICE_PASSWORD);
            
            // 数据库连接
            BaseSystemInfo.BusinessDbConnection = GetValue(BaseConfiguration.BUSINESS_DBCONNECTION);
            BaseSystemInfo.UserCenterDbConnection = GetValue(BaseConfiguration.USERCENTER_DBCONNECTION);

            BaseSystemInfo.DataBaseType = BaseConfiguration.GetDataBaseType(GetValue(BaseConfiguration.DATABASE_TYPE));
            BaseSystemInfo.RegisterKey = GetValue(BaseConfiguration.REGISTER_KEY);
        }
        #endregion

        #region private static void SetValues() 设置注册表的值
        /// <summary>
        /// 设置注册表的值
        /// </summary>
        private static void SetValues()
        {
            // 默认的信息写入注册表
            SubKey = "Software\\" + BaseConfiguration.COMPANY_NAME + "\\" + BaseSystemInfo.SoftName;
            SetValue(BaseConfiguration.CUSTOMER_COMPANYNAME, BaseSystemInfo.CustomerCompanyName);
            SetValue(BaseConfiguration.CONFIGURATION_FROM, BaseSystemInfo.RegisterKey.ToString());
            SetValue(BaseConfiguration.SOFT_NAME, BaseSystemInfo.SoftName);
            SetValue(BaseConfiguration.SOFT_FULLNAME, BaseSystemInfo.SoftFullName);
            SetValue(BaseConfiguration.RUN_MODE, RunMode.Local.ToString());
            SetValue(BaseConfiguration.ROOT_MENU_CODE, BaseSystemInfo.RootMenuCode);
            SetValue(BaseConfiguration.CURRENT_LANGUAGE, BaseSystemInfo.CurrentLanguage);

            // 这里是服务器设置项
            SetValue(BaseConfiguration.WEBSERVICE, "www." + BaseConfiguration.COMPANY_NAME + ".com");
            SetValue(BaseConfiguration.WEBSERVICE_USERNAME, BaseConfiguration.COMPANY_NAME);
            SetValue(BaseConfiguration.WEBSERVICE_PASSWORD, BaseConfiguration.COMPANY_NAME);

            // 数据库连接
            SetValue(BaseConfiguration.DATABASE_TYPE, DataBaseType.Sqlserver.ToString());
            SetValue(BaseConfiguration.REGISTER_KEY, BaseConfiguration.COMPANY_NAME);
        }
        #endregion

        #region public static void GetConfig() 读取注册表信息
        /// <summary>
        /// 读取注册表信息
        /// 获取系统配置信息，在系统的原头解决问题，呵呵不错
        /// </summary>
        public static void GetConfig()
        {
            // 读取注册表信息
            // string subKey = "Software\\" + BaseConfiguration.COMPANY_NAME;
            if (!Exists("Software", BaseConfiguration.COMPANY_NAME))
            {
                // 设置注册表
                SetValues();
            }
            else
            {
                if (!Exists(BaseSystemInfo.SoftName))
                {
                    // 设置注册表
                    SetValues();
                }
            }
            // 检测是否已经有数据了，若已经有数据了，就不进行读取了。
            if (BaseSystemInfo.DataBase.Length == 0)
            {
                GetValues();
            }
        }
        #endregion
    }
}