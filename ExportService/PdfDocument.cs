namespace ExportService
{
    public sealed class PdfDocument : IDocument
    {
        private readonly byte[] _content;

        public PdfDocument(byte[] content)
        {
            _content = content;
        }

        public byte[] GetContent()
        {
            return _content;
        }

        public string GetFileName(string fileNameWithoutExtension)
        {
            return $"{fileNameWithoutExtension}.pdf";
        }

        public string GetMimeType()
        {
            return "application/pdf";
        }
    }
}
