using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpUtils
{
    public static class ImageUtil
    {
        #region GenerateThumbImage
        /// <summary>
        /// 生成指定图片的缩略图，并保存到指定的位置
        /// </summary>
        /// <param name="source">源图片位置</param>
        /// <param name="target">目标图片位置</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public static void GenerateThumbImage( string source, string target, int width, int height )
        {
            Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort( () => { return false; } );

            using( Image image = Image.FromFile( source ) )
            {
                using( var thumbImage = image.GetThumbnailImage( width, height, callback, IntPtr.Zero ) )
                {
                    thumbImage.Save( target, ImageFormat.Jpeg );
                }
            }
        }
        #endregion
    }
}
