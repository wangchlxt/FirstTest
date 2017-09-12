using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using System.Xml;

namespace CSharpUtils
{
    public class SecretUtil
    {
        /// <summary>
        /// 获取私钥
        /// </summary>
        /// <param name="digitalSignatureFile">数字证书文件</param>
        /// <param name="signedPassword">签名密码</param>
        /// <returns>私钥</returns>
        public static string GetPrivateKey(string digitalSignatureFile, string signedPassword)
        {
            // 0:这里需要处理异常信息
            // 1:定义私钥
            string privateKey = string.Empty;
            // 2:读取证书文件
            string digitalSignature = FileUtil.ReadBinaryFile(digitalSignatureFile);
            // 3:解密文件
            string xmlFile = StringCoding.Decrypt(digitalSignature);
            // 4:按XML文件读取
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlFile);
            string keySignedPassword = xmlDocument.SelectSingleNode("//DigitalSignature/Key").Attributes["SignedPassword"].Value;
            // 5:若签名密码不对，不应该能读取私钥
            if (StringCoding.EncodeMD5Encrypt32(signedPassword).Equals(keySignedPassword))
            {
                privateKey = xmlDocument.SelectSingleNode("//DigitalSignature/Key").Attributes["PrivateKey"].Value;
            }
            return privateKey;
        }

        /// <summary>
        /// 获取签名密码
        /// </summary>
        /// <param name="digitalSignatureFile">数字证书文件</param>
        /// <returns>私钥</returns>
        public static string GetSignedPassword(string digitalSignatureFile)
        {
            // 0:这里需要处理异常信息
            // 1:定义私钥
            string signedPassword = string.Empty;
            // 2:读取证书文件
            string digitalSignature = FileUtil.ReadBinaryFile(digitalSignatureFile);
            // 3:解密文件
            string xmlFile = StringCoding.Decrypt(digitalSignature);
            // 4:按XML文件读取
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlFile);
            signedPassword = xmlDocument.SelectSingleNode("//DigitalSignature/Key").Attributes["SignedPassword"].Value;
            return signedPassword;
        }

        /// <summary>
        /// 对数据进行签名
        /// 
        /// 将来需要改进为，对散列值进行签名
        /// </summary>
        /// <param name="dataToSign">需要签名的数据</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>签名结果</returns>
        public static string HashAndSign(string dataToSign, string privateKey)
        {
            ASCIIEncoding byteConverter = new ASCIIEncoding();
            byte[] DataToSign = byteConverter.GetBytes(dataToSign);
            try
            {
                RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();
                cryptoServiceProvider.ImportCspBlob(Convert.FromBase64String(privateKey));
                byte[] SignedData = cryptoServiceProvider.SignData(DataToSign, new SHA1CryptoServiceProvider());
                string signedData = Convert.ToBase64String(SignedData);
                return signedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// 验证数字签名
        /// 
        /// 将来需要改进为，按散列值进行验证
        /// </summary>
        /// <param name="dataToVerify">需要验证的数据</param>
        /// <param name="signedData">签名结果</param>
        /// <param name="publicKey">公钥</param>
        /// <returns>正确</returns>
        public static bool VerifySignedHash(string dataToVerify, string signedData, string publicKey)
        {
            byte[] SignedData = Convert.FromBase64String(signedData);

            ASCIIEncoding byteConverter = new ASCIIEncoding();
            byte[] DataToVerify = byteConverter.GetBytes(dataToVerify);
            try
            {
                RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();
                cryptoServiceProvider.ImportCspBlob(Convert.FromBase64String(publicKey));
                return cryptoServiceProvider.VerifyData(DataToVerify, new SHA1CryptoServiceProvider(), SignedData);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

    }
}