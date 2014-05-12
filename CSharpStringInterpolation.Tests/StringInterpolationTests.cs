using CSharpStringInterpolation.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpStringInterpolation.Tests
{
    [TestClass]
    public class StringInterpolationTests
    {
        [TestMethod]
        public void CanInterpolateASimpleString()
        {
            const string src = "Some string that is #{Replaceable}";
            const string expected = "Some string that is irreplaceable";
            var s = new Sample { Replaceable = "irreplaceable" };
            var o = (object)s;
            var actual = o.Interpolate(src);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanInterpolateASimpleStringWithDuplicates()
        {
            const string src = "Some string that is #{Replaceable} and #{Replaceable}";
            const string expected = "Some string that is irreplaceable and irreplaceable";
            var s = new Sample { Replaceable = "irreplaceable" };
            var o = (object)s;
            var actual = o.Interpolate(src);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanInterpolateASimpleStringWithTwoProps()
        {
            const string src = "Some string that is #{Replaceable} and #{AnotherString}";
            const string expected = "Some string that is irreplaceable and more";
            var s = new Sample { Replaceable = "irreplaceable", AnotherString = "more" };
            var o = (object)s;
            var actual = o.Interpolate(src);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CannotInterpolateIncorrectReplacer()
        {
            const string src = "Some string that is #{Replaceable";
            const string expected = "Some string that is #{Replaceable";
            var s = new Sample { Replaceable = "irreplaceable" };
            var o = (object)s;
            var actual = o.Interpolate(src);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanInterpolateASimpleStringv2()
        {
            const string src = "Some string that is #{Replaceable}";
            const string expected = "Some string that is irreplaceable";
            var s = new Sample { Replaceable = "irreplaceable" };
            var actual = s.Interpolate(src);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanInterpolateAnInteger()
        {
            const string src = "Id: #{Id}, Name: #{Name}";
            const string expected = "Id: 1, Name: Karthik";
            var c = new Complex { Id = 1, Name = "Karthik" };
            var actual = c.Interpolate(src);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanInterpolateAComplexNestedType()
        {
            const string src = "Id: #{Id}, Name: #{Name}, Point: #{Point}";
            const string expected = "Id: 1, Name: Karthik, Point: (1,2)";
            var c = new MoreComplex { Id = 1, Name = "Karthik", Point = new Point { X = 1, Y = 2 } };
            var actual = c.Interpolate(src);
            Assert.AreEqual(expected, actual);
        }
    }

    public class Sample
    {
        public string Replaceable { get; set; }
        public string AnotherString { get; set; }
    }

    public class Complex
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MoreComplex
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Point Point { get; set; }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return string.Format("({0},{1})", X, Y);
        }
    }

    public class Numbers
    {
        public string[] Num { get; set; }
        public int NumA { get; set; }
        public int NumB { get; set; }
    }
}
