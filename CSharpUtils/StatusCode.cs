//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

namespace CSharpUtils
{
    /// <summary>
    /// StateCode
    /// ��������״̬��
    /// 
    /// �޸ļ�¼
    /// 
    ///		2007.12.09 �汾��1.1 JiRiGaLa ��������Ϊ StatusCode��
    ///		2007.12.04 �汾��1.0 JiRiGaLa ���µ��������Ĺ淶����
    ///		
    /// �汾��1.0
    /// 
    /// <author>

    ///		<date>2007.05.18</date>
    /// </author> 
    /// </summary>    
    #region public enum StatusCode ��������״̬
    public enum StatusCode
    {
        Error = 0,                  //  0 ��������
        OK = 1,                     //  1 ���гɹ���
        OKAdd = 2,                  //  2 ��ӳɹ���
        CanNotLock = 3,             //  3 �����������ݡ�
        LockOK = 4,                 //  4 �ɹ��������ݡ�
        OKUpdate = 5,               //  5 �������ݳɹ���
        OKDelete = 6,               //  6 ɾ���ɹ���
        Exist = 7,                  //  7 �������ظ�,�������ظ���
        ErrorCodeExist = 8,         //  8 ����Ѵ���,�������ظ���
        ErrorNameExist = 9,         //  9 �������ظ�
        ErrorValueExist = 10,       // 10 ֵ���ظ�
        ErrorUserExist = 11,        // 11 �û������ظ�
        ErrorDataRelated = 12,      // 12 �����Ѿ������ã��й��������ڡ�
        ErrorDeleted = 13,          // 13 �����ѱ�������ɾ����
        ErrorChanged = 14,          // 14 �����ѱ��������޸ġ�
        NotFound = 15,              // 15 Ϊ�ҵ���¼��
        UserNotFound = 16,          // 16 �û�û���ҵ���
        PasswordError = 17,         // 17 �������
        LoginDeny = 18,             // 18 ��¼���ܾ���
        ErrorOnLine = 19,           // 19 ֻ�����¼һ��
        ErrorMacAddress = 20,       // 20 �Ƿ����û�������Mac��ַ
        ErrorIPAddress = 21,        // 21 �Ƿ����û�IP��ַ
        ErrorOnLineLimit = 22,      // 22 ͬʱ�����û���������
        PasswordCanNotBeNull = 23,  // 23 ���벻����Ϊ�ա�
        SetPasswordOK = 24,         // 24 ��������ɹ���
        OldPasswordError = 25,      // 25 ԭ�������
        ChangePasswordOK = 26,      // 26 �޸�����ɹ���
        UserNotEmail = 27,          // 27 �û�û�е����ʼ���ַ��
        UserLocked = 28,            // 28 �û���������
        UserNotActive = 29,         // 29 �û���δ�����
        UserIsActivate = 30,        // 30 �û��ѱ���������ظ����
    }
    #endregion
}

