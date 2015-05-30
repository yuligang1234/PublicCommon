
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace Napoleon.PublicCommon
{
    public static class NpoiFunc
    {

        /// <summary>
        ///  创建工作簿
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="topTitle">The top title.</param>
        /// <param name="title">The title.</param>
        /// <param name="titleFileds">titleFileds</param>
        /// Author  : Napoleon
        /// Created : 2015-01-15 13:59:30
        public static MemoryStream CreateSheet(this DataTable dt, string[] title, string[] titleFileds, string topTitle = "")
        {
            MemoryStream ms = new MemoryStream();
            HSSFWorkbook workbook = new HSSFWorkbook();

            #region 样式设置

            #region 头部大标题样式

            ICellStyle topTitleStyle = workbook.CreateCellStyle();//单元格样式
            topTitleStyle.Alignment = HorizontalAlignment.Center;//水平居中
            topTitleStyle.VerticalAlignment = VerticalAlignment.Center;//垂直居中
            IFont topTitleFont = workbook.CreateFont();//字体样式
            topTitleFont.FontName = "黑体";
            topTitleFont.FontHeightInPoints = 16;
            topTitleStyle.SetFont(topTitleFont);

            #endregion

            #region 标题样式

            ICellStyle titleStyle = workbook.CreateCellStyle();//单元格样式
            titleStyle.Alignment = HorizontalAlignment.Center;//水平居中
            titleStyle.VerticalAlignment = VerticalAlignment.Center;//垂直居中
            IFont titleFont = workbook.CreateFont();//字体样式
            titleFont.FontName = "宋体";
            titleFont.FontHeightInPoints = 12;
            titleFont.Boldweight = 10;
            titleStyle.SetFont(titleFont);

            #endregion

            #region 内容样式

            ICellStyle contentStyle = workbook.CreateCellStyle();//单元格样式
            contentStyle.Alignment = HorizontalAlignment.Center;//水平居中
            contentStyle.VerticalAlignment = VerticalAlignment.Center;//垂直居中
            IFont contentFont = workbook.CreateFont();//字体样式
            contentFont.FontName = "宋体";
            contentFont.FontHeightInPoints = 12;
            contentStyle.SetFont(contentFont);

            #endregion

            #endregion

            //创建一个名称为sheet1的工作表
            ISheet sheet = workbook.CreateSheet("sheet1");
            int rowIndex = 0;//从第一行开始
            if (!string.IsNullOrWhiteSpace(topTitle))
            {
                //大标题
                IRow topRow = sheet.CreateRow(rowIndex);
                ICell topCell = topRow.CreateCell(0);
                topCell.CellStyle = topTitleStyle;
                topCell.SetCellValue(topTitle);
                sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, dt.Columns.Count));
                rowIndex++;
            }
            if (title != null)
            {
                //标题
                IRow headerRow = sheet.CreateRow(rowIndex);
                //循环添加标题
                foreach (string filed in titleFileds)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        ICell cell = headerRow.CreateCell(column.Ordinal);
                        cell.CellStyle = contentStyle;
                        cell.SetCellValue(column.ColumnName);
                        break;
                    }
                }
                rowIndex++;
            }
            // 内容
            foreach (DataRow row in dt.Rows)
            {
                IRow newRow = sheet.CreateRow(rowIndex);
                //循环添加列的对应内容
                foreach (DataColumn column in dt.Columns)
                {
                    ICell cell = newRow.CreateCell(column.Ordinal);
                    cell.CellStyle = contentStyle;
                    cell.SetCellValue(row[column].ToString());
                }
                rowIndex++;
            }
            //列宽自适应
            for (int i = 0; i <= dt.Rows.Count; i++)
            {
                sheet.AutoSizeColumn(i, false);
            }
            //将表内容写入流 通知浏览器下载
            workbook.Write(ms);
            return ms;
        }

    }
}
