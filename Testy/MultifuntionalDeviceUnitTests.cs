using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ver1;

namespace ver1UnitTests
{
    [TestClass]
    
    public class MultifuntionalDeviceUnitTests
    {
        [TestMethod]
        public void TestFaxRegexErrorThrow()
        {
            var       multifuncDevice = new MultifuntionalDevice();
            Exception exceptionsOccured;
            multifuncDevice.PowerOn();
            string goodNumber = "+48111111111";
            string[]  badPhoneNumbers = new[]
            {
                "-11111111111",
                "+1111111111",
                "+111111111111",
                "+af142412411",
                "11111111111",
                "+48 111 111 111",
            };
            IDocument document        = new PDFDocument("aaa.pdf");
            foreach (var badPhoneNumber in badPhoneNumbers)
            {
                exceptionsOccured = null;
                try
                {
                    multifuncDevice.SendFax(in document, badPhoneNumber);
                }
                catch (Exception e)
                {
                    exceptionsOccured = e;
                }
                Assert.IsNotNull(exceptionsOccured);
            }

            exceptionsOccured = null;
            try
            {
                multifuncDevice.SendFax(in document, goodNumber);
            }
            catch (Exception e)
            {
                exceptionsOccured = e;
            }
            Assert.IsNull(exceptionsOccured);
        }

        [TestMethod]
        public void TestPower()
        {
            var multifuncDevice = new MultifuntionalDevice();
            Assert.IsTrue(multifuncDevice.Counter == 0);
            multifuncDevice.PowerOn();
            Assert.IsTrue(multifuncDevice.Counter == 1);
            multifuncDevice.PowerOff();
            Assert.IsTrue(multifuncDevice.Counter == 1);
            IDocument document = new PDFDocument("");
            Exception exceptionOccured = null;
            try
            {
                multifuncDevice.SendFax(document, "+48111111111");
            }
            catch (Exception exception)
            {
                exceptionOccured = exception;
            }
            Assert.IsNotNull(exceptionOccured);
            Assert.IsTrue( exceptionOccured.Message.Equals("Beep! Device Powered Off"));
        }
        
        [TestMethod]
        public void TestServices()
        {
            string number            = "+48111111111";
            var    multifuncDevice   = new MultifuntionalDevice();
            multifuncDevice.PowerOn();
            var    currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                var time = DateTime.Now;
                multifuncDevice.FullService(number);
                Assert.IsTrue(consoleOutput.GetOutput().Contains($"{time} Print: "));
                Assert.IsTrue(consoleOutput.GetOutput().Contains($"sent to: {number}"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains($"ImageScan{time}"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }
        
    }
}