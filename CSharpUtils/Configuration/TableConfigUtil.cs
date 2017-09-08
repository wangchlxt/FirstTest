//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

using System;
using System.Reflection;
using System.Xml;

namespace CSharpUtils.Configuration
{
    /// <summary>
    /// ConfigHelper
    /// �����û������ļ����ࡣ
    /// 
    /// �޸ļ�¼
    ///
    ///		2008.06.08 �汾��1.0 JiRiGaLa ������
    ///		
    ///	�汾��1.0
    /// 
    /// <author>

    ///		<date>2008.06.08</date>
    /// </author> 
    /// </summary>
    public class TableConfigUtil
    {
        private string fileName = string.Empty;

        public string FielName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                this.fileName = value;
            }
        }

        public string SelectPath = "//resultMaps/resultMap/result";

        #region public string GetValue(string key) ��ȡ������
        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="key">��</param>
        /// <returns>ֵ</returns>
        public string GetValue(string key)
        {
            return this.GetValue(this.FielName, this.SelectPath, key);
        }
        #endregion

        #region public string GetValue(string fileName, string key) ��ȡ������
        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="fileName">�����ļ�</param>
        /// <param name="key">��</param>
        /// <returns>ֵ</returns>
        public string GetValue(string fileName, string key)
        {
            return this.GetValue(fileName, this.SelectPath, key);
        }
        #endregion

        #region public string GetValue(string fileName, string selectPath, string key) ����������
        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="fileName">�����ļ�</param>
        /// <param name="selectPath">��ѯ����</param>
        /// <param name="key">��</param>
        /// <returns>ֵ</returns>
        public string GetValue(string fileName, string selectPath, string key)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            return this.GetValue(xmlDocument, selectPath, key);
        }
        #endregion

        #region public string GetValue(XmlDocument xmlDocument, string key) ��ȡ������
        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="XmlDocument">�����ļ�</param>
        /// <param name="key">��</param>
        /// <returns>ֵ</returns>
        public string GetValue(XmlDocument xmlDocument, string key)
        {
            return this.GetValue(xmlDocument, this.SelectPath, key);
        }
        #endregion

        #region public string GetValue(XmlDocument xmlDocument, string selectPath, string key) ����������
        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="XmlDocument">�����ļ�</param>
        /// <param name="selectPath">��ѯ����</param>
        /// <param name="key">��</param>
        /// <returns>ֵ</returns>
        public string GetValue(XmlDocument xmlDocument, string selectPath, string key)
        {
            string returnValue = string.Empty;
            XmlNodeList XmlNodeList = xmlDocument.SelectNodes(selectPath);
            foreach (XmlNode XmlNode in XmlNodeList)
            {
                if (XmlNode.Attributes["property"].Value.ToUpper().Equals(key.ToUpper()))
                {
                    returnValue = XmlNode.Attributes["column"].Value;
                    break;
                }
            }
            return returnValue;
        }
        #endregion

        #region public void GetConfig(string fileName, object table) ��ָ�����ļ���ȡ�ļ��ṹ����
        /// <summary>
        /// ��ָ�����ļ���ȡ�ļ��ṹ����
        /// </summary>
        /// <param name="fileName">�����ļ�</param>
        /// <param name="table">�ļ��ṹ����</param>
        public void GetConfig(string fileName, object table)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);

            Type tableType = table.GetType();
            string fieldName = string.Empty;
            string columnName = string.Empty;
            foreach (FieldInfo fieldInfo in tableType.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                if (fieldInfo.Name.StartsWith("Field"))
                {
                    // �ֶ���
                    fieldName = fieldInfo.Name.Substring("Field".Length);
                    // ����
                    columnName = this.GetValue(xmlDocument, fieldName);
                    fieldInfo.SetValue(table, columnName);
                }
            }
        }
        #endregion
    }
}