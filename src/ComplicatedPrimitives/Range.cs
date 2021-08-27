using DotNetExtensions;
using System;
using System.Collections.Generic;

namespace ComplicatedPrimitives
{
    /// <summary>
    /// Readonly structure representing range of comparable values.
    /// </summary>
    /// <typeparam name="T">Type of range value (domain).</typeparam>
    public struct Range<T> : IRange<Range<T>, T>, IEquatable<Range<T>> where T : IComparable<T>
    {
        public const string EmptyRangeString = "Ø";
        public const string LeftOpenLimitTypeString = "(";
        public const string LeftClosedLimitTypeString = "[";
        public const string RightOpenLimitTypeString = ")";
        public const string RightClosedLimitTypeString = "]";
        public const string ValueSeparatorString = ";";
        public const string LeftInfiniteValueString = LeftOpenLimitTypeString + LimitPoint<T>.InfinityString;
        public const string RightInfiniteValueString = LimitPoint<T>.InfinityString + RightOpenLimitTypeString;
        public const string InfiniteRangeString = LeftInfiniteValueString + ValueSeparatorString + RightInfiniteValueString;

        /// <summary>
        /// Represents empty range (often described using symbol ∅).
        /// </summary>
        public static readonly Range<T> Empty = new Range<T>();

        /// <summary>
        /// Represents infinite range (often described using symbol (∞;∞)).
        /// </summary>
        public static readonly Range<T> Infinite = new Range<T>(LimitPoint<T>.Infinity, LimitPoint<T>.Infinity);

        private readonly bool _isNotEmpty;

        /// <summary>
        /// Creates new range with given limits.
        /// </summary>
        /// <param name="left">Left limit of range.</param>
        /// <param name="right">Right limit of range.</param>
        /// <exception cref="ArgumentException">Thrown when:
        /// <list type="bullet">
        /// <item><description><paramref name="left"/> limit is of right side;</description></item>
        /// <item><description><paramref name="right"/> limit is of left side;</description></item>
        /// <item><description><paramref name="left"/> and <paramref name="right"/> limits don't intersect;</description></item>
        /// </list>
        /// </exception>
        /// <seealso cref="DirectedLimit{T}.Intersects(DirectedLimit{T})">Intersecting directed limits.</seealso>
        /// <seealso cref="DirectedLimit{T}.Side">Directed limit's side.</seealso>
        public Range(DirectedLimit<T> left, DirectedLimit<T> right)
        {
            if (left.Side == LimitSide.Right)
                throw new ArgumentException("Must be left or undefined limit.", nameof(left));
            if (right.Side == LimitSide.Left)
                throw new ArgumentException("Must be right or undefined limit.", nameof(right));
            if (!left.Intersects(right))
                throw new ArgumentException("The left and right limits must have intersection.", nameof(left));

            Left = left;
            Right = right;
            _isNotEmpty = true;
        }

        /// <summary>
        /// Creates new range with given limits.
        /// </summary>
        /// <param name="left">Left limit of range.</param>
        /// <param name="right">Right limit of range.</param>
        /// <exception cref="ArgumentException">Thrown when:
        /// <list type="bullet">
        /// <item><description><paramref name="left"/> and <paramref name="right"/> limits don't intersect;</description></item>
        /// </list>
        /// </exception>
        /// <seealso cref="DirectedLimit{T}.Intersects(DirectedLimit{T})">Intersecting directed limits.</seealso>
        public Range(LimitPoint<T> left, LimitPoint<T> right)
            : this(left: new DirectedLimit<T>(left, LimitSide.Left), right: new DirectedLimit<T>(right, LimitSide.Right)) { }

        /// <summary>
        /// Creates new range with given limits.
        /// </summary>
        /// <param name="left">Value of left limit of range.</param>
        /// <param name="right">Value of right limit of range.</param>
        /// <param name="leftLimit">Type of left limit of range.</param>
        /// <param name="rightLimit">Type of right limit of range.</param>
        /// <exception cref="ArgumentException">Thrown when:
        /// <list type="bullet">
        /// <item><description><paramref name="left"/> and <paramref name="right"/> limits don't intersect;</description></item>
        /// </list>
        /// </exception>
        /// <seealso cref="DirectedLimit{T}.Intersects(DirectedLimit{T})">Intersecting directed limits.</seealso>
        public Range(T left, T right, LimitPointType leftLimit = default(LimitPointType), LimitPointType rightLimit = default(LimitPointType))
            : this(left: new LimitPoint<T>(left, leftLimit), right: new LimitPoint<T>(right, rightLimit)) { }

        /// <summary>
        /// Gets the left limit of this range.
        /// </summary>
        public DirectedLimit<T> Left { get; }

        /// <summary>
        /// Gets the right limit of this range.
        /// </summary>
        public DirectedLimit<T> Right { get; }

        /// <summary>
        /// Gets the value of left limit of this range.
        /// </summary>
        public T LeftValue => Left.Value;

        /// <summary>
        /// Gets the value of right limit of this range.
        /// </summary>
        public T RightValue => Right.Value;

        /// <summary>
        /// Gets the value indicating whether this instance is not an empty range (∅).
        /// </summary>
        public bool IsNotEmpty => _isNotEmpty;

        /// <summary>
        /// Gets the value indicating whether this instance is an empty range (∅).
        /// </summary>
        public bool IsEmpty => !_isNotEmpty;

        /// <summary>
        /// Gets the value indicating whether left limit is infinite.
        /// </summary>
        public bool IsInfiniteLeft =>
            Left.Point.IsInfinite;

        /// <summary>
        /// Gets the value indicating whether right limit is infinite.
        /// </summary>
        public bool IsInfiniteRight =>
            Right.Point.IsInfinite;

        /// <summary>
        /// Gets the value indicating whether this range is of infinite limits.
        /// </summary>
        public bool IsInfinite =>
            IsInfiniteLeft
            && IsInfiniteRight;

        /// <summary>
        /// Functor mapping range's value with special states preservation.
        /// </summary>
        /// <typeparam name="TResult">Target type to map value to.</typeparam>
        /// <param name="mapper">Value mapping function.</param>
        /// <returns><see cref="Range{TResult}"/> with <see cref="Left"/> and <see cref="Right"/> limits mapped using <paramref name="mapper"/> function when this instance <see cref="IsNotEmpty"/>; otherwise <see cref="Range{TResult}.Empty"/>.</returns>
        public Range<TResult> Map<TResult>(Func<T, TResult> mapper)
            where TResult : IComparable<TResult> =>
            IsNotEmpty
            ? new Range<TResult>(
                left: Left.Map(mapper),
                right: Right.Map(mapper))
            : Range<TResult>.Empty;

        /// <summary>
        /// Checks if this range contains given <paramref name="value"/> (mathematical equivalent of expression: <paramref name="value"/> ∊ <c>this</c>).
        /// </summary>
        /// <param name="value">Value to check inclusion relation of.</param>
        /// <returns><see langword="true"/> if the <paramref name="value"/> belongs to this range; otherwise <see langword="false"/>.</returns>
        public bool Contains(T value) =>
            Left.Contains(value)
            && Right.Contains(value);

        /// <summary>
        /// Checks if this range intersects (has common elements) with given <paramref name="other"/> range (mathematical equivalent of expression: <c>this</c> ∩ <paramref name="other"/> ≠ ∅).
        /// </summary>
        /// <param name="other">Other range to check intersection relation with.</param>
        /// <returns><see langword="true"/> if the <paramref name="other"/> has common elements to this range; otherwise <see langword="false"/>.</returns>
        public bool Intersects(Range<T> other) =>
            !IsEmpty
            && !other.IsEmpty
            && DirectedLimit.Subset(Left, other.Left)
            .Intersects(DirectedLimit.Subset(Right, other.Right));

        /// <summary>
        /// Checks whether <c>this</c> range is a subset of the <paramref name="other"/> range (<c>this</c> ⊆ <paramref name="other"/>).
        /// </summary>
        /// <param name="other">Range to check inclusion relation with.</param>
        /// <returns>
        /// <see langword="true"/> if this range is a subset of the <paramref name="other"/> range; otherwise <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// This function checks the weak inclusion relation which means that a range is in a given relation with equal range. To check strict version of this relation (excluding equal ranges), use <see cref="IsProperSubsetOf(Range{T})"/>.
        /// <list type="bullet">
        ///   <listheader>
        /// 	<description>Following conditions apply to this function:</description>
        ///   </listheader>
        ///   <item>
        /// 	<description>(A = B) → (A ⊆ B ⋀ B ⊆ A);</description>
        ///   </item>
        ///   <item>
        /// 	<description>∅ ⊆ A for every range A (especially: ∅ ⊆ ∅);</description>
        ///   </item>
        ///   <item>
        /// 	<description>(∞;∞) ⊈ A for any finite range A;</description>
        ///   </item>
        ///   <item>
        /// 	<description>(∞;∞) ⊆ (∞;∞);</description>
        ///   </item>
        /// </list>
        /// </remarks>
        public bool IsSubsetOf(Range<T> other)
        {
            if (Equals(other))
                return true;
            if (IsEmpty)
                return true;
            if (other.IsEmpty)
                return false;
            if (IsInfinite)
                return false;
            if (other.IsInfinite)
                return true;

            return Left.IsSubsetOf(other.Left)
                && Right.IsSubsetOf(other.Right);
        }

        public bool IsProperSubsetOf(Range<T> other)
        {
            if (Equals(other))
                return false;
            if (IsEmpty)
                return true;
            if (other.IsEmpty)
                return false;
            if (IsInfinite)
                return false;
            if (other.IsInfinite)
                return true;

            return Left.IsSubsetOf(other.Left)
                && Right.IsSubsetOf(other.Right);
        }

        public bool IsSupersetOf(Range<T> other)
        {
            if (Equals(other))
                return true;
            if (IsEmpty)
                return false;
            if (other.IsEmpty)
                return true;
            if (IsInfinite)
                return true;
            if (other.IsInfinite)
                return false;

            return Left.IsSupersetOf(other.Left)
                && Right.IsSupersetOf(other.Right);
        }

        public bool IsProperSupersetOf(Range<T> other)
        {
            if (Equals(other))
                return false;
            if (IsEmpty)
                return false;
            if (other.IsEmpty)
                return true;
            if (IsInfinite)
                return true;
            if (other.IsInfinite)
                return false;

            return Left.IsSupersetOf(other.Left)
                && Right.IsSupersetOf(other.Right);
        }

        public Range<T> GetIntersection(Range<T> other)
        {
            if (IsEmpty || other.IsEmpty)
                return Empty;
            if (IsInfinite)
                return other;
            if (other.IsInfinite)
                return this;

            var left = DirectedLimit.Subset(Left, other.Left);
            var right = DirectedLimit.Subset(Right, other.Right);
            return
                left.Intersects(right)
                ? new Range<T>(left, right)
                : Empty;
        }

        public bool TryIntersect(Range<T> other, out Range<T> intersection)
        {
            var left = DirectedLimit.Subset(Left, other.Left);
            var right = DirectedLimit.Subset(Right, other.Right);
            bool intersects = left.Intersects(right);
            intersection =
                intersects
                ? new Range<T>(left, right)
                : Empty;
            return intersects;
        }

        public RangeUnion<Range<T>, T> GetUnion(Range<T> other)
        {
            if (IsEmpty && other.IsEmpty)
                return new RangeUnion<Range<T>, T>(true, Empty);
            if (IsEmpty)
                return new RangeUnion<Range<T>, T>(true, other);
            if (other.IsEmpty)
                return new RangeUnion<Range<T>, T>(true, this);

            var leftSubset = DirectedLimit.Subset(Left, other.Left);
            var rightSubset = DirectedLimit.Subset(Right, other.Right);
            if (leftSubset.Complements(rightSubset)
                || leftSubset.Intersects(rightSubset))
            {
                var left = DirectedLimit.Superset(Left, other.Left);
                var right = DirectedLimit.Superset(Right, other.Right);
                return new RangeUnion<Range<T>, T>(true, new Range<T>(left, right));
            }
            else
                return new RangeUnion<Range<T>, T>(true, this, other);
        }

        public RangeUnion<Range<T>, T> GetAbsoluteComplement()
        {
            if (IsEmpty)
                return new RangeUnion<Range<T>, T>(true, Infinite);
            if (IsInfinite)
                return RangeUnion<Range<T>, T>.Empty;
            if (IsInfiniteLeft)
                return new RangeUnion<Range<T>, T>(true, new Range<T>(Right.GetComplement(), DirectedLimit<T>.RightInfinity));
            if (IsInfiniteRight)
                return new RangeUnion<Range<T>, T>(true, new Range<T>(DirectedLimit<T>.LeftInfinity, Left.GetComplement()));

            return new RangeUnion<Range<T>, T>(true,
                new Range<T>(DirectedLimit<T>.LeftInfinity, Left.GetComplement()),
                new Range<T>(Right.GetComplement(), DirectedLimit<T>.RightInfinity));
        }

        public RangeUnion<Range<T>, T> GetComplementIn(Range<T> other)
        {
            if (other.IsEmpty)
                return RangeUnion<Range<T>, T>.Empty;
            if (IsEmpty)
                return other.IsEmpty
                    ? RangeUnion<Range<T>, T>.Empty
                    : new RangeUnion<Range<T>, T>(true, other);
            if (Equals(other))
                return RangeUnion<Range<T>, T>.Empty;
            if (!Intersects(other))
                return new RangeUnion<Range<T>, T>(true, other);

            var leftComplement = Empty;
            if (Left.IsSubsetOf(other.Left))
                leftComplement = new Range<T>(
                    left: other.Left,
                    right: Left.GetComplement());

            var rightComplement = Empty;
            if (Right.IsSubsetOf(other.Right))
                rightComplement = new Range<T>(
                    left: Right.GetComplement(),
                    right: other.Right);

            if (!leftComplement.IsEmpty && !rightComplement.IsEmpty)
                return new RangeUnion<Range<T>, T>(true, leftComplement, rightComplement);
            else if (!leftComplement.IsEmpty)
                return new RangeUnion<Range<T>, T>(true, leftComplement);
            else
                return new RangeUnion<Range<T>, T>(true, rightComplement);

        }

        public Range<T> Translate(Func<T, T> translation) =>
            new Range<T>(
                left: Left.Translate(translation),
                right: Right.Translate(translation));

        IEnumerable<Range<T>> IRange<Range<T>, T>.GetUnion(Range<T> other) =>
            GetUnion(other);

        IEnumerable<Range<T>> IRange<Range<T>, T>.GetAbsoluteComplement() =>
            GetAbsoluteComplement();

        IEnumerable<Range<T>> IRange<Range<T>, T>.GetComplementIn(Range<T> other) =>
            GetComplementIn(other);

        #region Boiler-plate code

        public override string ToString()
        {
            if (IsEmpty)
                return EmptyRangeString;
            if (IsInfinite)
                return InfiniteRangeString;

            string leftStr =
                Left.Point.IsInfinite
                ? LeftInfiniteValueString
                : string.Concat(
                    Left.Type.Match(open: () => LeftOpenLimitTypeString, closed: () => LeftClosedLimitTypeString),
                    Left.Value);
            string rightStr =
                Right.Point.IsInfinite
                ? RightInfiniteValueString
                : string.Concat(
                    Right.Value,
                    Right.Type.Match(open: () => RightOpenLimitTypeString, closed: () => RightClosedLimitTypeString));
            return string.Concat(leftStr, ValueSeparatorString, rightStr);
        }

        public bool Equals(Range<T> other) =>
            IsEmpty == other.IsEmpty
            && IsInfinite == other.IsInfinite
            && Left == other.Left
            && Right == other.Right;
        public override bool Equals(object obj) =>
            obj is Range<T> other
            && Equals(other);
        public override int GetHashCode() =>
            new HashCode()
            .Append(Left, Right)
            .CurrentHash;

        public static bool operator ==(Range<T> left, Range<T> right) =>
            left.Equals(right);
        public static bool operator !=(Range<T> left, Range<T> right) =>
            !left.Equals(right);

        public static RangeUnion<Range<T>, T> operator +(Range<T> left, Range<T> right) =>
            left.GetUnion(right);

        public static RangeUnion<Range<T>, T> operator -(Range<T> left, Range<T> right) =>
            right.GetComplementIn(left);

        public static Range<T> operator *(Range<T> left, Range<T> right) =>
            left.GetIntersection(right);

        #endregion
    }
}
