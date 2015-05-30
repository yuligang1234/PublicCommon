
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Napoleon.PublicCommon
{
    public static class NpoiFunc
    {

        #region 导出Excel

        /// <summary>
        /// DataTable导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">保存位置</param>
        public static void Export(DataTable dtSource, string strHeaderText, string strFileName)
        {
            using (MemoryStream ms = Export(dtSource, strHeaderText))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="headers">需要导出的列的列头</param>
        /// <param name="cellKes">需要导出的对应的列字段</param>
        /// <returns></returns>
        public static MemoryStream Export(DataTable dtSource, string strHeaderText, string[] headers, string[] cellKes)
        {
            // excel
            HSSFWorkbook workbook = new HSSFWorkbook();
            // 创建一个sheet页，已strHeaderText命名
            ISheet sheet = workbook.CreateSheet(strHeaderText);

            // 日期的样式
            ICellStyle dateStyle = workbook.CreateCellStyle();
            // 日期的格式化
            IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
            // 字体样式
            IFont datafont = workbook.CreateFont();
            // 字体大小
            datafont.FontHeightInPoints = 11;
            dateStyle.SetFont(datafont);
            // 边框
            dateStyle.BorderBottom = BorderStyle.Thin;
            dateStyle.BorderLeft = BorderStyle.Thin;
            dateStyle.BorderRight = BorderStyle.Thin;
            dateStyle.BorderTop = .BorderStyle.Thin;

            // 其他数据的样式
            ICellStyle cellStyle = workbook.CreateCellStyle();
            // 字体样式
            IFont cellfont = workbook.CreateFont();
            // 字体大小
            cellfont.FontHeightInPoints = 11;
            cellStyle.SetFont(cellfont);
            // 边框
            cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

            // 总的列数
            int colNum = headers.Length;
            // 每个列的宽度
            int[] arrColWidth = new int[colNum];
            // 初始化列的宽度为列头的长度，已需要显示的列头的名字长度计算
            for (int i = 0; i < headers.Length; i++)
            {
                arrColWidth[i] = Encoding.GetEncoding(936).GetBytes(headers[i]).Length * 3;
            }

            // 循环数据，取每列数据最宽的作为该列的宽度
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < cellKes.Length; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][cellKes[j]].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }

            // 记录生成的行数
            int rowIndex = 0;

            // DataTable中的列信息
            DataColumnCollection columns = dtSource.Columns;
            // 循环所有的行，向sheet页中添加数据
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    // 如果不是第一行，则创建一个新的sheet页
                    if (rowIndex != 0)
                    {
                        sheet = workbook.CreateSheet();
                    }

                    #region 表头及样式
                    {
                        // 在当前sheet页上创建第一行
                        IRow headerRow = sheet.CreateRow(0);
                        // 该行的高度
                        headerRow.HeightInPoints = 50;
                        // 设置第一列的值
                        headerRow.CreateCell(0).SetCellValue(strHeaderText);

                        // 设置列的样式
                        ICellStyle headStyle = workbook.CreateCellStyle();
                        // 内容居中显示
                        headStyle.Alignment = HorizontalAlignment.Center;
                        // 字体样式
                        IFont font = workbook.CreateFont();
                        // 字体大小
                        font.FontHeightInPoints = 20;
                        // 粗体显示
                        font.Boldweight = 700;
                        // 字体颜色
                        font.Color = NPOI.HSSF.Util.HSSFColor.Blue.Index;

                        headStyle.SetFont(font);
                        // 边框
                        headStyle.BorderBottom = BorderStyle.Thin;
                        headStyle.BorderLeft = BorderStyle.Thin;
                        headStyle.BorderRight = BorderStyle.Thin;
                        headStyle.BorderTop =BorderStyle.Thin;

                        // 设置单元格的样式
                        headerRow.GetCell(0).CellStyle = headStyle;

                        // 设置该行每个单元格的样式
                        for (int i = 1; i < colNum; i++)
                        {
                            headerRow.CreateCell(i);
                            headerRow.GetCell(i).CellStyle = headStyle;
                        }

                        // 合并单元格
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, colNum - 1));
                    }
                    #endregion


                    #region 列头及样式
                    {
                        // 创建第二行
                        IRow headerRow = sheet.CreateRow(1);
                        // 该行的高度
                        headerRow.HeightInPoints = 28;
                        // 列的样式
                        ICellStyle headStyle = workbook.CreateCellStyle();
                        // 单元格内容居中显示
                        headStyle.Alignment = HorizontalAlignment.Center;
                        // 字体样式
                        IFont font = workbook.CreateFont();
                        // 字体大小
                        font.FontHeightInPoints = 12;
                        // 粗体
                        font.Boldweight = 700;
                        // 字体颜色
                        font.Color = NPOI.HSSF.Util.HSSFColor.Black.Index;
                        headStyle.SetFont(font);
                        // 边框
                        headStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                        headStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                        headStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                        headStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

                        // 设置每列的样式和值
                        for (int i = 0; i < headers.Length; i++)
                        {
                            headerRow.CreateCell(i).SetCellValue(headers[i]);
                            headerRow.GetCell(i).CellStyle = headStyle;

                            //设置列宽
                            sheet.SetColumnWidth(i, (arrColWidth[i] + 1) * 256);
                        }
                    }
                    #endregion

                    rowIndex = 2;
                }
                #endregion


                #region 填充内容
                // 创建新的一行
                IRow dataRow = sheet.CreateRow(rowIndex);
                // 该行的高度
                dataRow.HeightInPoints = 23;
                // 循环需要写入的每列数据
                for (int i = 0; i < cellKes.Length; i++)
                {
                    // 创建列
                    ICell newCell = dataRow.CreateCell(i);
                    // 获取DataTable中该列对象
                    DataColumn column = columns[cellKes[i]];
                    // 该列的值
                    string drValue = row[column].ToString();

                    // 根据值得类型分别处理之后赋值
                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);

                            newCell.CellStyle = cellStyle;
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle;//格式化显示
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);

                            newCell.CellStyle = cellStyle;
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);

                            newCell.CellStyle = cellStyle;
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);

                            newCell.CellStyle = cellStyle;
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");

                            newCell.CellStyle = cellStyle;
                            break;
                        default:
                            newCell.SetCellValue("");

                            newCell.CellStyle = cellStyle;
                            break;
                    }
                }
                #endregion

                rowIndex++;
            }

            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// 用于Web导出
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">文件名</param>
        public static void ExportByWeb(DataTable dtSource, string strHeaderText, string strFileName)
        {
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8));
            curContext.Response.BinaryWrite(Export(dtSource, strHeaderText).GetBuffer());
            curContext.Response.End();
        }

        /// <summary>读取excel
        /// 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable Import(string strFileName)
        {
            DataTable dt = new DataTable();
            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            ISheet sheet = hssfworkbook.GetSheetAt(0);
            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;
            for (int j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }
            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        #endregion

        #region

        /// <summary>
        /// 将DataSet数据集转换HSSFworkbook对象，并保存为Stream流
        /// </summary>
        /// <param name="dt"></param>
        public static MemoryStream ExportDatasetToExcel(this DataTable dt)
        {
            try
            {
                //文件流对象
                //FileStream file = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                MemoryStream stream = new MemoryStream();
                //打开Excel对象
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象
                ISheet sheet = workbook.CreateSheet("sheet1");
                //set date format
                /*ICellStyle cellStyleDate = workbook.CreateCellStyle();
                IDataFormat format = workbook.CreateDataFormat();
                cellStyleDate.DataFormat = format.GetFormat("yyyy年m月d日");*/
                //取得列宽
                for (int columnNum = 0; columnNum <= dt.Columns.Count; columnNum++)
                {
                    int columnWidth = sheet.GetColumnWidth(columnNum) / 256;//获取当前列宽度
                    for (int rowNum = 1; rowNum <= sheet.LastRowNum; rowNum++)//在这一列上循环行
                    {
                        IRow currentRow = sheet.GetRow(rowNum);
                        ICell currentCell = currentRow.GetCell(columnNum);
                        int length = Encoding.UTF8.GetBytes(currentCell.ToString()).Length / 256;//获取当前单元格的内容宽度
                        if (columnWidth < length + 1)
                        {
                            columnWidth = length + 1;
                        }//若当前单元格内容宽度大于列宽，则调整列宽为当前单元格宽度，后面的+1是我人为的将宽度增加一个字符
                    }
                    sheet.SetColumnWidth(columnNum, columnWidth * 256);
                }
                //使用NPOI操作Excel表
                //将数据导入到excel表中
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow rows = sheet.CreateRow(i);
                    int count = 0;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ICell cell = rows.CreateCell(count++);
                        Type type = dt.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16)
                            || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                    }
                }
                /*//保存excel文档
                sheet.ForceFormulaRecalculation = true;*/
                
                //保存excel文档
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                return stream;
            }
            catch
            {
                return new MemoryStream();
            }
        }

        #endregion



    }
}
