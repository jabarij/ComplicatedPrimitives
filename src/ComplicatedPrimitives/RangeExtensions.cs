using DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComplicatedPrimitives
{
    /// <summary>
    /// Provides extension methods for <see cref="Range{T}"/> and <see cref="IComparativeSet{TSet, T}"/> types and their enumerables.
    /// </summary>
    public static class RangeExtensions
    {
        /// <summary>
        /// Normalizes collection of <see cref="Range{T}"/> by uniting intersecting ranges and removing ranges that are proper subsets of other ranges in the collection.
        /// </summary>
        /// <typeparam name="T">Type of range's value.</typeparam>
        /// <param name="source">Collection of ranges to merge.</param>
        /// <returns>
        /// Collection of non-intersecting ranges ordered by <see cref="Range{T}.LeftValue"/> covering the same space of <typeparamref name="T"/> as the input <paramref name="source"/>.
        /// </returns>
        public static IEnumerable<Range<T>> Merge<T>(this IEnumerable<Range<T>> source)
            where T : IComparable<T>
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
                                || (limit.Type == LimitPointType.Open
                                    && rightLimit.Value.Type == LimitPointType.Open))
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
            left.Point.Value.CompareTo(right.Point.Value);
    }
}
