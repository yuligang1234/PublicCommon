
using System;
using System.Data;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace Napoleon.PublicCommon
{
    public static class BaseFields
    {

        #region DataTable格式化Json

        /// <summary>
        ///  将DataTable转换成json(DataGrid格式){"total":"10","rows":[{"name":"123","age":"12"},{"name":"lity","age":"32"}]}
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="total">total</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-09-03 15:10:23
        public static string ConvertTableToJson(this DataTable dt, int total)
        {
            StringBuilder json = new StringBuilder();
            json.Append("{");
            json.AppendFormat("\"total\":\"{0}\",\"rows\":", total);
            json.Append("[");
            foreach (DataRow row in dt.Rows)
            {
                json.Append("{");
                foreach (DataColumn column in dt.Columns)
                {
                    json.AppendFormat("\"{0}\":\"{1}\",", column.ColumnName, row[column]);
                }
                json.Remove(json.Length - 1, 1);
                json.Append("},");
            }
            json.Remove(json.Length - 1, 1);
            json.Append("]");
            json.Append("}");
            return json.ToString();
        }

        /// <summary>
        ///  将DataTable序列化成Json(DataGrid格式)
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-10-25 10:54:37
        public static string SerializeTableToJson(this DataTable dt)
        {
            string json = "";
            if (dt.Rows.Count > 0)
            {
                json = JsonConvert.SerializeObject(dt);
            }
            return json;
        }

        #endregion

        #region Json和实体类的相互转换

        /// <summary>
        ///  json序列化为实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">The json.</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-10-28 14:15:41
        public static T DeserializeJsonToList<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        ///  实体类序列化为json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">The t.</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-10-28 14:30:14
        public static string SerializeListToJson<T>(this T t)
        {
            return JsonConvert.SerializeObject(t);
        }

        #endregion

        #region 格式化

        /// <summary>
        ///  将数组转换为 "'123','1234'"
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="split">分隔符</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-10-30 14:37:20
        public static string FormatArray(this string[] array, char split)
        {
            StringBuilder str = new StringBuilder();
            foreach (var ar in array)
            {
                str.AppendFormat("'{0}',", ar);
            }
            return str.ToString().Trim(split);
        }

        #endregion

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

        #region Cookies和Session

        /// <summary>
        ///  添加session
        /// </summary>
        /// <param name="sessionName">sessionName</param>
        /// <param name="obj">object</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-10-31 10:38:13
        public static void SessionInsert(string sessionName, object obj)
        {
            SessionRemove(sessionName);
            HttpContext.Current.Session.Add(sessionName, obj);
        }

        /// <summary>
        ///  移除session
        /// </summary>
        /// <param name="sessionName">sessionName</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-10-31 10:56:33
        public static void SessionRemove(string sessionName)
        {
            if (HttpContext.Current.Session[sessionName] != null)
            {
                HttpContext.Current.Session.Remove(sessionName);
            }
        }

        #endregion


    }
}
