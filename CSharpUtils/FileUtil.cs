//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace CSharpUtils
{
    public class FileUtil
    {
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>字节</returns>
        public static byte[] GetFile(string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            byte[] file = binaryReader.ReadBytes(((int)fileStream.Length));
            binaryReader.Close();
            fileStream.Close();
            return file;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="File">文件</param>
        /// <param name="fileName">文件名</param>
        public static void SaveFile(byte[] file, string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Create);
            fileStream.Write(file, 0, file.Length);
            fileStream.Close();
        }

        public static byte[] ImageToByte(Image Image)
        {
            MemoryStream memoryStream = new MemoryStream();
            Image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Gif);
            byte[] file = memoryStream.GetBuffer();
            memoryStream.Close();
            return file;
        }

        public static Image ByteToImage(byte[] buffer)
        {
            MemoryStream memoryStream = new MemoryStream();
            memoryStream = new System.IO.MemoryStream(buffer);
            Image image = Image.FromStream(memoryStream);
            memoryStream.Close();
            return image;
        }
        
    }
}