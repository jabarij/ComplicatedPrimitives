namespace ComplicatedPrimitives
{
    /// <summary>
    /// Enum indicating whether limit is open or closed.
    /// </summary>
    public enum LimitType
    {
        /// <summary>
        /// Indicates that limit is open which means it does not include the limit value.
        /// </summary>
        Open,
        /// <summary>
        /// Indicates that limit is closed which means it includes the limit value.
        /// </summary>
        Closed
    }
}