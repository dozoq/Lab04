using System;
using ver2;

namespace Zadanie3
{
    public class Copier : BaseDevice
    {
        private Scanner _scanner = new Scanner();
        private Printer _printer = new Printer();
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
    }
}