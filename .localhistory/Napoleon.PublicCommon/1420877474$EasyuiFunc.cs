using System.Data;
using System.Text;
using Newtonsoft.Json;

namespace Napoleon.PublicCommon
{
    public static class EasyuiFunc
    {

        #region DataTable格式化Json(datagrid)

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

        #region DataTable格式化Json(tree)

        /// <summary>
        ///  将DataTable转换成easyui使用的json
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2015-01-10 16:01:30
        public static string ConvertToTreeJson(DataTable dt)
        {
            StringBuilder json = new StringBuilder();
            json.Append("[");
            json.Append(ChildTreeJson(dt, "0"));//父节点为0
            json.Append("]");
            return json.ToString();
        }

        /// <summary>
        ///  子节点
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="parentId">The parent identifier.</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2015-01-10 16:00:40
        private static string ChildTreeJson(DataTable dt, string parentId)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                if (row["ParentId"].Equals(parentId))
                {
                    sb.Append("{");
                    sb.AppendFormat("\"id\":{0},", row["Id"]);
                    sb.AppendFormat("\"text\":\"{0}\"", row["Name"]);
                    //有子节点
                    if (GetChildTable(dt, row["Id"].ToString()).Rows.Count > 0)
                    {
                        sb.Append(",");
                    }
                    sb.Append("}");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        ///  获取子菜单
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="id">The identifier.</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2015-01-10 15:57:54
        private static DataTable GetChildTable(DataTable dt, string id)
        {
            DataTable child = new DataTable();
            foreach (DataRow row in dt.Rows)
            {
                if (row["ParentId"].Equals(id))
                {
                    child.Rows.Add(row.ItemArray);
                }
            }
            return child;
        }

        #endregion


    }
}
