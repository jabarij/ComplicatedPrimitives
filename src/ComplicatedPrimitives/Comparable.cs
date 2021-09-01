using System;
using System.Collections.Generic;

namespace ComplicatedPrimitives
{
    /// <include
    ///   file='ComplicatedPrimitives.xml'
    ///   path='//member[@name="T:ComplicatedPrimitives.Comparable"]' />
    public static class Comparable
    {
        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.Comparable.Min``1(``0,``0)"]' />
        public static T Min<T>(T value1, T value2) where T : IComparable<T> =>
            value1.CompareTo(value2) > 0
            ? value2
            : value1;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.Comparable.Max``1(``0,``0)"]' />
        public static T Max<T>(T value1, T value2) where T : IComparable<T> =>
            value1.CompareTo(value2) < 0
            ? value2
            : value1;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.Comparable.MinMax``1(``0,``0)"]' />
        public static (T min, T max) MinMax<T>(T value1, T value2) where T : IComparable<T> =>
            value1.CompareTo(value2) > 0
            ? (value2, value1)
            : (value1, value2);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.Comparable.Min``1(``0,``0,System.Collections.Generic.IComparer{``0})"]' />
        public static T Min<T>(T value1, T value2, IComparer<T> comparer) =>
            comparer.Compare(value1, value2) > 0
            ? value2
            : value1;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.Comparable.Max``1(``0,``0,System.Collections.Generic.IComparer{``0})"]' />
        public static T Max<T>(T value1, T value2, IComparer<T> comparer) =>
            comparer.Compare(value1, value2) < 0
            ? value2
            : value1;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.Comparable.MinMax``1(``0,``0,System.Collections.Generic.IComparer{``0})"]' />
        public static (T min, T max) MinMax<T>(T value1, T value2, IComparer<T> comparer) =>
            comparer.Compare(value1, value2) > 0
            ? (value2, value1)
            : (value1, value2);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.Comparable.Min``1(``0,``0,System.Func{``0,``0,System.Int32})"]' />
        public static T Min<T>(T value1, T value2, Func<T, T, int> comparer) =>
            comparer(value1, value2) > 0
            ? value2
            : value1;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.Comparable.Max``1(``0,``0,System.Func{``0,``0,System.Int32})"]' />
        public static T Max<T>(T value1, T value2, Func<T, T, int> comparer) =>
            comparer(value1, value2) < 0
            ? value2
            : value1;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.Comparable.MinMax``1(``0,``0,System.Func{``0,``0,System.Int32})"]' />
        public static (T min, T max) MinMax<T>(T value1, T value2, Func<T, T, int> comparer) =>
            comparer(value1, value2) > 0
            ? (value2, value1)
            : (value1, value2);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.Comparable.Min``2(``0,``0,System.Func{``0,``1})"]' />
        public static T Min<T, TValue>(T obj1, T obj2, Func<T, TValue> getter) where TValue : IComparable<TValue> =>
            getter(obj1).CompareTo(getter(obj2)) > 0
            ? obj2
            : obj1;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.Comparable.Max``2(``0,``0,System.Func{``0,``1})"]' />
        public static T Max<T, TValue>(T obj1, T obj2, Func<T, TValue> getter) where TValue : IComparable<TValue> =>
            getter(obj1).CompareTo(getter(obj2)) < 0
            ? obj2
            : obj1;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.Comparable.MinMax``2(``0,``0,System.Func{``0,``1})"]' />
        public static (T min, T max) MinMax<T, TValue>(T obj1, T obj2, Func<T, TValue> getter) where TValue : IComparable<TValue> =>
            getter(obj1).CompareTo(getter(obj2)) > 0
            ? (obj2, obj1)
            : (obj1, obj2);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.Comparable.Min``2(``0,``0,System.Func{``0,``1},System.Collections.Generic.IComparer{``1})"]' />
        public static T Min<T, TValue>(T obj1, T obj2, Func<T, TValue> getter, IComparer<TValue> comparer) =>
            comparer.Compare(getter(obj1), getter(obj2)) > 0
            ? obj2
            : obj1;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.Comparable.Max``2(``0,``0,System.Func{``0,``1},System.Collections.Generic.IComparer{``1})"]' />
        public static T Max<T, TValue>(T obj1, T obj2, Func<T, TValue> getter, IComparer<TValue> comparer) =>
            comparer.Compare(getter(obj1), getter(obj2)) < 0
            ? obj2
            : obj1;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.Comparable.MinMax``2(``0,``0,System.Func{``0,``1},System.Collections.Generic.IComparer{``1})"]' />
        public static (T min, T max) MinMax<T, TValue>(T obj1, T obj2, Func<T, TValue> getter, IComparer<TValue> comparer) =>
            comparer.Compare(getter(obj1), getter(obj2)) > 0
            ? (obj2, obj1)
            : (obj1, obj2);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.Comparable.Min``2(``0,``0,System.Func{``0,``1},System.Func{``1,``1,System.Int32})"]' />
        public static T Min<T, TValue>(T obj1, T obj2, Func<T, TValue> getter, Func<TValue, TValue, int> comparer) =>
            comparer(getter(obj1), getter(obj2)) > 0
            ? obj2
            : obj1;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.Comparable.Max``2(``0,``0,System.Func{``0,``1},System.Func{``1,``1,System.Int32})"]' />
        public static T Max<T, TValue>(T obj1, T obj2, Func<T, TValue> getter, Func<TValue, TValue, int> comparer) =>
            comparer(getter(obj1), getter(obj2)) < 0
            ? obj2
            : obj1;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.Comparable.MinMax``2(``0,``0,System.Func{``0,``1},System.Func{``1,``1,System.Int32})"]' />
        public static (T min, T max) MinMax<T, TValue>(T obj1, T obj2, Func<T, TValue> getter, Func<TValue, TValue, int> comparer) =>
            comparer(getter(obj1), getter(obj2)) > 0
            ? (obj2, obj1)
            : (obj1, obj2);
    }
}
