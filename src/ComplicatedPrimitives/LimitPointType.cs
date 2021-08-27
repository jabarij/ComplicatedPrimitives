namespace ComplicatedPrimitives
{
    /// <summary>
    /// Enum indicating whether the <see cref="LimitPoint{T}">limit point</see> is either open or closed.
    /// </summary>
    public enum LimitPointType
    {
        /// <summary>
        /// Indicates that <see cref="LimitPoint{T}">limit point</see> is open which means it does not include its value.
        /// </summary>
        Open,
        /// <summary>
        /// Indicates that <see cref="LimitPoint{T}">limit point</see> is closed which means it includes its value.
        /// </summary>
        Closed
    }
}