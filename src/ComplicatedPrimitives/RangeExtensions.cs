using DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComplicatedPrimitives;

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

    public static IEnumerable<Range<T>> Merge<T>(this IEnumerable<Range<T>> source) where T : IComparable<T>
    {
        var sortedLimits = source.SelectMany(e => new[] { e.Left, e.Right }).ToArray();
        QuickSort
            .ForComparer<DirectedLimit<T>>(_compareDirectedLimitByValue)
            .Sort(sortedLimits, usePassedArray: true);

        DirectedLimit<T>? leftLimit = null;
        DirectedLimit<T>? rightLimit = null;
        var limitsEnumerator = sortedLimits.GetEnumerator();
        bool wait = false;
        while (wait || limitsEnumerator.MoveNext())
        {
            wait = false;
            var limit = (DirectedLimit<T>)limitsEnumerator.Current;
            if (!leftLimit.HasValue)
            {
                if (limit.Side == LimitSide.Left)
                    leftLimit = limit;
            }
            else if (!rightLimit.HasValue)
            {
                if (limit.Side == LimitSide.Right)
                    rightLimit = limit;
            }
            else
            {
                switch (limit.Side)
                {
                    case LimitSide.Left:
                        if (limit.Value.CompareTo(rightLimit.Value.Value) != 0
                            || (limit.Type == LimitType.Open
                                && rightLimit.Value.Type == LimitType.Open))
                        {
                            yield return new Range<T>(leftLimit.Value, rightLimit.Value);
                            leftLimit = null;
                            rightLimit = null;
                            wait = true;
                        }
                        else
                        {
                            rightLimit = null;
                            continue;
                        }
                        break;
                    case LimitSide.Right:
                        rightLimit = limit;
                        break;
                    default:
                        break;
                }
            }
        }

        if (leftLimit.HasValue && rightLimit.HasValue)
            yield return new Range<T>(leftLimit.Value, rightLimit.Value);
    }

    private static int _compareDirectedLimitByValue<T>(DirectedLimit<T> left, DirectedLimit<T> right) where T : IComparable<T> =>
        left.LimitValue.Value.CompareTo(right.LimitValue.Value);
}