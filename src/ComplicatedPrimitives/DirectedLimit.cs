using DotNetExtensions;
using System;

namespace ComplicatedPrimitives
{
    /// <include
    ///   file='ComplicatedPrimitives.xml'
    ///   path='//member[@name="T:ComplicatedPrimitives.DirectedLimit`1"]' />
    public struct DirectedLimit<T> :
        IComparativeSet<DirectedLimit<T>, T>,
        IEquatable<DirectedLimit<T>>,
        IComparable<DirectedLimit<T>>
        where T : IComparable<T>
    {
        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="F:ComplicatedPrimitives.DirectedLimit`1.Undefined"]' />
        public static readonly DirectedLimit<T> Undefined = new DirectedLimit<T>();

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="F:ComplicatedPrimitives.DirectedLimit`1.LeftInfinity"]' />
        public static readonly DirectedLimit<T> LeftInfinity = new DirectedLimit<T>(LimitPoint<T>.Infinity, LimitSide.Left);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="F:ComplicatedPrimitives.DirectedLimit`1.RightInfinity"]' />
        public static readonly DirectedLimit<T> RightInfinity = new DirectedLimit<T>(LimitPoint<T>.Infinity, LimitSide.Right);

        private bool _isDefined;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DirectedLimit`1.#ctor(ComplicatedPrimitives.LimitPoint{`0},ComplicatedPrimitives.LimitSide)"]' />
        public DirectedLimit(LimitPoint<T> point, LimitSide side)
        {
            Point = point;

            if (side != LimitSide.Left && side != LimitSide.Right)
                throw Error.ArgumentIsUndefinedEnum(side, nameof(side));
            Side = side;
            _isDefined = true;
        }

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DirectedLimit`1.#ctor(`0,ComplicatedPrimitives.LimitPointType,ComplicatedPrimitives.LimitSide)"]' />
        public DirectedLimit(T value, LimitPointType type, LimitSide side)
            : this(new LimitPoint<T>(value, type), side) { }

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="P:ComplicatedPrimitives.DirectedLimit`1.Point"]' />
        public LimitPoint<T> Point { get; }

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="P:ComplicatedPrimitives.DirectedLimit`1.Side"]' />
        public LimitSide Side { get; }

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="P:ComplicatedPrimitives.DirectedLimit`1.IsDefined"]' />
        public bool IsDefined => _isDefined;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="P:ComplicatedPrimitives.DirectedLimit`1.IsUndefined"]' />
        public bool IsUndefined => !_isDefined;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="P:ComplicatedPrimitives.DirectedLimit`1.Value"]' />
        public T Value => Point.Value;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="P:ComplicatedPrimitives.DirectedLimit`1.Type"]' />
        public LimitPointType Type => Point.Type;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DirectedLimit`1.Map``1(System.Func{`0,``0})"]' />
        public DirectedLimit<TResult> Map<TResult>(Func<T, TResult> mapper)
            where TResult : IComparable<TResult> =>
            IsDefined
            ? new DirectedLimit<TResult>(
                point: Point.Map(mapper),
                side: Side)
            : DirectedLimit<TResult>.Undefined;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DirectedLimit`1.Translate(System.Func{`0,`0})"]' />
        public DirectedLimit<T> Translate(Func<T, T> translation) =>
            IsUndefined
            ? Undefined
            : new DirectedLimit<T>(
                point: Point.Translate(translation),
                side: Side);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DirectedLimit`1.GetComplement"]' />
        public DirectedLimit<T> GetComplement() =>
            IsDefined
            ? new DirectedLimit<T>(
                point: new LimitPoint<T>(Point.Value, Point.Type.Flip()),
                side: Side.Flip())
            : Undefined;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DirectedLimit`1.Complements(ComplicatedPrimitives.DirectedLimit{`0})"]' />
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

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DirectedLimit`1.Contains(`0)"]' />
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

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DirectedLimit`1.IntersectsWith(ComplicatedPrimitives.DirectedLimit{`0})"]' />
        public bool IntersectsWith(DirectedLimit<T> other)
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

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DirectedLimit`1.IsProperSubsetOf(ComplicatedPrimitives.DirectedLimit{`0})"]' />
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

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DirectedLimit`1.IsSubsetOf(ComplicatedPrimitives.DirectedLimit{`0})"]' />
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

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DirectedLimit`1.IsProperSupersetOf(ComplicatedPrimitives.DirectedLimit{`0})"]' />
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

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DirectedLimit`1.IsSupersetOf(ComplicatedPrimitives.DirectedLimit{`0})"]' />
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

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DirectedLimit`1.ToString"]' />
        public override string ToString()
        {
            T value = Point.Value;
            var type = Point.Type;
            return Side.Match(
                left: () => string.Concat(value, type.Match(open: () => Constants.RightClosedSign, closed: () => Constants.RightClosedSign)),
                right: () => string.Concat(type.Match(open: () => Constants.LeftOpenSign, closed: () => Constants.LeftClosedSign), value),
                undefined: () => Constants.UndefinedString);
        }

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DirectedLimit`1.Equals(ComplicatedPrimitives.DirectedLimit{`0})"]' />
        public bool Equals(DirectedLimit<T> other) =>
            IsUndefined && other.IsUndefined
            || Point == other.Point
                && Side == other.Side;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DirectedLimit`1.DirectedLimit`1.Equals(System.Object)"]' />
        public override bool Equals(object obj) =>
            obj is DirectedLimit<T> other
            && Equals(other);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DirectedLimit`1.GetHashCode"]' />
        public override int GetHashCode() =>
            new HashCode()
            .Append(Point, Side)
            .CurrentHash;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DirectedLimit`1.CompareTo(ComplicatedPrimitives.DirectedLimit{`0})"]' />
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

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DirectedLimit`1.op_Equality(ComplicatedPrimitives.DirectedLimit{`0},ComplicatedPrimitives.DirectedLimit{`0})"]' />
        public static bool operator ==(DirectedLimit<T> left, DirectedLimit<T> right) =>
            left.Equals(right);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DirectedLimit`1.op_Inequality(ComplicatedPrimitives.DirectedLimit{`0},ComplicatedPrimitives.DirectedLimit{`0})"]' />
        public static bool operator !=(DirectedLimit<T> left, DirectedLimit<T> right) =>
            !left.Equals(right);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DirectedLimit`1.op_OnesComplement(ComplicatedPrimitives.DirectedLimit{`0})"]' />
        public static DirectedLimit<T> operator ~(DirectedLimit<T> operand) =>
            operand.GetComplement();

        #endregion
    }
}
