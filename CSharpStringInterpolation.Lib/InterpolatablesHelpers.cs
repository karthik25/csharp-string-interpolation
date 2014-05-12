﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CSharpStringInterpolation.Lib.Concrete;

namespace CSharpStringInterpolation.Lib
{
    public static class InterpolatablesHelpers
    {
        public static List<Interpolatable<T>> InterpolatablesOf<T>(this T t, string src)
            where T : class
        {
            var matches = Interpolatables.Matches(src);
            var varList = (from Match match in matches
                           select ItemExtractor.Match(match.Value).Groups[3].Value).Distinct().ToList();
            var interpolatables = new List<Interpolatable<T>>();

            varList.ForEach(item => interpolatables.Add(new Interpolatable<T>
                {
                    Item = item,
                    Instance = t,
                    Type = item.FindType()
                }));

            return interpolatables;
        }

        public static List<Interpolatable<T>> InterpolatablesOfExpr<T>(this Interpolatable<T> interpolatable, T t,
                                                                    string src)
            where T : class
        {
            if (interpolatable.Type != InterpolatableType.Expression)
                throw new Exception("Interpolatable passed has to be of type \"Expression\"");

            var matches = ExprProps.Matches(interpolatable.Item);
            var matchValues = matches.Cast<Match>().Select(m => m.Groups[0].Value).Distinct();
            return matchValues.Select(m => new Interpolatable<T>
                {
                    Item = m,
                    Instance = t,
                    Type = m.FindType()
                }).ToList();
        }

        private static readonly Regex Interpolatables = new Regex(@"\#\{[a-zA-Z0-9\[\]\+\-\*\/ ]+\}");
        private static readonly Regex ItemExtractor = new Regex(@"(\#)(\{)([a-zA-Z0-9\[\]\+\-\*\/ ]+)(\})");
        private static readonly Regex ExprProps = new Regex(@"([a-zA-Z0-9\[\]]+)");
    }
}