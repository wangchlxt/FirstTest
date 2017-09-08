using EmitMapper;
using CSharpUtils;

namespace System
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// 是否存在于指定的数组中
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args">数组</param>
        /// <returns></returns>
        public static bool In( this object source, object[] args )
        {
            throw new NotImplementedException();
        }

        public static bool In( this object source, int[] args )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 把对象类型转化为指定类型，转化失败时返回该类型默认值
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="value">要转化的源对象</param>
        /// <returns>转化后的指定类型的对象，转化失败返回类型的默认值</returns>
        public static T CastTo<T>( this object value )
        {
            object result;
            Type type = typeof( T );
            try
            {
                if( type.IsEnum )
                {
                    result = Enum.Parse( type, value.ToString() );
                }
                else if( type == typeof( Guid ) )
                {
                    result = Guid.Parse( value.ToString() );
                }
                else
                {
                    result = Convert.ChangeType( value, type );
                }
            }
            catch( Exception ex )
            {
                LogHelper.Info( ex.ToString() );
                result = default( T );
            }

            return (T)result;
        }

        /// <summary>
        /// 强制转换指定类
        /// </summary>
        /// <typeparam name="TTo">目标结果类</typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TTo ToMapper<TTo>( this object obj )
            where TTo : class
        {
            var mapper = ObjectMapperManager.DefaultInstance.GetMapperImpl( obj.GetType(), typeof( TTo ), EmitMapper.MappingConfiguration.DefaultMapConfig.Instance );
            return mapper.Map( obj ) as TTo;
        }
    }
}
