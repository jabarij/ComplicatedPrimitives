using System;

namespace ComplicatedPrimitives
{
    public class RangeParser<T> : IRangeParser<T>
          where T : IComparable<T>
    {
        public const char Separator = ';';
        private readonly IParser<T> _parseValue;

        public RangeParser(IParser<T> valueParser)
        {
            _parseValue = valueParser ?? throw new ArgumentNullException(nameof(valueParser));
        }

        public Range<T> Parse(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentNullException(nameof(str));
            if (str.Length < 3)
                throw new ParsingException(str, "Too short value.");

            if (!TryParseLimit(str[0], out var leftLimit)
                || leftLimit.side != LimitSide.Left)
                throw new ParsingException(str[0].ToString(), "Unrecognized left limit descriptor.");

            if (!TryParseLimit(str[str.Length - 1], out var rightLimit)
                || rightLimit.side != LimitSide.Right)
                throw new ParsingException(str[str.Length - 1].ToString(), "Unrecognized right limit descriptor.");

            int separatorIndex = str.IndexOf(Separator, 1);
            if (separatorIndex == -1)
                throw new ParsingException(str, $"Separator '{Separator}' not found.");

            string[] values = str
                .Substring(1, str.Length - 2)
                .Split(new[] { Separator }, 2);
            var leftValue = _parseValue.Parse(values[0]);
            var rightValue = _parseValue.Parse(values[1]);
            return new Range<T>(
                left: new LimitPoint<T>(leftValue, type: leftLimit.type),
                right: new LimitPoint<T>(rightValue, type: rightLimit.type));
        }

        public bool TryParse(string str, out Range<T> range)
        {
            if (string.IsNullOrEmpty(str))
            {
                range = default(Range<T>);
                return false;
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

            string[] values = str
                .Substring(1, str.Length - 2)
                .Split(new[] { Separator }, 2);
            if (!_parseValue.TryParse(values[0], out var leftValue))
            {
                range = default(Range<T>);
                return false;
            }

            if (!_parseValue.TryParse(values[1], out var rightValue))
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
