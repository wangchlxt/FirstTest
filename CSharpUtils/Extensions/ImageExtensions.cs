using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Drawing
{
    public class ImageExtensions
    {
        private Image ResourceImage;

        /// <summary>   
        /// 类的构造函数   
        /// </summary>   
        /// <param name="ImageFileName">图片文件的全路径名称</param>   
        public ImageExtensions(string ImageFileName)
        {
            ResourceImage = Image.FromFile(ImageFileName);
        }

        public bool ThumbnailCallback()
        {
            return false;
        }
        
        /// <summary>   
        /// 生成缩略图重载方法，将缩略图文件保存到指定的路径   
        /// </summary>   
        /// <param name="Width">缩略图的宽度</param>   
        /// <param name="Height">缩略图的高度</param>   
        /// <param name="targetFilePath">缩略图保存的全文件名，(带路径)，参数格式：D:Images ilename.jpg</param>   
        /// <returns>成功返回true，否则返回false</returns>   
        public bool GetReducedImage(int Width, int Height, string targetFilePath)
        {
            try
            {
                Image ReducedImage;

                Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);

                ReducedImage = ResourceImage.GetThumbnailImage(Width, Height, callb, IntPtr.Zero);
                ReducedImage.Save(@targetFilePath, ImageFormat.Jpeg);

                ReducedImage.Dispose();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
