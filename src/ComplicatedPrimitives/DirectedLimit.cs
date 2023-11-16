using DotNetExtensions;
using System;

namespace ComplicatedPrimitives
{
    public readonly struct DirectedLimit<T> : IEquatable<DirectedLimit<T>>, IComparable<DirectedLimit<T>>
        where T : IComparable<T>
    {
        public static readonly DirectedLimit<T> Undefined = new DirectedLimit<T>();
        public static readonly DirectedLimit<T> LeftInfinity = new DirectedLimit<T>(LimitValue<T>.Infinity, LimitSide.Left);
        public static readonly DirectedLimit<T> RightInfinity = new DirectedLimit<T>(LimitValue<T>.Infinity, LimitSide.Right);

        public DirectedLimit(LimitValue<T> limitValue, LimitSide side)
        {
            LimitValue = limitValue;
            Side = side;
        }
        public DirectedLimit(T value, LimitType type, LimitSide side)
            : this(new LimitValue<T>(value, type), side) { }

        public LimitValue<T> LimitValue { get; }
        public LimitSide Side { get; }

        public bool IsUndefined => Side == 0;
        public T Value => LimitValue.Value;
        public LimitType Type => LimitValue.Type;

        public DirectedLimit<TResult> Map<TResult>(Func<T, TResult> mapper)
            where TResult : IComparable<TResult> =>
            IsUndefined
            ? DirectedLimit<TResult>.Undefined
            : new DirectedLimit<TResult>(
                limitValue: LimitValue.Map(mapper),
                side: Side);

        public DirectedLimit<T> Translate(Func<T, T> translation) =>
            IsUndefined
            ? Undefined
            : new DirectedLimit<T>(
                limitValue: LimitValue.Translate(translation),
                side: Side);

        public DirectedLimit<T> GetComplement() =>
            IsUndefined
            ? Undefined
            : new DirectedLimit<T>(
                limitValue: new LimitValue<T>(LimitValue.Value, LimitValue.Type.Flip()),
                side: Side.Flip());

        public bool IsComplementOf(DirectedLimit<T> other)
        {
            if (IsUndefined || other.IsUndefined)
                return false;
            if (LimitValue.IsInfinity ^ other.LimitValue.IsInfinity)
                return false;
            if (LimitValue.IsInfinity && other.LimitValue.IsInfinity)
                return false;

            return Equals(Value, other.Value)
                && (Type == LimitType.Closed && other.Type == LimitType.Open
                    || Type == LimitType.Open && other.Type == LimitType.Closed)
                && (Side == LimitSide.Left && other.Side == LimitSide.Right
                    || Side == LimitSide.Right && other.Side == LimitSide.Left);
        }

        public bool Contains(T value)
        {
            switch (Side)
            {
                case LimitSide.Left:
                    return LimitValue.IsInfinity || LimitValue.RightContains(value);
                case LimitSide.Right:
                    return LimitValue.IsInfinity || LimitValue.LeftContains(value);
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
            if (LimitValue.IsInfinity || other.LimitValue.IsInfinity)
                return true;

            int valueComparison = Value.CompareTo(other.Value);
            if (valueComparison == 0)
                return Type == LimitType.Closed && other.Type == LimitType.Closed;

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

            if (other.LimitValue.IsInfinity && !LimitValue.IsInfinity)
                return true;

            if (Side != other.Side)
                return false;

            int valueComparison = Value.CompareTo(other.Value);
            if (valueComparison == 0)
                return Type == LimitType.Open && other.Type == LimitType.Closed;

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

            if (other.LimitValue.IsInfinity)
                return true;

            if (Side != other.Side)
                return false;

            int valueComparison = Value.CompareTo(other.Value);
            if (valueComparison == 0)
                return
                    Type == other.Type
                    || Type == LimitType.Open && other.Type == LimitType.Closed;

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

            if (LimitValue.IsInfinity && !other.LimitValue.IsInfinity)
                return true;

            if (Side != other.Side)
                return false;

            int valueComparison = Value.CompareTo(other.Value);
            if (valueComparison == 0)
                return Type == LimitType.Closed && other.Type == LimitType.Open;

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

            if (LimitValue.IsInfinity)
                return true;

            if (Side != other.Side)
                return false;

            int valueComparison = Value.CompareTo(other.Value);
            if (valueComparison == 0)
                return
                    Type == other.Type
                    || Type == LimitType.Closed && other.Type == LimitType.Open;

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
            T value = LimitValue.Value;
            var type = LimitValue.Type;
            return Side.Match(
                left: () => string.Concat(value, type.Match(open: () => '<', closed: () => '≤')),
                right: () => string.Concat(type.Match(open: () => '<', closed: () => '≤'), value),
                undefined: () => "undefined");
        }

        public bool Equals(DirectedLimit<T> other) =>
            IsUndefined && other.IsUndefined
            || LimitValue == other.LimitValue
                && Side == other.Side;
        public override bool Equals(object obj) =>
            obj is DirectedLimit<T> other
            && Equals(other);
        public override int GetHashCode() =>
            new HashCode()
            .Append(LimitValue, Side)
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
            int compareValue = LimitValue.Value.CompareTo(other.LimitValue.Value);
            if (compareValue != 0)
                return compareValue;

            int compareSide = Side.CompareTo(other.Side);
            if (compareSide != 0)
                return compareSide;

            if (LimitValue.Type != other.LimitValue.Type)
                return Side == LimitSide.Left
                    ? (LimitValue.Type == LimitType.Closed ? -1 : 1)
                    : (LimitValue.Type == LimitType.Open ? -1 : 1);

            return 0;
        }

        public static bool operator ==(DirectedLimit<T> left, DirectedLimit<T> right) =>
            left.Equals(right);
        public static bool operator !=(DirectedLimit<T> left, DirectedLimit<T> right) =>
            !left.Equals(right);

        public void Deconstruct(out LimitValue<T> limitValue, out LimitSide side)
        {
            limitValue = LimitValue;
            side = Side;
        }

        #endregion
    }
}
