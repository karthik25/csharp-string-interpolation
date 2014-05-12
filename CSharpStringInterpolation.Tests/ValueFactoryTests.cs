using CSharpStringInterpolation.Lib;
using CSharpStringInterpolation.Lib.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpStringInterpolation.Tests
{
    [TestClass]
    public class ValueFactoryTests
    {
        [TestMethod]
        public void CanGetSimpleValue()
        {
            const string src = "Some string that is #{Replaceable}";
            var s = new Sample { Replaceable = "irreplaceable" };
            var interpolatable = new Interpolatable<Sample>
                {
                    Item = "Replaceable",
                    Type = InterpolatableType.Simple,
                    Instance = s
                };
            var value = interpolatable.Value;
            Assert.AreEqual("irreplaceable", value);
        }

        [TestMethod]
        public void CanGetArrayValue()
        {
            const string src = "This is the first number #{Num[0]}";
            var nums = new Numbers { Num = new[]{ "1","2","3" }};
            var interpolatable = new Interpolatable<Numbers>
                {
                    Item = "Num[0]",
                    Type = InterpolatableType.Array,
                    Instance = nums
                };
            var value = interpolatable.Value;
            Assert.AreEqual("1", value);
        }

        [TestMethod]
        public void CanGetExpressionValue()
        {
            const string src = "Sum of #{NumA + NumB} is stored in C";
            var nums = new Numbers { Num = new[] { "1", "2" }, NumA = 1, NumB = 2 };
            var interpolatable = new Interpolatable<Numbers>
                {
                    Item = "NumA + NumB",
                    Type = InterpolatableType.Expression,
                    Instance = nums
                };
            var value = interpolatable.Value;
            Assert.AreEqual("3", value);
        }

        [TestMethod]
        public void CanGetExpressionValue2()
        {
            const string src = "Sum of #{(NumA + NumB) * (NumB * NumB)} is stored in C";
            var nums = new Numbers { Num = new[] { "1", "2" }, NumA = 1, NumB = 2 };
            var interpolatable = new Interpolatable<Numbers>
            {
                Item = "(NumA + NumB) * (NumB * NumB)",
                Type = InterpolatableType.Expression,
                Instance = nums
            };
            var value = interpolatable.Value;
            Assert.AreEqual("12", value);
        }
    }
}
