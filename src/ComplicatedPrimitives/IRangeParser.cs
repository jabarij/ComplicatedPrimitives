using System;

namespace ComplicatedPrimitives
{
    public interface IRangeParser<T> : IParser<Range<T>>
        where T : IComparable<T>
    {
    }
}
