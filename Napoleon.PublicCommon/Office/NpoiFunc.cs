
using System.Data;
using System.IO;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace Napoleon.PublicCommon.Office
{
    public static class NpoiFunc
    {

        /// <summary>
        ///  创建工作簿
        /// MemoryStream fileStream = dt.CreateSheet("", titles);
        /// fileStream.Seek(0, SeekOrigin.Begin);
        /// File(fileStream, "application/vnd.ms-excel", PublicFields.LogExcelName);
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <param name="topTitle">大标题</param>
        /// <param name="titles">标题</param>
        /// <param name="titleFileds">字段</param>
        /// Author  : Napoleon
        /// Created : 2015-01-15 13:59:30
        public static MemoryStream CreateSheet(this DataTable dt, string[] titles, string[] titleFileds,
            string topTitle = "")
        {
            MemoryStream ms = new MemoryStream();
            HSSFWorkbook workbook = new HSSFWorkbook();
            int count = 0;

            #region 样式设置

            #region 头部大标题样式

            ICellStyle topTitleStyle = workbook.CreateCellStyle(); //单元格样式
            topTitleStyle.Alignment = HorizontalAlignment.Center; //水平居中
            topTitleStyle.VerticalAlignment = VerticalAlignment.Center; //垂直居中
            IFont topTitleFont = workbook.CreateFont(); //字体样式
            topTitleFont.FontName = "黑体";
            topTitleFont.FontHeightInPoints = 16;
            topTitleStyle.SetFont(topTitleFont);

            #endregion

            #region 标题样式

            ICellStyle titleStyle = workbook.CreateCellStyle(); //单元格样式
            titleStyle.Alignment = HorizontalAlignment.Center; //水平居中
            titleStyle.VerticalAlignment = VerticalAlignment.Center; //垂直居中
            IFont titleFont = workbook.CreateFont(); //字体样式
            titleFont.FontName = "宋体";
            titleFont.FontHeightInPoints = 12;
            titleFont.Boldweight = 10;
            titleStyle.SetFont(titleFont);

            #endregion

            #region 内容样式

            ICellStyle contentStyle = workbook.CreateCellStyle(); //单元格样式
            contentStyle.Alignment = HorizontalAlignment.Center; //水平居中
            contentStyle.VerticalAlignment = VerticalAlignment.Center; //垂直居中
            contentStyle.WrapText = true;//换行
            IFont contentFont = workbook.CreateFont(); //字体样式
            contentFont.FontName = "宋体";
            contentFont.FontHeightInPoints = 12;
            contentStyle.SetFont(contentFont);

            #endregion

            #endregion

            //创建一个名称为sheet1的工作表
            ISheet sheet = workbook.CreateSheet("sheet1");
            int rowIndex = 0; //从第一行开始
            //大标题
            if (!string.IsNullOrWhiteSpace(topTitle))
            {
                IRow topRow = sheet.CreateRow(rowIndex);
                ICell topCell = topRow.CreateCell(0);
                topCell.CellStyle = topTitleStyle;
                topCell.SetCellValue(topTitle);
                sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, dt.Columns.Count));
                rowIndex++;
            }
            //小标题
            if (titles != null)
            {
                IRow headerRow = sheet.CreateRow(rowIndex);
                //循环添加标题
                foreach (string title in titles)
                {
                    ICell cell = headerRow.CreateCell(count);
                    cell.CellStyle = contentStyle;
                    cell.SetCellValue(title);
                    count++;
                }
                rowIndex++;
            }
            //内容
            foreach (DataRow row in dt.Rows)
            {
                IRow newRow = sheet.CreateRow(rowIndex);
                //循环添加列的对应内容
                for (int i = 0; i < titleFileds.Length; i++)
                {
                    ICell cell = newRow.CreateCell(i);
                    cell.CellStyle = contentStyle;
                    cell.SetCellValue(row[titleFileds[i]].ToString());
                }
                rowIndex++;
            }
            //设置宽度
            for (int columnNum = 0; columnNum < titleFileds.Length; columnNum++)
            {
                int columnWidth = sheet.GetColumnWidth(columnNum) / 256;//获取当前列宽度
                for (int rowNum = 0; rowNum < sheet.LastRowNum; rowNum++)
                {
                    IRow currentRow = sheet.GetRow(rowNum);
                    ICell currentCell = currentRow.GetCell(columnNum);
                    int length = Encoding.UTF8.GetBytes(currentCell.ToString()).Length;//获取当前单元格的内容宽度
                    //若当前单元格内容宽度大于列宽，则调整列宽为当前单元格宽度
                    if (columnWidth < length)
                    {
                        columnWidth = length;
                    }
                    //宽度最高为255个字符
                    if (columnWidth > 250)
                    {
                        columnWidth = 250;
                    }
                }
                sheet.SetColumnWidth(columnNum, (columnWidth + 2) * 256);
            }
            //将表内容写入流 通知浏览器下载
            workbook.Write(ms);
            return ms;
        }



    }
}
