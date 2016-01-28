
using System;
using System.IO;
using System.Security.Cryptography;

namespace Napoleon.PublicCommon.Cryptography
{

    /// <summary>
    ///  算法类型
    /// </summary>
    /// Author  : Napoleon
    /// Created : 2016-01-28 22:03:13
    public enum HashType
    {
        Md5,
        Sha1
    }

    public static class CreateHash
    {

        /// <summary>
        ///  计算文件Hash值,默认为MD5
        /// </summary>
        /// <param name="fileName">文件完整路径</param>
        /// <param name="hashType">算法类型(默认MD5)</param>
        /// Author  : Napoleon
        /// Created : 2016-01-28 22:05:02
        public static string HashFile(this string fileName, HashType hashType = HashType.Md5)
        {
            string value;
            HashAlgorithm hash;
            if (!System.IO.File.Exists(fileName))
            {
                value = "";
            }
            else
            {
                switch (hashType)
                {
                    case HashType.Md5:
                        hash = MD5.Create();
                        break;
                    case HashType.Sha1:
                        hash = SHA1.Create();
                        break;
                    default:
                        hash = MD5.Create();
                        break;
                }
                using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    byte[] hashBytes = hash.ComputeHash(fileStream);
                    //将byte[]转换成16进制
                    value = BitConverter.ToString(hashBytes).Replace("-", "");
                }
            }
            return value;
        }

    }
}
