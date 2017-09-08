using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CSharpUtils
{
    public class SerializationUtil
    {
        /// <summary>
        /// 序列化对象到文件（二进制）
        /// </summary>
        /// <typeparam name="T">要序列化对象的类型</typeparam>
        /// <param name="obj">要序列化的对象</param>
        /// <param name="file">带全路径的文件名</param>
        public static void SerializationBinary<T>(T obj,string file)
        {
            FileStream stream = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.None);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            stream.Close();
        }

        /// <summary>
        /// 从文件反序列化对象（二进制）
        /// </summary>
        /// <typeparam name="T">要序列化对象的类型</typeparam>
        /// <param name="file">带全路径的文件名</param>
        /// <returns></returns>
        public static T DeserializationBinary<T>(string file)
        {
            FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryFormatter formatter = new BinaryFormatter();
            T obj = (T)formatter.Deserialize(stream);
            stream.Close();

            return obj;
        }
    }
}
