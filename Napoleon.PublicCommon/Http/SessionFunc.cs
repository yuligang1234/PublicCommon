using System.Web;

namespace Napoleon.PublicCommon.Http
{
    public static class SessionFunc
    {


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



    }
}
