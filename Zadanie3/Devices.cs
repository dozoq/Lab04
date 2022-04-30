using System;
using System.Text.RegularExpressions;

namespace ver2
{
    public interface IDevice
    {
        enum State {on, off};

        void PowerOn(); // uruchamia urządzenie, zmienia stan na `on`
        void PowerOff(); // wyłącza urządzenie, zmienia stan na `off
        State GetState(); // zwraca aktualny stan urządzenia

        int Counter {get;}  // zwraca liczbę charakteryzującą eksploatację urządzenia,
                            // np. liczbę uruchomień, liczbę wydrukow, liczbę skanów, ...
    }

    public abstract class BaseDevice : IDevice
    {
        protected IDevice.State state = IDevice.State.off;
        public IDevice.State GetState() => state;

        public void PowerOff()
        {
            state = IDevice.State.off;
            Console.WriteLine("... Device is off !");
        }

        public void PowerOn()
        {
            state = IDevice.State.on;
            Console.WriteLine("Device is on ...");  
        }

        public int Counter { get; private set; } = 0;
    }

    public interface IPrinter : IDevice
    {
        /// <summary>
        /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
        /// </summary>
        /// <param name="document">obiekt typu IDocument, różny od `null`</param>
        void Print(in IDocument document);
    }
    

    public interface IScanner : IDevice
    {
        // dokument jest skanowany, jeśli urządzenie włączone
        // w przeciwnym przypadku nic się dzieje
        void Scan(out IDocument document, IDocument.FormatType formatType);
    }
    public interface IFax : IDevice
    {
        public void SendFax(in IDocument document, string number);
    }
    
    public class Printer : IPrinter
    {
        public int PrintCounter { get; private set; }
        private IDevice.State state = IDevice.State.off;
        public void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                state = IDevice.State.on;
                Counter++;
            }
        }

        public void PowerOff()
        {
            if (state == IDevice.State.on)
                state = IDevice.State.off;
        }

        public IDevice.State GetState()
        {
            return state;
        }

        public int Counter { get; private set; }
        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
            {
                PrintCounter++;
                Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
            }
        }
       
    }

    public class Fax : IFax
    {
        private IDevice.State state = IDevice.State.off;
        public int ScanCounter { get; private set; }
       
        public void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                state = IDevice.State.on;
                Counter++;
            }
        }

        public void PowerOff()
        {
            if (state == IDevice.State.on)
                state = IDevice.State.off;
        }

        public IDevice.State GetState()
        {
            return state;
        }

        public int Counter { get; private set; }
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
    }

    public class Scanner : IScanner
    {
        private IDevice.State state = IDevice.State.off;
        public int ScanCounter { get; private set; }
       
        public void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                state = IDevice.State.on;
                Counter++;
            }
        }

        public void PowerOff()
        {
            if (state == IDevice.State.on)
                state = IDevice.State.off;
        }

        public IDevice.State GetState()
        {
            return state;
        }

        public int Counter { get; private set; }
         
        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            string concat = "";
            document = null;
            if (state == IDevice.State.on)
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
        
    }
}
