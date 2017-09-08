//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace CSharpUtils
{
    /// <summary>
    ///	FileUtil
    /// 文件帮助类
    /// 
    /// 修改纪录
    /// 
    ///		2010.07.10 版本：1.0	JiRiGaLa 创建。
    ///	
    /// 版本：1.0
    ///
    /// <author>

    ///		<date>2010.07.10</date>
    /// </author> 
    /// </summary>
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

        public string FileName = "Log.txt";

        #region public static WriteException(Exception ex) 写入异常情况
        /// <summary>
        /// 写入异常情况
        /// </summary>
        /// <param name="Exception">异常</param>
        public static void WriteException(Exception ex)
        {
            WriteException(ex, "Log.txt");
        }
        #endregion

        #region public static void WriteException(BaseUserInfo userInfo, Exception ex) 写入异常情况
        /// <summary>
        /// 写入异常情况
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="Exception">异常</param>
        public static void WriteException(BaseUserInfo userInfo, Exception ex)
        {
            string fileName = "Log.txt";
            if (userInfo.Username.Length > 0)
            {
                fileName = @"Log_" + userInfo.Username + ".txt";
            }
            WriteException(ex, fileName);
        }
        #endregion

        #region static void WriteException(Exception ex, string fileName) 写入异常情况
        /// <summary>
        /// 写入异常情况
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="Exception">异常</param>
        public static void WriteException(Exception ex, string fileName)
        {
            string returnValue = string.Empty;
            // 系统里应该可以配置是否记录异常现象
            if (!BaseSystemInfo.LogException)
            {
                return;
            }
            // 将异常信息写入本地文件中
            string writerFileName = BaseSystemInfo.StartupPath + @"\" + fileName;
            if (!File.Exists(writerFileName))
            {
                FileStream FileStream = new FileStream(writerFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                FileStream.Close();
            }
            StreamWriter StreamWriter = new StreamWriter(writerFileName, true, Encoding.Default);
            StreamWriter.WriteLine(DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + " " + ex.Message);
            StreamWriter.Close();
        }
        #endregion
    }
}