using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace ExportService
{
    public sealed class JSRuntimeDocumentStore : IDocumentStore
    {
        private readonly IJSRuntime _jsRuntime;

        public JSRuntimeDocumentStore(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SaveDocumentAsync<TDocument>(TDocument document, string fileNameWithoutExtension) where TDocument : IDocument
        {
            var fileContentBase64 = Convert.ToBase64String(document.GetContent());
            var fileName = document.GetFileName(fileNameWithoutExtension);
            var mimeType = document.GetMimeType();

            await _jsRuntime.InvokeVoidAsync("saveFile", fileContentBase64, mimeType, fileName);
        }
    }
}
