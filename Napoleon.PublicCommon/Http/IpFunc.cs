
using System.Management;
using System.Net;
using System.Web;

namespace Napoleon.PublicCommon.Http
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
            if (HttpContext.Current != null)
            {
                //判断客户端是否使用了代理(不一定能判断出来)
                if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null) 
                {
                    //得到真实的客户端地址
                    ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                else//如果没有使用代理服务器
                {
                    ip = HttpContext.Current.Request.UserHostAddress;
                    //得到服务端的地址要判断  System.Web.HttpContext.Current 为空的情况
                    //ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
            }
            return ip;
        }

        /// <summary>
        ///  获取电脑名称
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-26 14:26:36
        public static string GetHostName()
        {
            return Dns.GetHostName();
        }

        /// <summary>
        ///  获取本机MAC地址(服务器端)
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-26 14:29:22
        public static string GetLocalMacAddress()
        {
            string mac = string.Empty;
            ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection queryCollection = query.Get();
            foreach (ManagementBaseObject mo in queryCollection)
            {
                if (mo["IPEnabled"].ToString() == "True")
                {
                    mac = mo["MacAddress"].ToString();
                }
            }
            return mac;
        }


    }
}
