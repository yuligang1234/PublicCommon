
using System.Text;
using Napoleon.PublicCommon.Field;

namespace Napoleon.PublicCommon.Format
{
    public static class FormatFunc
    {

        #region 格式化通用类

        /// <summary>
        ///  数组转换通用类
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="split">分隔符</param>
        /// <param name="isQuote">是否需要引号</param>
        /// Author  : Napoleon
        /// Created : 2014-10-30 14:37:20
        public static string FormatArray(this string[] array, char split, bool isQuote = true)
        {
            StringBuilder str = new StringBuilder();
            foreach (var ar in array)
            {
                if (isQuote)
                {
                    str.AppendFormat("'{0}',", ar);
                }
                else
                {
                    str.AppendFormat("{0},", ar);
                }
            }
            return str.ToString().Trim(split);
        }

        #endregion

        #region 具体格式化形式

        /// <summary>
        ///  将{1,2,3}转换成{'1','2','3'}
        /// </summary>
        /// <param name="array">The array.</param>
        /// Author  : Napoleon
        /// Created : 2015-05-30 11:20:04
        public static string SwitchArray(this string array)
        {
            string[] arrays = array.Split(BaseFields.CommaSplit);
            string result = arrays.FormatArray(BaseFields.CommaSplit);
            return result;
        }

        #endregion


    }
}
