using System;
using System.Web;
using Napoleon.PublicCommon.Format;

namespace Napoleon.PublicCommon.Http
{
    public static class CookieSessionFunc
    {

        #region Cookies和Session

        /// <summary>
        ///  添加session
        /// </summary>
        /// <param name="sessionName">sessionName</param>
        /// <param name="obj">object</param>
        /// <param name="timeout">过期时间(默认一小时)</param>
        /// Author  : Napoleon
        /// Created : 2014-10-31 10:38:13
        public static void SessionInsert(this string sessionName, object obj, int timeout = 60)
        {
            SessionRemove(sessionName);
            HttpContext.Current.Session.Add(sessionName, obj);
            HttpContext.Current.Session.Timeout = timeout;
        }

        /// <summary>
        ///  移除session
        /// </summary>
        /// <param name="sessionName">sessionName</param>
        /// Author  : Napoleon
        /// Created : 2014-10-31 10:56:33
        public static void SessionRemove(this string sessionName)
        {
            if (HttpContext.Current.Session[sessionName] != null)
            {
                HttpContext.Current.Session.Remove(sessionName);
            }
        }

        /// <summary>
        ///  获取session
        /// </summary>
        /// <param name="sessionName">sessionName</param>
        /// Author  : Napoleon
        /// Created : 2014-10-31 10:56:33
        public static object GetSession(this string sessionName)
        {
            return HttpContext.Current.Session[sessionName];
        }

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
        /// Author  : Napoleon
        /// Created : 2015-01-07 20:03:23
        public static void WriteCookie<T>(this T t, string strname, int days = 1)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strname];
            if (cookie == null)
            {
                cookie = new HttpCookie(strname);
            }
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
        /// Author  : Napoleon
        /// Created : 2015-01-07 20:03:23
        public static T ReadCookie<T>(this string strname) where T : class
        {
            if (HttpContext.Current.Request.Cookies[strname] != null)
            {
                return HttpContext.Current.Request.Cookies[strname].Value.DSerializeToObject<T>();
            }
            return default(T);
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

        #endregion

    }
}
