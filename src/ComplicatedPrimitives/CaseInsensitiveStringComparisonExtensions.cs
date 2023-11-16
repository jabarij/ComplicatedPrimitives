using System;

namespace ComplicatedPrimitives;

public static class CaseInsensitiveStringComparisonExtensions
{
    public static StringComparison ToStringComparison(this CaseInsensitiveStringComparison value)
    {
        switch (value)
        {
            case CaseInsensitiveStringComparison.CurrentCulture:
                return StringComparison.CurrentCultureIgnoreCase;
            case CaseInsensitiveStringComparison.InvariantCulture:
                return StringComparison.InvariantCultureIgnoreCase;
            case CaseInsensitiveStringComparison.Ordinal:
                return StringComparison.OrdinalIgnoreCase;
            default:
                throw new InvalidOperationException($"Handling {value.GetType()}.{value} is not implemented.");
        }
    }

    public static CaseInsensitiveStringComparison ToCaseInsensitiveStringComparison(this StringComparison value)
    {
        switch (value)
        {
            case StringComparison.CurrentCulture:
            case StringComparison.CurrentCultureIgnoreCase:
                return CaseInsensitiveStringComparison.CurrentCulture;
            case StringComparison.InvariantCulture:
            case StringComparison.InvariantCultureIgnoreCase:
                return CaseInsensitiveStringComparison.InvariantCulture;
            case StringComparison.Ordinal:
            case StringComparison.OrdinalIgnoreCase:
                return CaseInsensitiveStringComparison.Ordinal;
            default:
                throw new InvalidOperationException($"Handling {value.GetType()}.{value} is not implemented.");
        }
    }
}