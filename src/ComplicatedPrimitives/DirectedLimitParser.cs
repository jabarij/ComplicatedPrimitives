using System;
using System.Linq;

namespace ComplicatedPrimitives
{
    public class DirectedLimitParser<T> : IDirectedLimitParser<T>
        where T : IComparable<T>
    {
        private static readonly string[] InfinityRepresentations = new[] { "oo", "∞" };

        private readonly IParser<LimitValue<T>> _parseValue;
        private readonly IParser<LimitSide> _parseSide;

        public DirectedLimitParser(
            IParser<LimitValue<T>> valueParser,
            IParser<LimitSide> sideParser)
        {
            _parseValue = valueParser
                ?? throw new ArgumentNullException(nameof(valueParser));
            _parseSide = sideParser 
                ?? throw new ArgumentNullException(nameof(sideParser));
        }
        public LimitValueParser(IParser<T> valueParser)
            : this(new LimitValueParser<T>(valueParser), new LimitSideParser()) { }

        public LimitValue<T> Parse(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentNullException(nameof(str));

            if (_isInfinity(str))
                return LimitValue<T>.Infinity;

            if (!TryGetParts(str, out var parts))
                throw new ParsingException(str, "Could not recognize limit type and value parts.");

            var (typePart, valuePart) = parts;
            var type = _parseType.Parse(typePart);
            var value = _parseValue.Parse(valuePart);
            return new LimitValue<T>(value, type);
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

        private static bool TryGetParts(string str, out (string typePart, string valuePart) result)
        {
            if (string.IsNullOrEmpty(str)
                || str.Length < 2)
            {
                result = ("", "");
                return false;
            }

            if (IsLimitTypeRepresentation(str[0]))
            {
                result = (str[0].ToString(), str.Substring(1));
                return true;
            }

            if (IsLimitTypeRepresentation(str[str.Length - 1]))
            {
                result = (str[str.Length - 1].ToString(), str.Substring(0, str.Length - 1));
                return true;
            }

            result = ("", "");
            return false;
        }

        private static bool IsLimitTypeRepresentation(char c) =>
            Parsing.OpenLimitTypeRepresentations.Contains(c)
            || Parsing.ClosedLimitTypeRepresentations.Contains(c);

        private static bool IsInfinity(string str) =>
            InfinityRepresentations
            .Any(e => string.Equals(e, str, StringComparison.OrdinalIgnoreCase));
    }
}
