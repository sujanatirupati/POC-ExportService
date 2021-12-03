using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ExportService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Telerik.DataSource;

namespace TelerikBlazorApp2.Services
{
    public class WeatherForecast
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }

   

    public class PdfExportService
    {
        [Inject]
        private IJSRuntime jsRuntime { get; set; }

        [Inject]
        private WeatherForecastService MyDataService { get; set; }

        public PdfExportService(WeatherForecastService dataService, IJSRuntime JsRuntimeInstance)
        {
            MyDataService = dataService;
            jsRuntime = JsRuntimeInstance;
        }
        public async Task GetPdf(DataSourceRequest dataSourceRequest, bool allPages, bool useExcelGeneration)
        {
            if (allPages)
            {
                dataSourceRequest.PageSize = 0;
                dataSourceRequest.Skip = 0; // for virtualization
            }

            IQueryable<WeatherForecast> queriableData = await MyDataService.GetForecasts();

            var pdfExporter = new PdfExporter();
            byte[] fileData = null;
           
            fileData = await pdfExporter.ExportWithRadPdfProcessing(queriableData, dataSourceRequest);
          

            string base64File = Convert.ToBase64String(fileData);

            await jsRuntime.InvokeVoidAsync("saveFile", base64File, "application/pdf", "NewPdfExportDoc.pdf");
        }
    }
}
