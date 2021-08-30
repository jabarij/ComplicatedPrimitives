using System;

namespace ComplicatedPrimitives
{
    /// <summary>
    /// Provides extension methods for <see cref="CaseInsensitiveStringComparison"/>.
    /// </summary>
    public static class CaseInsensitiveStringComparisonExtensions
    {
        /// <summary>
        /// Converts <see cref="CaseInsensitiveStringComparison"/> <paramref name="value"/> to its equivalent <see cref="StringComparison"/> value.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>
        /// Proper value of type <see cref="StringComparison"/> being logical equivalent of the given <paramref name="value"/>
        /// if valid (known) <see cref="CaseInsensitiveStringComparison"/> value was passe;
        /// otherwise throws exception.
        /// </returns>
        /// <exception cref="InvalidOperationException"><paramref name="value"/> is not a defined value of <see cref="CaseInsensitiveStringComparison"/></exception>
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

        /// <summary>
        /// Converts (casts down) <see cref="StringComparison"/> <paramref name="value"/> to its equivalent <see cref="CaseInsensitiveStringComparison"/> value.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>
        /// Proper value of type <see cref="CaseInsensitiveStringComparison"/> being logical equivalent of the given <paramref name="value"/>
        /// if valid (known) <see cref="StringComparison"/> value was passe;
        /// otherwise throws exception.
        /// </returns>
        /// <exception cref="InvalidOperationException"><paramref name="value"/> is not a defined value of <see cref="StringComparison"/></exception>
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
