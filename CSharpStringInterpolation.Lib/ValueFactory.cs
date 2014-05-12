using System;
using System.Linq;
using System.Text.RegularExpressions;
using CSharpStringInterpolation.Lib.Concrete;
using NCalc;

namespace CSharpStringInterpolation.Lib
{
    public static class ValueFactory
    {
        public static string GetValue<T>(Interpolatable<T> interpolatable)
            where T:class 
        {
            if (interpolatable.Type == InterpolatableType.Unknown)
                throw new Exception("Unknown interpolatable type");

            if (interpolatable.Type == InterpolatableType.Simple)
            {
                return GetPropertyValue(interpolatable.Instance, interpolatable.Item);
            }
            if (interpolatable.Type == InterpolatableType.Array)
            {
                var arrayIndexRegex = new Regex(@"([a-zA-Z0-9]+)(\[)(\d+)(\])");
                var arrayProp = arrayIndexRegex.Match(interpolatable.Item).Groups[1].Value;
                var arrayIndex = int.Parse(arrayIndexRegex.Match(interpolatable.Item).Groups[3].Value);
                return GetPropertyValue(interpolatable.Instance, arrayProp, arrayIndex);
            }
            if (interpolatable.Type == InterpolatableType.Expression)
            {
                var exprInterpolatables = interpolatable.InterpolatablesOfExpr((T)interpolatable.Instance, interpolatable.Item);
                var constructedString = interpolatable.Item;
                exprInterpolatables.ForEach(item =>
                    {
                        var value = GetPropertyValue(interpolatable.Instance, item.Item);
                        constructedString = constructedString.Replace(item.Item, value);                        
                    });
                var expression = new Expression(constructedString);
                return expression.Evaluate().ToString();
            }
            throw new NotImplementedException();
        }

        private static string GetPropertyValue(object t, string propertyName, int? index = null)
        {
            var propertyInfos = t.GetType().GetProperties();
            var prop = propertyInfos.Single(p => p.Name == propertyName);
            var pValue = prop.GetValue(t, null);
            if (index.HasValue)
            {
                var arrayValue = (object[]) pValue;
                return arrayValue[index.Value].ToString();
            }
            return pValue as string != null ? (string)pValue : pValue.ToString();
        }
    }
}