using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Data;
using iText.Kernel.Geom;

namespace SchoolManagement.Service
{
    public class PdfExporter : IExporter
    {
        private const string FILE_PATH = @"C:\Users\User\AppData\Roaming\Data\storage.pdf";

        public void Export(DataSet dataSet)
        {
            var writer = new PdfWriter(FILE_PATH);

            var pdfDocument = new PdfDocument(writer);
            pdfDocument.SetDefaultPageSize(PageSize.A4);

            var document = new Document(pdfDocument);

            foreach (DataTable dataTable in dataSet.Tables)
            {
                Paragraph header = new Paragraph(dataTable.TableName);
                header.SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(20)
                        .SetBold();
                
                document.Add(header);
                document.Add(CreateTable(dataTable));
            }

            document.Close();
        }

        private Table CreateTable(DataTable dataTable)
        {
            var table = new Table(dataTable.Columns.Count);

            #region Table Header

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                Cell headerCell = new Cell(1, 1)
                    .SetBackgroundColor(ColorConstants.GRAY)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontColor(ColorConstants.RED)
                    .SetFontSize(16)
                    .Add(new Paragraph(dataTable.Columns[i].ColumnName));

                table.AddCell(headerCell);
            }

            #endregion

            #region Table Rows

            for (int j = 1; j <= dataTable.Rows.Count; j++)
            {
                for (int x = 0; x < dataTable.Columns.Count; x++)
                {
                    string value = (dataTable.Rows[j - 1][x] == DBNull.Value) ? string.Empty : dataTable.Rows[j - 1][x].ToString();
                    Cell dataCell = new Cell(1, 1)
                        .SetFontSize(15)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .Add(new Paragraph(value));

                    table.AddCell(dataCell);
                }
            }

            #endregion

            return table;
        }
    }
}
