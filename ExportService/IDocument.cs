namespace ExportService
{
    public interface IDocument
    {
        byte[] GetContent();

        string GetFileName(string fileNameWithoutExtension);

        string GetMimeType();
    }
}
