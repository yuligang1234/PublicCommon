
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Napoleon.PublicCommon.Field;

namespace Napoleon.PublicCommon.Cryptography
{
    public static class EncrypteFunc
    {

        #region 单向散列算法

        /// <summary>
        ///  MD5加密
        /// </summary>
        /// <param name="value">明文</param>
        /// Author  : Napoleon
        /// Created : 2014-12-10 11:29:05
        public static string GetMd5(this string value)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            string bits = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(value)));
            return bits.Replace("-", BaseFields.Md5Replace);
        }

        /// <summary>
        ///  SHA1加密
        /// </summary>
        /// <param name="value">明文</param>
        /// Author  : Napoleon
        /// Created : 2014-12-10 15:25:52
        public static string GetSha1(this string value)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            string bits = BitConverter.ToString(sha1.ComputeHash(Encoding.Default.GetBytes(value)));
            return bits.Replace("-", BaseFields.Sha1Replace);
        }

        #endregion

        #region 常用密钥算法

        /// <summary>
        ///  密钥算法通用加密方法
        /// </summary>
        /// <param name="value">明文</param>
        /// <param name="cryptoTransform">加密器对象</param>
        /// Author  : Napoleon
        /// Created : 2014-12-11 14:44:11
        private static string EncrypteMethod(string value, ICryptoTransform cryptoTransform)
        {
            byte[] mbtEncryptString = Encoding.Default.GetBytes(value);
            MemoryStream mstream = new MemoryStream();
            CryptoStream mcstream = new CryptoStream(mstream, cryptoTransform, CryptoStreamMode.Write);
            mcstream.Write(mbtEncryptString, 0, mbtEncryptString.Length);
            mcstream.FlushFinalBlock();
            string encrypt = Convert.ToBase64String(mstream.ToArray());
            mstream.Close();
            mstream.Dispose();
            mcstream.Close();
            mcstream.Dispose();
            return encrypt;
        }

        /// <summary>
        ///  密钥算法通用解密方法
        /// </summary>
        /// <param name="value">密文</param>
        /// <param name="cryptoTransform">加密器对象</param>
        /// Author  : Napoleon
        /// Created : 2014-12-11 14:44:11
        private static string DecrypteMethod(string value, ICryptoTransform cryptoTransform)
        {
            byte[] mbtEncryptString = Convert.FromBase64String(value);
            MemoryStream mstream = new MemoryStream();
            CryptoStream mcstream = new CryptoStream(mstream, cryptoTransform, CryptoStreamMode.Write);
            mcstream.Write(mbtEncryptString, 0, mbtEncryptString.Length);
            mcstream.FlushFinalBlock();
            string decrypt = Encoding.Default.GetString(mstream.ToArray());
            mstream.Close();
            mstream.Dispose();
            mcstream.Close();
            mcstream.Dispose();
            return decrypt;
        }

        /// <summary>
        ///  DES加密
        /// </summary>
        /// <param name="value">明文</param>
        /// <param name="key">密钥</param>
        /// Author  : Napoleon
        /// Created : 2014-12-11 13:44:46
        public static string EncrypteDes(this string value, string key)
        {
            DESCryptoServiceProvider mDesProvider = new DESCryptoServiceProvider();
            ICryptoTransform cryptoTransform = mDesProvider.CreateEncryptor(Encoding.Default.GetBytes(key),
                BaseFields.MbtIv);
            return EncrypteMethod(value, cryptoTransform);
        }

        /// <summary>
        ///  DES解密
        /// </summary>
        /// <param name="value">密文</param>
        /// <param name="key">密钥</param>
        /// Author  : Napoleon
        /// Created : 2014-12-11 14:31:15
        public static string DecrypteDes(this string value, string key)
        {
            DESCryptoServiceProvider mDesProvider = new DESCryptoServiceProvider();
            ICryptoTransform cryptoTransform = mDesProvider.CreateDecryptor(Encoding.Default.GetBytes(key),
                BaseFields.MbtIv);
            return DecrypteMethod(value, cryptoTransform);
        }

        /// <summary>
        ///  RC2加密
        /// </summary>
        /// <param name="value">明文</param>
        /// <param name="key">密钥</param>
        /// Author  : Napoleon
        /// Created : 2014-12-11 13:44:46
        public static string EncrypteRc2(this string value, string key)
        {
            RC2CryptoServiceProvider mRc2Provider = new RC2CryptoServiceProvider();
            ICryptoTransform cryptoTransform = mRc2Provider.CreateEncryptor(Encoding.Default.GetBytes(key),
                BaseFields.MbtIv);
            return EncrypteMethod(value, cryptoTransform);
        }

        /// <summary>
        ///  RC2解密
        /// </summary>
        /// <param name="value">密文</param>
        /// <param name="key">密钥</param>
        /// Author  : Napoleon
        /// Created : 2014-12-11 14:31:15
        public static string DecrypteRc2(this string value, string key)
        {
            RC2CryptoServiceProvider mRc2Provider = new RC2CryptoServiceProvider();
            ICryptoTransform cryptoTransform = mRc2Provider.CreateDecryptor(Encoding.Default.GetBytes(key),
                BaseFields.MbtIv);
            return DecrypteMethod(value, cryptoTransform);
        }

        /// <summary>
        ///  AES加密
        /// </summary>
        /// <param name="value">明文</param>
        /// <param name="key">密钥</param>
        /// Author  : Napoleon
        /// Created : 2014-12-11 13:44:46
        public static string EncrypteAes(this string value, string key)
        {
            Rijndael aes = Rijndael.Create();
            ICryptoTransform cryptoTransform = aes.CreateEncryptor(Encoding.Default.GetBytes(key),
                BaseFields.MbtIv);
            return EncrypteMethod(value, cryptoTransform);
        }

        /// <summary>
        ///  AES解密
        /// </summary>
        /// <param name="value">密文</param>
        /// <param name="key">密钥</param>
        /// Author  : Napoleon
        /// Created : 2014-12-11 14:31:15
        public static string DecrypteAes(this string value, string key)
        {
            Rijndael aes = Rijndael.Create();
            ICryptoTransform cryptoTransform = aes.CreateDecryptor(Encoding.Default.GetBytes(key),
                BaseFields.MbtIv);
            return DecrypteMethod(value, cryptoTransform);
        }

        #endregion

    }
}
