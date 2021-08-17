using DotNetExtensions;
using System;

namespace ComplicatedPrimitives
{
    /// <summary>
    /// Structure representing limit point of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of limit point's value.</typeparam>
    public struct LimitValue<T> : IEquatable<LimitValue<T>>
        where T : IComparable<T>
    {
        /// <summary>
        /// Infinite limit point.
        /// </summary>
        public static readonly LimitValue<T> Infinity = new LimitValue<T>();

        private readonly bool _isFinite;
        private readonly LimitType _type;

        /// <summary>
        /// Creates a new <see cref="IsFinite">finite</see> instance of the <see cref="LimitValue{T}"/> structure with a specified <paramref name="value"/> and <paramref name="type"/>.
        /// </summary>
        /// <param name="value">Limit point's value.</param>
        /// <param name="type">Limit point's type.</param>
        public LimitValue(T value, LimitType type = default(LimitType))
        {
            Value = value;
            _type = type;
            _isFinite = true;
        }

        /// <summary>
        /// Gets the value of this <see cref="LimitValue{T}">limit point</see>.
        /// </summary>
        /// <remarks>
        /// For <see cref="IsInfinite">infinite</see> instances of <see cref="LimitValue{T}">limit point</see>, the value is always considered to be default of <typeparamref name="T"/>.
        /// </remarks>
        public T Value { get; }

        /// <summary>
        /// Gets the value indicating whether this <see cref="LimitValue{T}">limit point</see> is either <see cref="LimitType.Open">open</see> or <see cref="LimitType.Closed">closed</see>,
        /// which means - in other words - whether this <see cref="LimitValue{T}">limit point</see> covers it's <see cref="Value">value</see> or considers it unreachable.
        /// </summary>
        /// <remarks>
        /// For <see cref="IsInfinite">infinite</see> instances of <see cref="LimitValue{T}">limit point</see>, the type is always considered to be <see cref="LimitType.Open">open</see>.
        /// </remarks>
        public LimitType Type =>
            _isFinite
            ? _type
            : LimitType.Open;

        /// <summary>
        /// Indicates whether this instance represents a finite <see cref="LimitValue{T}">limit point</see> with certain <see cref="Value">value</see>.
        /// </summary>
        public bool IsFinite => _isFinite;

        /// <summary>
        /// Indicates whether this instance represents an infinite <see cref="LimitValue{T}">limit point</see>.
        /// </summary>
        public bool IsInfinite => !_isFinite;

        /// <summary>
        /// Maps this instance to <see cref="LimitValue{TResult}">limit point</see> of <typeparamref name="TResult"/> using given <paramref name="mapper"/>.
        /// </summary>
        /// <typeparam name="TResult">Target type to map <see cref="Value">value</see> to.</typeparam>
        /// <param name="mapper">Function that maps value of type <typeparamref name="T"/> to type <typeparamref name="TResult"/>.</param>
        /// <returns>
        /// When this instance <see cref="IsFinite">is finite</see>, new <see cref="LimitValue{TResult}">limit point</see> of <typeparamref name="TResult"/>
        /// with then same <see cref="Type">type</see>, but with <see cref="Value">value</see> being mapped using given <paramref name="mapper"/>;
        /// otherwise, <see cref="LimitValue{TResult}.Infinity">infinite limit point</see> of <typeparamref name="TResult"/>.
        /// </returns>
        public LimitValue<TResult> Map<TResult>(Func<T, TResult> mapper)
            where TResult : IComparable<TResult> =>
            _isFinite
            ? new LimitValue<TResult>(mapper(Value), Type)
            : LimitValue<TResult>.Infinity;

        /// <summary>
        /// Translates (moves) this <see cref="LimitValue{T}">limit point</see> using given <paramref name="translation"/>.
        /// </summary>
        /// <param name="translation">Function that translates limit point's value.</param>
        /// <returns>
        /// When this instance <see cref="IsFinite">is finite</see>, new <see cref="LimitValue{TResult}">limit point</see> with then same <see cref="Type">type</see>,
        /// but with <see cref="Value">value</see> being translated using given <paramref name="translation"/>;
        /// otherwise, <see cref="Infinity">infinity</see>.
        /// </returns>
        public LimitValue<T> Translate(Func<T, T> translation) =>
            IsInfinite
            ? Infinity
            : new LimitValue<T>(
                value: translation(Value),
                type: Type);

        /// <summary>
        /// Converts this <see cref="LimitValue{T}">limit point</see> to <see cref="LimitType.Open">open</see>.
        /// </summary>
        /// <returns>
        /// When this instance <see cref="IsFinite">is finite</see>, new <see cref="LimitValue{T}">limit point</see> with the same <see cref="Value">value</see>,
        /// but with <see cref="Type">type</see> equal to <seealso cref="LimitType.Open">open</seealso>;
        /// otherwise, <see cref="Infinity">infinity</see>.
        /// </returns>
        public LimitValue<T> AsOpen() =>
            IsInfinite
            ? Infinity
            : new LimitValue<T>(Value, LimitType.Open);

        /// <summary>
        /// Converts this <see cref="LimitValue{T}">limit point</see> to <see cref="LimitType.Closed">closed</see>.
        /// </summary>
        /// <returns>
        /// When this instance <see cref="IsFinite">is finite</see>, new <see cref="LimitValue{T}">limit point</see> with the same <see cref="Value">value</see>,
        /// but with <see cref="Type">type</see> equal to <seealso cref="LimitType.Closed">open</seealso>;
        /// otherwise, <see cref="Infinity">infinity</see>.
        /// </returns>
        public LimitValue<T> AsClosed() =>
            IsInfinite
            ? Infinity
            : new LimitValue<T>(Value, LimitType.Closed);

        /// <summary>
        /// Gets the value indicating whether this <see cref="LimitValue{T}">limit point</see> contains the <paramref name="value"/> on it's right side
        /// or in other words whether <paramref name="value"/> is part of the set limited by this <see cref="LimitValue{T}">limit point</see> from the left side.
        /// </summary>
        /// <param name="value">Value to compare with this instance.</param>
        /// <returns>
        /// <see langword="true"/> when:
        /// <list type="bullet">
        /// <item><description><paramref name="value"/> is greater than this <see cref="Value">value</see> for this <see cref="Type">type</see> being <seealso cref="LimitType.Open">open</seealso>;</description></item>
        /// <item><description><paramref name="value"/> is greater than or equal to this <see cref="Value">value</see> for this <see cref="Type">type</see> being <seealso cref="LimitType.Closed">closed</seealso>;</description></item>
        /// <item><description>this instance <see cref="IsInfinite">is infinite</see>;</description></item>
        /// </list>
        /// otherwise, <see langword="false"/>.
        /// </returns>
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

        /// <summary>
        /// Gets the value indicating whether this <see cref="LimitValue{T}">limit point</see> contains the <paramref name="value"/> on it's left side
        /// or in other words whether <paramref name="value"/> is part of the set limited by this <see cref="LimitValue{T}">limit point</see> from the right side.
        /// </summary>
        /// <param name="value">Value to compare with this instance.</param>
        /// <returns>
        /// <see langword="true"/> when:
        /// <list type="bullet">
        /// <item><description><paramref name="value"/> is lower than this <see cref="Value">value</see> for this <see cref="Type">type</see> being <seealso cref="LimitType.Open">open</seealso>;</description></item>
        /// <item><description><paramref name="value"/> is lower than or equal to this <see cref="Value">value</see> for this <see cref="Type">type</see> being <seealso cref="LimitType.Closed">closed</seealso>;</description></item>
        /// <item><description>this instance <see cref="IsInfinite">is infinite</see>;</description></item>
        /// </list>
        /// otherwise, <see langword="false"/>.
        /// </returns>
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

        /// <summary>
        /// Converts this <see cref="LimitValue{T}">limit point</see> to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of this <see cref="LimitValue{T}">limit point</see> consisting of:
        /// <list type="bullet">
        /// <item><description>signs representing <see cref="Type">type</see> (<c>&gt;</c>, <c>≥</c>, <c>≤</c>, <c>&lt;</c>);</description></item>
        /// <item><description>string representation of <see cref="Value">value</see> when this instance <see cref="IsFinite">is finite</see> or the <see cref="Infinity">infinity</see> symbol '∞' if it's not.</description></item>
        /// </list>
        /// </returns>
        public override string ToString() =>
            IsInfinite
            ? "∞"
            : string.Format(
                "{0}{1}{2}",
                Type.Match(open: () => '>', closed: () => '≥'),
                Value,
                Type.Match(open: () => '<', closed: () => '≤'));

        /// <summary>
        /// Checks whether this instance of <see cref="LimitValue{T}"/> is equal to the <paramref name="other"/> one.
        /// </summary>
        /// <param name="other">Object to check equality with this instance.</param>
        /// <returns>
        /// <see langword="true"/> if this instance is equal to the <paramref name="other"/> one which means:
        /// <list type="bullet">
        /// <item><description>both instances <see cref="IsInfinite">are infinite</see>;</description></item>
        /// <item><description>both instances <see cref="IsFinite">are finite</see> and their <see cref="Value">values</see> and <see cref="Type">types</see> are equal;</description></item>
        /// </list>
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(LimitValue<T> other) =>
            _isFinite == other._isFinite
            && (!_isFinite && !_isFinite
                || Equals(Value, other.Value)
                && Type == other.Type);

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is LimitValue<T> other
            && Equals(other);

        /// <inheritdoc/>
        public override int GetHashCode() =>
            IsInfinite
            ? "∞".GetHashCode()
            : new HashCode()
                .Append(Value, Type)
                .CurrentHash;

        /// <summary>
        /// Determines whether two specified <see cref="LimitValue{T}">limit points</see> have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="LimitValue{T}">limit point</see> to compare.</param>
        /// <param name="right">The second <see cref="LimitValue{T}">limit point</see> to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(LimitValue<T> left, LimitValue<T> right) =>
            left.Equals(right);

        /// <summary>
        /// Determines whether two specified <see cref="LimitValue{T}">limit points</see> have different values.
        /// </summary>
        /// <param name="left">The first <see cref="LimitValue{T}">limit point</see> to compare.</param>
        /// <param name="right">The second <see cref="LimitValue{T}">limit point</see> to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is different from the value of <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(LimitValue<T> left, LimitValue<T> right) =>
            !left.Equals(right);

        #endregion
    }
}
