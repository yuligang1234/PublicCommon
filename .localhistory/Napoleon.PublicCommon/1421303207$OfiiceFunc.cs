using System;
using System.Data;
using System.Text;

namespace Napoleon.PublicCommon
{
    public static class OfiiceFunc
    {

        #region 导出Excel

        /// <summary>
        ///  导出的Execl样式设置
        /// </summary>
        /// Author  : 俞立钢
        /// Company : 标点电子技术有限公司
        /// Created : 2014-08-15 11:27:53
        public static string Excel(this DataTable dt, string topTitle, string[] columns, string[] titles, bool isId, string footer)
        {
            int i = 0;//序号
            var sbHtml = new StringBuilder();
            sbHtml.Append("<table border='1' cellspacing='0' cellpadding='0'>");
            if (!string.IsNullOrWhiteSpace(topTitle))
            {
                //大标题
                sbHtml.Append("<tr>");
                //标题（只能是一级标题）
                sbHtml.AppendFormat("<td colspan='{0}' style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25'>{1}</td>", dt.Columns.Count, topTitle);
                sbHtml.Append("</tr>");
            }
            sbHtml.Append("<tr>");
            //标题（只能是一级标题）
            foreach (string title in titles)
            {
                sbHtml.AppendFormat("<td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25'>{0}</td>", title);
            }
            sbHtml.Append("</tr>");
            //内容
            foreach (DataRow row in dt.Rows)
            {
                sbHtml.Append("<tr>");
                if (isId)//是否显示序号
                {
                    i++;
                    sbHtml.AppendFormat("<td style='text-align:center;font-size: 12px;height:20px;'>{0}</td>", i);
                }
                foreach (DataColumn column in dt.Columns)
                {
                    foreach (string hiddenColumn in columns)
                    {
                        if (column.ColumnName.Equals(hiddenColumn, StringComparison.OrdinalIgnoreCase))
                        {
                            sbHtml.AppendFormat("<td style='text-align:center;font-size: 12px;height:20px;vnd.ms-excel.numberformat:@'>{0}</td>", row[column]);
                        }
                    }
                }
                sbHtml.Append("</tr>");
            }
            if (!string.IsNullOrWhiteSpace(footer))
            {
                sbHtml.Append(footer);//页脚
            }
            sbHtml.Append("</table>");
            return sbHtml.ToString();
        }

        #endregion


    }
}
