using System;

namespace ComplicatedPrimitives
{
    internal static class Throw
    {
        public static void ArgumentIsUndefinedEnum<TEnum>(TEnum enumValue, string paramName) =>
            throw new ArgumentException($"Invalid value {enumValue} of enum {typeof(TEnum)}", paramName);
    }
}
