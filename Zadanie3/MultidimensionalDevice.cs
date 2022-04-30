using ver2;

namespace Zadanie3
{
    public class MultidimensionalDevice : BaseDevice
    {
        private Scanner _scanner = new Scanner();
        private Printer _printer = new Printer();
        private Fax _fax = new Fax();
        public int PrintCounter
        {
            get => _printer.PrintCounter;
        }
        public int ScanCounter
        {
            get => _scanner.ScanCounter;
        }
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
                _printer.PowerOff();
                _scanner.PowerOff();
            }
        }

        public void SendFax(in IDocument document, string number)
        {
            if (state == IDevice.State.on)
            {
                _fax.PowerOn();
                _fax.SendFax(document, number);
                _fax.PowerOff();
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            document = null;
            if(state == IDevice.State.on)
            {
                _scanner.PowerOn();
                _scanner.Scan(out document, formatType);
                _scanner.PowerOff();
            }
        }

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
            {
                _printer.PowerOn();
                _printer.Print(document);
                _printer.PowerOff();
            }
        }

        public void ScanAndPrint()
        {
            if (state == IDevice.State.on)
            {
                IDocument document;
                Scan(out document);
                Print(document);
            }
        }
        
        public void FullService(string number)
        {
            if (state == IDevice.State.on)
            {
                IDocument document;
                _scanner.PowerOn();
                Scan(out document, IDocument.FormatType.JPG);
                _scanner.PowerOff();
                _printer.PowerOn();
                Print(in document);
                _printer.PowerOff();
                _fax.PowerOn();
                SendFax(in document, number);
                _fax.PowerOff();
            }
        }
    }
}