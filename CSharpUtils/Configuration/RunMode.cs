//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

namespace CSharpUtils.Configuration
{
    /// <summary>
    /// ProgramRunMode
    /// ��������ģʽ��
    /// 
    /// �޸ļ�¼
    /// 
    ///     2010.09.19 �汾��2.0 JiRiGaLa ���� WCF ����֧�֡�
    ///		2007.05.18 �汾��1.0 JiRiGaLa ���µ��������Ĺ淶����
    ///		
    /// �汾��1.0
    /// 
    /// <author>

    ///		<date>2007.05.18</date>
    /// </author> 
    /// </summary>    
    public enum RunMode
    {
        Local,          // ����ģʽ
        WebService,     // Զ��ģʽ
        Remoting,       // ������ģʽ
        WCF             // WCF����ģʽ
    }
}