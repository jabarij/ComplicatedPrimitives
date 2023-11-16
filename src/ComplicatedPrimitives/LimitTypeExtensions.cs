using System;

namespace ComplicatedPrimitives;

public static class LimitTypeExtensions
{
    public static LimitType Flip(this LimitType type)
    {
        switch (type)
        {
            case LimitType.Open:
                return LimitType.Closed;
            case LimitType.Closed:
                return LimitType.Open;
            default:
                throw new InvalidOperationException($"Handling {type.GetType().Name}.{type} was not implemented.");
        }
    }

    public static TResult Match<TResult>(this LimitType type, Func<TResult> open, Func<TResult> closed)
    {
        switch (type)
        {
            case LimitType.Open:
                return open();
            case LimitType.Closed:
                return closed();
            default:
                throw new InvalidOperationException($"Handling {type.GetType().Name}.{type} was not implemented.");
        }
    }

    public static TResult Match<TResult>(this LimitType type, TResult open, TResult closed)
    {
        switch (type)
        {
            case LimitType.Open:
                return open;
            case LimitType.Closed:
                return closed;
            default:
                throw new InvalidOperationException($"Handling {type.GetType().Name}.{type} was not implemented.");
        }
    }
}