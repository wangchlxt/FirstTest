//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

using System;
using Microsoft.Win32;
using System.Globalization;

namespace CSharpUtils.Configuration
{
    public class RegistryUtil
    {

        #region public static string GetValue(string key) ��ȡע���
        /// <summary>
		/// ��ȡע���
		/// </summary>
        /// <param name="subKey">ע�������</param>
		/// <param name="registryKey">��</param>
		/// <returns>ֵ</returns>
        public static string GetValue(string SubKey,string key)
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
        public static void SetValue(string SubKey, string key, string keyValue)
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
        public static bool Exists(string SubKey, string key)
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
        public static bool Exists(string SubKey, string subKey, string key)
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
        
    }
}