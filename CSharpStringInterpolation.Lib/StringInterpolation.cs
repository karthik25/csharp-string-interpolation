using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

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

            var matches = Interpolatables.Matches(str);
            var varList = (from Match match in matches
                           select PropertExtractor.Match(match.Value).Groups[3].Value).ToList();

            var propertyInfos = t.GetType().GetProperties();
            foreach (var prop in varList)
            {
                var pValue = GetPropertyValue(t, propertyInfos, prop);
                constructedString = constructedString.Replace(GetReplaceStrFunc(prop), pValue);
            }

            return constructedString;
        }

        private static string GetPropertyValue<T>(T t, IEnumerable<PropertyInfo> propertyInfos, string s)
                where T : class
        {
            var prop = propertyInfos.Single(p => p.Name == s);
            var pValue = prop.GetValue(t, null);
            return pValue as string != null ? (string)pValue : pValue.ToString();
        }

        private static readonly Regex Interpolatables = new Regex(@"\#\{[a-zA-Z]+\}");
        private static readonly Regex PropertExtractor = new Regex(@"(\#)(\{)([a-zA-Z]+)(\})");
        private static readonly Func<string, string> GetReplaceStrFunc = prop => string.Format(@"#{{{0}}}", prop);
    }
}
