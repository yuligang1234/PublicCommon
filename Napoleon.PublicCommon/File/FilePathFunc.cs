using System;
using System.IO;
using System.Web;

namespace Napoleon.PublicCommon.File
{
    public static class FilePathFunc
    {

        /// <summary>
        ///  将文件路径转换成程序路径
        /// </summary>
        /// <param name="path">路径</param>
        /// Author  : Napoleon
        /// Created : 2016-03-28 10:53:59
        public static string GetBasePath(this string path)
        {
            if (path.ToLower().StartsWith("http://"))
            {
                return path;
            }
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(path);
            }
            //多线程下
            path = path.Replace("/", "\\");
            if (path.StartsWith("\\"))
            {
                path = path.TrimStart('\\');
            }
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
        }

        /// <summary>
        ///  创建文件夹
        /// </summary>
        /// <param name="path">路径</param>
        /// Author  : Napoleon
        /// Created : 2016-03-28 11:20:50
        public static string OpenFloder(this string path)
        {
            string floderPath = GetBasePath(path);
            if (!Directory.Exists(floderPath))
            {
                Directory.CreateDirectory(floderPath);
            }
            return floderPath;
        }

        /// <summary>
        ///  创建文件
        /// </summary>
        /// <param name="path">路径</param>
        /// Author  : Napoleon
        /// Created : 2016-03-28 11:20:50
        public static string OpenFile(this string path)
        {
            string filePath = GetBasePath(path);
            if (!System.IO.File.Exists(filePath))
            {
                System.IO.File.Create(filePath).Close();
            }
            return filePath;
        }

    }
}
