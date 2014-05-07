using System;
using CSharpStringInterpolation.Lib;

namespace CSharpStringInterpolation
{
    class Program
    {
        static void Main(string[] args)
        {
            CallSamples();
        }

        public static void CallSamples()
        {
            Sample1();
            Sample2();
            Sample3();
            Sample4();
        }

        private static void Sample1()
        {
            var data = new Data { Id = "1", Name = "Karthik Anant", Email = "karthik@gmail.com" };
            var interpolated = "Id: #{Id}, Name: #{Name}, Email: #{Email}".InterpolateUsing(data);
            Console.WriteLine(interpolated);
            Console.WriteLine();
        }

        private static void Sample2()
        {
            var data = new Data { Id = "1", Name = "Karthik Anant", Email = "karthik@gmail.com" };
            const string str = "Id: #{Id}, Name: #{Name}, Email: #{Email}";
            var interpolated = data.InterpolateThis(str);
            Console.WriteLine(interpolated);
            Console.WriteLine();
        }

        private static void Sample3()
        {
            var data = new Data { Id = "1", Name = "Karthik Anant", Email = "karthik@gmail.com" };
            const string str = "Id: #{Id, Name: #{Name, Email: #{Email";
            var interpolated = data.InterpolateThis(str);
            Console.WriteLine(interpolated);
            Console.WriteLine();
        }

        private static void Sample4()
        {
            const string src = "Id: #{Id}, Name: #{Name}, Point: #{Point}";
            var c = new MoreComplex { Id = 1, Name = "Karthik", Point = new Point { X = 1, Y = 2 } };
            var interpolated = c.InterpolateThis(src);
            Console.WriteLine(interpolated);
            Console.WriteLine();
        }
    }

    public class SampleActual
    {
        public string Reference { get; set; }
    }

    public class Data
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
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
}
