using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpUtils
{
    public static class CRC16
    {
        private const ushort polynomial = 0xA001;
        private static ushort[] table = new ushort[256];

        #region CRC16
        /// <summary>
        /// 
        /// </summary>
        static CRC16()
        {
            ushort value;
            ushort temp;

            for( ushort i = 0; i < table.Length; ++i )
            {
                value = 0;
                temp = i;

                for( byte j = 0; j < 8; ++j )
                {
                    if( ( ( value ^ temp ) & 0x0001 ) != 0 )
                    {
                        value = (ushort)( ( value >> 1 ) ^ polynomial );
                    }
                    else
                    {
                        value >>= 1;
                    }

                    temp >>= 1;
                }

                table[i] = value;
            }
        }
        #endregion

        #region Checksum
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static ushort Checksum( byte[] buffer, int offset, int count )
        {
            ushort crc = 0;

            for( int i = offset; i < offset + count; ++i )
            {
                byte index = (byte)( crc ^ buffer[i] );

                crc = (ushort)( ( crc >> 8 ) ^ table[index] );
            }

            return crc;
        }
        #endregion
    }
}
