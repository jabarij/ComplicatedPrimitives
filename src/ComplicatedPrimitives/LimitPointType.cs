namespace ComplicatedPrimitives
{
    /// <summary>
    /// Enum indicating whether limit is open or closed.
    /// </summary>
    public enum LimitPointType
    {
        /// <summary>
        /// Indicates that limit is open which means it does not include its value.
        /// </summary>
        Open,
        /// <summary>
        /// Indicates that limit is closed which means it includes its value.
        /// </summary>
        Closed
    }
}