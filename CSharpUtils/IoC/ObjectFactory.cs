using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpUtils.IoC
{
    /// <summary>
    /// 对象创建工厂
    /// </summary>
    public static class ObjectFactory
    {
        #region GetInstance
        /// <summary>
        /// 获取指定接口的实例对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetInstance<T>()
        {
            return ObjectFactory<T>.GetInstance();
        }
        #endregion

        #region RegisterType
        /// <summary>
        /// 注册类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="createInstance">创建实例的方法的委托</param>
        /// <param name="mode">创建方式</param>
        public static void RegisterType<T>( Func<T> createInstance, InstanceMode mode = InstanceMode.InstanceSingleton )
        {
            ObjectFactory<T>.CreateInstance = createInstance;
            ObjectFactory<T>.InstanceMode = mode;
        }
        #endregion
    }
}
