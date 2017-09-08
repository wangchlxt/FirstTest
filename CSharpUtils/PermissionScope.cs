//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

using System;

namespace CSharpUtils
{
    /// <summary>
    /// PermissionScope
    /// 权限范围。
    /// 
    /// 修改纪录
    ///
    ///     2009.09.01 版本：1.0 JiRiGaLa 创建。
    ///		
    /// 版本：1.1
    /// 
    /// <author>

    ///		<date>2009.09.01</date>
    /// </author> 
    /// </summary>
    public enum PermissionScope
    {
        None = 0,            // 没有任何数据权限
        All = -1,            // 全部数据
        UserCompany = -2,    // 用户所在公司数据
        UserDepartment = -3, // 用户所在部门数据
        UserWorkgroup = -4,  // 用户所在工作组数据
        User = -5,           // 自己的数据
        Detail = -6          // 按详细设定的数据
    }
}