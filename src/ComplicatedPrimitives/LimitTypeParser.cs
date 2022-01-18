using System;
using System.Linq;
using ComplicatedPrimitives.Validation;

namespace ComplicatedPrimitives
{
    public class LimitTypeParser : IParser<LimitType>
    {
        private const int MaxFormatLength = 1;

        public LimitType Parse(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentNullException(nameof(str));
            if (str.Length > MaxFormatLength)
                throw new ParsingException(str, ErrorMessages.StringTooLong(MaxFormatLength));

            if (Parsing.OpenLimitTypeRepresentations.Contains(str[0]))
                return LimitType.Open;

            if (Parsing.ClosedLimitTypeRepresentations.Contains(str[0]))
                return LimitType.Closed;

            throw new ParsingException(str, ErrorMessages.UnknownFormat);
        }

        public bool TryParse(string str, out LimitType result)
        {
            if (string.IsNullOrEmpty(str)
                || str.Length > MaxFormatLength)
            {
                result = default(LimitType);
                return false;
            }

            if (Parsing.OpenLimitTypeRepresentations.Contains(str[0]))
            {
                result = LimitType.Open;
                return true;
            }

            if (Parsing.ClosedLimitTypeRepresentations.Contains(str[0]))
            {
                result = LimitType.Closed;
                return true;
            }

            result = default(LimitType);
            return false;
        }
    }
}