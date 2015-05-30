
using System.Text;

namespace Napoleon.PublicCommon
{
    public static class FormatFunc
    {

        #region 格式化

        /// <summary>
        ///  将数组转换为 "'123','1234'"
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
                str.AppendFormat("'{0}',", ar);
            }
            return str.ToString().Trim(split);
        }

        #endregion

    }
}
