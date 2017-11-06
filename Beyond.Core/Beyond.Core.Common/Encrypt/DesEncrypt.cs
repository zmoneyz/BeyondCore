using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Beyond.Core.Common.Encrypt
{
    public class DesEncrypt
    {
        private const string AesKey = "AllenLee&8888888";

        #region ========加密========
        public static string Encrypt(string Text)
        {
            return Encrypt(Text, "dms");
        }

        public static string Encrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        #endregion

        #region ========解密========
        public static string Decrypt(string Text)
        {
            return Decrypt(Text, "dms");
        }

        public static string Decrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        #endregion

        #region MD5 加解密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="toCryString">加密字符串</param>
        /// <returns></returns>
        public static string MD5(string toCryString)
        {
            MD5CryptoServiceProvider hashmd5;
            hashmd5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(toCryString))).Replace("-", "").ToLower();//asp是小写,把所有字符变小写

            //byte[] result = Encoding.UTF8.GetBytes(toCryString);    //tbPass为输入密码的文本框
            //MD5 md5 = new MD5CryptoServiceProvider();
            //byte[] output = md5.ComputeHash(result);
            //return BitConverter.ToString(output).Replace("-", "");

        }

        /// <summary>
        /// MD5加密，输出为大写
        /// </summary>
        /// <param name="convertString">要加密的字符串</param>
        /// <returns>string</returns>
        public static string MD5ToUpper(string convertString)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

            return BitConverter.ToString(hashmd5.ComputeHash(Encoding.UTF8.GetBytes(convertString))).Replace("-", "").ToUpper();
        }

        public static string getMd5Hash(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString().ToUpper();
        }
        #endregion

        #region AES 加解密
        /// <summary>
        ///  AES 加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AesEncrypt(string str, string key = "")
        {
            if (key == "")
            {
                key = AesKey;
            }
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = System.Security.Cryptography.CipherMode.ECB,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };

            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        ///  AES 解密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AesDecrypt(string str, string key = "")
        {
            if (key == "")
            {
                key = AesKey;
            }

            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Convert.FromBase64String(str);

            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = System.Security.Cryptography.CipherMode.ECB,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };

            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }



        public static string AesEncrypt(string toEncrypt, string key, string iv)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.IV = ivArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.Zeros;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string AesDecrypt(string toDecrypt, string key, string iv)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv);
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.IV = ivArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.Zeros;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        #endregion

        #region 新东网HASH算法
        /// <summary>
        /// 新东网HASH算法
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string EncryptPassword(string pwd)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            var encoding = Encoding.GetEncoding("gb2312");
            byte[] bytesPwd = encoding.GetBytes(pwd);

            byte[] bytesSha1 = sha1.ComputeHash(bytesPwd);

            string result = Convert.ToBase64String(bytesSha1);
            return result;
        }
        #endregion

        #region  Base64 加解密

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="codeName">加密采用的编码方式</param>
        /// <param name="source">待加密的明文</param>
        /// <returns></returns>
        public static string EncodeBase64(Encoding encode, string source)
        {
            string result = "";
            byte[] bytes = encode.GetBytes(source);
            try
            {
                result = Convert.ToBase64String(bytes);
            }
            catch
            {
                result = source;
            }
            return result;
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

        #region  DesECB加密解密
        /// <summary>
        /// DesECB加密
        /// </summary>
        /// <param name="str">加密字符串</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        public static string EncryptDesECB(string str, string key)
        {
            //if (key.Length < 8 || string.IsNullOrEmpty(str))
            //{
            //    throw new Exception("加密key小于8或者加密字符串为空！");
            //}
            byte[] bKey = GetKeyDesECB(key);// Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] bIV = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            byte[] bStr = Encoding.UTF8.GetBytes(str);
            try
            {
                DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
                desc.Padding = PaddingMode.PKCS7;//补位  
                desc.Mode = CipherMode.ECB;//CipherMode.CBC  
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, desc.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(bStr, 0, bStr.Length);
                        cStream.FlushFinalBlock();
                        StringBuilder ret = new StringBuilder();
                        byte[] res = mStream.ToArray();
                        foreach (byte b in res)
                        {
                            ret.AppendFormat("{0:x2}", b);
                        }
                        return ret.ToString();
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// DesECB解密
        /// </summary>
        /// <param name="text">解密字符串</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        public static string DecryptDesECB(string text, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Padding = PaddingMode.PKCS7;//补位  
            des.Mode = CipherMode.ECB;//CipherMode.CBC  
            int len;
            len = text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = GetKeyDesECB(key);
            des.IV = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        private static byte[] GetKeyDesECB(string key)
        {
            byte[] arrKey = Encoding.UTF8.GetBytes(key);
            //创建一个空的8位字节数组（默认值为0）
            byte[] arrB = new byte[8];

            //将原始字节数组转换为8位
            for (int i = 0; i < arrKey.Length && i < arrB.Length; i++)
            {
                arrB[i] = arrKey[i];
            }

            return arrB;
        }
        #endregion
    }
}
