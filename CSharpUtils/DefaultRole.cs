//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

using System;

namespace CSharpUtils
{
    /// <summary>
    /// DefaultRole
    /// Ĭ�Ͻ�ɫ��
    /// 
    /// ���Ӧ���ǽ�ɫ���Ŷԣ���Ӧ�����û����
    /// 
    /// �޸ļ�¼
    ///
    ///		2009.07.08 �汾��4.0 JiRiGaLa ����Ϊ DefaultRole�� 
    ///		2008.09.27 �汾��3.3 JiRiGaLa ����Ϊ Role�� 
    ///		2007.12.20 �汾��3.2 JiRiGaLa ���� Config�� 
    ///		2007.09.18 �汾��3.1 JiRiGaLa ���������� 
    ///		2007.04.14 �汾��3.0 JiRiGaLa �������ʽͨ�������ٽ����޸����������� 
    ///     2007.03.26 �汾��2.0 JiRiGaLa �ṹ�Ż�����
    ///     2007.11.23 �汾��1.1 JiRiGaLa �ṹ�Ż�����
    ///     2006.11.23 �汾��1.0 JiRiGaLa �ṹ�Ż�����
    ///		
    /// �汾��3.1
    /// 
    /// <author>

    ///		<date>2007.09.18</date>
    /// </author> 
    /// </summary>
    public enum DefaultRole
    {
        Config,                 // ϵͳ����Ա
        Administrator,          // ϵͳ����Ա
        Administrators,         // ϵͳ������
        ChairmanOfTheBoard,     // ���³�
        VicePrecident,          // ���ܲ�
        GeneralManager,         // �ܾ���
        ViceManager,            // ������
        Minister,               // ����
        ViceMinsiter,           // ������
        HumanResourceManager,   // ������Դ����
        HumanResource,          // ������Դ
        FinanceManager,         // ������Ա
        Finance,                // ������Ա
        EquipmentManager,       // �豸��������
        Equipment,              // �豸������Ա
        Staff,                  // ��ְͨԱ
        User                    // ��ͨ�û�
    }
}  