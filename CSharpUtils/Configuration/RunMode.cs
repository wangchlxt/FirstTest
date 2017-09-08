//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

namespace CSharpUtils.Configuration
{
    /// <summary>
    /// ProgramRunMode
    /// 程序运行模式。
    /// 
    /// 修改纪录
    /// 
    ///     2010.09.19 版本：2.0 JiRiGaLa 增加 WCF 技术支持。
    ///		2007.05.18 版本：1.0 JiRiGaLa 重新调整主键的规范化。
    ///		
    /// 版本：1.0
    /// 
    /// <author>

    ///		<date>2007.05.18</date>
    /// </author> 
    /// </summary>    
    public enum RunMode
    {
        Local,          // 本地模式
        WebService,     // 远程模式
        Remoting,       // 局域网模式
        WCF             // WCF技术模式
    }
}