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

        #region public static string GetValue(string key) 读取注册表
        /// <summary>
		/// 读取注册表
		/// </summary>
        /// <param name="subKey">注册表子项</param>
		/// <param name="registryKey">键</param>
		/// <returns>值</returns>
        public static string GetValue(string SubKey,string key)
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
        public static void SetValue(string SubKey, string key, string keyValue)
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
        public static bool Exists(string SubKey, string key)
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