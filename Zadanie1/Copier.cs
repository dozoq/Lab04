using System;
using ver1;

namespace Zadanie1
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        public int  PrintCounter { get; private set; }
        public int  ScanCounter  { get; private set; }
        public int  Counter      { get; private set; }
        
        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            string concat = "";
            switch (formatType)
            {
                case IDocument.FormatType.PDF:
                    concat = $"PDFScan{ScanCounter}.pdf";
                    break;
                case IDocument.FormatType.JPG:
                    concat = $"ImageScan{ScanCounter}.jpg";
                    break;
                case IDocument.FormatType.TXT:
                    concat = $"TextScan{ScanCounter}.txt";
                    break;
            }
            Console.WriteLine(concat);
        }

        public void Print(in IDocument document)
        {
            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
            
        }

        public void ScanAndPrint()
        {
            IDocument document;
            Scan(out document, IDocument.FormatType.JPG);
            Print(document);
        }
    }
}