using System;

namespace ComplicatedPrimitives.Tests
{
    public static class LimitPointExtensions
    {
        public static LimitPoint<T> FlipLimitType<T>(this LimitPoint<T> limitValue) where T : IComparable<T> =>
            new LimitPoint<T>(limitValue.Value, limitValue.Type.Flip());
    }
}
