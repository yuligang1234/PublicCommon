
using System;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Napoleon.PublicCommon
{
    public static class NpoiFunc
    {

        #region 导出Excel

        /// <summary>
        /// 将DataSet数据集转换HSSFworkbook对象，并保存为Stream流
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>返回数据流Stream对象</returns>
        public static MemoryStream ExportDatasetToExcel(this DataSet ds)
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
                ICellStyle cellStyleDate = workbook.CreateCellStyle();
                IDataFormat format = workbook.CreateDataFormat();
                cellStyleDate.DataFormat = format.GetFormat("yyyy年m月d日");
                //使用NPOI操作Excel表
                IRow row = sheet.CreateRow(0);
                int count = 0;
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++) //生成sheet第一行列名 
                {
                    ICell cell = row.CreateCell(count++);
                    cell.SetCellValue(ds.Tables[0].Columns[i].Caption);
                }
                //将数据导入到excel表中
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    IRow rows = sheet.CreateRow(i + 1);
                    count = 0;
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        ICell cell = rows.CreateCell(count++);
                        Type type = ds.Tables[0].Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16)
                            || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)ds.Tables[0].Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)ds.Tables[0].Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)ds.Tables[0].Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)ds.Tables[0].Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(ds.Tables[0].Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                    }
                }
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
