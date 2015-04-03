using System;

namespace Napoleon.PublicCommon
{
    public static class DateTimeFunc
    {

        #region 时间日期操作

        /// <summary>
        ///  得到本月的第一天日期
        /// </summary>
        /// <param name="isTime">是否有时间</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-11-25 13:12:53
        public static DateTime GetFirstDayOfMonth(bool isTime)
        {
            DateTime dt = GetFirstDayOfMonth(DateTime.Now);
            if (isTime)
            {
                string times = dt + "00:00:00";
                return Convert.ToDateTime(times);
            }
            return dt;
        }

        /// <summary>
        ///  得到本月的最后一天的日期
        /// </summary>
        /// <param name="isTime">是否有时间</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-11-25 13:12:53
        public static DateTime GetLastDayOfMonth(bool isTime)
        {
            DateTime dt = GetLastDayOfMonth(DateTime.Now);
            if (isTime)
            {
                string times = dt + "23:59:59";
                return Convert.ToDateTime(times);
            }
            return dt;
        }

        /// <summary>
        ///  得到一个月的第一天
        /// </summary>
        /// <param name="someday">这个月的随便一天</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-11-25 13:12:53
        private static DateTime GetFirstDayOfMonth(DateTime someday)
        {
            int ts = 1 - someday.Day;
            DateTime result = someday.AddDays(ts);
            return result;
        }

        /// <summary>
        ///  得到一个月的最后一天
        /// </summary>
        /// <param name="someday">这个月的随便一天</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-11-25 13:12:53
        private static DateTime GetLastDayOfMonth(DateTime someday)
        {
            int totalDays = DateTime.DaysInMonth(someday.Year, someday.Month);
            int ts = totalDays - someday.Day;
            DateTime result = someday.AddDays(ts);
            return result;
        }

        

        #endregion
    }
}
