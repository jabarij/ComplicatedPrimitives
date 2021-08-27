using DotNetExtensions;
using System;

namespace ComplicatedPrimitives
{
    /// <summary>
    /// Structure representing directed limit of type <typeparamref name="T"/>.
    /// In other words, it is limit point of type <typeparamref name="T"/> that is approachable from either left or right side.
    /// </summary>
    /// <typeparam name="T">Type of limit point's value (limit's domain).</typeparam>
    public struct DirectedLimit<T> : IEquatable<DirectedLimit<T>>, IComparable<DirectedLimit<T>>
        where T : IComparable<T>
    {
        /// <summary>
        /// Represents the <see cref="LimitPointType.Open">open</see> <see cref="Type">type</see> of <see cref="IsDefined">defined</see> <see cref="DirectedLimit{T}">directed limit</see>.
        /// </summary>
        public const char OpenSign = '<';

        /// <summary>
        /// Represents the <see cref="LimitPointType.Closed">closed</see> <see cref="Type">type</see> of <see cref="IsDefined">defined</see> <see cref="DirectedLimit{T}">directed limit</see>.
        /// </summary>
        public const char ClosedSign = '≤';

        /// <summary>
        /// Represents the string equivalent of <see cref="IsUndefined">undefined</see> <see cref="DirectedLimit{T}">directed limit</see>.
        /// </summary>
        public const string UndefinedString = "undefined";

        /// <summary>
        /// Represents an undefined directed limit. This field is read-only.
        /// </summary>
        public static readonly DirectedLimit<T> Undefined = new DirectedLimit<T>();

        /// <summary>
        /// Represents a left (negative in some contexts) infinity. This field is read-only.
        /// </summary>
        public static readonly DirectedLimit<T> LeftInfinity = new DirectedLimit<T>(LimitPoint<T>.Infinity, LimitSide.Left);

        /// <summary>
        /// Represents a right (positive in some contexts) infinity. This field is read-only.
        /// </summary>
        public static readonly DirectedLimit<T> RightInfinity = new DirectedLimit<T>(LimitPoint<T>.Infinity, LimitSide.Right);

        private bool _isDefined;

        /// <summary>
        /// Creates a new instance of the <see cref="DirectedLimit{T}"/> structure with a specified <paramref name="point"/> and <paramref name="side"/>.
        /// </summary>
        /// <param name="point">Extreme point of the created limit.</param>
        /// <param name="side">Side of the created limit.</param>
        /// <exception cref="ArgumentException"><paramref name="side"/> is not a defined value of <see cref="LimitSide"/> enum</exception>
        public DirectedLimit(LimitPoint<T> point, LimitSide side)
        {
            Point = point;

            if (side != LimitSide.Left && side != LimitSide.Right)
                Throw.ArgumentIsUndefinedEnum(side, nameof(side));
            Side = side;
            _isDefined = true;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DirectedLimit{T}"/> structure with a specified <paramref name="value"/>, <paramref name="type"/> and <paramref name="side"/>.
        /// </summary>
        /// <param name="value">Extreme point's value of the created limit.</param>
        /// <param name="type">Extreme point's type of the created limit.</param>
        /// <param name="side">Side (direction) of the created limit.</param>
        /// <exception cref="ArgumentException"><paramref name="side"/> is not a defined value of <see cref="LimitSide"/> enum</exception>
        public DirectedLimit(T value, LimitPointType type, LimitSide side)
            : this(new LimitPoint<T>(value, type), side) { }

        /// <summary>
        /// Gets the extreme point of this <see cref="DirectedLimit{T}">directed limit</see>.
        /// </summary>
        public LimitPoint<T> Point { get; }

        /// <summary>
        /// Gets the side of this <see cref="DirectedLimit{T}">directed limit</see>.
        /// </summary>
        public LimitSide Side { get; }

        /// <summary>
        /// Indicates whether this instance represents an undefined <see cref="DirectedLimit{T}">directed limit</see> with a certain <see cref="Side">side</see>.
        /// </summary>
        public bool IsDefined => _isDefined;

        /// <summary>
        /// Indicates whether this instance represents an undefined <see cref="DirectedLimit{T}">directed limit</see> without valid <see cref="Side">side</see>.
        /// </summary>
        public bool IsUndefined => !_isDefined;

        /// <summary>
        /// Gets the value of <see cref="Point">extreme point</see> of this <see cref="DirectedLimit{T}">directed limit</see>.
        /// </summary>
        /// <remarks>
        /// For more defails, see <seealso cref="LimitPoint{T}.Value"/> docs.
        /// </remarks>
        public T Value => Point.Value;

        /// <summary>
        /// Gets the type of <see cref="Point">extreme point</see> of this <see cref="DirectedLimit{T}">directed limit</see>.
        /// </summary>
        /// <remarks>
        /// For more defails, see <seealso cref="LimitPoint{T}.Type"/> docs.
        /// </remarks>
        public LimitPointType Type => Point.Type;

        /// <summary>
        /// Maps this instance to <see cref="DirectedLimit{TResult}">directed limit</see> of <typeparamref name="TResult"/> using given <paramref name="mapper"/>.
        /// </summary>
        /// <typeparam name="TResult">Target type to map <see cref="Point">extrem point</see> to.</typeparam>
        /// <param name="mapper">Function that maps extrem point of type <typeparamref name="T"/> to type <typeparamref name="TResult"/>.</param>
        /// <returns>
        /// When this instance <see cref="IsDefined">is defined</see>, new <see cref="DirectedLimit{TResult}">directed limit</see> of <typeparamref name="TResult"/>
        /// with then same <see cref="Side">side</see>, but with <see cref="Point">extreme point</see> being mapped using given <paramref name="mapper"/>;
        /// otherwise, <see cref="DirectedLimit{TResult}.Undefined">undefined directed limit</see> of <typeparamref name="TResult"/>.
        /// </returns>
        /// <remarks>
        /// For more defails about mapping <see cref="Point">extreme point</see>, see <seealso cref="LimitPoint{T}.Map{TResult}(Func{T, TResult})"/> docs.
        /// </remarks>
        public DirectedLimit<TResult> Map<TResult>(Func<T, TResult> mapper)
            where TResult : IComparable<TResult> =>
            IsDefined
            ? new DirectedLimit<TResult>(
                point: Point.Map(mapper),
                side: Side)
            : DirectedLimit<TResult>.Undefined;

        /// <summary>
        /// Translates (moves) this <see cref="DirectedLimit{T}">directed limit</see> using given <paramref name="translation"/>.
        /// </summary>
        /// <param name="translation">Function that translates directed limit's point.</param>
        /// <returns>
        /// When this instance <see cref="IsDefined">is defined</see>, new <see cref="DirectedLimit{TResult}">directed limit</see> with the same <see cref="Side">side</see>,
        /// but with <see cref="Point">point</see> being translated using given <paramref name="translation"/>;
        /// otherwise, <see cref="Undefined">undefined</see>.
        /// </returns>
        public DirectedLimit<T> Translate(Func<T, T> translation) =>
            IsUndefined
            ? Undefined
            : new DirectedLimit<T>(
                point: Point.Translate(translation),
                side: Side);

        /// <summary>
        /// Gets <see cref="DirectedLimit{T}">directed limit</see> being complement of this instance.
        /// </summary>
        /// <returns>
        /// When this instance <see cref="IsDefined">is defined</see>, new <see cref="DirectedLimit{TResult}">directed limit</see> with the same <see cref="Value">value</see>,
        /// but with flipped <see cref="Type">type</see> and <see cref="Side">side</see>;
        /// otherwise, <see cref="Undefined">undefined</see>.
        /// </returns>
        /// <remarks>
        /// <para>For more defails about flipping <see cref="LimitPointType"/>, see <seealso cref="LimitPointTypeExtensions.Flip(LimitPointType)">this</seealso>.</para>
        /// <para>For more defails about flipping <see cref="LimitSide"/>, see <seealso cref="LimitSideExtensions.Flip(LimitSide)">this</seealso>.</para>
        /// </remarks>
        public DirectedLimit<T> GetComplement() =>
            IsDefined
            ? new DirectedLimit<T>(
                point: new LimitPoint<T>(Point.Value, Point.Type.Flip()),
                side: Side.Flip())
            : Undefined;

        /// <summary>
        /// Gets the value indicating whether this <see cref="DirectedLimit{T}">directed limit</see> is exact complement of the <paramref name="other"/>.
        /// In other words, this function checks complementary relation between two directed limits. This relation is reversible.
        /// </summary>
        /// <param name="other">Directed limit to test complementary relation with.</param>
        /// <returns>
        /// <see langword="true"/> if this instance is complement of the <paramref name="other"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Complements(DirectedLimit<T> other)
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

        /// <summary>
        /// Gets the value indicating whether <paramref name="value"/> belongs to set defined by this <see cref="DirectedLimit{T}">directed limit</see>.
        /// </summary>
        /// <param name="value">Value of type <typeparamref name="T"/> to compare with this instance.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> belongs to set defined by this directed limit;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool Contains(T value)
        {
            switch (Side)
            {
                case LimitSide.Left:
                    return Point.RightContains(value);
                case LimitSide.Right:
                    return Point.LeftContains(value);
                case 0:
                    return false;
                default:
                    throw new InvalidOperationException($"Handling {Side.GetType().Name}.{Side} was not implemented.");
            }
        }

        /// <summary>
        /// Gets the value indicating whether this <see cref="DirectedLimit{T}">directed limit</see> and <paramref name="other"/> have non-empty intersection.
        /// In other words, this function checks if there exists any non-empty set of <typeparamref name="T"/> that belongs to both this and <paramref name="other"/> directed limit.
        /// </summary>
        /// <param name="other">Directed limit to test intersecting relation with.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="other"/> has non-empty intersection with this directed limit.
        /// otherwise, <see langword="false"/>.
        /// </returns>
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

        /// <summary>
        /// Gets the value indicating whether this <see cref="DirectedLimit{T}">directed limit</see> is proper subset of the <paramref name="other"/> one.
        /// In other words, this function checks if this directed limit <see cref="IsSubsetOf(DirectedLimit{T})">is subset</see> of the <paramref name="other"/>
        /// directed limit but <see cref="Equals(DirectedLimit{T})">is not equal</see> to it.
        /// </summary>
        /// <param name="other">Directed limit to test subset relation with.</param>
        /// <returns>
        /// <see langword="true"/> if this instance is proper subset of the <paramref name="other"/> one;
        /// otherwise, <see langword="false"/>.
        /// If either this or the <paramref name="other"/> directed limit <see cref="IsUndefined">is undefined</see>, the result is <see langword="false"/>.
        /// </returns>
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

        /// <summary>
        /// Gets the value indicating whether this <see cref="DirectedLimit{T}">directed limit</see> is subset of the <paramref name="other"/> one.
        /// In other words, this function checks if <paramref name="other"/> directed limit contains every <typeparamref name="T"/> from set defined by this directed limit.
        /// </summary>
        /// <param name="other">Directed limit to test subset relation with.</param>
        /// <returns>
        /// <see langword="true"/> if this instance is subset of the <paramref name="other"/> one;
        /// otherwise, <see langword="false"/>.
        /// If either this or the <paramref name="other"/> directed limit <see cref="IsUndefined">is undefined</see>, the result is <see langword="false"/>.
        /// </returns>
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

        /// <summary>
        /// Gets the value indicating whether this <see cref="DirectedLimit{T}">directed limit</see> is proper superset of the <paramref name="other"/> one.
        /// In other words, this function checks if this directed limit <see cref="IsSupersetOf(DirectedLimit{T})">is superset</see> of the <paramref name="other"/>
        /// directed limit but <see cref="Equals(DirectedLimit{T})">is not equal</see> to it.
        /// </summary>
        /// <param name="other">Directed limit to test superset relation with.</param>
        /// <returns>
        /// <see langword="true"/> if this instance is proper superset of the <paramref name="other"/> one;
        /// otherwise, <see langword="false"/>.
        /// If either this or the <paramref name="other"/> directed limit <see cref="IsUndefined">is undefined</see>, the result is <see langword="false"/>.
        /// </returns>
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

        /// <summary>
        /// Gets the value indicating whether this <see cref="DirectedLimit{T}">directed limit</see> is superset of the <paramref name="other"/> one.
        /// In other words, this function checks if this directed limit contains every <typeparamref name="T"/> from set defined by <paramref name="other"/> directed limit.
        /// </summary>
        /// <param name="other">Directed limit to test superset relation with.</param>
        /// <returns>
        /// <see langword="true"/> if this instance is superset of the <paramref name="other"/> one;
        /// otherwise, <see langword="false"/>.
        /// If either this or the <paramref name="other"/> directed limit <see cref="IsUndefined">is undefined</see>, the result is <see langword="false"/>.
        /// </returns>
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

        /// <summary>
        /// Converts this <see cref="DirectedLimit{T}">directed limit</see> to its equivalent string representation following format:
        /// <list type="bullet">
        /// <item><description><c>{value}{typeSign}</c>, when <see cref="Side">side</see> is <see cref="LimitSide.Left">left</see>;</description></item>
        /// <item><description><c>{typeSign}{value}</c>, when <see cref="Side">side</see> is <see cref="LimitSide.Right">right</see>;</description></item>
        /// </list>
        /// </summary>
        /// <returns>When this instance <see cref="IsDefined">is defined</see>, the string representation of this directed limit consisting of:
        /// <list type="bullet">
        /// <item><description>signs representing <see cref="Type">type</see> (<see cref="ClosedSign"><c>≤</c></see>, <see cref="OpenSign"><c>&lt;</c></see>);</description></item>
        /// <item><description>string representation of <see cref="Value">value</see>.</description></item>
        /// </list>
        /// When this instance <see cref="IsUndefined">is undefined</see>, the <see cref="UndefinedString"/> is returned.
        /// </returns>
        public override string ToString()
        {
            T value = Point.Value;
            var type = Point.Type;
            return Side.Match(
                left: () => string.Concat(value, type.Match(open: () => OpenSign, closed: () => ClosedSign)),
                right: () => string.Concat(type.Match(open: () => OpenSign, closed: () => ClosedSign), value),
                undefined: () => UndefinedString);
        }

        /// <summary>
        /// Checks whether this instance of <see cref="DirectedLimit{T}"/> is equal to the <paramref name="other"/> one.
        /// </summary>
        /// <param name="other">Object to check equality with this instance.</param>
        /// <returns>
        /// <see langword="true"/> if this instance is equal to the <paramref name="other"/> one which means:
        /// <list type="bullet">
        /// <item><description>both instances <see cref="IsUndefined">are undefined</see>;</description></item>
        /// <item><description>both instances <see cref="IsDefined">are defined</see> and their <see cref="Point">points</see> and <see cref="Side">sides</see> are equal;</description></item>
        /// </list>
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(DirectedLimit<T> other) =>
            IsUndefined && other.IsUndefined
            || Point == other.Point
                && Side == other.Side;

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is DirectedLimit<T> other
            && Equals(other);

        /// <inheritdoc/>
        public override int GetHashCode() =>
            new HashCode()
            .Append(Point, Side)
            .CurrentHash;

        /// <summary>
        /// Compares this <see cref="DirectedLimit{T}">directed limit</see> to the <paramref name="other"/> one and returns an integer number that indicates
        /// whether this directed limit precedes, follows or occurs in the same position in the sort order as the other object
        /// </summary>
        /// <param name="other">Object to check compare this instance to.</param>
        /// <returns>
        /// Integer number as below:
        /// <list type="bullet">
        /// <item><term>less than zero</term><description>this instance precedes <paramref name="other"/> in the sort order;</description></item>
        /// <item><term>zero</term><description>this instance occurs in the same position in the sort order as <paramref name="other"/>;</description></item>
        /// <item><term>greater than zero</term><description>this instance follows <paramref name="other"/> in the sort order;</description></item>
        /// </list>
        /// </returns>
        public int CompareTo(DirectedLimit<T> other)
        {
            bool isLeftUndefined = IsUndefined;
            bool isRightUndefined = other.IsUndefined;
            if (isLeftUndefined && isRightUndefined)
                return 0;
            else if (isLeftUndefined ^ isRightUndefined)
                return isLeftUndefined ? -1 : 1;

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

        /// <summary>
        /// Determines whether two specified <see cref="DirectedLimit{T}">directed limit</see> have the same value.
        /// </summary>
        /// <param name="left">The first directed limit to compare.</param>
        /// <param name="right">The second directed limit to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(DirectedLimit<T> left, DirectedLimit<T> right) =>
            left.Equals(right);

        /// <summary>
        /// Determines whether two specified <see cref="DirectedLimit{T}">directed limit</see> have different values.
        /// </summary>
        /// <param name="left">The first directed limit to compare.</param>
        /// <param name="right">The second directed limit to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is different from the value of <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(DirectedLimit<T> left, DirectedLimit<T> right) =>
            !left.Equals(right);

        /// <summary>
        /// Produces a complementary <see cref="DirectedLimit{T}">directed limit</see> of the given <paramref name="operand"/>.
        /// </summary>
        /// <param name="operand">Directed limit to produce complement of.</param>
        /// <returns>The result of <see cref="GetComplement"/> method called on <paramref name="operand"/>.</returns>
        public static DirectedLimit<T> operator ~(DirectedLimit<T> operand) =>
            operand.GetComplement();

        #endregion
    }
}
