namespace ExportService
{
    public sealed class XlsDocument : IDocument
    {
        private readonly byte[] _content;

        public XlsDocument(byte[] content)
        {
            _content = content;
        }

        public byte[] GetContent()
        {
            return _content;
        }

        public string GetFileName(string fileNameWithoutExtension)
        {
            return $"{fileNameWithoutExtension}.xlsx";
        }

        public string GetMimeType()
        {
            return "application/xlsx";
        }
    }
}
