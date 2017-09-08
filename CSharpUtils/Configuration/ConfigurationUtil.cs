//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

using System;
using System.Configuration;
using System.Globalization;

namespace CSharpUtils.Configuration
{
    /// <summary>
    /// ConfigurationHelper
    /// �������á�
    /// 
    /// �޸ļ�¼
    /// 
    ///		2008.06.08 �汾��1.0 JiRiGaLa ������� BaseConfiguration �����˷��롣
    /// 
    /// �汾��1.0
    /// 
    /// <author>

    ///		<date>2008.06.08</date>
    /// </author> 
    /// </summary>
    public class ConfigurationUtil
    {
         #region public static void GetConfig()
        /// <summary>
        /// ��������Ϣ��ȡ������Ϣ
        /// </summary>
        /// <param name="configuration">����</param>
        public static void GetConfig()
        {
            // �ͻ���Ϣ����
            BaseSystemInfo.CustomerCompanyName = ConfigurationManager.AppSettings[BaseConfiguration.CUSTOMER_COMPANYNAME];
            BaseSystemInfo.ConfigurationFrom = BaseConfiguration.GetConfiguration(ConfigurationManager.AppSettings[BaseConfiguration.CONFIGURATION_FROM]);
            BaseSystemInfo.SoftName = ConfigurationManager.AppSettings[BaseConfiguration.SOFT_NAME];
            BaseSystemInfo.SoftFullName = ConfigurationManager.AppSettings[BaseConfiguration.SOFT_FULLNAME];
            BaseSystemInfo.RunMode = BaseConfiguration.GetRunMode(ConfigurationManager.AppSettings[BaseConfiguration.RUN_MODE]);
            BaseSystemInfo.RootMenuCode = ConfigurationManager.AppSettings[BaseConfiguration.ROOT_MENU_CODE];
            BaseSystemInfo.CurrentLanguage = ConfigurationManager.AppSettings[BaseConfiguration.CURRENT_LANGUAGE];
            BaseSystemInfo.Version = ConfigurationManager.AppSettings[BaseConfiguration.VERSION];
            
            // �����Ƿ�����������
            BaseSystemInfo.WebService = ConfigurationManager.AppSettings[BaseConfiguration.WEBSERVICE];

            BaseSystemInfo.WebServiceUsername = ConfigurationManager.AppSettings[BaseConfiguration.WEBSERVICE_USERNAME];
            BaseSystemInfo.WebServicePassword = ConfigurationManager.AppSettings[BaseConfiguration.WEBSERVICE_PASSWORD];

            BaseSystemInfo.UsePermissionScope = (String.Compare(ConfigurationManager.AppSettings[BaseConfiguration.USE_PERMISSIONS_COPE], "TRUE", true, CultureInfo.CurrentCulture) == 0);
            BaseSystemInfo.UseLicensedPermission = (String.Compare(ConfigurationManager.AppSettings[BaseConfiguration.USE_LICENSED_PERMISSION], "TRUE", true, CultureInfo.CurrentCulture) == 0);

            BaseSystemInfo.ServiceFactory = ConfigurationManager.AppSettings[BaseConfiguration.SERVICE_FACTORY];
            BaseSystemInfo.ServicePath = ConfigurationManager.AppSettings[BaseConfiguration.SERVICE_PATH];
            BaseSystemInfo.DbHelperClass = ConfigurationManager.AppSettings[BaseConfiguration.DBHELPER_CLASSNAME];
            BaseSystemInfo.DbHelperAssmely = ConfigurationManager.AppSettings[BaseConfiguration.DBHELPER_ASSMELY];
            BaseSystemInfo.RecordLog = (String.Compare(ConfigurationManager.AppSettings[BaseConfiguration.RECORD_LOG], "TRUE", true, CultureInfo.CurrentCulture) == 0);

            BaseSystemInfo.LoginAssembly = ConfigurationManager.AppSettings[BaseConfiguration.LOGIN_ASSEMBLY];
            BaseSystemInfo.LoginForm = ConfigurationManager.AppSettings[BaseConfiguration.LOGIN_FORM];
            BaseSystemInfo.MainForm = ConfigurationManager.AppSettings[BaseConfiguration.MAIN_FORM];

            BaseSystemInfo.LoadUser = (String.Compare(ConfigurationManager.AppSettings[BaseConfiguration.LOAD_USER], "TRUE", true, CultureInfo.CurrentCulture) == 0);
            BaseSystemInfo.AllowUserRegister = (String.Compare(ConfigurationManager.AppSettings[BaseConfiguration.ALLOW_USER_REGISTER], "TRUE", true, CultureInfo.CurrentCulture) == 0); 

            // ���ݿ�����
            BaseSystemInfo.BusinessDbConnection = ConfigurationManager.AppSettings[BaseConfiguration.BUSINESS_DBCONNECTION];
            BaseSystemInfo.UserCenterDbConnection = ConfigurationManager.AppSettings[BaseConfiguration.USERCENTER_DBCONNECTION];

            BaseSystemInfo.DataBaseType = BaseConfiguration.GetDataBaseType(ConfigurationManager.AppSettings[BaseConfiguration.DATABASE_TYPE]);
            BaseSystemInfo.RegisterKey = ConfigurationManager.AppSettings[BaseConfiguration.REGISTER_KEY];
        }
        #endregion
    }
}