//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

using System;
using System.Globalization;

namespace CSharpUtils.Configuration
{
    /// <summary>
    /// BaseConfiguration
    /// �������á�
    /// 
    /// �޸ļ�¼
    /// 
    ///		2008.06.08 �汾��3.5 JiRiGaLa ����ȡ�����ļ����з��롣
    ///		2008.05.08 �汾��3.4 JiRiGaLa ��ò�ͬ�����ݿ������ַ��� OracleConnection��SqlConnection��OleDbConnection��
    ///		2007.11.28 �汾��3.2 JiRiGaLa ������������ַ��������������ļ��Ķ��Ĵ���������ܡ�
    ///		2007.05.23 �汾��3.1 JiRiGaLa ���� public const string ���岿�֡�
    ///		2007.04.14 �汾��3.0 JiRiGaLa �������ʽͨ�������ٽ����޸�����������
    ///		2006.11.17 �汾��2.4 JiRiGaLa GetFromRegistryKey() ����������������ɾ����������á�
    ///		2006.05.02 �汾��2.3 JiRiGaLa GetFromConfig ���Ӵ������ļ���ȡ���ݿ����ӵķ�����
    ///		2006.04.18 �汾��2.2 JiRiGaLa ���µ��������Ĺ淶����
    ///		2006.02.02 �汾��2.0 JiRiGaLa ɾ�����ݿ����ӳص��뷨���޸������������淶���������ˡ� 
    ///		2005.12.29 �汾��1.0 JiRiGaLa �������ļ���ȡ���ݿ����ӡ�
    /// 
    /// �汾��3.5
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

        // �ͻ���Ϣ����
        public const string CUSTOMER_COMPANYNAME    = "CustomerCompanyName";
        public const string CONFIGURATION_FROM      = "ConfigurationFrom";
        public const string SOFT_NAME               = "SoftName";
        public const string SOFT_FULLNAME           = "SoftFullName";
        public const string RUN_MODE                = "RunMode";
        public const string ROOT_MENU_CODE            = "RootMenuCode";
        public const string CURRENT_LANGUAGE        = "CurrentLanguage";
        public const string VERSION                 = "Version";
        
        
        // �����Ƿ�����������
        public const string WEBSERVICE              = "WebService";
        public const string WEBSERVICE_USERNAME     = "WebServiceUsername";
        public const string WEBSERVICE_PASSWORD     = "WebServicePassword";

        public const string USE_PERMISSIONS_COPE     = "UsePermissionScope";          // ���÷ּ�����ΧȨ������
        public const string USE_LICENSED_PERMISSION = "UseLicensedPermission";  // ��������Ȩ

        public const string SERVICE_FACTORY         = "ServiceFactory";
        public const string SERVICE_PATH            = "ServicePath";
        public const string DBHELPER_CLASSNAME      = "DbHelperClass";
        public const string DBHELPER_ASSMELY        = "DbHelperAssmely";
        public const string RECORD_LOG              = "RecordLog";        

        // ��¼����
        public const string LOGIN_ASSEMBLY      = "LoginAssembly";
        public const string LOGIN_FORM          = "LoginForm";
        public const string MAIN_FORM           = "MainForm";

        public const string LOAD_USER           = "LoadUser";
        public const string ALLOW_USER_REGISTER = "AllowUserRegister";
        
        // ���ݿ�����
        public const string USERCENTER_DBCONNECTION = "UserCenterDbConnection";
        public const string BUSINESS_DBCONNECTION = "BusinessDbConnection";
        
        public const string DATABASE_TYPE       = "DataBaseType";
        public const string REGISTER_KEY        = "RegisterKey";

        // ���󱨸�
        public const string ERROR_REPORT_TO = "ErrorReportTo";
        public const string ERROR_REPORT_MAIL_USERNAME = "ErrorReportMailUsername";
        public const string ERROR_REPORT_MAIL_PASSWORD = "ErrorReportMailPassword";

        #region public BaseConfiguration()
        /// <summary>
        /// ���췽��
        /// </summary>
        public BaseConfiguration()
        {
        }
        #endregion

        #region public BaseConfiguration(string softName)
        /// <summary>
        /// �趨��ǰ���ID
        /// </summary>
        /// <param name="softName">��ǰ���ID</param>
        public BaseConfiguration(string softName)
        {
            BaseSystemInfo.SoftName = softName;
        }
        #endregion

        #region public static DataBaseType GetDataBaseType(string dataBase)
        /// <summary>
        /// ���ݿ����ӵ������ж�
        /// </summary>
        /// <param name="dataBase">���ݿ�����</param>
        /// <returns>���ݿ�����</returns>
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
        /// ������Ϣ�������ж�
        /// </summary>
        /// <param name="configuration">������Ϣ����</param>
        /// <returns>������Ϣ����</returns>
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
        /// ����ģʽ����
        /// </summary>
        /// <param name="runMode">����ģʽ</param>
        /// <returns>������Ϣ����</returns>
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
        /// ��ȡ������Ϣ
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