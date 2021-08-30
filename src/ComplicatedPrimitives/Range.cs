using DotNetExtensions;
using System;

namespace ComplicatedPrimitives
{
    /// <summary>
    /// Readonly structure representing range of comparable values.
    /// </summary>
    /// <typeparam name="T">Type of range value (domain).</typeparam>
    public struct Range<T> : IEquatable<Range<T>> where T : IComparable<T>
    {
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
        /// <seealso cref="DirectedLimit{T}.IntersectsWith(DirectedLimit{T})">Intersecting directed limits.</seealso>
        /// <seealso cref="DirectedLimit{T}.Side">Directed limit's side.</seealso>
        public Range(DirectedLimit<T> left, DirectedLimit<T> right)
        {
            if (left.Side == LimitSide.Right)
                throw new ArgumentException("Must be left or undefined limit.", nameof(left));
            if (right.Side == LimitSide.Left)
                throw new ArgumentException("Must be right or undefined limit.", nameof(right));
            if (!left.IntersectsWith(right))
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
        /// <seealso cref="DirectedLimit{T}.IntersectsWith(DirectedLimit{T})">Intersecting directed limits.</seealso>
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
        /// <seealso cref="DirectedLimit{T}.IntersectsWith(DirectedLimit{T})">Intersecting directed limits.</seealso>
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
        /// Gets the value indicating whether this instance is an <see cref="Empty">empty</see> range (∅).
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
        /// Gets the value indicating whether this range is of finite limits.
        /// </summary>
        public bool IsFinite =>
            !IsInfiniteLeft
            && !IsInfiniteRight;

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

        /// <inheritdoc/>
        public bool Contains(T value) =>
            Left.Contains(value)
            && Right.Contains(value);

        /// <inheritdoc/>
        public bool Intersects(Range<T> other) =>
            !IsEmpty
            && !other.IsEmpty
            && DirectedLimit.Subset(Left, other.Left)
            .IntersectsWith(DirectedLimit.Subset(Right, other.Right));

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <summary>
        /// Gets intersection (common part) of this <see cref="Range{T}">range</see> and the <paramref name="other"/> range.
        /// </summary>
        /// <param name="other">Range to find intersection with.</param>
        /// <returns>Range being intersection of this instance and the <paramref name="other"/>; if ranges have no intersection, <see cref="Empty">empty</see> range is returned.</returns>
        public Range<T> IntersectWith(Range<T> other)
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
                left.IntersectsWith(right)
                ? new Range<T>(left, right)
                : Empty;
        }

        /// <summary>
        /// Tries to get intersection (common part) of this <see cref="Range{T}">range</see> and the <paramref name="other">other range</paramref>.
        /// </summary>
        /// <param name="other">Range to find intersection with.</param>
        /// <param name="result">
        /// When this method returns, contains the range being intersection of this instance and the <paramref name="other"/> if non-empty intersection exists,
        /// or <see cref="Empty">empty range</see> if it doesn't.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if non-empty intersection exists; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryIntersectWith(Range<T> other, out Range<T> result)
        {
            var left = DirectedLimit.Subset(Left, other.Left);
            var right = DirectedLimit.Subset(Right, other.Right);
            bool intersects = left.IntersectsWith(right);
            result =
                intersects
                ? new Range<T>(left, right)
                : Empty;
            return intersects;
        }

        /// <summary>
        /// Gets union (sum) of this <see cref="Range{T}">range</see> and the <paramref name="other">other range</paramref>.
        /// </summary>
        /// <param name="other">Range to unite with.</param>
        /// <returns>Range union of this instance and the <paramref name="other"/>.</returns>
        public RangeUnion<T> UnionWith(Range<T> other)
        {
            if (IsEmpty && other.IsEmpty)
                return new RangeUnion<T>(true, Empty);
            if (IsEmpty)
                return new RangeUnion<T>(true, other);
            if (other.IsEmpty)
                return new RangeUnion<T>(true, this);

            var leftSubset = DirectedLimit.Subset(Left, other.Left);
            var rightSubset = DirectedLimit.Subset(Right, other.Right);
            if (leftSubset.Complements(rightSubset)
                || leftSubset.IntersectsWith(rightSubset))
            {
                var left = DirectedLimit.Superset(Left, other.Left);
                var right = DirectedLimit.Superset(Right, other.Right);
                return new RangeUnion<T>(true, new Range<T>(left, right));
            }
            else
                return new RangeUnion<T>(true, this, other);
        }

        /// <summary>
        /// Gets absolute complement of this <see cref="Range{T}">range</see> (complement in <see cref="Infinite">infinite</see> range).
        /// </summary>
        /// <returns>Range union representing absolute complement of this instance.</returns>
        public RangeUnion<T> GetAbsoluteComplement()
        {
            if (IsEmpty)
                return new RangeUnion<T>(true, Infinite);
            if (IsInfinite)
                return RangeUnion<T>.Empty;
            if (IsInfiniteLeft)
                return new RangeUnion<T>(true, new Range<T>(Right.GetComplement(), DirectedLimit<T>.RightInfinity));
            if (IsInfiniteRight)
                return new RangeUnion<T>(true, new Range<T>(DirectedLimit<T>.LeftInfinity, Left.GetComplement()));

            return new RangeUnion<T>(true,
                new Range<T>(DirectedLimit<T>.LeftInfinity, Left.GetComplement()),
                new Range<T>(Right.GetComplement(), DirectedLimit<T>.RightInfinity));
        }

        /// <summary>
        /// Gets complement of this <see cref="Range{T}">range</see> in the <paramref name="other"/> range.
        /// </summary>
        /// <returns>Range union representing complement of this instance in the <paramref name="other"/> range.</returns>
        public RangeUnion<T> GetComplementIn(Range<T> other)
        {
            if (other.IsEmpty)
                return RangeUnion<T>.Empty;
            if (IsEmpty)
                return other.IsEmpty
                    ? RangeUnion<T>.Empty
                    : new RangeUnion<T>(true, other);
            if (Equals(other))
                return RangeUnion<T>.Empty;
            if (!Intersects(other))
                return new RangeUnion<T>(true, other);

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
                return new RangeUnion<T>(true, leftComplement, rightComplement);
            else if (!leftComplement.IsEmpty)
                return new RangeUnion<T>(true, leftComplement);
            else
                return new RangeUnion<T>(true, rightComplement);

        }

        /// <summary>
        /// Translates (moves) limits of this range by a given <paramref name="translation"/>, preserving their <see cref="DirectedLimit{T}.Side">sides</see> abd <see cref="LimitPoint{T}.Type">types</see>.
        /// </summary>
        /// <param name="translation">Function that translates range's limit value.</param>
        /// <returns>Range with both limits translated using given <paramref name="translation"/>.</returns>
        /// <remarks>
        /// This transformation preserves limits' sides which means that it can potentially cause errors if translated values violate any rules applied
        /// when creating instance of <see cref="Range{T}"/>. E.g. the following code:
        /// <code>var range = new Range&lt;int&gt;(0, 10);</code>
        /// <code>var result = range.Translate(e => e * -1);</code>
        /// will throw exception, because left limit value, after translation will be greater than the right limit value.
        /// </remarks>
        public Range<T> Translate(Func<T, T> translation) =>
            new Range<T>(
                left: Left.Translate(translation),
                right: Right.Translate(translation));

        #region Boiler-plate code

        /// <summary>
        /// Converts this <see cref="Range{T}">range</see> to its equivalent string representation following format:
        /// <code>{ left-limit-type }{ left-limit-value }{ separator }{ right-limit-type }{ right-limit-value }</code>
        /// </summary>
        /// <returns>String representation of this range consisting of:
        /// <list type="bullet">
        /// <item><term>left-limit-type</term><description>sign representing left limit type: '<c>(</c>' for open, '<c>[</c>' for closed,</description></item>
        /// <item><term>left-limit-value</term><description>string representation of <see cref="LeftValue">left value</see> or infinity sign '∞', if this instance is <see cref="IsInfiniteLeft">left infinite</see>,</description></item>
        /// <item><term>separator</term><description>string representation of separator '<c>;</c>',</description></item>
        /// <item><term>right-limit-value</term><description>string representation of <see cref="RightValue">right value</see> or infinity sign '∞', if this instance is <see cref="IsInfiniteRight">right infinite</see>,</description></item>
        /// <item><term>right-limit-type</term><description>sign representing right limit type: '<c>)</c>' for open, '<c>]</c>' for closed.</description></item>
        /// </list>
        /// </returns>
        public override string ToString()
        {
            if (IsEmpty)
                return Constants.EmptySetString;
            if (IsInfinite)
                return Constants.InfiniteRangeString;

            string leftStr =
                Left.Point.IsInfinite
                ? Constants.LeftInfiniteValueString
                : string.Concat(
                    Left.Type.Match(open: () => Constants.LeftOpenLimitTypeString, closed: () => Constants.LeftClosedLimitTypeString),
                    Left.Value);
            string rightStr =
                Right.Point.IsInfinite
                ? Constants.RightInfiniteValueString
                : string.Concat(
                    Right.Value,
                    Right.Type.Match(open: () => Constants.RightOpenLimitTypeString, closed: () => Constants.RightClosedLimitTypeString));
            return string.Concat(leftStr, Constants.ValueSeparatorString, rightStr);
        }

        /// <summary>
        /// Checks whether this instance of <see cref="Range{T}">range</see> is equal to the <paramref name="other"/> range.
        /// </summary>
        /// <param name="other">Range to check equality with this instance.</param>
        /// <returns>
        /// <see langword="true"/> if this instance is equal to the <paramref name="other"/> range which means:
        /// <list type="bullet">
        /// <item><description>both instances <see cref="IsEmpty">are empty</see>;</description></item>
        /// <item><description>both instances <see cref="IsInfinite">are infinite</see>;</description></item>
        /// <item><description>both instances <see cref="IsFinite">are finite</see> and their <see cref="Left">left</see> and <see cref="Right">right</see> limits are equal;</description></item>
        /// </list>
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(Range<T> other) =>
            IsEmpty == other.IsEmpty
            && IsInfinite == other.IsInfinite
            && Left == other.Left
            && Right == other.Right;

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is Range<T> other
            && Equals(other);

        /// <inheritdoc/>
        public override int GetHashCode() =>
            new HashCode()
            .Append(Left, Right)
            .CurrentHash;

        /// <summary>
        /// Determines whether two specified <see cref="Range{T}">ranges</see> have the same value.
        /// </summary>
        /// <param name="left">The first range to compare.</param>
        /// <param name="right">The second range to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Range<T> left, Range<T> right) =>
            left.Equals(right);

        /// <summary>
        /// Determines whether two specified <see cref="Range{T}">ranges</see> have different values.
        /// </summary>
        /// <param name="left">The first range to compare.</param>
        /// <param name="right">The second range to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is different from the value of <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Range<T> left, Range<T> right) =>
            !left.Equals(right);

        /// <summary>
        /// Gets union (sum) of <paramref name="left"/> and <paramref name="right"/> <see cref="Range{T}">range</see>>.
        /// </summary>
        /// <param name="left">First range to unite.</param>
        /// <param name="right">Second range to unite.</param>
        /// <returns>Range union of <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static RangeUnion<T> operator +(Range<T> left, Range<T> right) =>
            left.UnionWith(right);

        /// <summary>
        /// Gets complement of <paramref name="left"/> <see cref="Range{T}">range</see> in the <paramref name="right"/> range.
        /// </summary>
        /// <param name="left">Range to get complement of.</param>
        /// <param name="right">Range to get complement in.</param>
        /// <returns>Range union representing complement of <paramref name="left"/> in <paramref name="right"/>.</returns>
        public static RangeUnion<T> operator -(Range<T> left, Range<T> right) =>
            right.GetComplementIn(left);

        /// <summary>
        /// Gets intersection (common part) of <paramref name="left"/> and <paramref name="right"/> <see cref="Range{T}">range</see>.
        /// </summary>
        /// <param name="left">First range to unite.</param>
        /// <param name="right">Second range to unite.</param>
        /// <returns>Range being intersection of <paramref name="left"/> and the <paramref name="right"/>; if ranges have no intersection, <see cref="Empty">empty</see> range is returned.</returns>
        public static Range<T> operator *(Range<T> left, Range<T> right) =>
            left.IntersectWith(right);

        #endregion
    }
}
