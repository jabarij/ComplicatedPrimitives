using System.Globalization;

namespace ComplicatedPrimitives;

public static class CaseInsensitiveStringCompareOptionsExtensions
{
    public static CompareOptions ToStringCompareOptions(this CaseInsensitiveStringCompareOptions value)
    {
        var result = CompareOptions.IgnoreCase;
            
        if (value.HasFlag(CaseInsensitiveStringCompareOptions.IgnoreNonSpace))
            result |= CompareOptions.IgnoreNonSpace;
            
        if (value.HasFlag(CaseInsensitiveStringCompareOptions.IgnoreSymbols))
            result |= CompareOptions.IgnoreSymbols;
            
        if (value.HasFlag(CaseInsensitiveStringCompareOptions.IgnoreKanaType))
            result |= CompareOptions.IgnoreKanaType;
            
        if (value.HasFlag(CaseInsensitiveStringCompareOptions.IgnoreWidth))
            result |= CompareOptions.IgnoreWidth;
            
        if (value.HasFlag(CaseInsensitiveStringCompareOptions.StringSort))
            result |= CompareOptions.StringSort;
            
        if (value.HasFlag(CaseInsensitiveStringCompareOptions.Ordinal))
            result |= CompareOptions.Ordinal;
            
        return result;
    }
}