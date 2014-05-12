using CSharpStringInterpolation.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpStringInterpolation.Tests
{
    [TestClass]
    public class TypeFactoryTests
    {
        [TestMethod]
        public void CanIdentifyASimpleType()
        {
            const string src = "Replaceable";
            var type = src.FindType();
            Assert.AreEqual(InterpolatableType.Simple, type);
        }

        [TestMethod]
        public void CanIdentifyASimpleType2()
        {
            const string src = "Num1";
            var type = src.FindType();
            Assert.AreEqual(InterpolatableType.Simple, type);
        }

        [TestMethod]
        public void CanIdentifyAnArrayType()
        {
            const string src = "Emails[4]";
            var type = src.FindType();
            Assert.AreEqual(InterpolatableType.Array, type);
        }

        [TestMethod]
        public void CanIdentifyAnExpression()
        {
            const string src = "FirstNumber + LastNumber";
            var type = src.FindType();
            Assert.AreEqual(InterpolatableType.Expression, type);
        }

        [TestMethod]
        public void CanIdentifyAnExpression2()
        {
            const string src = "Num1 + Num2";
            var type = src.FindType();
            Assert.AreEqual(InterpolatableType.Expression, type);
        }
    }
}
