using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpUtils.IoC
{
    internal static class ObjectFactory<T>
    {
        private static object thelocker = new object();
        private static T singleton;

        /// <summary>
        /// 实例创建方式
        /// </summary>
        public static InstanceMode InstanceMode { get; set; }

        /// <summary>
        /// 实例对象创建方法
        /// </summary>
        public static Func<T> CreateInstance { get; set; }

        #region GetInstance
        /// <summary>
        /// 获取实例对象
        /// </summary>
        /// <returns></returns>
        public static T GetInstance()
        {
            if( CreateInstance == null )
            {
                throw new InvalidOperationException( "未将创建方法绑定到接口类型" );
            }

            switch( InstanceMode )
            {
                case InstanceMode.InstanceSingleton:
                    if( singleton != null )
                    {
                        return singleton;
                    }

                    lock( thelocker )
                    {
                        if( singleton != null )
                        {
                            return singleton;
                        }

                        singleton = CreateInstance();

                        return singleton;
                    }
                case InstanceMode.InstancePerRequest:
                    return CreateInstance();
                default:
                    throw new NotSupportedException( "不支持的实例创建方式" );
            }
        }
        #endregion
    }
}
