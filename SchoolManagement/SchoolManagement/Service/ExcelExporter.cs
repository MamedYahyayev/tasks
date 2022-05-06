using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;

namespace SchoolManagement.Service
{
    public class ExcelExporter : IExporter
    {
        private const string FILE_PATH = @"C:\Users\User\AppData\Roaming\Data\storage.xlsx";

        public void Export(DataSet dataSet)
        {
            IWorkbook workbook = new XSSFWorkbook();

            foreach (DataTable dataTable in dataSet.Tables)
            {
                using (var fileStream =
                new FileStream(FILE_PATH, FileMode.Create, FileAccess.Write))
                {
                    CreateSheet(workbook, dataTable);
                    workbook.Write(fileStream);
                }
            }

        }

        private void CreateSheet(IWorkbook workbook, DataTable dataTable)
        {
            ISheet sheet = workbook.CreateSheet(dataTable.TableName);

            #region Sheet Header

            IRow row = sheet.CreateRow(0);

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.CellStyle = GetHeaderCellStyle(workbook);
                cell.SetCellValue(dataTable.Columns[i].ColumnName);
                sheet.AutoSizeColumn(i);
            }

            #endregion

            #region Sheet Data

            for (int j = 1; j <= dataTable.Rows.Count; j++)
            {
                IRow dataRow = sheet.CreateRow(j);

                for (int x = 0; x < dataTable.Columns.Count; x++)
                {
                    ICell dataCell = dataRow.CreateCell(x);
                    string value = (dataTable.Rows[j - 1][x] == DBNull.Value) ? string.Empty : dataTable.Rows[j - 1][x].ToString();
                    dataCell.SetCellValue(value);
                    sheet.AutoSizeColumn(x);
                }

            }

            #endregion

        }


        private XSSFCellStyle GetHeaderCellStyle(IWorkbook workBook)
        {
            XSSFFont defaultFont = (XSSFFont)workBook.CreateFont();
            defaultFont.FontHeightInPoints = (short)10;
            defaultFont.FontName = "Arial";
            defaultFont.Color = IndexedColors.Red.Index;
            defaultFont.IsBold = true;

            XSSFCellStyle cellStyle = (XSSFCellStyle)workBook.CreateCellStyle();
            cellStyle.SetFont(defaultFont);

            return cellStyle;
        }
    }
}
