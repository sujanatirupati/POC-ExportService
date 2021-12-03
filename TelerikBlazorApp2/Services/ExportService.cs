using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace TelerikBlazorApp2.Services
{
    public class ExportService
    {
        public string ExportExcelFile(DataTable dataTable, string fileName)
        {
            // Convert a DataTable to Workbook
            Workbook workbook = CreateWorkbook(dataTable);
            IWorkbookFormatProvider formatProvider = new Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx.XlsxFormatProvider();

            string folderPath = "\\\\mfch10\\import\\Sujana"; //AppDomain.CurrentDomain.BaseDirectory;
            string filePath = folderPath + "\\" + fileName;
            using (Stream output = new FileStream(filePath, FileMode.Create))
            {
                formatProvider.Export(workbook, output); 
            }

            return filePath;
        }

        public void ExportPdf(DataTable dataTable, string fileName)
        {
            Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf.PdfFormatProvider pdfFormatProvider = new Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf.PdfFormatProvider();
            using (Stream output = File.OpenWrite(fileName))
            {
                Workbook workbook = CreateWorkbook(dataTable); 
                pdfFormatProvider.Export(workbook, output);
            }
        }

        public Workbook CreateWorkbook(DataTable dataTable)
        {
            DataTableFormatProvider provider = new DataTableFormatProvider();

            Workbook workbook = new Workbook();
            Worksheet worksheet = workbook.Worksheets.Add();

            provider.Import(dataTable, worksheet);

            return workbook;
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }

}
