
using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace Napoleon.PublicCommon
{
    public static class BaseFunc
    {

        #region DataTable格式化Json

        /// <summary>
        ///  将DataTable转换成json(DataGrid格式){"total":"10","rows":[{"name":"123","age":"12"},{"name":"lity","age":"32"}]}
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="total">total</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-09-03 15:10:23
        public static string ConvertTableToJson(this DataTable dt, int total)
        {
            StringBuilder json = new StringBuilder();
            json.Append("{");
            json.AppendFormat("\"total\":\"{0}\",\"rows\":", total);
            json.Append("[");
            foreach (DataRow row in dt.Rows)
            {
                json.Append("{");
                foreach (DataColumn column in dt.Columns)
                {
                    json.AppendFormat("\"{0}\":\"{1}\",", column.ColumnName, row[column]);
                }
                json.Remove(json.Length - 1, 1);
                json.Append("},");
            }
            json.Remove(json.Length - 1, 1);
            json.Append("]");
            json.Append("}");
            return json.ToString();
        }

        /// <summary>
        ///  将DataTable序列化成Json(DataGrid格式)
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-10-25 10:54:37
        public static string SerializeTableToJson(this DataTable dt)
        {
            string json = "";
            if (dt.Rows.Count > 0)
            {
                json = JsonConvert.SerializeObject(dt);
            }
            return json;
        }

        #endregion

        #region Json和实体类的相互转换

        /// <summary>
        ///  json序列化为实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">The json.</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-10-28 14:15:41
        public static T DeserializeJsonToList<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        ///  实体类序列化为json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">The t.</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-10-28 14:30:14
        public static string SerializeListToJson<T>(this T t)
        {
            return JsonConvert.SerializeObject(t);
        }

        #endregion

        #region 格式化

        /// <summary>
        ///  将数组转换为 "'123','1234'"
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="split">分隔符</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-10-30 14:37:20
        public static string FormatArray(this string[] array, char split)
        {
            StringBuilder str = new StringBuilder();
            foreach (var ar in array)
            {
                str.AppendFormat("'{0}',", ar);
            }
            return str.ToString().Trim(split);
        }

        #endregion

    }
}
