using DotNetExtensions;
using System;

namespace ComplicatedPrimitives
{
    /// <summary>
    /// Structure representing limit value and type.
    /// </summary>
    /// <typeparam name="T">Type of value.</typeparam>
    public struct LimitValue<T> : IEquatable<LimitValue<T>>
        where T : IComparable<T>
    {
        /// <summary>
        /// Infinite limit value.
        /// </summary>
        public static readonly LimitValue<T> Infinity = new LimitValue<T>();
        
        private readonly bool _isInfinite;

        /// <summary>
        /// Initializes a new instance of the <see cref="LimitValue{T}"/> structure to a specified <paramref name="value"/> and <paramref name="type"/>.
        /// </summary>
        /// <param name="value">Limit value.</param>
        /// <param name="type">Limit type.</param>
        public LimitValue(T value, LimitType type = default(LimitType))
        {
            Value = value;
            Type = type;
            _isInfinite = true;
        }

        /// <summary>
        /// Gets the limit value.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Gets the limit type.
        /// </summary>
        public LimitType Type { get; }

        public LimitValue<TResult> Map<TResult>(Func<T, TResult> mapper)
            where TResult : IComparable<TResult> =>
            _isFinite
            ? new LimitValue<TResult>(mapper(Value), Type)
            : LimitValue<TResult>.Infinity;

        /// <summary>
        /// Indicates whether this instance represents an infinite limit.
        /// </summary>
        public bool IsInfinite => !_isFinite;

        /// <summary>
        /// Translates (moves) limit value with the given <paramref name="translation"/>.
        /// </summary>
        /// <param name="translation">Function translating limit's value.</param>
        /// <returns>New instance of <see cref="LimitValue{T}"/> with the same <see cref="Type"/>, but with <see cref="Value"/> translated using the given <paramref name="translation"/>.</returns>
        public LimitValue<T> Translate(Func<T, T> translation) =>
            IsInfinite
            ? Infinity
            : new LimitValue<T>(
                value: translation(Value),
                type: Type);

        /// <summary>
        /// Converts this instance to <see cref="LimitType.Open"/>.
        /// </summary>
        /// <returns>New instance of <see cref="LimitValue{T}"/> with the same <see cref="Value"/>, but with <see cref="Type"/> equal to <see cref="LimitType.Open"/>.</returns>
        public LimitValue<T> AsOpen() =>
            IsInfinite
            ? Infinity
            : new LimitValue<T>(Value, LimitType.Open);

        /// <summary>
        /// Converts this instance to <see cref="LimitType.Closed"/>.
        /// </summary>
        /// <returns>New instance of <see cref="LimitValue{T}"/> with the same <see cref="Value"/>, but with <see cref="Type"/> equal to <see cref="LimitType.Closed"/>.</returns>
        public LimitValue<T> AsClosed() =>
            IsInfinite
            ? Infinity
            : new LimitValue<T>(Value, LimitType.Closed);

        /// <summary>
        /// Gets the value indicating whether the <paramref name="value"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool RightContains(T value)
        {
            if (IsInfinite)
                return true;

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
            if (IsInfinite)
                return true;

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
            IsInfinite
            ? "∞"
            : string.Format(
                "{0}{1}{2}",
                Type.Match(open: () => '>', closed: () => '≥'),
                Value,
                Type.Match(open: () => '<', closed: () => '≤'));

        public bool Equals(LimitValue<T> other) =>
            IsInfinite && other.IsInfinite
            || Equals(Value, other.Value)
            && Type == other.Type;
        public override bool Equals(object obj) =>
            obj is LimitValue<T> other
            && Equals(other);
        public override int GetHashCode() =>
            IsInfinite
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
