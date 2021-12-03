using ExportService;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telerik.Blazor.Components;
using TelerikBlazorApp2.Models;
using TelerikBlazorApp2.Services;

namespace TelerikBlazorApp2.Components
{
    public partial class ExportFunctionality
    {
        [Inject]
        public IDocumentExporter<PdfDocument> PdfExporter { get; set; }

        [Inject]
        public IDocumentExporter<XlsDocument> XlsExporter { get; set; }

        [Inject]
        public IDocumentStore DocumentStore { get; set; }

        [Inject]
        public MockDataService DataService { get; set; }

        public TelerikGrid<MockData> GridRef { get; set; }

        public TelerikDrawer<DrawerItem> DrawerRef { get; set; }

        public bool Expanded { get; set; }

        public IReadOnlyCollection<MockData> GridData { get; set; }

        public IReadOnlyCollection<DrawerItem> Data { get; } = new List<DrawerItem>
        {
            new DrawerItem { ID = DrawerItemType.ExportAsPdf, Text = "Export as PDF" },
            new DrawerItem { ID = DrawerItemType.ExportAsXls, Text = "Export as XLS" }
        };

        // TODO: Need to get these dynamically.
        public Dictionary<string, string> ColumnHeaders { get; } = new()
        {
            { nameof(MockData.Id), "Product Id" },
            { nameof(MockData.ProductName), "Product Name" },
            { nameof(MockData.Units), "Units in Stock" },
            { nameof(MockData.Price), "Price" },
            { nameof(MockData.Discontinued), "Discontinued" },
            { nameof(MockData.ReleaseDate), "Product Release Date" },
        };

        protected override void OnInitialized()
        {
            GridData = DataService.GetData();
        }

        public async void SelectedItemChangedHandler(DrawerItem item)
        {
            switch (item.ID)
            {
                case DrawerItemType.ExportAsPdf: await ExportItemClicked(PdfExporter); break;
                case DrawerItemType.ExportAsXls: await ExportItemClicked(XlsExporter); break;
            }
        }

        public async Task ExportItemClicked<TDocument>(IDocumentExporter<TDocument> exporter) where TDocument : IDocument
        {
            var gridData = new TelerikGridData<MockData>(GridRef, ColumnHeaders);
            var document = await exporter.ExportAsync(gridData);

            await DocumentStore.SaveDocumentAsync(document, "TelerikGridExport");
        }
    }
}
