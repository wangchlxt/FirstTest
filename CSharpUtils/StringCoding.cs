using System;
using System.Web.Security;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using CSharpUtils;

namespace CSharpUtils
{
    public class StringCoding
    {
        #region ( 1 ) QueryString 加密与解密

        /// <summary>
        /// QueryString 加密
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string QueryStringEncodeCode(string code)
        {
            string result = "";
            if (code == null || code == "")
            {
                result = "";
            }
            else
            {
                result = Convert.ToBase64String(System.Text.Encoding.Default.GetBytes("" + code + "")).Replace("+", "%2B");
            }
            return result;
        }

        /// <summary>
        /// QueryString 解密
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string QueryStringDncodeCode(string code)
        {
            string result = "";

            if (code == null || code == "")
            {
                result = "";
            }
            else
            {
                try
                {
                    result = System.Text.Encoding.Default.GetString(Convert.FromBase64String(code.Replace("%2B", "+")));
                }
                catch (FormatException e)///抛出异常 [错误信息“Base-64字符数组的无效长度”]
                {
                    result = "0";
                    LogHelper.Error(e.ToString());
                }
            }
            return result;
        }
        
        #endregion (1) QueryString 加密与解密
        
        #region ( 2 ) Rijndael算法

        private static SymmetricAlgorithm mobjCryptoService = new RijndaelManaged();
        private static string Key = "Guz(%&hj7x89H$yuBI0456FtmaT5&fvHUFCy76*h%(HilJ$lhj!y6&(*jkP87jH7";

        /// <summary>
        /// 获得密钥 Rijndael算法
        /// </summary>
        /// <returns>密钥</returns>
        private byte[] GetLegalKey()
        {
            string sTemp = Key;
            mobjCryptoService.GenerateKey();
            byte[] bytTemp = mobjCryptoService.Key;
            int KeyLength = bytTemp.Length;
            if (sTemp.Length > KeyLength)
                sTemp = sTemp.Substring(0, KeyLength);
            else if (sTemp.Length < KeyLength)
                sTemp = sTemp.PadRight(KeyLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        /// <summary>
        /// 获得初始向量IV Rijndael算法
        /// </summary>
        /// <returns>初试向量IV</returns>
        private byte[] GetLegalIV()
        {
            string sTemp = "E4ghj*Ghg7!rNIfb&95GUY86GfghUb#er57HBh(u%g6HJ($jhWk7&!hg4ui%$hjk";
            mobjCryptoService.GenerateIV();
            byte[] bytTemp = mobjCryptoService.IV;
            int IVLength = bytTemp.Length;
            if (sTemp.Length > IVLength)
                sTemp = sTemp.Substring(0, IVLength);
            else if (sTemp.Length < IVLength)
                sTemp = sTemp.PadRight(IVLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        /// <summary>
        /// 加密方法 Rijndael 算法
        /// </summary>
        /// <param name="Source">待加密的串</param>
        /// <returns>经过加密的串</returns>
        public string RijndaelEncrypt(string Source)
        {
            byte[] bytIn = UTF8Encoding.UTF8.GetBytes(Source);
            MemoryStream ms = new MemoryStream();
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();
            ms.Close();
            byte[] bytOut = ms.ToArray();
            return Convert.ToBase64String(bytOut);
        }

        /// <summary>
        /// 解密方法 Rijndael 算法
        /// </summary>
        /// <param name="Source">待解密的串</param>
        /// <returns>经过解密的串</returns>
        public string RijndaelDecrypt(string Source)
        {
            try
            {
                byte[] bytIn = Convert.FromBase64String(Source);
                MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
                mobjCryptoService.Key = GetLegalKey();
                mobjCryptoService.IV = GetLegalIV();
                ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
        
        #region ( 3 ) Base64 与 UTF8 混用

        //字符串加密
        public string BUEncrypt(string bb)
        {
            byte[] by = new byte[bb.Length];
            by = System.Text.Encoding.UTF8.GetBytes(bb);

            string r = Convert.ToBase64String(by);
            return r;
        }

        //字符串解密
        public string BUDecrypt(string aa)
        {
            try
            {
                byte[] by = Convert.FromBase64String(aa);

                string r = Encoding.UTF8.GetString(by);

                return r;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion
        
        #region ( 4 ) 固定密钥算法

        public static Byte[] Iv64 = { 11, 22, 33, 44, 55, 66, 77, 88 };

        public static Byte[] byKey64 = { 10, 20, 30, 40, 50, 60, 70, 80 };
        /// <summary>
        /// 字符串加密 固定密钥算法 
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public string SKeyEncrypt(string strText)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey64, Iv64), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //字符串解密
        /// <summary>
        /// 字符串解密 固定密钥算法 
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public string SKeyDecrypt(string strText)
        {
            Byte[] inputByteArray = new byte[strText.Length];
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey64, Iv64), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
        
        #region ( 5 ) DES算法

        public static byte[] DESKey = new byte[] { 0x82, 0xBC, 0xA1, 0x6A, 0xF5, 0x87, 0x3B, 0xE6, 0x59, 0x6A, 0x32, 0x64, 0x7F, 0x3A, 0x2A, 0xBB, 0x2B, 0x68, 0xE2, 0x5F, 0x06, 0xFB, 0xB8, 0x2D, 0x67, 0xB3, 0x55, 0x19, 0x4E, 0xB8, 0xBF, 0xDD };
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="strSource">待加密字串</param>
        /// <param name="key">32位Key值</param>
        /// <returns>加密后的字符串</returns>
        public string DESEncrypt(string strSource)
        {
            return DESEncryptF(strSource, DESKey);
        }

        private string DESEncryptF(string strSource, byte[] key)
        {
            SymmetricAlgorithm sa = Rijndael.Create();
            sa.Key = key;
            sa.Mode = CipherMode.ECB;
            sa.Padding = PaddingMode.Zeros;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, sa.CreateEncryptor(), CryptoStreamMode.Write);
            byte[] byt = Encoding.Unicode.GetBytes(strSource);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="strSource">待解密的字串</param>
        /// <param name="key">32位Key值</param>
        /// <returns>解密后的字符串</returns>
        public string DESDecrypt(string strSource)
        {
            return DESDecryptF(strSource, DESKey);
        }

        private string DESDecryptF(string strSource, byte[] key)
        {
            try
            {
                SymmetricAlgorithm sa = Rijndael.Create();
                sa.Key = key;
                sa.Mode = CipherMode.ECB;
                sa.Padding = PaddingMode.Zeros;
                ICryptoTransform ct = sa.CreateDecryptor();
                byte[] byt = Convert.FromBase64String(strSource);
                MemoryStream ms = new MemoryStream(byt);
                CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cs, Encoding.Unicode);
                return sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
        
        #region ( 6 ) 加密密码MD5T和SHA1

        /// <summary>
        /// 加密密码MD5T和SHA1
        /// </summary>
        /// <param name="strSource">字符串</param>
        /// <param name="strFlag">加密类别：1.MD5 2.SHA1</param>
        /// <param name="substringlen">加密长度：16,32</param>
        /// <returns></returns>
        /// 
        public string Encrypting(string strSource, int strFlag, int substringlen)
        {
            string ss = "";
            if (strFlag == 1)///MD5加密
            {
                if (substringlen == 16)//16位MD5加密（取32位加密的9~25字符）
                {
                    ss = FormsAuthentication.HashPasswordForStoringInConfigFile(strSource, "MD5").ToLower().Substring(8, 16);

                }
                else if (substringlen == 32)//32位加密
                {
                    ss = FormsAuthentication.HashPasswordForStoringInConfigFile(strSource, "MD5").ToLower();
                }
            }
            else if (strFlag == 2)///SHA1加密
            {
                if (substringlen == 16)//SHA1 16位加密
                {
                    ss = FormsAuthentication.HashPasswordForStoringInConfigFile(strSource, "SHA1").ToLower().Substring(8, 16);
                }
                else if (substringlen == 32)//SHA1 40位加密
                {
                    ss = FormsAuthentication.HashPasswordForStoringInConfigFile(strSource, "SHA1").ToLower();
                }
            }
            else
            {
                ss = "";
            }
            return ss;
        }

        #endregion


        public string GetAppKey(string str)
        {
            string appkey = SKeyEncrypt(str);
            appkey = appkey.Replace('=', '*');

            return appkey;
        }

        public string GetAppKeyDe(string str)
        {
            string appkey = str.Replace("*", "=");
            appkey = SKeyDecrypt(appkey);

            return appkey;
        }

        public string GetSecretKey(string str)
        { 
            string appkey = RijndaelEncrypt(str);
            appkey = appkey.Replace('=', '*');

            return appkey;
        }

        public string GetSecretKeyDe(string str)
        {
            string appkey = str.Replace('*', '=');
            appkey = RijndaelDecrypt(appkey);

            return appkey;
        }

        #region MD5 加密
        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string EncodeMD5(string content)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] buffer = md5.ComputeHash(Encoding.UTF8.GetBytes(content));

            StringBuilder buider = new StringBuilder();

            for (int i = 0; i < buffer.Length; i++)
            {
                buider.Append(buffer[i].ToString("X2"));
            }

            return buider.ToString();
        }
        #endregion

        #region 16位 MD5 加密
        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncodeMD5Encrypt16(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(password)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }

        #endregion

        #region 32位MD5加密
        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncodeMD5Encrypt32(string password)
        {
            string cl = password;
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }
        #endregion

        #region Hmacsha1
        /// <summary>
        /// Hmacsha1 加密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Hmacsha1(string key, string content)
        {
            HMACSHA1 hmacsha1 = new HMACSHA1();
            hmacsha1.Key = Encoding.UTF8.GetBytes(key);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(content);
            byte[] bytes = hmacsha1.ComputeHash(buffer);
            return Convert.ToBase64String(bytes);
        }
        #endregion

        #region 实现Base64加密解密
        /// <summary> 
        /// Base64加密 
        /// </summary> 
        /// <param name="codeName">加密采用的编码方式</param> 
        /// <param name="source">待加密的明文</param> 
        /// <returns></returns> 
        public static string EncodeBase64(Encoding encoding, string source)
        {
            string encode = "";
            byte[] bytes = encoding.GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = source;
            }
            return encode;
        }

        /// <summary> 
        /// Base64加密，采用utf8编码方式加密 
        /// </summary> 
        /// <param name="source">待加密的明文</param> 
        /// <returns>加密后的字符串</returns> 
        public static string EncodeBase64(string source)
        {
            return EncodeBase64(Encoding.UTF8, source);
        }

        /// <summary> 
        /// Base64解密 
        /// </summary> 
        /// <param name="codeName">解密采用的编码方式，注意和加密时采用的方式一致</param> 
        /// <param name="result">待解密的密文</param> 
        /// <returns>解密后的字符串</returns> 
        public static string DecodeBase64(Encoding encode, string result)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encode.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }

        /// <summary> 
        /// Base64解密，采用utf8编码方式解密 
        /// </summary> 
        /// <param name="result">待解密的密文</param> 
        /// <returns>解密后的字符串</returns> 
        public static string DecodeBase64(string result)
        {
            return DecodeBase64(Encoding.UTF8, result);
        }
        #endregion


        /// <summary>  
        /// SHA1 加密，返回大写字符串  
        /// </summary>  
        /// <param name="content">需要加密字符串</param>  
        /// <returns>返回40位UTF8 大写</returns>  
        public static string SHA1(string content)
        {
            return SHA1(content, Encoding.UTF8);
        }
        /// <summary>  
        /// SHA1 加密，返回大写字符串  
        /// </summary>  
        /// <param name="content">需要加密字符串</param>  
        /// <param name="encode">指定加密编码</param>  
        /// <returns>返回40位大写字符串</returns>  
        public static string SHA1(string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错：" + ex.Message);
            }
        }


        #region DES 加解密
        /// <summary>
        /// DES数据加密
        /// </summary>
        /// <param name="targetValue">目标字段</param>
        /// <returns>加密</returns>
        public static string Encrypt(string targetValue)
        {
            return Encrypt(targetValue, "DotNetKey");
        }

        /// <summary>
        /// DES数据加密
        /// </summary>
        /// <param name="targetValue">目标值</param>
        /// <param name="key">密钥</param>
        /// <returns>加密值</returns>
        public static string Encrypt(string targetValue, string key)
        {
            if (string.IsNullOrEmpty(targetValue))
            {
                return string.Empty;
            }

            var returnValue = new StringBuilder();
            var des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(targetValue);
            // 通过两次哈希密码设置对称算法的初始化向量   
            des.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile
                                                  (FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").
                                                       Substring(0, 8), "sha1").Substring(0, 8));
            // 通过两次哈希密码设置算法的机密密钥   
            des.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile
                                                 (FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5")
                                                      .Substring(0, 8), "md5").Substring(0, 8));
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            foreach (byte b in ms.ToArray())
            {
                returnValue.AppendFormat("{0:X2}", b);
            }
            return returnValue.ToString();
        }


        /// <summary>
        /// DES数据解密
        /// </summary>
        /// <param name="targetValue">目标字段</param>
        /// <returns>解密</returns>
        public static string Decrypt(string targetValue)
        {
            return Decrypt(targetValue, "DotNetKey");
        }

        /// <summary>
        /// DES数据解密
        /// </summary>
        /// <param name="targetValue"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string targetValue, string key)
        {
            if (string.IsNullOrEmpty(targetValue))
            {
                return string.Empty;
            }
            // 定义DES加密对象
            var des = new DESCryptoServiceProvider();
            int len = targetValue.Length / 2;
            var inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(targetValue.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            // 通过两次哈希密码设置对称算法的初始化向量   
            des.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile
                                                  (FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").
                                                       Substring(0, 8), "sha1").Substring(0, 8));
            // 通过两次哈希密码设置算法的机密密钥   
            des.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile
                                                 (FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5")
                                                      .Substring(0, 8), "md5").Substring(0, 8));
            // 定义内存流
            var ms = new MemoryStream();
            // 定义加密流
            var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
        #endregion
        
    }
}
