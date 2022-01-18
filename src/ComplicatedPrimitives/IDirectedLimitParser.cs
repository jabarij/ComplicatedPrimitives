using System;

namespace ComplicatedPrimitives
{
    public interface IDirectedLimitParser<T> : IParser<DirectedLimit<T>>
        where T : IComparable<T>
    {
    }
}