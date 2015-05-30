
namespace Napoleon.PublicCommon
{
    public static class BaseFields
    {

        #region 分隔符

        /// <summary>
        ///  分隔符(,)
        /// </summary>
        /// Author  : 俞立钢
        /// Company : 标点电子技术有限公司
        /// Created : 2014-08-15 16:27:13
        public static char CommaSplit = ',';

        /// <summary>
        ///  分隔符(-)
        /// </summary>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-10-30 10:38:05
        public static char BrSplit = '-';

        /// <summary>
        ///  分隔符(_)
        /// </summary>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-11-27 10:43:20
        public static char UnderLineSplit = '_';

        /// <summary>
        ///  分隔符(|)
        /// </summary>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-10-30 10:38:52
        public static char VerticalSplit = '|';

        #endregion

        #region 格式

        /// <summary>
        ///  时间格式
        /// </summary>
        /// Author  : 俞立钢
        /// Company : 标点电子技术有限公司
        /// Created : 2014-08-15 16:42:14
        public static string TimeFromat = string.Format("yyyy-MM-dd:hh:mm:ss");

        /// <summary>
        ///  时间格式
        /// </summary>
        /// Author  : 俞立钢
        /// Company : 标点电子技术有限公司
        /// Created : 2014-08-15 16:42:14
        public static string DateFromat = string.Format("yyyy-MM-dd");

        /// <summary>
        ///  导出名称的时间格式
        /// </summary>
        /// Author  : 俞立钢
        /// Company : 标点电子技术有限公司
        /// Created : 2014-08-15 16:42:14
        public static string ExeclTimeFromat = string.Format("yyyyMMddhhmmss");

        #endregion

        #region 加解密

        /// <summary>
        ///  对称加密算法的初始化向量(IV)
        /// </summary>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-12-11 14:46:59
        public static byte[] MbtIv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        #endregion


    }
}
