using DotNetExtensions;
using System;

namespace ComplicatedPrimitives
{
    /// <summary>
    /// Structure representing limit point of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of limit point's value (limit's domain).</typeparam>
    public struct LimitPoint<T> : IEquatable<LimitPoint<T>>
        where T : IComparable<T>
    {
        /// <summary>
        /// Represents an infinite limit point. This field is read-only.
        /// </summary>
        public static readonly LimitPoint<T> Infinity = new LimitPoint<T>();

        private readonly bool _isFinite;
        private readonly LimitPointType _type;

        /// <summary>
        /// Creates a new <see cref="IsFinite">finite</see> instance of the <see cref="LimitPoint{T}"/> structure with a specified <paramref name="value"/> and <paramref name="type"/>.
        /// </summary>
        /// <param name="value">Limit point's value.</param>
        /// <param name="type">Limit point's type.</param>
        /// <exception cref="ArgumentException"><paramref name="type"/> is not a defined value of <see cref="LimitPointType"/> enum</exception>
        public LimitPoint(T value, LimitPointType type = default(LimitPointType))
        {
            Value = value;

            if (type != LimitPointType.Open && type != LimitPointType.Closed)
                throw Error.ArgumentIsUndefinedEnum(type, nameof(type));
            _type = type;
            _isFinite = true;
        }

        /// <summary>
        /// Gets the value of this <see cref="LimitPoint{T}">limit point</see>.
        /// </summary>
        /// <remarks>
        /// For <see cref="IsInfinite">infinite</see> instances of <see cref="LimitPoint{T}">limit point</see>, the value is always considered to be default of <typeparamref name="T"/>.
        /// </remarks>
        public T Value { get; }

        /// <summary>
        /// Gets the value indicating whether this <see cref="LimitPoint{T}">limit point</see> is either <see cref="LimitPointType.Open">open</see> or <see cref="LimitPointType.Closed">closed</see>,
        /// which means - in other words - whether this <see cref="LimitPoint{T}">limit point</see> covers it's <see cref="Value">value</see> or considers it unreachable.
        /// </summary>
        /// <remarks>
        /// For <see cref="IsInfinite">infinite</see> instances of <see cref="LimitPoint{T}">limit point</see>, the type is always considered to be <see cref="LimitPointType.Open">open</see>.
        /// </remarks>
        public LimitPointType Type =>
            _isFinite
            ? _type
            : LimitPointType.Open;

        /// <summary>
        /// Indicates whether this instance represents a finite <see cref="LimitPoint{T}">limit point</see> with certain <see cref="Value">value</see>.
        /// </summary>
        public bool IsFinite => _isFinite;

        /// <summary>
        /// Indicates whether this instance represents an infinite <see cref="LimitPoint{T}">limit point</see>.
        /// </summary>
        public bool IsInfinite => !_isFinite;

        /// <summary>
        /// Maps this instance to <see cref="LimitPoint{TResult}">limit point</see> of <typeparamref name="TResult"/> using given <paramref name="mapper"/>.
        /// </summary>
        /// <typeparam name="TResult">Target type to map <see cref="Value">value</see> to.</typeparam>
        /// <param name="mapper">Function that maps value of type <typeparamref name="T"/> to type <typeparamref name="TResult"/>.</param>
        /// <returns>
        /// When this instance <see cref="IsFinite">is finite</see>, new <see cref="LimitPoint{TResult}">limit point</see> of <typeparamref name="TResult"/>
        /// with then same <see cref="Type">type</see>, but with <see cref="Value">value</see> being mapped using given <paramref name="mapper"/>;
        /// otherwise, <see cref="LimitPoint{TResult}.Infinity">infinite limit point</see> of <typeparamref name="TResult"/>.
        /// </returns>
        public LimitPoint<TResult> Map<TResult>(Func<T, TResult> mapper)
            where TResult : IComparable<TResult> =>
            _isFinite
            ? new LimitPoint<TResult>(mapper(Value), Type)
            : LimitPoint<TResult>.Infinity;

        /// <summary>
        /// Translates (moves) this <see cref="LimitPoint{T}">limit point</see> using given <paramref name="translation"/>.
        /// </summary>
        /// <param name="translation">Function that translates limit point's value.</param>
        /// <returns>
        /// When this instance <see cref="IsFinite">is finite</see>, new <see cref="LimitPoint{TResult}">limit point</see> with the same <see cref="Type">type</see>,
        /// but with <see cref="Value">value</see> being translated using given <paramref name="translation"/>;
        /// otherwise, <see cref="Infinity">infinity</see>.
        /// </returns>
        public LimitPoint<T> Translate(Func<T, T> translation) =>
            IsInfinite
            ? Infinity
            : new LimitPoint<T>(
                value: translation(Value),
                type: Type);

        /// <summary>
        /// Converts this <see cref="LimitPoint{T}">limit point</see> to <see cref="LimitPointType.Open">open</see>.
        /// </summary>
        /// <returns>
        /// When this instance <see cref="IsFinite">is finite</see>, new <see cref="LimitPoint{T}">limit point</see> with the same <see cref="Value">value</see>,
        /// but with <see cref="Type">type</see> equal to <seealso cref="LimitPointType.Open">open</seealso>;
        /// otherwise, <see cref="Infinity">infinity</see>.
        /// </returns>
        public LimitPoint<T> AsOpen() =>
            IsInfinite
            ? Infinity
            : new LimitPoint<T>(Value, LimitPointType.Open);

        /// <summary>
        /// Converts this <see cref="LimitPoint{T}">limit point</see> to <see cref="LimitPointType.Closed">closed</see>.
        /// </summary>
        /// <returns>
        /// When this instance <see cref="IsFinite">is finite</see>, new <see cref="LimitPoint{T}">limit point</see> with the same <see cref="Value">value</see>,
        /// but with <see cref="Type">type</see> equal to <seealso cref="LimitPointType.Closed">open</seealso>;
        /// otherwise, <see cref="Infinity">infinity</see>.
        /// </returns>
        public LimitPoint<T> AsClosed() =>
            IsInfinite
            ? Infinity
            : new LimitPoint<T>(Value, LimitPointType.Closed);

        /// <summary>
        /// Gets the value indicating whether this <see cref="LimitPoint{T}">limit point</see> contains the <paramref name="value"/> on it's right side
        /// or in other words whether <paramref name="value"/> is part of the set limited by this <see cref="LimitPoint{T}">limit point</see> from the left side.
        /// </summary>
        /// <param name="value">Value to compare with this instance.</param>
        /// <returns>
        /// <see langword="true"/> when:
        /// <list type="bullet">
        /// <item><description><paramref name="value"/> is greater than this <see cref="Value">value</see> for this <see cref="Type">type</see> being <seealso cref="LimitPointType.Open">open</seealso>;</description></item>
        /// <item><description><paramref name="value"/> is greater than or equal to this <see cref="Value">value</see> for this <see cref="Type">type</see> being <seealso cref="LimitPointType.Closed">closed</seealso>;</description></item>
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
                case LimitPointType.Open:
                    return Value.CompareTo(value) < 0;
                case LimitPointType.Closed:
                    return Value.CompareTo(value) <= 0;
                default:
                    throw new InvalidOperationException($"Handling {Type.GetType().Name}.{Type} was not implemented.");
            }
        }

        /// <summary>
        /// Gets the value indicating whether this <see cref="LimitPoint{T}">limit point</see> contains the <paramref name="value"/> on it's left side
        /// or in other words whether <paramref name="value"/> is part of the set limited by this <see cref="LimitPoint{T}">limit point</see> from the right side.
        /// </summary>
        /// <param name="value">Value to compare with this instance.</param>
        /// <returns>
        /// <see langword="true"/> when:
        /// <list type="bullet">
        /// <item><description><paramref name="value"/> is lower than this <see cref="Value">value</see> for this <see cref="Type">type</see> being <seealso cref="LimitPointType.Open">open</seealso>;</description></item>
        /// <item><description><paramref name="value"/> is lower than or equal to this <see cref="Value">value</see> for this <see cref="Type">type</see> being <seealso cref="LimitPointType.Closed">closed</seealso>;</description></item>
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
                case LimitPointType.Open:
                    return Value.CompareTo(value) > 0;
                case LimitPointType.Closed:
                    return Value.CompareTo(value) >= 0;
                default:
                    throw new InvalidOperationException($"Handling {Type.GetType().Name}.{Type} was not implemented.");
            }
        }

        #region Boiler-plate code

        /// <summary>
        /// Converts this <see cref="DirectedLimit{T}">directed limit</see> to its equivalent string representation following format:
        /// <code>{ left-type }{ value }{ right-type }</code>
        /// </summary>
        /// <returns>When this instance <see cref="IsFinite">is finite</see>, the string representation of this directed limit consisting of:
        /// <list type="bullet">
        /// <item>
        /// <term>left-type</term>
        /// <description>
        /// sign representing <see cref="Type">type</see>: '<c>≥</c>' for <see cref="LimitPointType.Closed">closed</see>, '<c>&gt;</c>' for <see cref="LimitPointType.Open">open</see>,
        /// </description>
        /// </item>
        /// <item><description>string representation of <see cref="Value">value</see>.</description></item>
        /// </list>
        /// When this instance <see cref="IsInfinite">is infinite</see>, the infinity sign '∞' is returned.
        /// </returns>
        public override string ToString() =>
            IsInfinite
            ? Constants.InfinityString
            : string.Format(
                "{0}{1}{2}",
                Type.Match(open: () => Constants.LeftOpenSign, closed: () => Constants.LeftClosedSign),
                Value,
                Type.Match(open: () => Constants.RightOpenSign, closed: () => Constants.RightClosedSign));

        /// <summary>
        /// Checks whether this instance of <see cref="LimitPoint{T}"/> is equal to the <paramref name="other"/> one.
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
        public bool Equals(LimitPoint<T> other) =>
            _isFinite == other._isFinite
            && (!_isFinite && !_isFinite
                || Equals(Value, other.Value)
                && Type == other.Type);

        /// <summary>
        /// Determines whether this instance of <see cref="LimitPoint{T}"/> and the <paramref name="obj"/> are equal.
        /// </summary>
        /// <param name="obj">Object to compare to this instance.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="obj"/> is instance of <see cref="LimitPoint{T}"/> and equal to this instance;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) =>
            obj is LimitPoint<T> other
            && Equals(other);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode() =>
            IsInfinite
            ? "∞".GetHashCode()
            : new HashCode()
                .Append(Value, Type)
                .CurrentHash;

        /// <summary>
        /// Determines whether two specified <see cref="LimitPoint{T}">limit points</see> have the same value.
        /// </summary>
        /// <param name="left">The first limit point to compare.</param>
        /// <param name="right">The second limit point to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(LimitPoint<T> left, LimitPoint<T> right) =>
            left.Equals(right);

        /// <summary>
        /// Determines whether two specified <see cref="LimitPoint{T}">limit points</see> have different values.
        /// </summary>
        /// <param name="left">The first limit point to compare.</param>
        /// <param name="right">The second limit point to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is different from the value of <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(LimitPoint<T> left, LimitPoint<T> right) =>
            !left.Equals(right);

        #endregion
    }
}
