using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class ByteExtensions
    {
        /// <summary>
        /// 比较两个字节数组是否一样
        /// </summary>
        /// <param name="object"></param>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static bool Equals( this byte[] @object, byte[] buffer, int offset, int count )
        {
            if( @object.Length != count )
            {
                return false;
            }

            for( int i = 0; i < @object.Length; i++ )
            {
                if( @object[i] != buffer[offset + i] )
                {
                    return false;
                }
            }

            return true;
        }
    }
}
