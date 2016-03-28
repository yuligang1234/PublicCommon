using System.Management;
using System.Net;
using System.Web;

namespace Napoleon.PublicCommon.Http
{
    public static class IpFunc
    {

        /// <summary>
        ///  获取用户ip地址
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-12 15:05:27
        public static string GetIp()
        {
            string ip = "";
            if (HttpContext.Current != null)//在多线程中不能使用
            {
                ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ip))
                {
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (string.IsNullOrEmpty(ip))
                {
                    ip = HttpContext.Current.Request.UserHostAddress;
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
