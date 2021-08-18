using System;
using System.Collections.Generic;

namespace ComplicatedPrimitives
{
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) methods for operating on objects that implement <see cref="IComparable{T}"/> or provides any other way of comparison.
    /// </summary>
    public static class Comparable
    {
        /// <summary>
        /// Returns lower of the two values comparing them using <see cref="IComparable{T}.CompareTo(T)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="value1">First value to compare.</param>
        /// <param name="value2">Second value to compare.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><description><paramref name="value1"/> when <paramref name="value1"/> is lower than or equal to <paramref name="value2"/>;</description></item>
        /// <item><description><paramref name="value2"/> when <paramref name="value1"/> is greater than <paramref name="value2"/>.</description></item>
        /// </list>
        /// </returns>
        public static T Min<T>(T value1, T value2) where T : IComparable<T> =>
            value1.CompareTo(value2) > 0
            ? value2
            : value1;

        /// <summary>
        /// Returns greater of the two values comparing them using <see cref="IComparable{T}.CompareTo(T)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="value1">First value to compare.</param>
        /// <param name="value2">Second value to compare.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><description><paramref name="value1"/> when <paramref name="value1"/> is greater than or equal to <paramref name="value2"/>;</description></item>
        /// <item><description><paramref name="value2"/> when <paramref name="value1"/> is lower than <paramref name="value2"/>.</description></item>
        /// </list>
        /// </returns>
        public static T Max<T>(T value1, T value2) where T : IComparable<T> =>
            value1.CompareTo(value2) < 0
            ? value2
            : value1;

        /// <summary>
        /// Returns given values as ordered pair of min and max, comparing them using <see cref="IComparable{T}.CompareTo(T)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="value1">First value to compare.</param>
        /// <param name="value2">Second value to compare.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><description>(min: <paramref name="value1"/>, max: <paramref name="value2"/>) when <paramref name="value1"/> is greater than or equal to <paramref name="value2"/>;</description></item>
        /// <item><description>(min: <paramref name="value2"/>, max: <paramref name="value1"/>) when <paramref name="value1"/> is lower than <paramref name="value2"/>.</description></item>
        /// </list>
        /// </returns>
        public static (T min, T max) MinMax<T>(T value1, T value2) where T : IComparable<T> =>
            value1.CompareTo(value2) > 0
            ? (value2, value1)
            : (value1, value2);

        /// <summary>
        /// Returns lower of the two values comparing them using <see cref="IComparer{T}.Compare(T, T)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="value1">First value to compare.</param>
        /// <param name="value2">Second value to compare.</param>
        /// <param name="comparer">Value comparer.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><description><paramref name="value1"/> when <paramref name="value1"/> is lower than or equal to <paramref name="value2"/>;</description></item>
        /// <item><description><paramref name="value2"/> when <paramref name="value1"/> is greater than <paramref name="value2"/>.</description></item>
        /// </list>
        /// </returns>
        public static T Min<T>(T value1, T value2, IComparer<T> comparer) =>
            comparer.Compare(value1, value2) > 0
            ? value2
            : value1;

        /// <summary>
        /// Returns greater of the two values comparing them using <see cref="IComparer{T}.Compare(T, T)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="value1">First value to compare.</param>
        /// <param name="value2">Second value to compare.</param>
        /// <param name="comparer">Value comparer.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><description><paramref name="value1"/> when <paramref name="value1"/> is greater than or equal to <paramref name="value2"/>;</description></item>
        /// <item><description><paramref name="value2"/> when <paramref name="value1"/> is lower than <paramref name="value2"/>.</description></item>
        /// </list>
        /// </returns>
        public static T Max<T>(T value1, T value2, IComparer<T> comparer) =>
            comparer.Compare(value1, value2) < 0
            ? value2
            : value1;

        /// <summary>
        /// Returns given values as ordered pair of min and max, comparing them using <see cref="IComparer{T}.Compare(T, T)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="value1">First value to compare.</param>
        /// <param name="value2">Second value to compare.</param>
        /// <param name="comparer">Value comparer.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><description>(min: <paramref name="value1"/>, max: <paramref name="value2"/>) when <paramref name="value1"/> is greater than or equal to <paramref name="value2"/>;</description></item>
        /// <item><description>(min: <paramref name="value2"/>, max: <paramref name="value1"/>) when <paramref name="value1"/> is lower than <paramref name="value2"/>.</description></item>
        /// </list>
        /// </returns>
        public static (T min, T max) MinMax<T>(T value1, T value2, IComparer<T> comparer) =>
            comparer.Compare(value1, value2) > 0
            ? (value2, value1)
            : (value1, value2);

        /// <summary>
        /// Returns lower of the two values comparing them using <paramref name="comparer"/> function.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="value1">First value to compare.</param>
        /// <param name="value2">Second value to compare.</param>
        /// <param name="comparer">Function comparing values.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><description><paramref name="value1"/> when <paramref name="value1"/> is lower than or equal to <paramref name="value2"/>;</description></item>
        /// <item><description><paramref name="value2"/> when <paramref name="value1"/> is greater than <paramref name="value2"/>.</description></item>
        /// </list>
        /// </returns>
        public static T Min<T>(T value1, T value2, Func<T, T, int> comparer) =>
            comparer(value1, value2) > 0
            ? value2
            : value1;

        /// <summary>
        /// Returns greater of the two values comparing them using <paramref name="comparer"/> function.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="value1">First value to compare.</param>
        /// <param name="value2">Second value to compare.</param>
        /// <param name="comparer">Function comparing values.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><description><paramref name="value1"/> when <paramref name="value1"/> is greater than or equal to <paramref name="value2"/>;</description></item>
        /// <item><description><paramref name="value2"/> when <paramref name="value1"/> is lower than <paramref name="value2"/>.</description></item>
        /// </list>
        /// </returns>
        public static T Max<T>(T value1, T value2, Func<T, T, int> comparer) =>
            comparer(value1, value2) < 0
            ? value2
            : value1;

        /// <summary>
        /// Returns given values as ordered pair of min and max, comparing them using <paramref name="comparer"/> function.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="value1">First value to compare.</param>
        /// <param name="value2">Second value to compare.</param>
        /// <param name="comparer">Function comparing values.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><description>(min: <paramref name="value1"/>, max: <paramref name="value2"/>) when <paramref name="value1"/> is greater than or equal to <paramref name="value2"/>;</description></item>
        /// <item><description>(min: <paramref name="value2"/>, max: <paramref name="value1"/>) when <paramref name="value1"/> is lower than <paramref name="value2"/>.</description></item>
        /// </list>
        /// </returns>
        public static (T min, T max) MinMax<T>(T value1, T value2, Func<T, T, int> comparer) =>
            comparer(value1, value2) > 0
            ? (value2, value1)
            : (value1, value2);

        /// <summary>
        /// Returns lower of values obtained from the given objects using <paramref name="getter"/>, comparing them using <see cref="IComparable{TValue}.CompareTo(TValue)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of source object.</typeparam>
        /// <typeparam name="TValue">Type of value.</typeparam>
        /// <param name="obj1">First source object to get value to compare.</param>
        /// <param name="obj2">Second source object to get value to compare.</param>
        /// <param name="getter">Function obtaining comparable value to compare from source object.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><description><paramref name="obj1"/> when <paramref name="getter"/>(<paramref name="obj1"/>) is lower than or equal to <paramref name="getter"/>(<paramref name="obj2"/>);</description></item>
        /// <item><description><paramref name="obj2"/> when <paramref name="getter"/>(<paramref name="obj1"/>) is greater than <paramref name="getter"/>(<paramref name="obj2"/>).</description></item>
        /// </list>
        /// </returns>
        public static T Min<T, TValue>(T obj1, T obj2, Func<T, TValue> getter) where TValue : IComparable<TValue> =>
            getter(obj1).CompareTo(getter(obj2)) > 0
            ? obj2
            : obj1;

        /// <summary>
        /// Returns greater of values obtained from the given objects using <paramref name="getter"/>, comparing them using <see cref="IComparable{TValue}.CompareTo(TValue)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of source object.</typeparam>
        /// <typeparam name="TValue">Type of value.</typeparam>
        /// <param name="obj1">First source object to get value to compare.</param>
        /// <param name="obj2">Second source object to get value to compare.</param>
        /// <param name="getter">Function obtaining comparable value to compare from source object.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><description><paramref name="obj1"/> when <paramref name="getter"/>(<paramref name="obj1"/>) is greater than or equal to <paramref name="getter"/>(<paramref name="obj2"/>);</description></item>
        /// <item><description><paramref name="obj2"/> when <paramref name="getter"/>(<paramref name="obj1"/>) is lower than <paramref name="getter"/>(<paramref name="obj2"/>).</description></item>
        /// </list>
        /// </returns>
        public static T Max<T, TValue>(T obj1, T obj2, Func<T, TValue> getter) where TValue : IComparable<TValue> =>
            getter(obj1).CompareTo(getter(obj2)) < 0
            ? obj2
            : obj1;

        /// <summary>
        /// Returns values obtained from the given objects using <paramref name="getter"/> as ordered pair of min and max, comparing them using <see cref="IComparable{TValue}.CompareTo(TValue)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of source object.</typeparam>
        /// <typeparam name="TValue">Type of value.</typeparam>
        /// <param name="obj1">First source object to get value to compare.</param>
        /// <param name="obj2">Second source object to get value to compare.</param>
        /// <param name="getter">Function obtaining comparable value to compare from source object.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><description>(min: <paramref name="obj1"/>, max: <paramref name="obj2"/>) when <paramref name="getter"/>(<paramref name="obj1"/>) is greater than or equal to <paramref name="getter"/>(<paramref name="obj2"/>);</description></item>
        /// <item><description>(min: <paramref name="obj2"/>, max: <paramref name="obj1"/>) when <paramref name="getter"/>(<paramref name="obj1"/>) is lower than <paramref name="getter"/>(<paramref name="obj2"/>).</description></item>
        /// </list>
        /// </returns>
        public static (T min, T max) MinMax<T, TValue>(T obj1, T obj2, Func<T, TValue> getter) where TValue : IComparable<TValue> =>
            getter(obj1).CompareTo(getter(obj2)) > 0
            ? (obj2, obj1)
            : (obj1, obj2);

        /// <summary>
        /// Returns lower of values obtained from the given objects using <paramref name="getter"/>, comparing them using <see cref="IComparer{TValue}.Compare(TValue, TValue)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of source object.</typeparam>
        /// <typeparam name="TValue">Type of value.</typeparam>
        /// <param name="obj1">First source object to get value to compare.</param>
        /// <param name="obj2">Second source object to get value to compare.</param>
        /// <param name="getter">Function obtaining value to compare from source object.</param>
        /// <param name="comparer">Value comparer.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><description><paramref name="obj1"/> when <paramref name="getter"/>(<paramref name="obj1"/>) is lower than or equal to <paramref name="getter"/>(<paramref name="obj2"/>);</description></item>
        /// <item><description><paramref name="obj2"/> when <paramref name="getter"/>(<paramref name="obj1"/>) is greater than <paramref name="getter"/>(<paramref name="obj2"/>).</description></item>
        /// </list>
        /// </returns>
        public static T Min<T, TValue>(T obj1, T obj2, Func<T, TValue> getter, IComparer<TValue> comparer) =>
            comparer.Compare(getter(obj1), getter(obj2)) > 0
            ? obj2
            : obj1;

        /// <summary>
        /// Returns greater of values obtained from the given objects using <paramref name="getter"/>, comparing them using <see cref="IComparer{TValue}.Compare(TValue, TValue)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of source object.</typeparam>
        /// <typeparam name="TValue">Type of value.</typeparam>
        /// <param name="obj1">First source object to get value to compare.</param>
        /// <param name="obj2">Second source object to get value to compare.</param>
        /// <param name="getter">Function obtaining value to compare from source object.</param>
        /// <param name="comparer">Value comparer.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><description><paramref name="obj1"/> when <paramref name="getter"/>(<paramref name="obj1"/>) is greater than or equal to <paramref name="getter"/>(<paramref name="obj2"/>);</description></item>
        /// <item><description><paramref name="obj2"/> when <paramref name="getter"/>(<paramref name="obj1"/>) is lower than <paramref name="getter"/>(<paramref name="obj2"/>).</description></item>
        /// </list>
        /// </returns>
        public static T Max<T, TValue>(T obj1, T obj2, Func<T, TValue> getter, IComparer<TValue> comparer) =>
            comparer.Compare(getter(obj1), getter(obj2)) < 0
            ? obj2
            : obj1;

        /// <summary>
        /// Returns values obtained from the given objects using <paramref name="getter"/> as ordered pair of min and max, comparing them using <see cref="IComparer{TValue}.Compare(TValue, TValue)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of source object.</typeparam>
        /// <typeparam name="TValue">Type of value.</typeparam>
        /// <param name="obj1">First source object to get value to compare.</param>
        /// <param name="obj2">Second source object to get value to compare.</param>
        /// <param name="getter">Function obtaining value to compare from source object.</param>
        /// <param name="comparer">Value comparer.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><description>(min: <paramref name="obj1"/>, max: <paramref name="obj2"/>) when <paramref name="getter"/>(<paramref name="obj1"/>) is greater than or equal to <paramref name="getter"/>(<paramref name="obj2"/>);</description></item>
        /// <item><description>(min: <paramref name="obj2"/>, max: <paramref name="obj1"/>) when <paramref name="getter"/>(<paramref name="obj1"/>) is lower than <paramref name="getter"/>(<paramref name="obj2"/>).</description></item>
        /// </list>
        /// </returns>
        public static (T min, T max) MinMax<T, TValue>(T obj1, T obj2, Func<T, TValue> getter, IComparer<TValue> comparer) =>
            comparer.Compare(getter(obj1), getter(obj2)) > 0
            ? (obj2, obj1)
            : (obj1, obj2);

        /// <summary>
        /// Returns lower of values obtained from the given objects using <paramref name="getter"/>, comparing them using <paramref name="comparer"/> function.
        /// </summary>
        /// <typeparam name="T">Type of source object.</typeparam>
        /// <typeparam name="TValue">Type of value.</typeparam>
        /// <param name="obj1">First source object to get value to compare.</param>
        /// <param name="obj2">Second source object to get value to compare.</param>
        /// <param name="getter">Function obtaining value to compare from source object.</param>
        /// <param name="comparer">Function comparing values.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><description><paramref name="obj1"/> when <paramref name="getter"/>(<paramref name="obj1"/>) is lower than or equal to <paramref name="getter"/>(<paramref name="obj2"/>);</description></item>
        /// <item><description><paramref name="obj2"/> when <paramref name="getter"/>(<paramref name="obj1"/>) is greater than <paramref name="getter"/>(<paramref name="obj2"/>).</description></item>
        /// </list>
        /// </returns>
        public static T Min<T, TValue>(T obj1, T obj2, Func<T, TValue> getter, Func<TValue, TValue, int> comparer) =>
            comparer(getter(obj1), getter(obj2)) > 0
            ? obj2
            : obj1;

        /// <summary>
        /// Returns greater of values obtained from the given objects using <paramref name="getter"/>, comparing them using <paramref name="comparer"/> function.
        /// </summary>
        /// <typeparam name="T">Type of source object.</typeparam>
        /// <typeparam name="TValue">Type of value.</typeparam>
        /// <param name="obj1">First source object to get value to compare.</param>
        /// <param name="obj2">Second source object to get value to compare.</param>
        /// <param name="getter">Function obtaining value to compare from source object.</param>
        /// <param name="comparer">Function comparing values.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><description><paramref name="obj1"/> when <paramref name="getter"/>(<paramref name="obj1"/>) is greater than or equal to <paramref name="getter"/>(<paramref name="obj2"/>);</description></item>
        /// <item><description><paramref name="obj2"/> when <paramref name="getter"/>(<paramref name="obj1"/>) is lower than <paramref name="getter"/>(<paramref name="obj2"/>).</description></item>
        /// </list>
        /// </returns>
        public static T Max<T, TValue>(T obj1, T obj2, Func<T, TValue> getter, Func<TValue, TValue, int> comparer) =>
            comparer(getter(obj1), getter(obj2)) < 0
            ? obj2
            : obj1;

        /// <summary>
        /// Returns values obtained from the given objects using <paramref name="getter"/> as ordered pair of min and max, comparing them using <paramref name="comparer"/> function.
        /// </summary>
        /// <typeparam name="T">Type of source object.</typeparam>
        /// <typeparam name="TValue">Type of value.</typeparam>
        /// <param name="obj1">First source object to get value to compare.</param>
        /// <param name="obj2">Second source object to get value to compare.</param>
        /// <param name="getter">Function obtaining value to compare from source object.</param>
        /// <param name="comparer">Function comparing values.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><description>(min: <paramref name="obj1"/>, max: <paramref name="obj2"/>) when <paramref name="getter"/>(<paramref name="obj1"/>) is greater than or equal to <paramref name="getter"/>(<paramref name="obj2"/>);</description></item>
        /// <item><description>(min: <paramref name="obj2"/>, max: <paramref name="obj1"/>) when <paramref name="getter"/>(<paramref name="obj1"/>) is lower than <paramref name="getter"/>(<paramref name="obj2"/>).</description></item>
        /// </list>
        /// </returns>
        public static (T min, T max) MinMax<T, TValue>(T obj1, T obj2, Func<T, TValue> getter, Func<TValue, TValue, int> comparer) =>
            comparer(getter(obj1), getter(obj2)) > 0
            ? (obj2, obj1)
            : (obj1, obj2);
    }
}
