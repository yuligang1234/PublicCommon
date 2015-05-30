
using System.Net;
using System.Text;

namespace Napoleon.PublicCommon.Http
{

    /// <summary>
    ///  提交请求的方式
    /// </summary>
    /// Author  : Napoelon
    /// Created : 2014-10-18 14:12:38
    public enum HttpMethod
    {
        Get,
        Post,
    }

    public static class WebClientFunc
    {

        #region WebClient方式

        /// <summary>
        ///  获取数据（GET/POST）
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="method">(get/post)</param>
        /// <param name="parameter">参数</param>
        /// Author  : Napoelon
        /// Created : 2014-10-18 13:55:48
        public static string RequestUpDownData(this string url, HttpMethod method, string parameter = "")
        {
            WebClient webClient = new WebClient();
            webClient.Credentials = CredentialCache.DefaultCredentials;
            byte[] byteParameter = Encoding.UTF8.GetBytes(parameter);
            byte[] byteResult;
            switch (method)
            {
                case HttpMethod.Get:
                    byteResult = webClient.DownloadData(url);
                    break;
                case HttpMethod.Post:
                    byteResult = webClient.UploadData(url, "POST", byteParameter);
                    break;
                default:
                    byteResult = webClient.DownloadData(url);
                    break;

            }
            string result = Encoding.UTF8.GetString(byteResult);
            return result;
        }

        /// <summary>
        ///  上传文件，并返回结果
        /// </summary>
        /// <param name="httpUrl">The HTTP URL.</param>
        /// <param name="method">The method.</param>
        /// <param name="fileUrl">The file URL.</param>
        /// Author  : Napoleon
        /// Created : 2014-10-18 14:29:28
        public static string RequestUploadFile(this string httpUrl, string method, string fileUrl)
        {
            WebClient webClient = new WebClient();
            webClient.Credentials = CredentialCache.DefaultCredentials;
            byte[] byteResult = webClient.UploadFile(httpUrl, method, fileUrl);
            string result = Encoding.UTF8.GetString(byteResult);
            return result;
        }

        /// <summary>
        ///  上传文件，并返回结果
        /// </summary>
        /// <param name="httpUrl">The HTTP URL.</param>
        /// <param name="method">The method.</param>
        /// <param name="bytes">The file URL.</param>
        /// Author  : Napoleon
        /// Created : 2014-10-18 14:29:28
        public static string RequestUploadData(this string httpUrl, string method, byte[] bytes)
        {
            WebClient webClient = new WebClient();
            webClient.Credentials = CredentialCache.DefaultCredentials;
            byte[] byteResult = webClient.UploadData(httpUrl, method, bytes);
            string result = Encoding.UTF8.GetString(byteResult);
            return result;
        }

        /// <summary>
        ///  上传文件，并返回结果
        /// </summary>
        /// <param name="httpUrl">The HTTP URL.</param>
        /// <param name="method">The method.</param>
        /// <param name="json">The file URL.</param>
        /// Author  : Napoleon
        /// Created : 2014-10-18 14:29:28
        public static string RequestUploadString(this string httpUrl, string method, string json)
        {
            WebClient webClient = new WebClient();
            webClient.Credentials = CredentialCache.DefaultCredentials;
            webClient.Encoding = Encoding.UTF8;
            string result = webClient.UploadString(httpUrl, method, json);
            return result;
        }

        /// <summary>
        ///  下载文件
        /// </summary>
        /// <param name="httpUrl">httpUrl</param>
        /// <param name="fileUrl">fileUrl</param>
        /// Author  : Napoleon
        /// Created : 2014-10-18 14:38:41
        public static void RequestDownloadFile(this string httpUrl, string fileUrl)
        {
            WebClient webClient = new WebClient();
            webClient.Credentials = CredentialCache.DefaultCredentials;
            webClient.DownloadFile(httpUrl, fileUrl);
        }

        #endregion

    }
}
