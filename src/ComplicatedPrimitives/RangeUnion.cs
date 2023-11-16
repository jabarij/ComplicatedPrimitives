using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ComplicatedPrimitives;

public class RangeUnion<TRange, T> : IEnumerable<TRange>
    where TRange : IRange<TRange, T>
    where T : IComparable<T>
{
    public static RangeUnion<TRange, T> Empty => new(true);

    private readonly List<TRange> _ranges;

    public RangeUnion()
    {
        _ranges = new List<TRange>();
        IsNormalized = true;
    }
    public RangeUnion(TRange range)
    {
        _ranges = new List<TRange> { range };
        IsNormalized = true;
    }
    public RangeUnion(IEnumerable<TRange> ranges)
    {
        if (ranges == null)
            throw new ArgumentNullException(nameof(ranges));
        _ranges = new List<TRange>(ranges);
        IsNormalized = _ranges.Count < 2;
    }
    internal RangeUnion(bool isNormalized, params TRange[] ranges)
    {
        IsNormalized = isNormalized;
        _ranges = new List<TRange>(ranges.Length);
        _ranges.AddRange(ranges);
    }

    public TRange this[int index] =>
        _ranges[index];

    public bool Contains(T value)
    {
        if (_ranges.Count == 0)
            return false;

        for (int rangeIndex = 0; rangeIndex < _ranges.Count; rangeIndex++)
            if (_ranges[rangeIndex].Contains(value))
                return true;

        return false;
    }

    public bool Intersects(TRange other)
    {
        for (int rangeIndex = 0; rangeIndex < _ranges.Count; rangeIndex++)
            if (_ranges[rangeIndex].Intersects(other))
                return true;

        return false;
    }

    public RangeUnion<TRange, T> Intersect(TRange other)
    {
        if (_ranges.Count == 0)
            return Empty;

        var intersectionIndicators = new (bool, TRange)[_ranges.Count];
        int intersectionsCount = 0;
        for (int rangeIndex = 0; rangeIndex < intersectionIndicators.Length; rangeIndex++)
        {
            var intersection = _ranges[rangeIndex].GetIntersection(other);
            bool intersects = !intersection.IsEmpty;
            intersectionIndicators[rangeIndex] = (intersects, intersection);
            if (intersects) intersectionsCount++;
        }

        var intersections = new TRange[intersectionsCount];
        int intersectionIndex = 0;
        for (int rangeIndex = 0; rangeIndex < intersectionIndicators.Length; rangeIndex++)
        {
            var indicator = intersectionIndicators[rangeIndex];
            if (indicator.Item1)
                intersections[intersectionIndex] = indicator.Item2;
        }

        return new RangeUnion<TRange, T>(intersections);
    }

    public bool IsNormalized { get; }

    public bool IsEmpty =>
        _ranges.Count == 0 || _ranges.All(e => e.IsEmpty);

    public int RangesCount =>
        _ranges.Count;

    public RangeUnion<TRange, T> ToNormalized() => new(true, _ranges.Merge<TRange, T>().ToArray());

    #region IEnumerable<TRange>

    public IEnumerator<TRange> GetEnumerator() =>
        _ranges.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    #endregion
}