//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

namespace CSharpUtils.Configuration
{
    /// <summary>
    /// DataBaseType
    /// 有关数据库连接类型定义。
    /// 
    /// 修改纪录
    /// 
    ///		2007.04.14 版本：3.0 JiRiGaLa 检查程序格式通过，不再进行修改主键操作。
    ///		2006.11.17 版本：2.1 JiRiGaLa 变量命规范化。
    ///		2006.04.18 版本：2.0 JiRiGaLa 重新调整主键的规范化。
    ///		
    /// 版本：3.0
    /// 
    /// <author>

    ///		<date>2007.04.14</date>
    /// </author> 
    /// </summary>
    public enum DataBaseType
    {
        Oracle,
        Sqlserver,
        Access,
        Bd2,
        MySql,
        SQLite
    }
}