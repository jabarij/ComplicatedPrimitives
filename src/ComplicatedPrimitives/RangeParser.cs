using System;

namespace ComplicatedPrimitives
{
    /// <summary>
    /// Provides default implementation of <see cref="IRangeParser{T}"/> that is compatible with default <see cref="Range{T}.ToString"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RangeParser<T> : IRangeParser<T>
          where T : IComparable<T>
    {
        private const string Separator = Range<T>.ValueSeparatorString;

        private readonly IParser<T> _valueParser;

        /// <summary>
        /// Creates a new instance of the <see cref="RangeParser{T}"/> with a specified <paramref name="valueParser"/>.
        /// </summary>
        /// <param name="valueParser">Object parsing range's <typeparamref name="T"/> values.</param>
        public RangeParser(IParser<T> valueParser)
        {
            _valueParser = valueParser
                ?? throw Error.ArgumentIsNull(nameof(valueParser));
        }

        /// <summary>
        /// Converts the specified string to an instance of <see cref="Range{T}"/>.
        /// </summary>
        /// <param name="str">
        /// A string that contains a string representation of <see cref="Range{T}"/> as follows:
        /// <code>{ left_limit_type }{ left_limit_value }{ separator }{ right_limit_value }{ right_limit_type }</code>
        /// Where:
        /// <list type="bullet">
        /// <item>
        /// <term><c>left_limit_type</c></term>
        /// <description>string that represents left limit type, either <see cref="Range{T}.LeftOpenLimitTypeString">open</see> or <see cref="Range{T}.LeftClosedLimitTypeString">closed</see>;</description>
        /// </item>
        /// <item>
        /// <term><c>left_limit_value</c></term>
        /// <description>string that represents left limit value parsed using parser object specified when creating this instance of <see cref="RangeParser{T}"/>;</description>
        /// </item>
        /// <item>
        /// <term><c>separator</c></term>
        /// <description>string that represents range value <see cref="Range{T}.ValueSeparatorString">separator</see>;</description>
        /// </item>
        /// <item>
        /// <term><c>right_limit_value</c></term>
        /// <description>string that represents right limit value parsed using parser object specified when creating this instance of <see cref="RangeParser{T}"/>;</description>
        /// </item>
        /// <item>
        /// <term><c>right_limit_type</c></term>
        /// <description>string that represents right limit type, either <see cref="Range{T}.RightOpenLimitTypeString">open</see> or <see cref="Range{T}.RightClosedLimitTypeString">closed</see>;</description>
        /// </item>
        /// </list>
        /// </param>
        /// <returns>
        /// An instance of <see cref="Range{T}"/> that is equivalent to the given <paramref name="str"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="str"/> is either null or empty string</exception>
        /// <exception cref="ParsingException"><paramref name="str"/> is not a valid representation of a <see cref="Range{T}"/></exception>
        public Range<T> Parse(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw Error.ArgumentIsNullOrEmptyString(nameof(str));

            if (string.Equals(str, Range<T>.EmptyRangeString, StringComparison.Ordinal))
                return Range<T>.Empty;

            if (string.Equals(str, Range<T>.InfiniteRangeString, StringComparison.Ordinal))
                return Range<T>.Infinite;

            if (str.Length < 3)
                throw Error.ParsingFormatIsIncorrect(str, "Too short value.");

            if (!TryParseLimit(str[0], out var leftLimit)
                || leftLimit.side != LimitSide.Left)
                throw Error.ParsingFormatIsIncorrect(str[0].ToString(), "Unrecognized left limit descriptor.");

            if (!TryParseLimit(str[str.Length - 1], out var rightLimit)
                || rightLimit.side != LimitSide.Right)
                throw Error.ParsingFormatIsIncorrect(str[str.Length - 1].ToString(), "Unrecognized right limit descriptor.");

            int separatorIndex = str.IndexOf(Separator, 1);
            if (separatorIndex == -1)
                throw Error.ParsingFormatIsIncorrect(str, $"Separator '{Separator}' not found.");

            string leftValueStr = str.Substring(1, separatorIndex - 1);
            var leftValue = _valueParser.Parse(leftValueStr);

            string rightValueStr = str.Substring(separatorIndex + 1, str.Length - separatorIndex - 2);
            var rightValue = _valueParser.Parse(rightValueStr);

            return new Range<T>(
                left: new LimitPoint<T>(leftValue, type: leftLimit.type),
                right: new LimitPoint<T>(rightValue, type: rightLimit.type));
        }

        /// <summary>
        /// Tries to convert the specified string to an instance of <see cref="Range{T}"/> and returns a value indicating whether the conversion succeeded.
        /// </summary>
        /// <param name="str">
        /// A string that contains a string representation of <see cref="Range{T}"/> as follows:
        /// <code>{ left_limit_type }{ left_limit_value }{ separator }{ right_limit_value }{ right_limit_type }</code>
        /// Where:
        /// <list type="bullet">
        /// <item>
        /// <term><c>left_limit_type</c></term>
        /// <description>string that represents left limit type, either <see cref="Range{T}.LeftOpenLimitTypeString">open</see> or <see cref="Range{T}.LeftClosedLimitTypeString">closed</see>;</description>
        /// </item>
        /// <item>
        /// <term><c>left_limit_value</c></term>
        /// <description>string that represents left limit value parsed using parser object specified when creating this instance of <see cref="RangeParser{T}"/>;</description>
        /// </item>
        /// <item>
        /// <term><c>separator</c></term>
        /// <description>string that represents range value <see cref="Range{T}.ValueSeparatorString">separator</see>;</description>
        /// </item>
        /// <item>
        /// <term><c>right_limit_value</c></term>
        /// <description>string that represents right limit value parsed using parser object specified when creating this instance of <see cref="RangeParser{T}"/>;</description>
        /// </item>
        /// <item>
        /// <term><c>right_limit_type</c></term>
        /// <description>string that represents right limit type, either <see cref="Range{T}.RightOpenLimitTypeString">open</see> or <see cref="Range{T}.RightClosedLimitTypeString">closed</see>;</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="range">
        /// When this method returns, contains the <see cref="Range{T}"/> value equivalent to the <paramref name="str"/> if the conversion succeeded,
        /// or <c>default</c> of <see cref="Range{T}"/> if the conversion failed.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="str"/> was converted successfully; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryParse(string str, out Range<T> range)
        {
            if (string.IsNullOrEmpty(str))
            {
                range = default(Range<T>);
                return false;
            }

            if (string.Equals(str, Range<T>.EmptyRangeString, StringComparison.Ordinal))
            {
                range = Range<T>.Empty;
                return true;
            }

            if (string.Equals(str, Range<T>.InfiniteRangeString, StringComparison.Ordinal))
            {
                range = Range<T>.Infinite;
                return true;
            }

            if (str.Length < 3)
            {
                range = default(Range<T>);
                return false;
            }

            if (!TryParseLimit(str[0], out var leftLimit)
                || leftLimit.side != LimitSide.Left)
            {
                range = default(Range<T>);
                return false;
            }

            if (!TryParseLimit(str[str.Length - 1], out var rightLimit)
                || rightLimit.side != LimitSide.Right)
            {
                range = default(Range<T>);
                return false;
            }

            int separatorIndex = str.IndexOf(Separator, 1);
            if (separatorIndex == -1)
            {
                range = default(Range<T>);
                return false;
            }

            string leftValueStr = str.Substring(1, separatorIndex - 1);
            if (!_valueParser.TryParse(leftValueStr, out var leftValue))
            {
                range = default(Range<T>);
                return false;
            }

            string rightValueStr = str.Substring(separatorIndex + 1, str.Length - separatorIndex - 1);
            if (!_valueParser.TryParse(rightValueStr, out var rightValue))
            {
                range = default(Range<T>);
                return false;
            }

            range = new Range<T>(
                left: new LimitPoint<T>(leftValue, type: leftLimit.type),
                right: new LimitPoint<T>(rightValue, type: rightLimit.type));
            return true;
        }

        private static bool TryParseLimit(char limitChar, out (LimitPointType type, LimitSide side) result)
        {
            switch (limitChar)
            {
                case '(':
                    result = (LimitPointType.Open, LimitSide.Left);
                    return true;
                case '[':
                    result = (LimitPointType.Closed, LimitSide.Left);
                    return true;
                case ']':
                    result = (LimitPointType.Closed, LimitSide.Right);
                    return true;
                case ')':
                    result = (LimitPointType.Open, LimitSide.Right);
                    return true;
                default:
                    result = default((LimitPointType, LimitSide));
                    return false;
            }
        }
    }
}
