using System.Threading.Tasks;

namespace ExportService
{
    public interface IDocumentStore
    {
        Task SaveDocumentAsync<TDocument>(TDocument document, string fileNameWithoutExtension) where TDocument : IDocument;
    }
}
