using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Napoleon.PublicCommon
{
    public static class IpFunc
    {

        /// <summary>
        /// 获取ip地址
        /// </summary>
        /// <returns></returns>
        public static string GetIp()
        {
            string ip = string.Empty;
            try
            {
                if (System.Web.HttpContext.Current != null)
                {
                    if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null) // 服务器
                    {
                        //得到真实的客户端地址
                        ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]; 
                    }
                    else//如果没有使用代理服务器或者得不到客户端的ip not using proxy or can't get the Client IP
                    {
                        //得到服务端的地址要判断  System.Web.HttpContext.Current 为空的情况
                        ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]; 

                    }
                }
            }
            catch (Exception ep)
            {
                ip = "没有正常获取IP，" + ep.Message;
            }

            return ip;
        }

    }
}
