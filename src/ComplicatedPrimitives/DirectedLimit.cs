using DotNetExtensions;
using System;

namespace ComplicatedPrimitives
{
    public struct DirectedLimit<T> : IEquatable<DirectedLimit<T>>, IComparable<DirectedLimit<T>>
        where T : IComparable<T>
    {
        public static readonly DirectedLimit<T> Undefined = new DirectedLimit<T>();
        public static readonly DirectedLimit<T> LeftInfinity = new DirectedLimit<T>(LimitPoint<T>.Infinity, LimitSide.Left);
        public static readonly DirectedLimit<T> RightInfinity = new DirectedLimit<T>(LimitPoint<T>.Infinity, LimitSide.Right);

        public DirectedLimit(LimitPoint<T> point, LimitSide side)
        {
            Point = point;
            Side = side;
        }
        public DirectedLimit(T value, LimitPointType type, LimitSide side)
            : this(new LimitPoint<T>(value, type), side) { }

        public LimitPoint<T> Point { get; }
        public LimitSide Side { get; }

        public bool IsUndefined => Side == 0;
        public T Value => Point.Value;
        public LimitPointType Type => Point.Type;

        public DirectedLimit<TResult> Map<TResult>(Func<T, TResult> mapper)
            where TResult : IComparable<TResult> =>
            IsUndefined
            ? DirectedLimit<TResult>.Undefined
            : new DirectedLimit<TResult>(
                point: Point.Map(mapper),
                side: Side);

        public DirectedLimit<T> Translate(Func<T, T> translation) =>
            IsUndefined
            ? Undefined
            : new DirectedLimit<T>(
                point: Point.Translate(translation),
                side: Side);

        public DirectedLimit<T> GetComplement() =>
            IsUndefined
            ? Undefined
            : new DirectedLimit<T>(
                point: new LimitPoint<T>(Point.Value, Point.Type.Flip()),
                side: Side.Flip());

        public bool IsComplementOf(DirectedLimit<T> other)
        {
            if (IsUndefined || other.IsUndefined)
                return false;
            if (Point.IsInfinite ^ other.Point.IsInfinite)
                return false;
            if (Point.IsInfinite && other.Point.IsInfinite)
                return false;

            return Equals(Value, other.Value)
                && (Type == LimitPointType.Closed && other.Type == LimitPointType.Open
                    || Type == LimitPointType.Open && other.Type == LimitPointType.Closed)
                && (Side == LimitSide.Left && other.Side == LimitSide.Right
                    || Side == LimitSide.Right && other.Side == LimitSide.Left);
        }

        public bool Contains(T value)
        {
            switch (Side)
            {
                case LimitSide.Left:
                    return Point.RightContains(value);
                case LimitSide.Right:
                    return Point.LeftContains(value);
                case 0:
                    throw new InvalidOperationException("Cannot check value belonging for undefined limit.");
                default:
                    throw new InvalidOperationException($"Handling {Side.GetType().Name}.{Side} was not implemented.");
            }
        }

        public bool Intersects(DirectedLimit<T> other)
        {
            if (IsUndefined || other.IsUndefined)
                return false;
            if (Side == other.Side)
                return true;
            if (Point.IsInfinite || other.Point.IsInfinite)
                return true;

            int valueComparison = Value.CompareTo(other.Value);
            if (valueComparison == 0)
                return Type == LimitPointType.Closed && other.Type == LimitPointType.Closed;

            switch (Side)
            {
                case LimitSide.Left:
                    return valueComparison < 0;
                case LimitSide.Right:
                    return valueComparison > 0;
                default:
                    throw new InvalidOperationException($"Handling {Side.GetType().Name}.{Side} was not implemented.");
            }
        }

        public bool IsProperSubsetOf(DirectedLimit<T> other)
        {
            if (IsUndefined || other.IsUndefined)
                return false;

            if (other.Point.IsInfinite && !Point.IsInfinite)
                return true;

            if (Side != other.Side)
                return false;

            int valueComparison = Value.CompareTo(other.Value);
            if (valueComparison == 0)
                return Type == LimitPointType.Open && other.Type == LimitPointType.Closed;

            switch (Side)
            {
                case LimitSide.Left:
                    return valueComparison > 0;
                case LimitSide.Right:
                    return valueComparison < 0;
                default:
                    throw new InvalidOperationException($"Handling {Side.GetType().Name}.{Side} was not implemented.");
            }
        }

        public bool IsSubsetOf(DirectedLimit<T> other)
        {
            if (IsUndefined || other.IsUndefined)
                return false;

            if (other.Point.IsInfinite)
                return true;

            if (Side != other.Side)
                return false;

            int valueComparison = Value.CompareTo(other.Value);
            if (valueComparison == 0)
                return
                    Type == other.Type
                    || Type == LimitPointType.Open && other.Type == LimitPointType.Closed;

            switch (Side)
            {
                case LimitSide.Left:
                    return valueComparison > 0;
                case LimitSide.Right:
                    return valueComparison < 0;
                default:
                    throw new InvalidOperationException($"Handling {Side.GetType().Name}.{Side} was not implemented.");
            }
        }

        public bool IsProperSupersetOf(DirectedLimit<T> other)
        {
            if (IsUndefined || other.IsUndefined)
                return false;

            if (Point.IsInfinite && !other.Point.IsInfinite)
                return true;

            if (Side != other.Side)
                return false;

            int valueComparison = Value.CompareTo(other.Value);
            if (valueComparison == 0)
                return Type == LimitPointType.Closed && other.Type == LimitPointType.Open;

            switch (Side)
            {
                case LimitSide.Left:
                    return valueComparison < 0;
                case LimitSide.Right:
                    return valueComparison > 0;
                default:
                    throw new InvalidOperationException($"Handling {Side.GetType().Name}.{Side} was not implemented.");
            }
        }

        public bool IsSupersetOf(DirectedLimit<T> other)
        {
            if (IsUndefined || other.IsUndefined)
                return false;

            if (Point.IsInfinite)
                return true;

            if (Side != other.Side)
                return false;

            int valueComparison = Value.CompareTo(other.Value);
            if (valueComparison == 0)
                return
                    Type == other.Type
                    || Type == LimitPointType.Closed && other.Type == LimitPointType.Open;

            switch (Side)
            {
                case LimitSide.Left:
                    return valueComparison < 0;
                case LimitSide.Right:
                    return valueComparison > 0;
                default:
                    throw new InvalidOperationException($"Handling {Side.GetType().Name}.{Side} was not implemented.");
            }
        }

        #region Boiler-plate code

        public override string ToString()
        {
            T value = Point.Value;
            var type = Point.Type;
            return Side.Match(
                left: () => string.Concat(value, type.Match(open: () => '<', closed: () => '≤')),
                right: () => string.Concat(type.Match(open: () => '<', closed: () => '≤'), value),
                undefined: () => "undefined");
        }

        public bool Equals(DirectedLimit<T> other) =>
            IsUndefined && other.IsUndefined
            || Point == other.Point
                && Side == other.Side;
        public override bool Equals(object obj) =>
            obj is DirectedLimit<T> other
            && Equals(other);
        public override int GetHashCode() =>
            new HashCode()
            .Append(Point, Side)
            .CurrentHash;

        public int CompareTo(DirectedLimit<T> other)
        {
            bool isLeftUndefined = IsUndefined;
            bool isRightUndefined = other.IsUndefined;
            if (isLeftUndefined && isRightUndefined)
                return 0;
            else if (isLeftUndefined ^ isRightUndefined)
                return isLeftUndefined ? -1 : 1;

            return CompareInternal(other);
        }

        private int CompareInternal(DirectedLimit<T> other)
        {
            int compareValue = Point.Value.CompareTo(other.Point.Value);
            if (compareValue != 0)
                return compareValue;

            int compareSide = Side.CompareTo(other.Side);
            if (compareSide != 0)
                return compareSide;

            if (Point.Type != other.Point.Type)
                return Side == LimitSide.Left
                    ? (Point.Type == LimitPointType.Closed ? -1 : 1)
                    : (Point.Type == LimitPointType.Open ? -1 : 1);

            return 0;
        }

        public static bool operator ==(DirectedLimit<T> left, DirectedLimit<T> right) =>
            left.Equals(right);
        public static bool operator !=(DirectedLimit<T> left, DirectedLimit<T> right) =>
            !left.Equals(right);

        #endregion
    }
}
