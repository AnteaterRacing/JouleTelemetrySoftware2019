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
            Assert.AreEqual(10, Data.Map(10, 10, 10, 10, 10));
            Assert.AreEqual(15, Data.Map(15, 0, 10, 15, 15));
            Assert.AreEqual(1, Data.Map(10, 0, 100, 0, 10));
        }

        [TestMethod]
        public void TestFibonacci()
        {
            Assert.AreEqual(0, Data.Fibonacci(-10));
            Assert.AreEqual(0, Data.Fibonacci(0));
            Assert.AreEqual(1, Data.Fibonacci(1));
            Assert.AreEqual(2, Data.Fibonacci(3));
            Assert.AreEqual(5, Data.Fibonacci(5));
            Assert.AreEqual(13, Data.Fibonacci(7));
            Assert.AreEqual(34, Data.Fibonacci(9));
            Assert.AreEqual(89, Data.Fibonacci(11));
            Assert.AreEqual(233, Data.Fibonacci(13));
            Assert.AreEqual(610, Data.Fibonacci(15));
            Assert.AreEqual(1836311903, Data.Fibonacci(46));
        }
    }
}
