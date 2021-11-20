﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Markdown
{
    public static class EnumerableExtension
    {
        internal static IEnumerable<TSource> ForEachPairs<TSource>(
            this IEnumerable<TSource> source,
            Func<(TSource, TSource), (TSource, TSource)> action)
        where TSource : class
        {
            // ReSharper disable once PossibleMultipleEnumeration
            // because in this method TSource should change by input action
            foreach (var pair in source.Pairs())
                action(pair);
            
            // ReSharper disable once PossibleMultipleEnumeration
            return source;
        }

        private static IEnumerable<(TSource, TSource)> Pairs<TSource>(this IEnumerable<TSource> source)
        {
            var fixedSource = source.ToList();

            for (var i = 0; i < fixedSource.Count - 1; i++)
                for (var j = i + 1; j < fixedSource.Count; j++)
                    yield return (fixedSource[i], fixedSource[j]);
        }
    }
}