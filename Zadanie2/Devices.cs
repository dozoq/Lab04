using System;
using System.Text.RegularExpressions;

namespace ver1
{
    public interface IFax : IDevice
    {
        public void SendFax(in IDocument document, string number);
    }

    public class MultifuntionalDevice : IFax, IPrinter, IScanner
    {
        protected  IDevice.State state = IDevice.State.off;
        public new int           Counter { get; private set; } = 0;

        public void          PowerOn()
        {
            if (state == IDevice.State.off)
            {
                state = IDevice.State.@on;
                Counter++;
                Console.WriteLine($"Device is turned on {Counter} time");
            }
        }

        public void          PowerOff()
        {
            if (state == IDevice.State.@on)
            {
                state = IDevice.State.off;
                Console.WriteLine("Device is turned off...");
            }
        }

        public IDevice.State GetState()
        {
            return state;
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            if (this.state == IDevice.State.@on)
            {
                document = null;
                string concat = "";
                switch (formatType)
                {
                    case IDocument.FormatType.PDF:
                        concat   = $"PDFScan{DateTime.Now}.pdf";
                        document = new PDFDocument("");
                        break;
                    case IDocument.FormatType.JPG:
                        concat   = $"ImageScan{DateTime.Now}.jpg";
                        document = new ImageDocument("");
                        break;
                    case IDocument.FormatType.TXT:
                        concat   = $"TextScan{DateTime.Now}.txt";
                        document = new TextDocument("");
                        break;
                }

                Console.WriteLine(concat);
            }
            else
            {
                throw new Exception("Beep! Device Powered Off");
            }
        }

        public void Print(in IDocument document)
        {
            if (this.state == IDevice.State.@on)
            {
                Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
            }
            else
            {
                throw new Exception("Beep! Device Powered Off");
            }
        }
        
        public void SendFax(in IDocument document, string number)
        {
            if (state == IDevice.State.@on)
            {
                Regex phoneNumberRegex = new Regex(@"^\+[0-9]{11}$");
                if (phoneNumberRegex.IsMatch(number))
                {
                    Console.WriteLine($"{document.GetFileName()} sent to: {number}");
                }
                else
                {
                    throw new ArgumentException("Phone number does not match pattern");
                }
            }
            else
            {
                throw new Exception("Beep! Device Powered Off");
            }
        }

        public void FullService(string number)
        {
            IDocument document;
            Scan(out document, IDocument.FormatType.JPG);
            Print(in document);
            SendFax(in document, number);
        }

        public void ScanAndPrint()
        {
            IDocument document;
            Scan(out document, IDocument.FormatType.JPG);
            Print(in document);
        }

        public void ScanAndSend(string number)
        {
            IDocument document;
            Scan(out document, IDocument.FormatType.JPG);
            SendFax(in document, number);
        }
    }
}