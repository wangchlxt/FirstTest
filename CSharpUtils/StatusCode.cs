//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

namespace CSharpUtils
{
    /// <summary>
    /// StateCode
    /// 程序运行状态。
    /// 
    /// 修改纪录
    /// 
    ///		2007.12.09 版本：1.1 JiRiGaLa 重新命名为 StatusCode。
    ///		2007.12.04 版本：1.0 JiRiGaLa 重新调整主键的规范化。
    ///		
    /// 版本：1.0
    /// 
    /// <author>

    ///		<date>2007.05.18</date>
    /// </author> 
    /// </summary>    
    #region public enum StatusCode 程序运行状态
    public enum StatusCode
    {
        Error = 0,                  //  0 发生错误。
        OK = 1,                     //  1 运行成功。
        OKAdd = 2,                  //  2 添加成功。
        CanNotLock = 3,             //  3 不能锁定数据。
        LockOK = 4,                 //  4 成功锁定数据。
        OKUpdate = 5,               //  5 更新数据成功。
        OKDelete = 6,               //  6 删除成功。
        Exist = 7,                  //  7 数据已重复,不可以重复。
        ErrorCodeExist = 8,         //  8 编号已存在,不可以重复。
        ErrorNameExist = 9,         //  9 名称已重复
        ErrorValueExist = 10,       // 10 值已重复
        ErrorUserExist = 11,        // 11 用户名已重复
        ErrorDataRelated = 12,      // 12 数据已经被引用，有关联数据在。
        ErrorDeleted = 13,          // 13 数据已被其他人删除。
        ErrorChanged = 14,          // 14 数据已被其他人修改。
        NotFound = 15,              // 15 为找到记录。
        UserNotFound = 16,          // 16 用户没有找到。
        PasswordError = 17,         // 17 密码错误。
        LoginDeny = 18,             // 18 登录被拒绝。
        ErrorOnLine = 19,           // 19 只允许登录一次
        ErrorMacAddress = 20,       // 20 是否检查用户的网卡Mac地址
        ErrorIPAddress = 21,        // 21 是否检查用户IP地址
        ErrorOnLineLimit = 22,      // 22 同时在线用户数量限制
        PasswordCanNotBeNull = 23,  // 23 密码不允许为空。
        SetPasswordOK = 24,         // 24 设置密码成功。
        OldPasswordError = 25,      // 25 原密码错误。
        ChangePasswordOK = 26,      // 26 修改密码成功。
        UserNotEmail = 27,          // 27 用户没有电子邮件地址。
        UserLocked = 28,            // 28 用户被锁定。
        UserNotActive = 29,         // 29 用户还未被激活。
        UserIsActivate = 30,        // 30 用户已被激活，不用重复激活。
    }
    #endregion
}

