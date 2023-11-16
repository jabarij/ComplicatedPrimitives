using System;

namespace ComplicatedPrimitives.Tests;

public static class LimitValueExtensions
{
    public static LimitValue<T> FlipLimitType<T>(this LimitValue<T> limitValue) where T : IComparable<T> =>
        new(limitValue.Value, limitValue.Type.Flip());
}