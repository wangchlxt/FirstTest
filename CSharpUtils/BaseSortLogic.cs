//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

using System;
using System.Data;

namespace CSharpUtils
{
    /// <summary>
    ///	BaseSortLogic
    /// ͨ�������߼����ࣨ����OK��
    /// 
    /// �޸ļ�¼
    /// 
    ///		2010.10.09 �汾��   1.4 JiRiGaLa ���º�����Ϊ*Id��
    ///		2007.12.10 �汾��   1.3 JiRiGaLa �Ľ� ���в�����ĳ������⡣
    ///		2007.11.01 �汾��   1.2 JiRiGaLa �Ľ� BUOperatorInfo ȥ���������������������ܣ�����ٶȣ��������һ�η�Ծ��
    ///		2007.03.01 �汾��   1.0 JiRiGaLa �������� BUBaseBusinessLogic ����������
    ///	
    /// �汾��1.3
    ///
    /// <author>

    ///		<date>2007.12.10</date>
    /// </author> 
    /// </summary>
    public class BaseSortLogic
    {
        //
        // ����������ڴ��е����㷽ʽ����
        //

        #region public static string GetNextId(DataTable dataTable, string id) ��ȡ��һ����¼����
        /// <summary>
        /// ��ȡ��һ����¼����
        /// </summary>
        /// <param name="dataTable">���ݱ�</param>
        /// <param name="tableName">��ǰ��</param>
        /// <param name="id">��ǰ����</param>
        /// <returns>����</returns>
        public static string GetNextId(DataTable dataTable, string id)
        {
            return GetNextId(dataTable.DefaultView, id, BaseBusinessLogic.FieldId);
        }
        #endregion

        public static string GetNextId(DataView dataView, string id)
        {
            return GetNextId(dataView, id, BaseBusinessLogic.FieldId);
        }

        #region public static string GetNextId(DataTable dataTable, string id, string field) ��ȡ��һ����¼����
        /// <summary>
        /// ��ȡ��һ����¼����
        /// </summary>
        /// <param name="dataTable">���ݱ�</param>
        /// <param name="tableName">��ǰ��</param>
        /// <param name="id">��ǰ����ID</param>
        /// <param name="field">��ǰ�ֶ�</param>
        /// <returns>����</returns>
        public static string GetNextId(DataTable dataTable, string id, string field)
        {
            return GetNextId(dataTable.DefaultView, id, field);
        }
        #endregion

        public static string GetNextId(DataView dataView, string id, string field)
        {
            string returnValue = string.Empty;
            bool find = false;
            foreach (DataRowView dataRow in dataView)
            {
                if (find)
                {
                    returnValue = dataRow[field].ToString();
                    break;
                }
                if (dataRow[field].ToString().Equals(id))
                {
                    find = true;
                }
            }
            return returnValue;
        }

        #region public static string GetPreviousId(DataTable dataTable, string id) ��ȡ��һ����¼����
        /// <summary>
        /// ��ȡ��һ����¼����
        /// </summary>
        /// <param name="dataTable">���ݱ�</param>
        /// <param name="tableName">��ǰ��</param>
        /// <param name="id">��ǰ����</param>
        /// <returns>����</returns>
        public static string GetPreviousId(DataTable dataTable, string id)
        {
            return GetPreviousId(dataTable.DefaultView, id, BaseBusinessLogic.FieldId);
        }
        #endregion

        public static string GetPreviousId(DataView dataView, string id)
        {
            return GetPreviousId(dataView, id, BaseBusinessLogic.FieldId);
        }

        #region public static string GetPreviousId(DataTable dataTable, string id) ��ȡ��һ����¼����
        /// <summary>
        /// ��ȡ��һ����¼����
        /// </summary>
        /// <param name="dataTable">���ݱ�</param>
        /// <param name="tableName">��ǰ��</param>
        /// <param name="id">��ǰ����</param>
        /// <param name="field">��ǰ�ֶ�</param>
        /// <returns>����</returns>
        public static string GetPreviousId(DataTable dataTable, string id, string field)
        {
            return GetPreviousId(dataTable.DefaultView, id, field);
        }
        #endregion

        public static string GetPreviousId(DataView dataView, string id, string field)
        {
            string returnValue = string.Empty;
            foreach (DataRowView dataRow in dataView)
            {
                if (dataRow[field].ToString().Equals(id))
                {
                    break;
                }
                returnValue = dataRow[field].ToString();
            }
            return returnValue;
        }

        #region public static int Swap(DataTable dataTable, string id, string targetId) ������¼��������˳��
        /// <summary>
        /// ������¼��������˳��
        /// </summary>
        /// <param name="dataTable">���ݱ�</param>
        /// <param name="id">Ҫ�ƶ��ļ�¼����</param>
        /// <param name="targetId">Ŀ���¼����</param>
        /// <returns>Ӱ������</returns>
        public static int Swap(DataTable dataTable, string id, string targetId)
        {
            int returnValue = 0;
            string sortCode = BaseBusinessLogic.GetProperty(dataTable, id, BaseBusinessLogic.FieldSortCode);
            string targetSortCode = BaseBusinessLogic.GetProperty(dataTable, targetId, BaseBusinessLogic.FieldSortCode);
            returnValue = BaseBusinessLogic.SetProperty(dataTable, id, BaseBusinessLogic.FieldSortCode, targetSortCode);
            returnValue += BaseBusinessLogic.SetProperty(dataTable, targetId, BaseBusinessLogic.FieldSortCode, sortCode);
            return returnValue;
        }
        #endregion
    }
}