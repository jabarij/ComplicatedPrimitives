using DotNetExtensions;
using System;
using System.Collections.Generic;

namespace ComplicatedPrimitives
{
    public readonly struct Range<T> : IRange<Range<T>, T>, IEquatable<Range<T>> where T : IComparable<T>
    {
        public static readonly Range<T> Empty = new Range<T>();
        public static readonly Range<T> Infinite = new Range<T>(LimitValue<T>.Infinity, LimitValue<T>.Infinity);

        private readonly bool _isNotEmpty;

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
        public Range(LimitValue<T> left, LimitValue<T> right)
            : this(left: new DirectedLimit<T>(left, LimitSide.Left), right: new DirectedLimit<T>(right, LimitSide.Right)) { }
        public Range(T left, T right, LimitType leftLimit = default(LimitType), LimitType rightLimit = default(LimitType))
            : this(left: new LimitValue<T>(left, leftLimit), right: new LimitValue<T>(right, rightLimit)) { }

        public DirectedLimit<T> Left { get; }
        public DirectedLimit<T> Right { get; }

        public T LeftValue => Left.Value;
        public T RightValue => Right.Value;

        public bool IsEmpty =>
            !_isNotEmpty;

        public bool IsLimitedLeft =>
            !Left.LimitValue.IsInfinity;

        public bool IsLimitedRight =>
            !Right.LimitValue.IsInfinity;

        public bool IsInfinite =>
            !IsLimitedLeft
            && !IsLimitedRight;

        public Range<TResult> Map<TResult>(Func<T, TResult> mapper)
            where TResult : IComparable<TResult> =>
            IsEmpty
            ? Range<TResult>.Empty
            : new Range<TResult>(
                left: Left.Map(mapper),
                right: Right.Map(mapper));

        public bool Contains(T value) =>
            Left.Contains(value)
            && Right.Contains(value);

        public bool Intersects(Range<T> other) =>
            !IsEmpty
            && !other.IsEmpty
            && Comparable.Subset(Left, other.Left)
            .Intersects(Comparable.Subset(Right, other.Right));

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

            var left = Comparable.Subset(Left, other.Left);
            var right = Comparable.Subset(Right, other.Right);
            return
                left.Intersects(right)
                ? new Range<T>(left, right)
                : Empty;
        }

        public bool TryIntersect(Range<T> other, out Range<T> intersection)
        {
            var left = Comparable.Subset(Left, other.Left);
            var right = Comparable.Subset(Right, other.Right);
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

            var leftSubset = Comparable.Subset(Left, other.Left);
            var rightSubset = Comparable.Subset(Right, other.Right);
            if (leftSubset.IsComplementOf(rightSubset)
                || leftSubset.Intersects(rightSubset))
            {
                var left = Comparable.Superset(Left, other.Left);
                var right = Comparable.Superset(Right, other.Right);
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
            if (!IsLimitedLeft)
                return new RangeUnion<Range<T>, T>(true, new Range<T>(Right.GetComplement(), DirectedLimit<T>.RightInfinity));
            if (!IsLimitedRight)
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
                return "Ø";
            if (IsInfinite)
                return "(∞;∞)";
            string leftStr =
                Left.LimitValue.IsInfinity
                ? "(∞"
                : string.Format("{0}{1}",
                    Left.Type.Match(open: () => '(', closed: () => '['),
                    Left.Value);
            string rightStr =
                Right.LimitValue.IsInfinity
                ? "∞)"
                : string.Format("{0}{1}",
                    Right.Value,
                    Right.Type.Match(open: () => ')', closed: () => ']'));
            return string.Concat(leftStr, ';', rightStr);
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

        public void Deconstruct(out DirectedLimit<T> left, out DirectedLimit<T> right)
        {
            left = Left;
            right = Right;
        }

        #endregion
    }
}
