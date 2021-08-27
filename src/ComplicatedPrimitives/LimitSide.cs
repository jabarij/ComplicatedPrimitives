namespace ComplicatedPrimitives
{
    /// <summary>
    /// Enum indicating whether <see cref="DirectedLimit{T}">directed limit</see> lays on either left or right side of its domain.
    /// </summary>
    public enum LimitSide
    {
        /// <summary>
        /// Indicates that <see cref="DirectedLimit{T}">directed limit</see> lays on the left side of its domain conventional axis.
        /// In other words, this option indicates that values of the limit's domain approach the limit point from the right.
        /// </summary>
        Left = -1,
        /// <summary>
        /// Indicates that <see cref="DirectedLimit{T}">directed limit</see> lays on the right side of its domain conventional axis.
        /// In other words, this option indicates that values of the limit's domain approach the limit point from the left.
        /// </summary>
        Right = 1
    }
}