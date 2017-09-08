//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2009 , Jirisoft , Ltd. 
//------------------------------------------------------------

using System;
using System.Data;

namespace CSharpUtils.Configuration
{
    /// <summary>
    /// IConfiguration
    /// 读取配置文件接口定义
    /// 
    /// 修改纪录
    /// 
    ///		2009.07.08 版本：1.0 JiRiGaLa 创建代码。
    ///		
    /// 版本：1.0
    ///
    /// <author>

    ///		<date>2009.07.08</date>
    /// </author> 
    /// </summary>
    public interface IConfiguration
    {
        String GetValue(String key);
    }
}
