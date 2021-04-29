using System;

namespace ComplicatedPrimitives
{
    public static class LimitSideExtensions
    {
        public static LimitSide Flip(this LimitSide side)
        {
            switch (side)
            {
                case LimitSide.Left:
                    return LimitSide.Right;
                case LimitSide.Right:
                    return LimitSide.Left;
                case 0:
                    return 0;
                default:
                    throw new InvalidOperationException($"Handling {side.GetType().Name}.{side} was not implemented.");
            }
        }

        public static TResult Match<TResult>(this LimitSide side, Func<TResult> left, Func<TResult> right, Func<TResult> undefined = null)
        {
            switch (side)
            {
                case LimitSide.Left:
                    return left();
                case LimitSide.Right:
                    return right();
                default:
                    if (side == 0 && undefined != null)
                        return undefined();
                    throw new InvalidOperationException($"Handling {side.GetType().Name}.{side} was not implemented.");
            }
        }
    }
}
