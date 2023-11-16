using DotNetExtensions;
using System;

namespace ComplicatedPrimitives
{
    public readonly struct LimitValue<T> : IEquatable<LimitValue<T>>
        where T : IComparable<T>
    {
        public static readonly LimitValue<T> Infinity = new LimitValue<T>();

        private readonly bool _isFinite;

        public LimitValue(T value, LimitType type = default(LimitType))
        {
            Value = value;
            Type = type;
            _isFinite = true;
        }

        public T Value { get; }
        public LimitType Type { get; }
        public bool IsInfinity => !_isFinite;

        public LimitValue<TResult> Map<TResult>(Func<T, TResult> mapper)
            where TResult : IComparable<TResult> =>
            _isFinite
            ? new LimitValue<TResult>(mapper(Value), Type)
            : LimitValue<TResult>.Infinity;

        public LimitValue<T> Translate(Func<T, T> translation) =>
            IsInfinity
            ? Infinity
            : new LimitValue<T>(
                value: translation(Value),
                type: Type);

        public LimitValue<T> AsOpen() =>
            IsInfinity
            ? Infinity
            : new LimitValue<T>(Value, LimitType.Open);

        public LimitValue<T> AsClosed() =>
            IsInfinity
            ? Infinity
            : new LimitValue<T>(Value, LimitType.Closed);

        public bool RightContains(T value)
        {
            if (IsInfinity)
                return false;

            switch (Type)
            {
                case LimitType.Open:
                    return Value.CompareTo(value) < 0;
                case LimitType.Closed:
                    return Value.CompareTo(value) <= 0;
                default:
                    throw new InvalidOperationException($"Handling {Type.GetType().Name}.{Type} was not implemented.");
            }
        }

        public bool LeftContains(T value)
        {
            if (IsInfinity)
                return false;

            switch (Type)
            {
                case LimitType.Open:
                    return Value.CompareTo(value) > 0;
                case LimitType.Closed:
                    return Value.CompareTo(value) >= 0;
                default:
                    throw new InvalidOperationException($"Handling {Type.GetType().Name}.{Type} was not implemented.");
            }
        }

        #region Boiler-plate code

        public override string ToString() =>
            IsInfinity
            ? "∞"
            : string.Format(
                "{0}{1}{2}",
                Type.Match(open: () => '>', closed: () => '≥'),
                Value,
                Type.Match(open: () => '<', closed: () => '≤'));

        public bool Equals(LimitValue<T> other) =>
            IsInfinity && other.IsInfinity
            || Equals(Value, other.Value)
            && Type == other.Type;
        public override bool Equals(object obj) =>
            obj is LimitValue<T> other
            && Equals(other);
        public override int GetHashCode() =>
            IsInfinity
            ? "∞".GetHashCode()
            : new HashCode()
                .Append(Value, Type)
                .CurrentHash;

        public static bool operator ==(LimitValue<T> left, LimitValue<T> right) =>
            left.Equals(right);
        public static bool operator !=(LimitValue<T> left, LimitValue<T> right) =>
            !left.Equals(right);

        #endregion
    }
}
