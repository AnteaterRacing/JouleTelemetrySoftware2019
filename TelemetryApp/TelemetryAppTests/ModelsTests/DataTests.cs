using Microsoft.VisualStudio.TestTools.UnitTesting;
using TelemetryApp.Models;

namespace TelemetryAppTests
{
    [TestClass]
    public class DataTests
    {
        [TestMethod]
        public void TestMap()
        {
            Assert.AreEqual(10, DataHelper.Map(10, 10, 10, 10, 10));
            Assert.AreEqual(15, DataHelper.Map(15, 0, 10, 15, 15));
            Assert.AreEqual(1, DataHelper.Map(10, 0, 100, 0, 10));
        }

        [TestMethod]
        public void TestFibonacci()
        {
            Assert.AreEqual(0, DataHelper.Fibonacci(-10));
            Assert.AreEqual(0, DataHelper.Fibonacci(0));
            Assert.AreEqual(1, DataHelper.Fibonacci(1));
            Assert.AreEqual(2, DataHelper.Fibonacci(3));
            Assert.AreEqual(5, DataHelper.Fibonacci(5));
            Assert.AreEqual(13, DataHelper.Fibonacci(7));
            Assert.AreEqual(34, DataHelper.Fibonacci(9));
            Assert.AreEqual(89, DataHelper.Fibonacci(11));
            Assert.AreEqual(233, DataHelper.Fibonacci(13));
            Assert.AreEqual(610, DataHelper.Fibonacci(15));
            Assert.AreEqual(1836311903, DataHelper.Fibonacci(46));
        }
    }
}
