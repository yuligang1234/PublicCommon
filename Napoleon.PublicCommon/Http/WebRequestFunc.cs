
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Napoleon.PublicCommon.Http
{

    /// <summary>
    ///  表单参数
    /// </summary>
    /// Author  : Napoleon
    /// Created : 2015-05-30 10:53:07
    public class FormItem
    {
        public string Name { get; set; }
        public ParamType ParamType { get; set; }
        public string Value { get; set; }
    }

    /// <summary>
    ///  参数类型
    /// </summary>
    /// Author  : Napoleon
    /// Created : 2015-05-30 10:55:29
    public enum ParamType
    {
        ///
        /// 文本类型
        ///
        Text,
        ///
        /// 文件类型(路径,c:\a.jpg)
        ///
        File
    }

    public static class WebRequestFunc
    {

        #region WebRequest方式

        /// <summary>
        ///  模拟form表单提交
        /// </summary>
        /// <param name="list">表单参数</param>
        /// <param name="httpUrl">提交地址</param>
        /// <param name="encoding">编码方式</param>
        /// Author  : Napoleon
        /// Created : 2015-05-29 16:30:55
        public static string PostFormData(this List<FormItem> list, string httpUrl, Encoding encoding)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            //请求 
            WebRequest request = WebRequest.Create(httpUrl);
            request.Method = "POST";
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            //组织表单数据 
            StringBuilder form = new StringBuilder();
            foreach (FormItem item in list)
            {
                switch (item.ParamType)
                {
                    case ParamType.Text:
                        form.Append("--" + boundary);
                        form.Append("\r\n");
                        form.Append("Content-Disposition: form-data; name=\"" + item.Name + "\"");
                        form.Append("\r\n\r\n");
                        form.Append(item.Value);
                        form.Append("\r\n");
                        break;
                    case ParamType.File:
                        form.Append("--" + boundary);
                        form.Append("\r\n");
                        form.Append("Content-Disposition: form-data; name=\"" + item.Name + "\"; filename=\"" + item.Value + "\"");
                        form.Append("\r\n");
                        form.Append("Content-Type: application/octet-stream");
                        form.Append("\r\n\r\n");
                        break;
                }
            }
            byte[] formData = encoding.GetBytes(form.ToString());
            //结尾 
            byte[] footData = encoding.GetBytes("\r\n--" + boundary + "--\r\n");
            List<FormItem> fileList = list.Where(f => f.ParamType == ParamType.File).ToList();
            long length = formData.Length + footData.Length;//post字节总长度
            foreach (FormItem fi in fileList)
            {
                FileStream fileStream = new FileStream(fi.Value, FileMode.Open, FileAccess.Read);
                length += fileStream.Length;
                fileStream.Close();
            }
            request.ContentLength = length;
            Stream requestStream = request.GetRequestStream();
            //发送表单参数 
            requestStream.Write(formData, 0, formData.Length);
            foreach (FormItem fd in fileList)
            {
                using (FileStream fileStream = new FileStream(fd.Value, FileMode.Open, FileAccess.Read))
                {
                    //文件内容 
                    byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)fileStream.Length))];
                    int bytesRead;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        requestStream.Write(buffer, 0, bytesRead);
                    }
                    //结尾 
                    requestStream.Write(footData, 0, footData.Length);
                }
            }
            requestStream.Close();
            //接受返回值 
            WebResponse pos = request.GetResponse();
            string json = "";
            Stream responseStream = pos.GetResponseStream();
            if (responseStream != null)
            {
                StreamReader sr = new StreamReader(responseStream, encoding);
                json = sr.ReadToEnd().Trim();
                sr.Close();
            }
            pos.Close();
            return json;
        }

        /// <summary>
        ///  form提交数据
        /// </summary>
        /// <param name="httpUrl">提交地址</param>
        /// <param name="parameter">参数/json/数值等</param>
        /// <param name="method">提交方式</param>
        /// <param name="encoding">编码方式(UTF-8,GB2312等)</param>
        /// <param name="contentType">提交数据类型</param>
        /// Author  : Napoleon
        /// Created : 2015-05-29 16:34:48
        public static string PostJsonData(this string httpUrl, string parameter, HttpMethod method = HttpMethod.Post, string encoding = "UTF-8", string contentType = ContentTypes.FormType)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(httpUrl);
            request.Method = method.ToString();
            request.ContentType = contentType;
            byte[] payload = Encoding.GetEncoding(encoding).GetBytes(parameter);
            request.ContentLength = payload.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = response.GetResponseStream();
            string strValue = "";
            if (s != null)
            {
                StreamReader reader = new StreamReader(s, Encoding.GetEncoding(encoding));
                strValue = reader.ReadToEnd().Trim();
            }
            return strValue;
        }

        #endregion

    }
}
