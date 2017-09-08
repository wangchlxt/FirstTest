using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpUtils.IoC
{
    /// <summary>
    /// 实例对象创建方式
    /// </summary>
    public enum InstanceMode
    {
        /// <summary>
        /// 单例
        /// </summary>
        InstanceSingleton = 1,

        /// <summary>
        /// 每次请求创建一次
        /// </summary>
        InstancePerRequest = 2
    }
}
