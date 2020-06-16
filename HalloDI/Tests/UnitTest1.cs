using System;
using HalloDI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void MyTestMethod()
        {

            Form1 f1 = new Form1();
            var fakeDevice = new TestBeeper();

            Assert.ThrowsException<ArgumentException>(() => f1.MachLaut(49, fakeDevice));
            Assert.ThrowsException<ArgumentException>(() => f1.MachLaut(50, fakeDevice));
            f1.MachLaut(51, fakeDevice);

        }

        [TestMethod]
        public void TestMethod1()
        {
            Form1 f1 = new Form1();
            var fakeDevice = new TestBeeper();

            f1.MachLaut(70, fakeDevice);

            Assert.AreEqual(2, fakeDevice.CallCount);
        }
    }

    class TestBeeper : ISoundDevice
    {
        public int CallCount { get; set; }
        public void MakeBeep(int hz, int dur)
        {
            CallCount++;
        }
    }

}
