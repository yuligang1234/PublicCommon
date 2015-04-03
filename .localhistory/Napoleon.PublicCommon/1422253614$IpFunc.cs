
using System.Net;

namespace Napoleon.PublicCommon
{
    public static class IpFunc
    {

        /// <summary>
        ///  获取ip地址
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-12 15:05:27
        public static string GetIp()
        {
            string ip = string.Empty;
            if (System.Web.HttpContext.Current != null)
            {
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null) // 服务器
                {
                    //得到真实的客户端地址
                    ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                else//如果没有使用代理服务器或者得不到客户端的
                {
                    //得到服务端的地址要判断  System.Web.HttpContext.Current 为空的情况
                    ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
            }
            return ip;
        }

        public static string GetHostName()
        {
            return Dns.GetHostName();
        }

    }
}
