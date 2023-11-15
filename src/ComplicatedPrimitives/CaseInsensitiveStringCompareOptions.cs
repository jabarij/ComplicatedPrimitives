using System;

namespace ComplicatedPrimitives
{
    [Flags]
    public enum CaseInsensitiveStringCompareOptions
    {
        None = 0,
        IgnoreNonSpace = 1,
        IgnoreSymbols = 2,
        IgnoreKanaType = 4,
        IgnoreWidth = 8,
        StringSort = 16,
        Ordinal = 32
    }
}