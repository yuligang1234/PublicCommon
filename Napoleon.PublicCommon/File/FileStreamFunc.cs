
using System.IO;

namespace Napoleon.PublicCommon.File
{
    public static class FileStreamFunc
    {

        /// <summary>
        ///  将Stream转换成byte[]
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// Author  : Napoleon
        /// Created : 2015-05-30 11:26:19
        public static byte[] StreamToBytes(this Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary>
        ///  将byte[]转换成stream
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// Author  : Napoleon
        /// Created : 2015-05-30 11:26:37
        public static Stream BytesToStream(this byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary>
        ///  将stream转换成文件
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="fileName">c:/a.jpg</param>
        /// Author  : Napoleon
        /// Created : 2015-05-30 11:27:16
        public static void StreamToFile(this Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[]
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            // 把 byte[] 写入文件
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        /// <summary>
        ///  将文件转换成stream
        /// </summary>
        /// <param name="fileName">c:/a.jpg</param>
        /// Author  : Napoleon
        /// Created : 2015-05-30 11:28:01
        public static Stream FileToStream(this string fileName)
        {
            // 打开文件
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[]
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

    }
}
