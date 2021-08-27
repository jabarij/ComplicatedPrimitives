using System;

namespace ComplicatedPrimitives
{
    /// <summary>
    /// Defines a generalized parsing methods that a value type or class implements to restore an instance of <see cref="Range{T}"/> from its string equivalent.
    /// </summary>
    /// <typeparam name="T">Type of range value (domain).</typeparam>
    public interface IRangeParser<T> : IParser<Range<T>>
        where T : IComparable<T>
    {
    }
}
