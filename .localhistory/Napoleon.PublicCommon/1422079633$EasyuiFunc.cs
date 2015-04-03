using System.Data;
using System.Text;

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
        /// Author  : Napoleon
        /// Created : 2014-09-03 15:10:23
        public static string ConvertTableToGridJson(this DataTable dt, int total)
        {
            StringBuilder json = new StringBuilder();
            json.Append("{");
            json.AppendFormat("\"total\":\"{0}\",\"rows\":", total);
            json.Append("[");
            if (dt.Rows.Count > 0)//如果不判断的话，在dt没有数据的情况下，会出现格式错误{[]}
            {
                foreach (DataRow row in dt.Rows)
                {
                    json.Append("{");
                    foreach (DataColumn column in dt.Columns)
                    {
                        json.AppendFormat("\"{0}\":\"{1}\",", column.ColumnName, row[column].ToString().Replace("\r\n", ""));//去掉换行符
                    }
                    json.Remove(json.Length - 1, 1);
                    json.Append("},");
                }
                json.Remove(json.Length - 1, 1);
            }
            json.Append("]");
            json.Append("}");
            return json.ToString();
        }

        #endregion

        #region DataTable格式化Json(tree)

        /// <summary>
        ///  将DataTable转换成easyui使用的json
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-10 16:01:30
        public static string ConvertToTreeJson(this DataTable dt)
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
        /// Author  : Napoleon
        /// Created : 2015-01-10 16:00:40
        private static string ChildTreeJson(DataTable dt, string parentId)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                if (row["ParentId"].Equals(parentId))
                {
                    sb.Append("{");
                    sb.AppendFormat("\"id\":\"{0}\",", row["Id"]);
                    sb.AppendFormat("\"text\":\"{0}\"", row["Name"]);
                    sb.AppendFormat(",\"url\":\"{0}\"", row["Url"]);
                    if (!string.IsNullOrWhiteSpace(row["Icon"].ToString()))
                    {
                        sb.AppendFormat(",\"iconCls\":\"{0}\"", row["Icon"]);
                    }
                    //有子节点
                    if (GetChildTable(dt, row["Id"].ToString()).Rows.Count > 0)
                    {
                        sb.Append(",");
                        sb.Append("\"children\":[");
                        sb.Append(ChildTreeJson(dt, row["Id"].ToString()));
                        sb.Append("]");
                    }
                    sb.Append("},");
                }
            }
            return sb.ToString().Substring(0, sb.Length - 1);
        }

        /// <summary>
        ///  获取子菜单
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="id">The identifier.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-10 15:57:54
        private static DataTable GetChildTable(DataTable dt, string id)
        {
            DataTable child = dt.Clone();
            child.Clear();
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

        #region DataTable格式化Json(treegrid)

        /// <summary>
        ///  将DataTable转换成easyui使用的json
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-10 16:01:30
        public static string ConvertToTreeGridJson(this DataTable dt)
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
        /// Author  : Napoleon
        /// Created : 2015-01-10 16:00:40
        private static string ChildTreeGridJson(DataTable dt, string parentId)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                if (row["ParentId"].Equals(parentId))
                {
                    sb.Append("{");
                    sb.AppendFormat("\"id\":\"{0}\",", row["Id"]);
                    sb.AppendFormat("\"text\":\"{0}\"", row["Name"]);
                    sb.AppendFormat(",\"url\":\"{0}\"", row["Url"]);
                    if (!string.IsNullOrWhiteSpace(row["Icon"].ToString()))
                    {
                        sb.AppendFormat(",\"iconCls\":\"{0}\"", row["Icon"]);
                    }
                    //有子节点
                    if (GetChildTable(dt, row["Id"].ToString()).Rows.Count > 0)
                    {
                        sb.Append(",");
                        sb.Append("\"children\":[");
                        sb.Append(ChildTreeJson(dt, row["Id"].ToString()));
                        sb.Append("]");
                    }
                    sb.Append("},");
                }
            }
            return sb.ToString().Substring(0, sb.Length - 1);
        }

        #endregion

        #region DataTable格式化Json(combobox)

        /// <summary>
        ///  下拉框数据格式
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="columnId">columnId</param>
        /// <param name="columnText">columnText</param>
        /// Author  : Napoleon
        /// Created : 2015-01-19 09:40:15
        public static string ConvertToComboboxJson(this DataTable dt, string columnId, string columnText)
        {
            StringBuilder json = new StringBuilder();
            json.Append("[");
            if (dt.Rows.Count > 0)//如果不判断的话，在dt没有数据的情况下，会出现格式错误{[]}
            {
                foreach (DataRow row in dt.Rows)
                {
                    json.Append("{");
                    json.AppendFormat("\"id\":\"{0}\",", row[columnId]);
                    json.AppendFormat("\"text\":\"{0}\"", row[columnText]);
                    json.Append("},");
                }
                json.Remove(json.Length - 1, 1);
            }
            json.Append("]");
            return json.ToString();
        }

        #endregion


    }
}
