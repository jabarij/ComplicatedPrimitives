using System;

namespace ComplicatedPrimitives
{
    /// <summary>
    /// Provides extension methods for <see cref="LimitPointType"/>.
    /// </summary>
    public static class LimitPointTypeExtensions
    {
        /// <summary>
        /// Flips type giving its oposite value.
        /// </summary>
        /// <param name="type">Value to flip.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><description><see cref="LimitPointType.Open">Open</see> when <paramref name="type"/> was <see cref="LimitPointType.Closed">closed</see>;</description></item>
        /// <item><description><see cref="LimitPointType.Closed">Closed</see> when <paramref name="type"/> was <see cref="LimitPointType.Open">open</see>.</description></item>
        /// </list>
        /// </returns>
        public static LimitPointType Flip(this LimitPointType type)
        {
            switch (type)
            {
                case LimitPointType.Open:
                    return LimitPointType.Closed;
                case LimitPointType.Closed:
                    return LimitPointType.Open;
                default:
                    throw new InvalidOperationException($"Handling {type.GetType().Name}.{type} was not implemented.");
            }
        }

        /// <summary>
        /// Provides functional catamorphism for <see cref="LimitPointType"/> enum (similar to match expressions from functional programming).
        /// </summary>
        /// <typeparam name="TResult">Result type.</typeparam>
        /// <param name="type">Type to match.</param>
        /// <param name="open">Function to execute when <paramref name="type"/> is <see cref="LimitPointType.Open">open</see>.</param>
        /// <param name="closed">Function to execute when <paramref name="type"/> is <see cref="LimitPointType.Closed">closed</see>.</param>
        /// <returns>
        /// Result of:
        /// <list type="bullet">
        /// <item><description><paramref name="open"/> when <paramref name="type"/> was <see cref="LimitPointType.Open">open</see>;</description></item>
        /// <item><description><paramref name="closed"/> when <paramref name="type"/> was <see cref="LimitPointType.Closed">closed</see>.</description></item>
        /// </list>
        /// </returns>
        public static TResult Match<TResult>(this LimitPointType type, Func<TResult> open, Func<TResult> closed)
        {
            switch (type)
            {
                case LimitPointType.Open:
                    return open();
                case LimitPointType.Closed:
                    return closed();
                default:
                    throw new InvalidOperationException($"Handling {type.GetType().Name}.{type} was not implemented.");
            }
        }

        /// <summary>
        /// Provides simple-value catamorphism for <see cref="LimitPointType"/> enum (similar to match expressions from functional programming).
        /// </summary>
        /// <typeparam name="TResult">Result value type.</typeparam>
        /// <param name="type">Type to match.</param>
        /// <param name="open">Value to return when <paramref name="type"/> is <see cref="LimitPointType.Open">open</see>.</param>
        /// <param name="closed">Function to return when <paramref name="type"/> is <see cref="LimitPointType.Closed">closed</see>.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><description><paramref name="open"/> value when <paramref name="type"/> was <see cref="LimitPointType.Open">open</see>;</description></item>
        /// <item><description><paramref name="closed"/> value when <paramref name="type"/> was <see cref="LimitPointType.Closed">closed</see>.</description></item>
        /// </list>
        /// </returns>
        public static TResult Match<TResult>(this LimitPointType type, TResult open, TResult closed)
        {
            switch (type)
            {
                case LimitPointType.Open:
                    return open;
                case LimitPointType.Closed:
                    return closed;
                default:
                    throw new InvalidOperationException($"Handling {type.GetType().Name}.{type} was not implemented.");
            }
        }
    }
}
