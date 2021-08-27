using System;

namespace ComplicatedPrimitives
{
    internal static class Error
    {
        public static ArgumentNullException ArgumentIsNull(string paramName) =>
            new ArgumentNullException(paramName);

        public static ArgumentNullException ArgumentIsNullOrEmptyString(string paramName) =>
            new ArgumentNullException(paramName, "Argument is either null or empty string.");

        public static ArgumentException ArgumentIsUndefinedEnum<TEnum>(TEnum enumValue, string paramName) =>
            new ArgumentException($"Invalid value {enumValue} of enum {typeof(TEnum)}", paramName);

        public static ParsingException ParsingFormatIsIncorrect(string format, string message) =>
                throw new ParsingException(format, message);
    }
}
