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
        #region QueryString 算法

        /// <summary>
        /// 字符串加密 QueryString 算法
        /// </summary>
        /// <param name="sourceString">要加密的字符串</param>
        /// <returns></returns>
        public static string EncodeQueryString(string sourceString)
        {
            if(string.IsNullOrEmpty(sourceString))
            {
                return string.Empty;
            }
            
            return Convert.ToBase64String(System.Text.Encoding.Default.GetBytes("" + sourceString + "")).Replace("+", "%2B");
        }

        /// <summary>
        /// 字符串解密 QueryString 算法
        /// </summary>
        /// <param name="sourceString">要解密的字符串</param>
        /// <returns></returns>
        public static string DecodeQueryString(string sourceString)
        {
            if(string.IsNullOrEmpty(sourceString))
            {
                return string.Empty;
            }
            
            return System.Text.Encoding.Default.GetString(Convert.FromBase64String(sourceString.Replace("%2B", "+")));
        }
        
        #endregion
        
        #region Rijndael算法

        private static SymmetricAlgorithm mobjCryptoService = new RijndaelManaged();
        private static string Key = "Guz(%&hj7x89H$yuBI0456FtmaT5&fvHUFCy76*h%(HilJ$lhj!y6&(*jkP87jH7";

        /// <summary>
        /// 获得密钥 Rijndael算法
        /// </summary>
        /// <returns>密钥</returns>
        private static byte[] GetLegalKey()
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
        private static byte[] GetLegalIV()
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
        /// 字符串加密 Rijndael 算法
        /// </summary>
        /// <param name="sourceString">要加密的字符串</param>
        /// <returns>经过加密的串</returns>
        public static string EncodeRijndael(string sourceString)
        {
            byte[] bytIn = UTF8Encoding.UTF8.GetBytes(sourceString);
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
        /// 字符串解密 Rijndael 算法
        /// </summary>
        /// <param name="sourceString">要解密的字符串</param>
        /// <returns>经过解密的串</returns>
        public static string DecodeRijndael(string sourceString)
        {
            try
            {
                byte[] bytIn = Convert.FromBase64String(sourceString);
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

        #region Base64 算法

        /// <summary> 
        /// 字符串加密 Base64 算法
        /// </summary> 
        /// <param name="encoding">加密采用的编码方式</param> 
        /// <param name="sourceString">要加密的字符串</param> 
        /// <returns></returns> 
        public static string EncodeBase64(Encoding encoding, string sourceString)
        {
            byte[] bytes = encoding.GetBytes(sourceString);
            return Convert.ToBase64String(bytes);
        }

        /// <summary> 
        /// 字符串解密 Base64 算法
        /// </summary> 
        /// <param name="encoding">解密采用的编码方式，注意和加密时采用的方式一致</param> 
        /// <param name="sourceString">要解密的字符串</param> 
        /// <returns>解密后的字符串</returns> 
        public static string DecodeBase64(Encoding encoding, string sourceString)
        {
            byte[] bytes = Convert.FromBase64String(sourceString);
            return encoding.GetString(bytes);
        }

        #endregion
        
        #region 固定密钥算法

        public static Byte[] Iv64 = { 11, 22, 33, 44, 55, 66, 77, 88 };

        public static Byte[] byKey64 = { 10, 20, 30, 40, 50, 60, 70, 80 };
        /// <summary>
        /// 字符串加密 固定密钥算法
        /// </summary>
        /// <param name="sourceString">要加密的字符串</param>
        /// <returns></returns>
        public static string EncodeSKey(string sourceString)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = Encoding.UTF8.GetBytes(sourceString);
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
        /// <param name="sourceString">要解密的字符串</param>
        /// <returns></returns>
        public static string DecodeSKeyDecrypt(string sourceString)
        {
            Byte[] inputByteArray = new byte[sourceString.Length];
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(sourceString);
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

        #region DES 算法

        /// <summary>
        /// 字符串加密 DES 算法
        /// </summary>
        /// <param name="sourceString">要加密的字符串。</param>
        /// <param name="key">密钥，且必须为8位。</param>
        /// <returns>以Base64格式返回的加密字符串。</returns>
        public static string EncodeDES(string sourceString, string key)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(sourceString);
                des.Key = ASCIIEncoding.ASCII.GetBytes(key);
                des.IV = ASCIIEncoding.ASCII.GetBytes(key);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Convert.ToBase64String(ms.ToArray());
                ms.Close();
                return str;
            }
        }

        /// <summary>
        /// 字符串解密 DES 算法
        /// </summary>
        /// <param name="sourceString">要解密的以Base64</param>
        /// <param name="key">密钥，且必须为8位</param>
        /// <returns>已解密的字符串</returns>
        public static string DecodeDES(string sourceString, string key)
        {
            byte[] inputByteArray = Convert.FromBase64String(sourceString);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(key);
                des.IV = ASCIIEncoding.ASCII.GetBytes(key);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
        }

        #endregion

        #region MD5 算法

        /// <summary>
        /// 字符串加密 MD5 算法
        /// </summary>
        /// <param name="sourceString">要加密的字符串</param>
        /// <returns></returns>
        public static string EncodeMD5(string sourceString)
        {
            byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(sourceString));
            
            StringBuilder buider = new StringBuilder();

            for (int i = 0; i < buffer.Length; i++)
            {
                buider.Append(buffer[i].ToString("X2"));
            }

            return buider.ToString();
        }

        /// <summary>
        /// 字符串加密 16位MD5算法
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public static string EncodeMD5Encrypt16(string sourceString)
        {
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(sourceString)), 4, 8).Replace("-","");
        }

        /// <summary>
        /// 字符串加密 32位MD5算法
        /// </summary>
        /// <param name="sourceString">要加密的字符串</param>
        /// <returns></returns>
        public static string EncodeMD5Encrypt32(string sourceString)
        {
            byte[] buffer = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(sourceString));
            
            StringBuilder buider = new StringBuilder();

            for (int i = 0; i < buffer.Length; i++)
            {
                buider.Append(buffer[i].ToString("X2"));
            }

            return buider.ToString();
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
    }
}
