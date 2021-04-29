using System;
using System.Collections.Generic;
using System.Linq;

namespace ComplicatedPrimitives
{
    public static class RangeExtensions
    {
        public static IEnumerable<TRange> Merge<TRange, T>(this IEnumerable<TRange> source)
            where TRange : IRange<TRange, T> =>
            source
            .OrderBy(e => e.LeftValue)
            .Aggregate(
                seed: new Stack<TRange>(),
                func: (acc, range) =>
                {
                    if (acc.Count == 0)
                        acc.Push(range);
                    else
                    {
                        var previousRange = acc.Peek();
                        var union = range.GetUnion(previousRange).ToList();
                        switch (union.Count)
                        {
                            case 1:
                                acc.Pop();
                                acc.Push(union[0]);
                                break;
                            case 2:
                                acc.Push(range);
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }
                    return acc;
                });
    }
}
