using System;

namespace CSharpStringInterpolation.Lib
{
    public static class StringInterpolation
    {
        public static string InterpolateUsing<T>(this string str, T t)
            where T : class
        {
            return Interpolate(t, str);
        }

        public static string InterpolateThis<T>(this T t, string str)
            where T : class
        {
            return Interpolate(t, str);
        }

        public static string Interpolate<T>(this T t, string str)
               where T : class
        {
            var constructedString = str;
            
            var interpolatables = t.InterpolatablesOf(str);
            interpolatables.ForEach(interpolatable =>
                {
                    constructedString = constructedString.Replace(GetReplaceStrFunc(interpolatable.Item), interpolatable.Value);    
                });

            return constructedString;
        }

        private static readonly Func<string, string> GetReplaceStrFunc = prop => string.Format(@"#{{{0}}}", prop);
    }
}
