using System;
using System.Linq;

namespace ComplicatedPrimitives
{
    public class LimitSideParser : IParser<LimitSide>
    {
        public LimitSide Parse(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentNullException(nameof(str));
            if (str.Length > 1)
                throw new ParsingException(str, "Too long value.");

            if (Parsing.LeftLimitSideRepresentations.Contains(str[0]))
                return LimitSide.Left;

            if (Parsing.RightLimitSideRepresentations.Contains(str[0]))
                return LimitSide.Right;

            throw new ParsingException(str, "Could not parse value.");
        }

        public bool TryParse(string str, out LimitSide result)
        {
            if (string.IsNullOrEmpty(str)
                || str.Length > 1)
            {
                result = default(LimitSide);
                return false;
            }

            if (Parsing.LeftLimitSideRepresentations.Contains(str[0]))
            {
                result = LimitSide.Left;
                return true;
            }

            if (Parsing.RightLimitSideRepresentations.Contains(str[0]))
            {
                result = LimitSide.Right;
                return true;
            }

            result = default(LimitSide);
            return false;
        }
    }
}