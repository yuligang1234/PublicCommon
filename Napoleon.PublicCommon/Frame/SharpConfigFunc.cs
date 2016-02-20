using System.Web;
using SharpConfig;

namespace Napoleon.PublicCommon.Frame
{
    public class SharpConfigFunc
    {

        #region 读取操作

        /// <summary>
        ///  读取xml通用类
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="section">节点</param>
        /// Author  : Napoleon
        /// Created : 2016-02-20 09:43:31
        private static Section GetConfig(string fileName, string section)
        {
            string file = HttpRuntime.AppDomainAppPath + fileName;
            Configuration config = Configuration.LoadFromFile(file);
            Section select = config[section];
            return select;
        }

        /// <summary>
        ///  读取string
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2016-02-20 09:57:32
        public static string GetConfigString(string url, string section, string name)
        {
            return GetConfig(url, section)[name].StringValue;
        }

        /// <summary>
        ///  读取int
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2016-02-20 09:57:51
        public static int GetConfigInt(string url, string section, string name)
        {
            return GetConfig(url, section)[name].IntValue;
        }

        /// <summary>
        ///  读取bool
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2016-02-20 10:07:34
        public static bool GetConfigBool(string url, string section, string name)
        {
            return GetConfig(url, section)[name].BoolValue;
        }

        /// <summary>
        ///  读取float
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2016-02-20 10:07:48
        public static float GetConfigFloat(string url, string section, string name)
        {
            return GetConfig(url, section)[name].FloatValue;
        }

        /// <summary>
        ///  读取double
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2016-02-20 10:07:58
        public static double GetConfigDouble(string url, string section, string name)
        {
            return GetConfig(url, section)[name].DoubleValue;
        }

        #endregion


    }
}
