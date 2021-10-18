using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ComplicatedPrimitives
{
    /// <summary>
    /// Represents union of <see cref="Range{T}">ranges</see>.
    /// </summary>
    /// <typeparam name="T">Type of range's value.</typeparam>
    public struct RangeUnion<T> :
        IComparativeSet<Range<T>, T>,
        IEnumerable<Range<T>>
        where T : IComparable<T>
    {
        /// <summary>
        /// Gets new <see cref="IsNormalized">normalized</see> empty range union (often described using symbol ∅).
        /// </summary>
        public static RangeUnion<T> Empty => new RangeUnion<T>();

        private readonly List<Range<T>> _ranges;
        private readonly bool? _isNormalized;

        ///// <summary>
        ///// Creates new <see cref="IsNormalized">normalized</see> empty range union (often described using symbol ∅).
        ///// </summary>
        //public RangeUnion()
        //{
        //    _ranges = new List<Range<T>>();
        //    IsNormalized = true;
        //}

        /// <summary>
        /// Creates new <see cref="IsNormalized">normalized</see> range union with a given <paramref name="range"/>.
        /// </summary>
        /// <param name="range"></param>
        public RangeUnion(Range<T> range)
        {
            _ranges = new List<Range<T>> { range };
            _isNormalized = true;
        }

        /// <summary>
        /// Creates new range union with a given collection of <paramref name="ranges"/>.
        /// </summary>
        /// <param name="ranges">Collection of ranges to include in the new union.</param>
        public RangeUnion(IEnumerable<Range<T>> ranges)
        {
            if (ranges == null)
                throw new ArgumentNullException(nameof(ranges));
            _ranges = new List<Range<T>>(ranges);
            _isNormalized = _ranges.Count < 2;
        }

        internal RangeUnion(bool isNormalized, params Range<T>[] ranges)
        {
            _isNormalized = isNormalized;
            _ranges = new List<Range<T>>(ranges.Length);
            _ranges.AddRange(ranges);
        }

        /// <summary>
        /// Gets the value indicating whether this <see cref="RangeUnion{T}">range union</see> is normalized, which means
        /// if it consists only of disjoint ranges.
        /// </summary>
        public bool IsNormalized =>
            _isNormalized
            ?? true;

        /// <summary>
        /// Gets the value indicating whether this <see cref="RangeUnion{T}">range union</see> is empty, which means
        /// it contains no ranges at all or cotains only <see cref="Range{T}.IsEmpty">empty ranges</see>.
        /// </summary>
        public bool IsEmpty =>
            _ranges is null
            || _ranges.Count == 0
            || _ranges.All(e => e.IsEmpty);

        /// <summary>
        /// Gets intersection (common part) of this <see cref="RangeUnion{T}">range union</see> and the given <paramref name="range"/>.
        /// </summary>
        /// <param name="range">Range to find intersection with.</param>
        /// <returns>Range union being intersection of this instance and the <paramref name="range"/>; if there is no intersection, <see cref="Empty">empty</see> range union is returned.</returns>
        public RangeUnion<T> IntersectWith(Range<T> range)
        {
            if (_ranges.Count == 0)
                return Empty;

            var intersectionIndicators = new (bool, Range<T>)[_ranges.Count];
            int intersectionsCount = 0;
            for (int rangeIndex = 0; rangeIndex < intersectionIndicators.Length; rangeIndex++)
            {
                var intersection = _ranges[rangeIndex].IntersectWith(range);
                bool intersects = !intersection.IsEmpty;
                intersectionIndicators[rangeIndex] = (intersects, intersection);
                if (intersects) intersectionsCount++;
            }

            var intersections = new Range<T>[intersectionsCount];
            int intersectionIndex = 0;
            for (int rangeIndex = 0; rangeIndex < intersectionIndicators.Length; rangeIndex++)
            {
                var indicator = intersectionIndicators[rangeIndex];
                if (indicator.Item1)
                    intersections[intersectionIndex] = indicator.Item2;
            }

            return new RangeUnion<T>(intersections);
        }

        /// <summary>
        /// Converts this <see cref="RangeUnion{T}">range union</see> to normalized by merging its ranges.
        /// </summary>
        /// <returns>Range union covering the same space of <typeparamref name="T"/> but with all ranges merged into non-intersecting collection.</returns>
        public RangeUnion<T> AsNormalized() =>
            IsNormalized
            ? this
            : new RangeUnion<T>(true, _ranges.Merge().ToArray());

        #region IEnumerable<Range<T>>

        /// <summary>
        /// Creates an enumerator that iterates through all ranges belonging to this instance of <see cref="RangeUnion{T}"/>.
        /// </summary>
        /// <returns>New instance of enumerator for this instance.</returns>
        public IEnumerator<Range<T>> GetEnumerator() =>
            _ranges.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        #endregion

        #region IComparativeSet<Range<T>, T>

        /// <inheritdoc/>
        public bool Contains(T value)
        {
            if (_ranges.Count == 0)
                return false;

            for (int rangeIndex = 0; rangeIndex < _ranges.Count; rangeIndex++)
                if (_ranges[rangeIndex].Contains(value))
                    return true;

            return false;
        }

        /// <inheritdoc/>
        public bool IntersectsWith(Range<T> other)
        {
            for (int rangeIndex = 0; rangeIndex < _ranges.Count; rangeIndex++)
                if (_ranges[rangeIndex].Intersects(other))
                    return true;

            return false;
        }

        /// <inheritdoc/>
        public bool IsSubsetOf(Range<T> other) =>
            _ranges.All(e => e.IsSubsetOf(other));

        /// <inheritdoc/>
        public bool IsProperSubsetOf(Range<T> other) =>
            _ranges.All(e => e.IsSubsetOf(other));

        /// <inheritdoc/>
        public bool IsSupersetOf(Range<T> other) =>
            _ranges.All(e => other.IsSubsetOf(e));

        /// <inheritdoc/>
        public bool IsProperSupersetOf(Range<T> other) =>
            _ranges.All(e => other.IsProperSubsetOf(e));

        #endregion
    }
}