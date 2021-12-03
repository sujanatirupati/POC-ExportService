using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace ExportService
{
    public sealed class XlsDocumentExporter : IDocumentExporter<XlsDocument>
    {
        public async Task<XlsDocument> ExportAsync<T>(TelerikGridData<T> gridData)
        {
            return new XlsDocument(await ExportData(gridData));
        }

        public async Task<byte[]> ExportData<T>(TelerikGridData<T> gridData)
        {
            Workbook workbook = CreateWorkbookFromGrid<T>(gridData);
            IBinaryWorkbookFormatProvider formatProvider = new XlsxFormatProvider();

            // use your own Workbook instance here
            byte[] fileToDownloadAsByteArray = formatProvider.Export(workbook);

            return await Task.FromResult(fileToDownloadAsByteArray);
        }

        public Workbook CreateWorkbookFromGrid<T>(TelerikGridData<T> gridData)
        {
            List<T> items = gridData.Grid.Data.ToList();
            var columnHeaders = gridData.ColumnHeaders;

            DataTable dataTable = new DataTable(typeof(T).Name);
            Type typeParameterType = typeof(T);
            var fieldsList = typeParameterType.GetProperties();
            List<string> ColumnFields = fieldsList.Select(c => c.Name).ToList();
            var columnsState = gridData.Grid.GetState().ColumnStates;
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

            var provider = new DataTableFormatProvider();
            var workbook = new Workbook();
            var worksheet = workbook.Worksheets.Add();

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
