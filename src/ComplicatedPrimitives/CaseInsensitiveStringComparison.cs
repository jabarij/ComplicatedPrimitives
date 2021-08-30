namespace ComplicatedPrimitives
{
    /// <summary>
    /// Enumeration specifying the culture and sort rules to be used by certain overloads of the <see cref="CaseInsensitiveString.Equals(object)"/>.
    /// </summary>
    public enum CaseInsensitiveStringComparison
    {
        /// <summary>
        /// Compare <see cref="CaseInsensitiveString">case insensitive strings</see> using culture-sensitive sort rules and the current culture.
        /// </summary>
        CurrentCulture,
        /// <summary>
        /// Compare <see cref="CaseInsensitiveString">case insensitive strings</see> using culture-sensitive sort rules and the invariant culture.
        /// </summary>
        InvariantCulture,
        /// <summary>
        /// Compare <see cref="CaseInsensitiveString">case insensitive strings</see> using ordinal (binary) sort rules.
        /// </summary>
        Ordinal
    }
}