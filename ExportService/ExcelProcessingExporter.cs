using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Editing.Flow;
using Telerik.Windows.Documents.Fixed.Model.Fonts;
using Telerik.Windows.Documents.Fixed.Model.Graphics;
using Telerik.Windows.Documents.Fixed.Model.Editing.Tables;
using System.Threading.Tasks;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;
using System.Reflection;
using Telerik.Windows.Documents.Spreadsheet.Model;
using System.Data;
using Telerik.Blazor.Components;

namespace ExportService
{
    public class ExcelProcessingExporter
    {
        public async Task<byte[]> ExportData<T>(TelerikGridData<T> gridData)
        {
            Workbook workbook = CreateWorkbookFromGrid<T>(gridData);
            Telerik.Windows.Documents.Spreadsheet.FormatProviders.IBinaryWorkbookFormatProvider formatProvider =
                           new Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx.XlsxFormatProvider();

            // use your own Workbook instance here
            byte[] fileToDownloadAsByteArray = formatProvider.Export(workbook);

            return await Task.FromResult(fileToDownloadAsByteArray);
        }

        public Workbook CreateWorkbookFromGrid<T>(TelerikGridData<T> gridData)
        {
            List<T> items = (List<T>)gridData.grid.Data;
            var columnHeaders = gridData.dicColumnHeaders;

            DataTable dataTable = new DataTable(typeof(T).Name);
            Type typeParameterType = typeof(T);
            var fieldsList = typeParameterType.GetProperties();
            List<string> ColumnFields = fieldsList.Select(c => c.Name).ToList();
            var columnsState = gridData.grid.GetState().ColumnStates;
            Dictionary<int, string> dicColumns = new Dictionary<int, string>();
            int index = 0;
            foreach (var columnState in columnsState)
            {
                var columnField = ColumnFields[columnState.Index];
                dicColumns.Add(index, columnField);
                index++;
            }

            foreach (var item in dicColumns)
            {
                string headerText = columnHeaders[item.Value];
                dataTable.Columns.Add(headerText);
            }

            for (int i = 0; i < items.Count; i++)
            {
                var values = new object[dicColumns.Count];
                foreach (var item in dicColumns)
                {
                    values[item.Key] = GetFieldValue(items[i], item.Value);
                }
                dataTable.Rows.Add(values);
            }

            Telerik.Windows.Documents.Spreadsheet.FormatProviders.DataTableFormatProvider provider = new
                  Telerik.Windows.Documents.Spreadsheet.FormatProviders.DataTableFormatProvider();

            Workbook workbook = new Workbook();
            Worksheet worksheet = workbook.Worksheets.Add();

            provider.Import(dataTable, worksheet);

            return workbook;
        }

        private object GetFieldValue(object item, string fieldName)
        {
            PropertyInfo propertyInfo = item.GetType().GetProperty(fieldName);
            if (propertyInfo != null)
            {
                return propertyInfo.GetValue(item);
            }
            return null;
        }       
    }
}
