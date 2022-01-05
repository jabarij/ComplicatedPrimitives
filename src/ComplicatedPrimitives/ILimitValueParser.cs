using System;

namespace ComplicatedPrimitives
{
    public interface ILimitValueParser<T> : IParser<LimitValue<T>>
        where T : IComparable<T>
    {
    }
}