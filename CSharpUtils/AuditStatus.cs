//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

namespace CSharpUtils
{
    /// <summary>
    /// AuditStatus
    /// 审核状态。
    /// 
    /// 修改纪录
    /// 
    ///		2009.09.04 版本：1.0 JiRiGaLa 重新调整主键的规范化。
    ///		
    /// 版本：1.0
    /// 
    /// <author>

    ///		<date>2009.09.04</date>
    /// </author> 
    /// </summary>    
    #region public enum AuditStatus 审核状态
    public enum AuditStatus
    {
        /// <summary>
        /// 01 开始审核
        /// </summary>
        StartAudit = 1,

        /// <summary>
        /// 02 审核通过
        /// </summary>
        AuditPass = 2, 
        
        /// <summary>
        /// 03 待审核
        /// </summary>
        WaitForAudit = 3,  

        /// <summary>
        /// 04 转发
        /// </summary>
        Transmit = 4,  
        
        /// <summary>
        /// 05 已驳回
        /// </summary>
        AuditReject = 5,
        
        /// <summary>
        /// 06 审核结束
        /// </summary>
        AuditComplete = 6,

        /// <summary>
        /// 07 撤销
        /// </summary>
        AuditQuash = 7
    }
    #endregion
}

