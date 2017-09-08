//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

namespace CSharpUtils
{
    #region public public enum OperationCode Ȩ��ö������
    /// <summary>
    /// Ȩ��ö������
    /// 
    /// �޸ļ�¼
    ///
    ///		2008.05.10 �汾��1.1 JiRiGaLa ����Ϊ OperationCode�� 
    ///		2007.12.08 �汾��1.0 JiRiGaLa ��� Config��UpLoad��DownLoad Ȩ�ޡ� 
    ///		
    /// �汾��1.0
    ///
    /// <author>

    ///		<date>2007.12.08</date>
    /// </author> 
    /// </remarks>
    public enum OperationCode
    {
        Access = 1,     // ����Ȩ��
        Add = 2,        // ����Ȩ��
        Edit = 3,       // �༭Ȩ��
        View = 4,       // ���Ȩ��
        Delete = 5,     // ɾ��Ȩ��
        Search = 6,     // ��ѯȨ��
        Import = 7,     // ����Ȩ��
        Export = 8,     // ����Ȩ��
        Print = 9,      // ��ӡȨ��
        Auditing = 10,  // ���Ȩ��
        Admin = 11,     // ����Ȩ��
        Config = 12,    // ����Ȩ��
        UpLoad = 13,    // �ϴ�Ȩ��
        DownLoad = 14   // ����Ȩ��
    }
    #endregion
}