using System;
using System.Collections.Generic;

namespace ComplicatedPrimitives;

public static class Comparable
{
    public static T Min<T>(T value1, T value2) where T : IComparable<T> =>
        value1.CompareTo(value2) > 0
            ? value2
            : value1;

    public static T Max<T>(T value1, T value2) where T : IComparable<T> =>
        value1.CompareTo(value2) < 0
            ? value2
            : value1;

    public static (T min, T max) MinMax<T>(T value1, T value2) where T : IComparable<T> =>
        value1.CompareTo(value2) > 0
            ? (value2, value1)
            : (value1, value2);

    public static T Min<T>(T value1, T value2, IComparer<T> comparer) =>
        comparer.Compare(value1, value2) > 0
            ? value2
            : value1;

    public static T Max<T>(T value1, T value2, IComparer<T> comparer) =>
        comparer.Compare(value1, value2) < 0
            ? value2
            : value1;

    public static (T min, T max) MinMax<T>(T value1, T value2, IComparer<T> comparer) =>
        comparer.Compare(value1, value2) > 0
            ? (value2, value1)
            : (value1, value2);

    public static T Min<T>(T value1, T value2, Func<T, T, int> comparer) =>
        comparer(value1, value2) > 0
            ? value2
            : value1;

    public static T Max<T>(T value1, T value2, Func<T, T, int> comparer) =>
        comparer(value1, value2) < 0
            ? value2
            : value1;

    public static (T min, T max) MinMax<T>(T value1, T value2, Func<T, T, int> comparer) =>
        comparer(value1, value2) > 0
            ? (value2, value1)
            : (value1, value2);

    public static T Min<T, TValue>(T value1, T value2, Func<T, TValue> getter) where TValue : IComparable<TValue> =>
        getter(value1).CompareTo(getter(value2)) > 0
            ? value2
            : value1;

    public static T Max<T, TValue>(T value1, T value2, Func<T, TValue> getter) where TValue : IComparable<TValue> =>
        getter(value1).CompareTo(getter(value2)) < 0
            ? value2
            : value1;

    public static (T min, T max) MinMax<T, TValue>(T value1, T value2, Func<T, TValue> getter) where TValue : IComparable<TValue> =>
        getter(value1).CompareTo(getter(value2)) > 0
            ? (value2, value1)
            : (value1, value2);

    public static T Min<T, TValue>(T value1, T value2, Func<T, TValue> getter, IComparer<TValue> comparer) =>
        comparer.Compare(getter(value1), getter(value2)) > 0
            ? value2
            : value1;

    public static T Max<T, TValue>(T value1, T value2, Func<T, TValue> getter, IComparer<TValue> comparer) =>
        comparer.Compare(getter(value1), getter(value2)) < 0
            ? value2
            : value1;

    public static (T min, T max) MinMax<T, TValue>(T value1, T value2, Func<T, TValue> getter, IComparer<TValue> comparer) =>
        comparer.Compare(getter(value1), getter(value2)) > 0
            ? (value2, value1)
            : (value1, value2);

    public static T Min<T, TValue>(T value1, T value2, Func<T, TValue> getter, Func<TValue, TValue, int> comparer) =>
        comparer(getter(value1), getter(value2)) > 0
            ? value2
            : value1;

    public static T Max<T, TValue>(T value1, T value2, Func<T, TValue> getter, Func<TValue, TValue, int> comparer) =>
        comparer(getter(value1), getter(value2)) < 0
            ? value2
            : value1;

    public static (T min, T max) MinMax<T, TValue>(T value1, T value2, Func<T, TValue> getter, Func<TValue, TValue, int> comparer) =>
        comparer(getter(value1), getter(value2)) > 0
            ? (value2, value1)
            : (value1, value2);

    public static DirectedLimit<T> ProperSubset<T>(DirectedLimit<T> limit1, DirectedLimit<T> limit2) where T : IComparable<T>
    {
        if (limit1.IsProperSubsetOf(limit2))
            return limit1;

        if (limit2.IsProperSubsetOf(limit1))
            return limit2;

        throw new InvalidOperationException($"Neither limit is a proper subset of the other one.");
    }

    public static DirectedLimit<T> ProperSuperset<T>(DirectedLimit<T> limit1, DirectedLimit<T> limit2) where T : IComparable<T>
    {
        if (limit1.IsProperSupersetOf(limit2))
            return limit1;

        if (limit2.IsProperSupersetOf(limit1))
            return limit2;

        throw new InvalidOperationException($"Neither limit is a proper superset of the other one.");
    }

    public static DirectedLimit<T> Subset<T>(DirectedLimit<T> limit1, DirectedLimit<T> limit2) where T : IComparable<T>
    {
        if (limit1.IsSubsetOf(limit2))
            return limit1;

        if (limit2.IsSubsetOf(limit1))
            return limit2;

        throw new InvalidOperationException($"Neither limit is a subset of the other one.");
    }

    public static DirectedLimit<T> Superset<T>(DirectedLimit<T> limit1, DirectedLimit<T> limit2) where T : IComparable<T>
    {
        if (limit1.IsSupersetOf(limit2))
            return limit1;

        if (limit2.IsSupersetOf(limit1))
            return limit2;

        throw new InvalidOperationException($"Neither limit is a superset of the other one.");
    }
}