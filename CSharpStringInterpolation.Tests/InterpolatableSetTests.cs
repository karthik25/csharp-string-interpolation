using System;
using System.Linq;
using CSharpStringInterpolation.Items;
using CSharpStringInterpolation.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpStringInterpolation.Tests
{
    [TestClass]
    public class InterpolatableSetTests
    {
        [TestMethod]
        public void CanCreateASimpleListOfInterpolatables()
        {
            const string src = "Some string that is #{Replaceable}";
            var s = new Sample { Replaceable = "irreplaceable" };
            var interpolatables = s.InterpolatablesOf(src);
            Assert.IsNotNull(interpolatables);
            Assert.AreEqual(1, interpolatables.Count);
            var interpolatable = interpolatables.First();
            Assert.AreEqual("Replaceable", interpolatable.Item);
            Assert.AreEqual(InterpolatableType.Simple, interpolatable.Type);
            Assert.AreEqual(s, interpolatable.Instance);
            Assert.AreEqual("irreplaceable", interpolatable.Value);
        }

        [TestMethod]
        public void CanCreateASimpleListOfInterpolatablesWithTwoProps()
        {
            const string src = "Some string that is #{Replaceable} and #{AnotherString}";
            var s = new Sample { Replaceable = "irreplaceable", AnotherString = "more" };
            var interpolatables = s.InterpolatablesOf(src);
            Assert.IsNotNull(interpolatables);
            Assert.AreEqual(2, interpolatables.Count);
            var interpolatable1 = interpolatables.First(i => i.Item == "Replaceable");
            Assert.IsNotNull(interpolatable1);
            Assert.AreEqual(InterpolatableType.Simple, interpolatable1.Type);
            Assert.AreEqual(s, interpolatable1.Instance);
            Assert.AreEqual("irreplaceable", interpolatable1.Value);
            var interpolatable2 = interpolatables.First(i => i.Item == "AnotherString");
            Assert.IsNotNull(interpolatable2);
            Assert.AreEqual(InterpolatableType.Simple, interpolatable2.Type);
            Assert.AreEqual(s, interpolatable2.Instance);
            Assert.AreEqual("more", interpolatable2.Value);
        }

        [TestMethod]
        public void CanCreateAListOfInterpolatablesWithAnArray()
        {
            const string src = "This is the first number #{Num[0]}";
            var nums = new Numbers { Num = new string[] { "1", "2", "3" } };
            var interpolatables = nums.InterpolatablesOf(src);
            Assert.IsNotNull(interpolatables);
            Assert.AreEqual(1, interpolatables.Count);
            var interpolatable = interpolatables.First();
            Assert.AreEqual("Num[0]", interpolatable.Item);
            Assert.AreEqual(nums, interpolatable.Instance);
            Assert.AreEqual(InterpolatableType.Array, interpolatable.Type);
            Assert.AreEqual("1", interpolatable.Value);
        }

        [TestMethod]
        public void CanCreateASubListOfInterpolatablesWithAnExpression()
        {
            const string src = "Sum of #{NumA + NumB} is stored in C";
            var nums = new Numbers { Num = new[] { "1", "2" }, NumA = 1, NumB = 2 };
            var interpolatables = nums.InterpolatablesOf(src);
            Assert.IsNotNull(interpolatables);
            Assert.AreEqual(1, interpolatables.Count);
            var interpolatable = interpolatables.First();
            var subList = interpolatable.InterpolatablesOfExpr(nums, src);
            Assert.IsNotNull(subList);
            Assert.AreEqual(2, subList.Count);
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void ThrowsAnExceptionForUnsupportedInterpolatableTypes()
        {
            const string src = "This is the first number #{Num[0]}";
            var nums = new Numbers { Num = new string[] { "1", "2", "3" } };
            var interpolatables = nums.InterpolatablesOf(src);
            Assert.IsNotNull(interpolatables);
            Assert.AreEqual(1, interpolatables.Count);
            var interpolatable = interpolatables.First();
            var subList = interpolatable.InterpolatablesOfExpr(nums, src);
        }

        [TestMethod]
        public void CanCreateAListOfInterpolatablesWithAnExpression()
        {
            const string src = "Sum of #{NumA + NumB} is stored in C";
            var nums = new Numbers { Num = new[] { "1", "2" }, NumA = 1, NumB = 2 };
            var interpolatables = nums.InterpolatablesOf(src);
            Assert.IsNotNull(interpolatables);
            Assert.AreEqual(1, interpolatables.Count);
            var interpolatable = interpolatables.First();
            Assert.AreEqual("NumA + NumB", interpolatable.Item);
            Assert.AreEqual(nums, interpolatable.Instance);
            Assert.AreEqual(InterpolatableType.Expression, interpolatable.Type);
            Assert.AreEqual("3", interpolatable.Value);
        }
    }
}
