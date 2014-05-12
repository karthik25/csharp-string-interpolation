using System.Text.RegularExpressions;

namespace CSharpStringInterpolation.Lib
{
    public static class TypeFactory
    {
        public static InterpolatableType FindType(this string src)
        {
            if (ExprRegex.IsMatch(src))
                return InterpolatableType.Expression;
            if (ArrayRegex.IsMatch(src))
                return InterpolatableType.Array;
            return SimpleComplexRegex.IsMatch(src) ? InterpolatableType.Simple : InterpolatableType.Unknown;
        }

        private static readonly Regex ExprRegex = new Regex(@"[a-zA-Z0-9]+[ ]*[\+\-\*\/]{1}[ ]*[a-zA-Z0-9]+");
        private static readonly Regex ArrayRegex = new Regex(@"[a-zA-Z0-9]+\[\d+\]");
        private static readonly Regex SimpleComplexRegex = new Regex(@"[a-zA-Z0-9]+");
    }
}