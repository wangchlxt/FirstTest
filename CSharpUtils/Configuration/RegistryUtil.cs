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
    /// ����ע�����ࡣ
    /// 
    /// �޸ļ�¼
    ///
    ///		2008.06.08 �汾��3.2 JiRiGaLa �����޸�Ϊ RegistryHelper�� 
    ///		2007.07.30 �汾��3.1 JiRiGaLa Exists �������淶���� 
    ///		2007.04.14 �汾��3.0 JiRiGaLa �������ʽͨ�������ٽ����޸����������� 
    ///     2006.11.17 �汾��2.2 JiRiGaLa ��ӷ���CheckExistSubKey()��
    ///     2006.09.08 �汾��2.1 JiRiGaLa ���������淶����
    ///     2006.04.18 �汾��2.0 JiRiGaLa ���µ��������Ĺ淶����
    ///		2005.08.08 �汾��1.0 JiRiGaLa ר�Ŷ�ȡע�����࣬������д��ʽ�Ľ���
    ///		
    ///	�汾��3.0
    /// 
    /// <author>

    ///		<date>2007.04.14</date>
    /// </author> 
    /// </summary>
    public class RegistryUtil
    {
        /// <summary>
        /// ע����е�λ��
        /// </summary>
        public static string SubKey = "Software\\" + BaseConfiguration.COMPANY_NAME;

        #region public static string GetValue(string key) ��ȡע���
        /// <summary>
		/// ��ȡע���
		/// </summary>
        /// <param name="subKey">ע�������</param>
		/// <param name="registryKey">��</param>
		/// <returns>ֵ</returns>
        public static string GetValue(string key)
		{
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(SubKey, false);
            return (string)registryKey.GetValue(key);
		}
		#endregion

        #region public static void SetValue(string key, string keyValue) ����ע���
        /// <summary>
        /// ����ע���
        /// </summary>
        /// <param name="subKey">ע�������</param>
        /// <param name="registryKey">��</param>
        /// <param name="keyValue">ֵ</param>
        public static void SetValue(string key, string keyValue)
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(SubKey, true);
            registryKey.SetValue(key, keyValue);
        }
        #endregion

        #region public static bool Exists(string key) �ж�һ��ע������Ƿ����
        /// <summary>
        /// �ж�һ��ע������Ƿ����
        /// </summary>
        /// <param name="key">��</param>
        /// <returns>�Ƿ����</returns>
        public static bool Exists(string key)
        {
            return Exists(SubKey, key);
        }
        #endregion

        #region public static bool Exists(string subKey, string key)
        /// <summary>
        /// �ж�һ��ע������Ƿ����
        /// </summary>
        /// <param name="subKey">ע�������</param>
        /// <param name="key">��</param>
        /// <returns>�Ƿ����</returns>
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

        #region private static void GetValues() ��ȡע����ֵ
        /// <summary>
        /// ��ȡע����ֵ
        /// </summary>
        private static void GetValues()
        {
            SubKey = "Software\\" + BaseConfiguration.COMPANY_NAME + "\\" + BaseSystemInfo.SoftName;
            // �ͻ���Ϣ����
            BaseSystemInfo.CustomerCompanyName = GetValue(BaseConfiguration.CUSTOMER_COMPANYNAME);
            BaseSystemInfo.ConfigurationFrom = BaseConfiguration.GetConfiguration(GetValue(BaseConfiguration.CONFIGURATION_FROM));
            BaseSystemInfo.SoftName = GetValue(BaseConfiguration.SOFT_NAME);
            BaseSystemInfo.SoftFullName = GetValue(BaseConfiguration.SOFT_FULLNAME);
            BaseSystemInfo.RunMode = BaseConfiguration.GetRunMode(GetValue(BaseConfiguration.RUN_MODE));
            BaseSystemInfo.RootMenuCode = GetValue(BaseConfiguration.ROOT_MENU_CODE);
            BaseSystemInfo.CurrentLanguage = GetValue(BaseConfiguration.CURRENT_LANGUAGE);
            BaseSystemInfo.Version = GetValue(BaseConfiguration.VERSION);            

            // �����Ƿ�����������
            BaseSystemInfo.WebService = GetValue(BaseConfiguration.WEBSERVICE);
            BaseSystemInfo.WebServiceUsername = GetValue(BaseConfiguration.WEBSERVICE_USERNAME);
            BaseSystemInfo.WebServicePassword = GetValue(BaseConfiguration.WEBSERVICE_PASSWORD);
            
            // ���ݿ�����
            BaseSystemInfo.BusinessDbConnection = GetValue(BaseConfiguration.BUSINESS_DBCONNECTION);
            BaseSystemInfo.UserCenterDbConnection = GetValue(BaseConfiguration.USERCENTER_DBCONNECTION);

            BaseSystemInfo.DataBaseType = BaseConfiguration.GetDataBaseType(GetValue(BaseConfiguration.DATABASE_TYPE));
            BaseSystemInfo.RegisterKey = GetValue(BaseConfiguration.REGISTER_KEY);
        }
        #endregion

        #region private static void SetValues() ����ע����ֵ
        /// <summary>
        /// ����ע����ֵ
        /// </summary>
        private static void SetValues()
        {
            // Ĭ�ϵ���Ϣд��ע���
            SubKey = "Software\\" + BaseConfiguration.COMPANY_NAME + "\\" + BaseSystemInfo.SoftName;
            SetValue(BaseConfiguration.CUSTOMER_COMPANYNAME, BaseSystemInfo.CustomerCompanyName);
            SetValue(BaseConfiguration.CONFIGURATION_FROM, BaseSystemInfo.RegisterKey.ToString());
            SetValue(BaseConfiguration.SOFT_NAME, BaseSystemInfo.SoftName);
            SetValue(BaseConfiguration.SOFT_FULLNAME, BaseSystemInfo.SoftFullName);
            SetValue(BaseConfiguration.RUN_MODE, RunMode.Local.ToString());
            SetValue(BaseConfiguration.ROOT_MENU_CODE, BaseSystemInfo.RootMenuCode);
            SetValue(BaseConfiguration.CURRENT_LANGUAGE, BaseSystemInfo.CurrentLanguage);

            // �����Ƿ�����������
            SetValue(BaseConfiguration.WEBSERVICE, "www." + BaseConfiguration.COMPANY_NAME + ".com");
            SetValue(BaseConfiguration.WEBSERVICE_USERNAME, BaseConfiguration.COMPANY_NAME);
            SetValue(BaseConfiguration.WEBSERVICE_PASSWORD, BaseConfiguration.COMPANY_NAME);

            // ���ݿ�����
            SetValue(BaseConfiguration.DATABASE_TYPE, DataBaseType.Sqlserver.ToString());
            SetValue(BaseConfiguration.REGISTER_KEY, BaseConfiguration.COMPANY_NAME);
        }
        #endregion

        #region public static void GetConfig() ��ȡע�����Ϣ
        /// <summary>
        /// ��ȡע�����Ϣ
        /// ��ȡϵͳ������Ϣ����ϵͳ��ԭͷ������⣬�Ǻǲ���
        /// </summary>
        public static void GetConfig()
        {
            // ��ȡע�����Ϣ
            // string subKey = "Software\\" + BaseConfiguration.COMPANY_NAME;
            if (!Exists("Software", BaseConfiguration.COMPANY_NAME))
            {
                // ����ע���
                SetValues();
            }
            else
            {
                if (!Exists(BaseSystemInfo.SoftName))
                {
                    // ����ע���
                    SetValues();
                }
            }
            // ����Ƿ��Ѿ��������ˣ����Ѿ��������ˣ��Ͳ����ж�ȡ�ˡ�
            if (BaseSystemInfo.DataBase.Length == 0)
            {
                GetValues();
            }
        }
        #endregion
    }
}