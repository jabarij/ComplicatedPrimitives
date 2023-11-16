using System;
using System.Collections.Generic;

namespace ComplicatedPrimitives;

public interface IRange<TRange, T> : IEquatable<TRange>
{
    T LeftValue { get; }
    T RightValue { get; }

    bool IsInfinite { get; }
    bool IsEmpty { get; }
    bool Contains(T value);
    bool Intersects(TRange other);

    bool IsSubsetOf(TRange other);
    bool IsProperSubsetOf(TRange other);
    bool IsSupersetOf(TRange other);
    bool IsProperSupersetOf(TRange other);

    TRange GetIntersection(TRange other);
    IEnumerable<TRange> GetUnion(TRange other);
    IEnumerable<TRange> GetComplementIn(TRange other);
    IEnumerable<TRange> GetAbsoluteComplement();
}