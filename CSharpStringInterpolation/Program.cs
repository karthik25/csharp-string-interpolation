using System;
using CSharpStringInterpolation.Items;
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
            Sample5();
        }

        private static void Sample1()
        {
            var data = new Data { Id = "1", Name = "Karthik Anant", Email = "karthik@gmail.com" };
            var interpolated = "Id: #{Id}, Name: #{Name}, Email: #{Email}".InterpolateUsing(data);
            Print("Id: #{Id}, Name: #{Name}, Email: #{Email}", interpolated);
        }

        private static void Sample2()
        {
            var data = new Data { Id = "1", Name = "Karthik Anant", Email = "karthik@gmail.com" };
            const string str = "Id: #{Id}, Name: #{Name}, Email: #{Email}";
            var interpolated = data.InterpolateThis(str);
            Print(str, interpolated);
        }

        private static void Sample3()
        {
            var data = new Data { Id = "1", Name = "Karthik Anant", Email = "karthik@gmail.com" };
            const string str = "Id: #{Id, Name: #{Name, Email: #{Email";
            var interpolated = data.InterpolateThis(str);
            Print(str, interpolated);
        }

        private static void Sample4()
        {
            const string src = "Id: #{Id}, Name: #{Name}, Point: #{Point}";
            var c = new MoreComplex { Id = 1, Name = "Karthik", Point = new Point { X = 1, Y = 2 } };
            var interpolated = c.InterpolateThis(src);
            Print(src, interpolated);
        }

        public static void Sample5()
        {
            const string src = "This is the first number #{Num[0]}";
            var nums = new Numbers { Num = new string[] { "1", "2", "3" } };
            var interpolated = nums.InterpolateThis(src);
            Print(src, interpolated);
        }

        private static readonly Action<string, string> Print = (src, interpolated) =>
            {
                var oldColor = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Transformed: ");
                Console.ForegroundColor = oldColor;

                Console.WriteLine(src);

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("In to: ");
                Console.ForegroundColor = oldColor;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(interpolated);
                Console.WriteLine();
                Console.WriteLine();

                Console.ForegroundColor = oldColor;
            };
    }
}
