
namespace Napoleon.PublicCommon
{
    public static class BaseFields
    {

        #region 符号

        /// <summary>
        ///  换行符
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-02-11 11:09:59
        public static string SymbolNewLine = "\r\n";

        /// <summary>
        ///  空白符号
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-02-11 11:10:39
        public static string SymbolBlank = "";

        /// <summary>
        ///  单引号
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-02-11 11:14:49
        public static string SymbolSigleQuotes = "'";

        /// <summary>
        ///  双引号
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-02-11 11:19:33
        public static string SymbolDoubleQuotes = "\"";

        #endregion


        #region 分隔符

        /// <summary>
        ///  分隔符(,)
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2014-08-15 16:27:13
        public static char CommaSplit = ',';

        /// <summary>
        ///  分隔符(-)
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2014-10-30 10:38:05
        public static char BrSplit = '-';

        /// <summary>
        ///  分隔符(_)
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2014-11-27 10:43:20
        public static char UnderLineSplit = '_';

        /// <summary>
        ///  分隔符(|)
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2014-10-30 10:38:52
        public static char VerticalSplit = '|';

        #endregion

        #region 格式

        /// <summary>
        ///  时间格式
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2014-08-15 16:42:14
        public static string TimeFromat = string.Format("yyyy-MM-dd hh:mm:ss");

        /// <summary>
        ///  时间格式
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2014-08-15 16:42:14
        public static string DateFromat = string.Format("yyyy-MM-dd");

        /// <summary>
        ///  导出名称的时间格式
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2014-08-15 16:42:14
        public static string ExeclTimeFromat = string.Format("yyyyMMddhhmmssffff");

        #endregion

        #region 加解密

        /// <summary>
        ///  对称加密算法的初始化向量(IV)
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2014-12-11 14:46:59
        public static byte[] MbtIv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        ///  MD5字符替换
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2014-12-11 13:41:56
        public static string Md5Replace = string.Format("");

        /// <summary>
        ///  SHA1字符替换
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2014-12-11 13:41:56
        public static string Sha1Replace = string.Format("");

        #endregion


    }
}
