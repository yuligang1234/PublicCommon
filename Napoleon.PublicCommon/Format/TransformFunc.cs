
using System;

namespace Napoleon.PublicCommon.Format
{
    public static class TransformFunc
    {

        /// <summary>
        ///  将string转换成int,int的默认值设置为0
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="defaultNo">int默认值</param>
        /// Author  : Napoleon
        /// Created : 2015-06-19 11:14:58
        public static int StringToInt(this string value, int defaultNo = 0)
        {
            int number;
            if (!int.TryParse(value, out number))
            {
                number = defaultNo;
            }
            return number;
        }

        /// <summary>
        ///  将DateTime转换成String
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="defaultTime">默认时间</param>
        /// Author  : Napoleon
        /// Created : 2015-06-19 11:26:34
        public static DateTime StringToTime(this string value, DateTime defaultTime)
        {
            DateTime dt;
            if (!DateTime.TryParse(value, out dt))
            {
                dt = defaultTime;
            }
            return dt;
        }

        /// <summary>
        ///  将string转换成Decimal,Decimal的默认值设置为0
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="defaultDecimal">Decimal的默认值</param>
        /// Author  : Napoleon
        /// Created : 2015-06-19 13:45:04
        public static decimal StringToDecimal(this string value, decimal defaultDecimal = 0)
        {
            decimal dc;
            if (!decimal.TryParse(value, out dc))
            {
                dc = defaultDecimal;
            }
            return dc;
        }

    }
}
