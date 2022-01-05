using System;
using System.Linq;

namespace ComplicatedPrimitives
{
    public class LimitValueParser<T> : ILimitValueParser<T>
        where T : IComparable<T>
    {
        private static readonly string[] InfinityRepresentations = new[] { "oo", "∞" };
        private static readonly char[] OpenLimitTypeRepresentations = new[] { '(', ')' };
        private static readonly char[] ClosedLimitTypeRepresentations = new[] { '[', ']' };

        private readonly IParser<T> _parseValue;
        private readonly IParser<LimitType> _parseType;
        private readonly Predicate<string> _isInfinity;

        public LimitValueParser(
            IParser<T> valueParser,
            IParser<LimitType> parseType,
            Predicate<string> infinityStringPredicate)
        {
            _parseValue = valueParser
                ?? throw new ArgumentNullException(nameof(valueParser));
            _parseType = parseType
                ?? throw new ArgumentNullException(nameof(parseType));
            _isInfinity = infinityStringPredicate
                ?? throw new ArgumentNullException(nameof(infinityStringPredicate));
        }
        public LimitValueParser(
            IParser<T> valueParser,
            IParser<LimitType> typeParser)
            : this(valueParser, typeParser, IsInfinity) { }
        public LimitValueParser(IParser<T> valueParser)
            : this(valueParser, new LimitTypeParser(), IsInfinity) { }

        public LimitValue<T> Parse(string str)
        {
            throw new NotImplementedException();
        }

        public bool TryParse(string str, out LimitValue<T> result)
        {
            if (string.IsNullOrEmpty(str))
            {
                result = default(LimitValue<T>);
                return false;
            }

            if (_isInfinity(str))
            {
                result = LimitValue<T>.Infinity;
                return true;
            }

            if (!TryGetParts(str, out var parts))
            {
                result = default(LimitValue<T>);
                return false;
            }

            var (typePart, valuePart) = parts;

            if (!_parseType.TryParse(typePart, out var type))
            {
                result = default(LimitValue<T>);
                return false;
            }

            if (!_parseValue.TryParse(valuePart, out var value))
            {
                result = default(LimitValue<T>);
                return false;
            }

            result = new LimitValue<T>(value, type);
            return true;
        }

        private static bool TryGetParts(string str, out (char typePart, string valuePart) result)
        {
            if (string.IsNullOrEmpty(str)
                || str.Length < 2)
            {
                result = ('?', "");
                return false;
            }

            if (IsLimitTypeReporesentation(str[0]))
            {
                result = (str[0], str.Substring(1));
                return true;
            }

            if (IsLimitTypeReporesentation(str[str.Length - 1]))
            {
                result = (str[str.Length - 1], str.Substring(0, str.Length - 1));
                return true;
            }

            result = ('?', "");
            return false;
        }

        private static bool IsLimitTypeReporesentation(char c) =>
            OpenLimitTypeRepresentations.Contains(c)
            || ClosedLimitTypeRepresentations.Contains(c);

        private static bool IsInfinity(string str) =>
            InfinityRepresentations
            .Any(e => string.Equals(e, str, StringComparison.OrdinalIgnoreCase));
    }
}
