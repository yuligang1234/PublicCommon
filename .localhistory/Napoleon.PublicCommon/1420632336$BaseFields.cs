
using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace Napoleon.PublicCommon
{
    public static class BaseFields
    {

        #region 序列化和反序列化

        /// <summary>
        /// 序列化为字节流
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject(object obj)
        {
            string result;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                byte[] byt = ms.ToArray();
                result = Convert.ToBase64String(byt);
                ms.Flush();
            }
            return result;
        }

        /// <summary>
        /// 反序列化字节流
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T DSerializeToObject<T>(string str) where T : class
        {
            T t;
            BinaryFormatter bf = new BinaryFormatter();
            byte[] byt = Convert.FromBase64String(str);
            using (MemoryStream ms = new MemoryStream(byt, 0, byt.Length))
            {
                t = bf.Deserialize(ms) as T;
            }
            return t;
        }

        #endregion

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

        /// <summary>
        ///  写cookie
        /// </summary>
        /// <param name="strname">cookie名称</param>
        /// <param name="strvalue">cookie值</param>
        /// <param name="days">保存天数</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2015-01-07 20:03:23
        public static void WriteCookie(string strname, string strvalue, int days = 1)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strname];
            if (cookie == null)
                cookie = new HttpCookie(strname);
            cookie.Value = strvalue;
            if (days > 0)
                cookie.Expires = DateTime.Now.AddDays(days);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        ///  写cookie
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strname">The strname.</param>
        /// <param name="t">The t.</param>
        /// <param name="days">The days.</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2015-01-07 20:03:23
        public static void WriteCookie<T>(string strname, T t, int days = 1)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strname];
            if (cookie == null)
                cookie = new HttpCookie(strname);
            cookie.Value = SerializeObject(t);
            if (days > 0)
                cookie.Expires = DateTime.Now.AddDays(days);

            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        ///  读取cookie值
        /// </summary>
        /// <param name="strname">The strname.</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2015-01-07 20:03:23
        public static string ReadCookie(string strname)
        {
            if (HttpContext.Current.Request.Cookies[strname] != null)
            {
                return HttpContext.Current.Request.Cookies[strname].Value;
            }
            return "";
        }

        /// <summary>
        ///  读取cookie并返回Entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strname">The strname.</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2015-01-07 20:03:23
        public static T ReadCookieAsObj<T>(string strname) where T : class
        {
            if (HttpContext.Current.Request.Cookies[strname] != null)
            {
                return DSerializeToObject<T>(HttpContext.Current.Request.Cookies[strname].Value);
            }
            return default(T);
        }

        /// <summary>
        ///  删除cookie
        /// </summary>
        /// <param name="strname">The strname.</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2015-01-07 20:03:23
        public static void DeleteCookie(string strname)
        {
            if (HttpContext.Current.Request.Cookies[strname] != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[strname];
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.AppendCookie(cookie);
            }
        }

        #endregion


    }
}
