using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Blazor.Components;
using Telerik.DataSource;

namespace ExportService
{
    public class Exporter
    {
        public async Task<byte[]> ExportWithPdfProcessing<T>(TelerikGridData<T> data)
        {
            var pdfExporter = new PdfProcessingExporter();
            return await pdfExporter.ExportData(data);
        }

        public async Task<byte[]> ExportWithExcelProcessing1<T>(TelerikGridData<T> data)
        {
            var pdfExporter = new ExcelProcessingExporter();
            return await pdfExporter.ExportData(data);
        }
    }  
}
