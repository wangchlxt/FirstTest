using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpUtils
{
    public class DesktopUtil
    {
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfoA")]
        static extern Int32 SystemParametersInfo(Int32 uAction, Int32 uParam, string lpvparam, Int32 fuwinIni);
        private const int SPI_SETDESKWALLPAPER = 20;

        /// <summary>
        /// 设置桌面背景图片
        /// </summary>
        /// <param name="imgFile">带完整路径的图片文件名</param>
        public static void SetDesktopImg(string imgFile)
        {
            string sfiletype = imgFile.Substring(imgFile.LastIndexOf(".") + 1, (imgFile.Length - imgFile.LastIndexOf(".") - 1));
            sfiletype = sfiletype.ToLower();
            string sfilename = imgFile.Substring(imgFile.LastIndexOf("//") + 1, (imgFile.LastIndexOf(".") - imgFile.LastIndexOf("//") - 1));

            // 判断文件类型是否是bmp格式。。
            if (sfiletype == "bmp")
            {
                // 调用，filename为图片地址，最后一个参数需要为1，0的话在重启后就变回原来的了
                SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, imgFile, 1);
            }
            else
            {
                string path = sfilename + ".bmp";
                FileInfo fi = new FileInfo(path);

                // 判断文件是否存在
                if (fi.Exists)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = Image.FromFile(imgFile);
                }
                else
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = Image.FromFile(imgFile);
                    pb.Image.Save(sfilename + ".bmp", ImageFormat.Bmp);
                }

                // 调用，filename为图片地址，最后一个参数需要为1，0的话在重启后就变回原来的了
                SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, 1);
            }
        }
    }
}
