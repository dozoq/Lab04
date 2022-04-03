using System;
using ver1;

namespace Zadanie1
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        public int PrintCounter { get; private set; }
        public int ScanCounter  { get; private set; }
        public new int Counter      { get; private set; }

        public new void PowerOn()
        {
            if (this.state == IDevice.State.off)
            {
                base.PowerOn();
                Counter++;   
            }
        }

        public new void PowerOff()
        {
            if (this.state == IDevice.State.@on)
            {
                base.PowerOff();
            }
        }


        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            string concat = "";
            document = null;
            if (this.state == IDevice.State.@on)
            {
                ScanCounter++;
                switch (formatType)
                {
                    case IDocument.FormatType.PDF:
                        concat   = $"PDFScan{ScanCounter}.pdf";
                        document = new PDFDocument("");
                        break;
                    case IDocument.FormatType.JPG:
                        concat   = $"ImageScan{ScanCounter}.jpg";
                        document = new ImageDocument("");
                        break;
                    case IDocument.FormatType.TXT:
                        concat   = $"TextScan{ScanCounter}.txt";
                        document = new TextDocument("");
                        break;
                }

                Console.WriteLine(concat);
            }
        }

        public void Print(in IDocument document)
        {
            if (this.state == IDevice.State.@on)
            {
                PrintCounter++;
                Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
            }
        }

        public void ScanAndPrint()
        {
            if (this.state == IDevice.State.@on)
            {
                IDocument document;
                Scan(out document, IDocument.FormatType.JPG);
                Print(document);
            }
        }
    }
}