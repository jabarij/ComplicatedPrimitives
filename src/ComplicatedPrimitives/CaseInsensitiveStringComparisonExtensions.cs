using System;

namespace ComplicatedPrimitives
{
    /// <include
    ///   file='ComplicatedPrimitives.xml'
    ///   path='//member[@name="T:ComplicatedPrimitives.CaseInsensitiveStringComparisonExtensions"]' />
    public static class CaseInsensitiveStringComparisonExtensions
    {
        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.CaseInsensitiveStringComparisonExtensions.ToStringComparison(ComplicatedPrimitives.CaseInsensitiveStringComparison)"]' />
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

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.CaseInsensitiveStringComparisonExtensions.ToCaseInsensitiveStringComparison(System.StringComparison)"]' />
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
}
