namespace CSharpStringInterpolation.Items
{
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