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

        /// <summary>
        /// 测试向从二进制文件中读取数据，并显示到终端上.
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>文件内容</returns>
        public static string ReadBinaryFile(string fileName)
        {
            string message = string.Empty;
            // Console.WriteLine("读取二进制文件信息开始。");
            FileStream fs = null;
            BinaryReader br = null;
            try
            {
                // 首先判断，文件是否已经存在
                if (!File.Exists(fileName))
                {
                    // 如果文件不存在，那么提示无法读取！
                    // Console.WriteLine("二进制文件{0}不存在！", fileName);
                    return string.Empty;
                }
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                br = new BinaryReader(fs);
                // int a = br.ReadInt32();
                // double b = br.ReadDouble();
                // int c = br.ReadInt32();
                // byte len = br.ReadByte();
                // char[] d = br.ReadChars(len);
                // Console.WriteLine("第一个数据:{0}", a);
                // Console.WriteLine("第二个数据:{0}", b);
                // Console.WriteLine("第三个数据:{0}", c);
                // Console.WriteLine("第四个数据 (长度为{0}):", len);
                // foreach (char ch in d)
                // {
                //    Console.Write(ch);
                // }
                // Console.WriteLine();
                //完整的读取文件类容需要获取文件的长度
                int count = (int)fs.Length;
                byte[] buffer = new byte[count];
                br.Read(buffer, 0, buffer.Length);
                message = Encoding.Default.GetString(buffer);
                // message = br.ReadString();
                // 读取完毕，关闭.
                br.Close();
                fs.Close();
                br = null;
                fs = null;
            }
            catch (Exception ex)
            {
                // Console.WriteLine("在读取文件的过程中，发生了异常！");
                // Console.WriteLine(ex.Message);
                // Console.WriteLine(ex.StackTrace);
                throw ex;
            }
            finally
            {
                if (br != null)
                {
                    try
                    {
                        br.Close();
                    }
                    catch
                    {
                        // 最后关闭文件，无视 关闭是否会发生错误了.
                    }
                }
                if (fs != null)
                {
                    try
                    {
                        fs.Close();
                    }
                    catch
                    {
                        // 最后关闭文件，无视 关闭是否会发生错误了.
                    }
                }
            }
            // Console.WriteLine("读取二进制文件信息结束。");
            return message;
        }

    }
}