using System;
using System.Web;
using Napoleon.PublicCommon.Format;

namespace Napoleon.PublicCommon.Http
{
    public static class CookieFunc
    {


        /// <summary>
        ///  写cookie
        /// </summary>
        /// <param name="strname">cookie名称</param>
        /// <param name="strvalue">cookie值</param>
        /// <param name="days">保存天数</param>
        /// Author  : Napoleon
        /// Created : 2015-01-07 20:03:23
        public static void WriteCookie(this string strvalue, string strname, int days = 1)
        {
            var cookie = HttpContext.Current.Request.Cookies[strname];
            cookie = cookie ?? new HttpCookie(strname);
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
        /// Author  : Napoleon
        /// Created : 2015-01-07 20:03:23
        public static void WriteCookie<T>(this T t, string strname, int days = 1)
        {
            var cookie = HttpContext.Current.Request.Cookies[strname];
            cookie = cookie ?? new HttpCookie(strname);
            cookie.Value = t.SerializeObject();
            if (days > 0)
            {
                cookie.Expires = DateTime.Now.AddDays(days);
            }
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        ///  读取cookie值
        /// </summary>
        /// <param name="strname">The strname.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-07 20:03:23
        public static string ReadCookie(this string strname)
        {
            var cookies = HttpContext.Current.Request.Cookies[strname];
            return cookies != null ? cookies.Value : "";
        }

        /// <summary>
        ///  读取cookie并返回Entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strname">The strname.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-07 20:03:23
        public static T ReadCookie<T>(this string strname) where T : class
        {
            var cookies = HttpContext.Current.Request.Cookies[strname];
            return cookies != null ? cookies.Value.DSerializeToObject<T>() : default(T);
        }

        /// <summary>
        ///  删除cookie
        /// </summary>
        /// <param name="strname">The strname.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-07 20:03:23
        public static void DeleteCookie(this string strname)
        {
            if (HttpContext.Current.Request.Cookies[strname] != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[strname];
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.AppendCookie(cookie);
            }
        }

    }
}
