using System.Threading.Tasks;

namespace ExportService
{
    public interface IDocumentExporter<TDocument> where TDocument : IDocument
    {
        Task<TDocument> ExportAsync<T>(TelerikGridData<T> data);
    }
}
