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
        /// <param name="dt">数据源</param>
        /// <param name="topTitle">大标题</param>
        /// <param name="columns">显示列</param>
        /// <param name="titles">标题</param>
        /// <param name="isId">是否显示自增长序号</param>
        /// <param name="footerRow">总计等</param>
        /// <param name="footerValue">页脚</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2015-01-15 14:25:38
        public static string Excel(this DataTable dt, string topTitle, string[] columns, string[] titles, bool isId, string[] footerRow, string footerValue)
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
                            sbHtml.AppendFormat("<td style='width:auto;text-align:center;font-size: 12px;vnd.ms-excel.numberformat:@'>{0}</td>", row[column]);
                        }
                    }
                }
                sbHtml.Append("</tr>");
            }
            if (footerRow != null && footerRow.Length > 0)
            {
                //总计等
                sbHtml.Append("<tr>");
                foreach (string value in footerRow)
                {
                    sbHtml.AppendFormat("<td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25'>{0}</td>", value);
                }
                sbHtml.Append("</tr>");
            }
            if (!string.IsNullOrWhiteSpace(footerValue))
            {
                sbHtml.Append(footerValue);//页脚
            }
            sbHtml.Append("</table>");
            return sbHtml.ToString();
        }

        #endregion


    }
}
