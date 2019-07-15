using System;
using Arithmetic_Expression;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PostfixNotationTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(CalculateExpression("(1+2)*4"), 12);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.AreEqual(CalculateExpression("1+2*4"), 9);
        }

        [TestMethod]
        public void TestMethod3()
        {
            Assert.AreEqual(CalculateExpression("1/4"), 0.25);
        }

        [TestMethod]
        public void TestMethod4()
        {
            Assert.AreEqual(CalculateExpression("1"), 1);
        }

        [TestMethod]
        public void TestMethod5()
        {
            Assert.AreEqual(CalculateExpression("( 8 + 2 * 5 ) / ( 1 + 3 * 2 - 4 )"), 6);
        }

        public double CalculateExpression(string expression)
        {
            ArithmeticExpression a = new ArithmeticExpression(expression);
            return a.Value;
        }
    }
}
