using System.IO.Enumeration;

namespace Zadanie1
{
    public abstract class AbstractDocument: IDocument
    {
        private string               fileName;
        public  abstract IDocument.FormatType GetFormatType();
        public string               GetFileName()
        {
            return fileName;
        }

        public AbstractDocument(string fileName)
        {
            this.fileName = fileName;
        }

        public void ChangeFileName(string fileName)
        {
            this.fileName = fileName;
        }
    }
}