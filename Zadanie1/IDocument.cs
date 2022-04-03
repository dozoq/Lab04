namespace Zadanie1
{
    public interface IDocument
    {
        public FormatType GetFormatType();
        public string     GetFileName();
        
        public enum FormatType
        {
            TXT, PDF, JPG
        }
        
    }

    
}