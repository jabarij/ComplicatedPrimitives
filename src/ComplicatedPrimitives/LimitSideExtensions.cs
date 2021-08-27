using System;

namespace ComplicatedPrimitives
{
    /// <summary>
    /// Provides extension methods for <see cref="LimitSide"/> enum.
    /// </summary>
    public static class LimitSideExtensions
    {
        /// <summary>
        /// Flips (inverses) the given <paramref name="side"/>.
        /// </summary>
        /// <param name="side">Value to flip.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><term><see cref="LimitSide.Left"/></term><description>when <paramref name="side"/> is <see cref="LimitSide.Right"/>;</description></item>
        /// <item><term><see cref="LimitSide.Right"/></term><description>when <paramref name="side"/> is <see cref="LimitSide.Left"/>.</description></item>
        /// </list>
        /// </returns>
        /// <exception cref="InvalidOperationException"><paramref name="side"/> is of undefined -or- unknown value</exception>
        public static LimitSide Flip(this LimitSide side)
        {
            switch (side)
            {
                case LimitSide.Left:
                    return LimitSide.Right;
                case LimitSide.Right:
                    return LimitSide.Left;
                case 0:
                    throw new InvalidOperationException("Cannot flip undefined limit side.");
                default:
                    throw new InvalidOperationException($"Handling {side.GetType().Name}.{side} was not implemented.");
            }
        }

        /// <summary>
        /// Provides an equivalent of 'match expression' for functional-like operating on <see cref="LimitSide"/> in C#.
        /// </summary>
        /// <typeparam name="TResult">Type of matching result.</typeparam>
        /// <param name="side">Value to match.</param>
        /// <param name="left">Function to perform when <paramref name="side"/> is <see cref="LimitSide.Left"/>.</param>
        /// <param name="right">Function to perform when <paramref name="side"/> is <see cref="LimitSide.Right"/>.</param>
        /// <param name="undefined">Optional function to perform when <paramref name="side"/> is <see cref="LimitSide.Undefined">undefined</see>.</param>
        /// <param name="unknown">Optional function to perform when <paramref name="side"/> is of unknown value.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><term>result of <c><paramref name="left"/>()</c></term><description>when <paramref name="side"/> is <see cref="LimitSide.Left">left</see>.</description></item>
        /// <item><term>result of <c><paramref name="right"/>()</c></term><description>when <paramref name="side"/> is <see cref="LimitSide.Right">right</see>.</description></item>
        /// <item>
        /// <term>result of <c><paramref name="undefined"/>()</c></term>
        /// <description>
        /// when <paramref name="side"/> is <see cref="LimitSide.Undefined">undefined</see>. If <paramref name="undefined"/> function was not specified, the <c>default</c> of <typeparamref name="TResult"/> is returned.
        /// </description>
        /// </item>
        /// <item>
        /// <term>result of <c><paramref name="unknown"/>()</c></term>
        /// <description>
        /// when <paramref name="side"/> is of unknown value. If <paramref name="unknown"/> function was not specified, the <c>default</c> of <typeparamref name="TResult"/> is returned.
        /// </description>
        /// </item>
        /// </list>
        /// 
        /// </returns>
        public static TResult Match<TResult>(this LimitSide side, 
            Func<TResult> left, 
            Func<TResult> right,
            Func<TResult> undefined = null,
            Func<TResult> unknown = null)
        {
            switch (side)
            {
                case LimitSide.Left:
                    return left();
                case LimitSide.Right:
                    return right();
                default:
                    if (side == default(LimitSide) && undefined != null)
                        return undefined();
                    if (unknown != null)
                        return unknown();
                    return default(TResult);
            }
        }
    }
}
