
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

    /// <summary>
    ///  格式类型
    /// </summary>
    /// Author  : Napoleon
    /// Created : 2016-01-18 22:03:44
    public class ContentTypes
    {
        public const string JsonType = "application/json";
        public const string FormType = "application/x-www-form-urlencoded";
        public const string PdfType = "application/pdf";
    }

    public static class WebClientFunc
    {

        #region WebClient方式

        /// <summary>
        ///  获取数据（GET/POST）
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="parameter">参数</param>
        /// <param name="method">提交方式(默认get)</param>
        /// <param name="encoding">编码方式(默认utf-8)</param>
        /// <param name="contentType">数据类型(默认json)</param>
        /// Author  : Napoelon
        /// Created : 2014-10-18 13:55:48
        public static string RequestUpDownData(this string url, string parameter = "", HttpMethod method = HttpMethod.Get, string encoding = "UTF-8", string contentType = ContentTypes.JsonType)
        {
            WebClient webClient = new WebClient();
            webClient.Credentials = CredentialCache.DefaultCredentials;
            byte[] byteParameter = Encoding.GetEncoding(encoding).GetBytes(parameter);
            byte[] byteResult;
            switch (method)
            {
                case HttpMethod.Get:
                    byteResult = webClient.DownloadData(url);
                    break;
                case HttpMethod.Post:
                    webClient.Headers.Add("Content-Type", contentType);
                    byteResult = webClient.UploadData(url, "POST", byteParameter);
                    break;
                default:
                    byteResult = webClient.DownloadData(url);
                    break;

            }
            string result = Encoding.GetEncoding(encoding).GetString(byteResult);
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
        ///  上传数据，并返回结果
        /// </summary>
        /// <param name="httpUrl">The HTTP URL.</param>
        /// <param name="method">The method.</param>
        /// <param name="bytes">参数</param>
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
        ///  上传数据，并返回结果
        /// </summary>
        /// <param name="httpUrl">URL</param>
        /// <param name="json">参数格式(json格式)</param>
        /// <param name="method">提交方式(默认post)</param>
        /// <param name="contentType">数据类型(默认json)</param>
        /// <param name="encoding">编码格式</param>
        /// Author  : Napoleon
        /// Created : 2014-10-18 14:29:28
        public static string RequestUploadJson(this string httpUrl, string json, string method = "POST", string contentType = ContentTypes.JsonType, string encoding = "UTF-8")
        {
            WebClient webClient = new WebClient();
            webClient.Credentials = CredentialCache.DefaultCredentials;
            webClient.Encoding = Encoding.GetEncoding(encoding);
            webClient.Headers.Add("Content-Type", contentType);
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
