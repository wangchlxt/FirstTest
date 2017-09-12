using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace CSharpUtils.Net
{
    public class HttpUtil
    {
        /// <summary>
        /// POST获取数据
        /// </summary>
        /// <param name="postUrl">请求服务路径</param>
        /// <param name="paramData">参数</param>
        /// <param name="dataEncode">字符编码格式</param>
        /// <returns></returns>
        public static string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
        {
            string rtnString = string.Empty;
            HttpWebResponse response = null;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                request.Method = "POST";
                request.Accept = "application/json";
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;
                Stream writer = request.GetRequestStream();
                writer.Write(byteArray, 0, byteArray.Length);
                writer.Close();
                response = (HttpWebResponse)request.GetResponse();
                if (response != null)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        rtnString = reader.ReadToEnd();
                        reader.Close();
                        response.Close();
                        response = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return rtnString;
        }

        /// <summary>
        /// GET获取数据
        /// </summary>
        /// <param name="getUrl">请求服务路径</param>
        /// <param name="paramData">参数</param>
        /// <param name="dataEncode">字符编码格式</param>
        /// <returns></returns>
        public static string GetWebRequest(string getUrl, string paramData, Encoding dataEncode)
        {
            string rtnString = "";
            HttpWebResponse response = null;
            try
            {
               
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getUrl + (paramData == "" ? "" : "?") + paramData);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                response = (HttpWebResponse)request.GetResponse();
                if (response != null)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), dataEncode))
                    {
                        rtnString = reader.ReadToEnd();
                        response.Close();
                        response = null;
                    }
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            finally
            {

            }
    
            return rtnString;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url">下载路径</param>
        /// <param name="path">保存的完整路径</param>
        /// <returns>是否成功</returns>
        public static bool DownloadFile(string url, string path)
        {
            bool isSuccess = false;
            HttpWebResponse response = null;
            Stream stream = null;
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = "GET";
                response = (HttpWebResponse)webRequest.GetResponse();
                stream = response.GetResponseStream();
                List<byte> byteList = new List<byte>();
                while (true)
                {
                    int data = stream.ReadByte();
                    if (data == -1)
                        break;
                    else
                    {
                        byte byteData = (byte)data;
                        byteList.Add(byteData);
                    }
                }
                byte[] bytes = byteList.ToArray();
                System.IO.File.WriteAllBytes(path, bytes);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream = null;
                }
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }
            return isSuccess;
        }

        /// <summary>
        /// post方法通过请求的地址和数据来获取数据
        /// </summary>
        /// <param name="url">访问地址.</param>
        /// <param name="postDataStr">参数数据</param>
        /// <returns>System.String.</returns>
        /// <remarks>post方法通过请求的地址和数据来获取数据</remarks>
        public static string HttpPost(string url, string postDataStr,string authorization=null)
        {
            try
            {
                string postdata = (postDataStr);
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] data = Encoding.UTF8.GetBytes(postdata);// encoding.GetBytes(postdata); 
                 // Prepare web request...
                 HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "POST";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentLength = data.Length;

                //认证
                if(!string.IsNullOrEmpty(authorization))
                {
                    myRequest.Headers.Add(HttpRequestHeader.Authorization, authorization);

                }

                Stream newStream = myRequest.GetRequestStream();
                // Send the data.
                newStream.Write(data, 0, data.Length);
                newStream.Close();
                // Get response
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string content = reader.ReadToEnd();
                return content;
            }
            catch(Exception ex)
            {
                return string.Empty;
            }
        }




        /// <summary>
        /// post方法通过请求的地址和数据来获取数据
        /// </summary>
        /// <param name="url">访问地址.</param>
        /// <param name="postDataStr">参数数据</param>
        /// <returns>System.String.</returns>
        /// <remarks>post方法通过请求的地址和数据来获取数据</remarks>
        public static string HttpPost(string url, Stream ms)
        {
            try
            {
                //string postdata = (postDataStr);
                ASCIIEncoding encoding = new ASCIIEncoding();
                //byte[] data =ms. encoding.GetBytes(postdata);
                // Prepare web request...
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "POST";
                myRequest.Proxy = null;
                //myRequest.UserAgent = null;
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentLength = ms.Length;// data.Length;
                Stream newStream = myRequest.GetRequestStream();
                byte[] bytes = new byte[ms.Length];
                ms.Read(bytes, 0, bytes.Length);

                // 设置当前流的位置为流的开始 
                ms.Seek(0, SeekOrigin.Begin);
                newStream.Write(bytes, 0, bytes.Length);
                //// Send the data.
                //newStream.Write(data, 0, data.Length);
                //newStream.Close();
                // Get response
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string content = reader.ReadToEnd();
                return content;
            }
            catch
            {
                return string.Empty;
            }
        }


        public static string HttpGet(string getUrl, string paramData, string authorization = null)
        {
            string rtnString = "";
            HttpWebResponse response = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getUrl + (paramData == "" ? "" : "?") + paramData);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";

                //认证
                if (!string.IsNullOrEmpty(authorization))
                {
                    request.Headers.Add(HttpRequestHeader.Authorization, authorization);

                }

                response = (HttpWebResponse)request.GetResponse();
                if (response != null)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")))
                    {
                        rtnString = reader.ReadToEnd();
                        response.Close();
                        response = null;
                    }
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            finally
            {

            }
            return rtnString;
        }


    }
}
