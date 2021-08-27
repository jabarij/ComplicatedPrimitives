using System;
using System.Linq;

namespace ComplicatedPrimitives
{
    /// <summary>
    /// Provides operations for performing 'limit algebra' on <see cref="DirectedLimit{T}">directed limits</see>.
    /// </summary>
    public static class DirectedLimit
    {
        /// <summary>
        /// Chooses the <see cref="DirectedLimit{T}.IsProperSubsetOf(DirectedLimit{T})">proper subset</see> of the two <see cref="DirectedLimit{T}">directed limits</see>.
        /// </summary>
        /// <typeparam name="T">Type of limit point's value (limit's domain).</typeparam>
        /// <param name="limit1">Directed limit to check 'proper subset' relation against the <paramref name="limit2"/>.</param>
        /// <param name="limit2">Directed limit to check 'proper subset' relation against the <paramref name="limit1"/>.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><term><paramref name="limit1"/></term><description>when <paramref name="limit1"/> is a proper subset of <paramref name="limit2"/>;</description></item>
        /// <item><term><paramref name="limit2"/></term><description>when <paramref name="limit2"/> is a proper subset of <paramref name="limit1"/>;</description></item>
        /// </list>
        /// </returns>
        /// <exception cref="InvalidOperationException">neither of given limits is a proper subset of the other one</exception>
        public static DirectedLimit<T> ProperSubset<T>(DirectedLimit<T> limit1, DirectedLimit<T> limit2) where T : IComparable<T>
        {
            if (limit1.IsProperSubsetOf(limit2))
                return limit1;

            if (limit2.IsProperSubsetOf(limit1))
                return limit2;

            throw new InvalidOperationException($"Neither limit is a proper subset of the other one.");
        }

        /// <summary>
        /// Chooses the <see cref="DirectedLimit{T}.IsProperSupersetOf(DirectedLimit{T})">proper superset</see> of the two <see cref="DirectedLimit{T}">directed limits</see>.
        /// </summary>
        /// <typeparam name="T">Type of limit point's value (limit's domain).</typeparam>
        /// <param name="limit1">Directed limit to check 'proper superset' relation against the <paramref name="limit2"/>.</param>
        /// <param name="limit2">Directed limit to check 'proper superset' relation against the <paramref name="limit1"/>.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><term><paramref name="limit1"/></term><description>when <paramref name="limit1"/> is a proper superset of <paramref name="limit2"/>;</description></item>
        /// <item><term><paramref name="limit2"/></term><description>when <paramref name="limit2"/> is a proper superset of <paramref name="limit1"/>;</description></item>
        /// </list>
        /// </returns>
        /// <exception cref="InvalidOperationException">neither of given limits is a proper superset of the other one</exception>
        public static DirectedLimit<T> ProperSuperset<T>(DirectedLimit<T> limit1, DirectedLimit<T> limit2) where T : IComparable<T>
        {
            if (limit1.IsProperSupersetOf(limit2))
                return limit1;

            if (limit2.IsProperSupersetOf(limit1))
                return limit2;

            throw new InvalidOperationException($"Neither limit is a proper superset of the other one.");
        }

        /// <summary>
        /// Chooses the <see cref="DirectedLimit{T}.IsSubsetOf(DirectedLimit{T})">subset</see> of the two <see cref="DirectedLimit{T}">directed limits</see>.
        /// </summary>
        /// <typeparam name="T">Type of limit point's value (limit's domain).</typeparam>
        /// <param name="limit1">Directed limit to check 'subset' relation against the <paramref name="limit2"/>.</param>
        /// <param name="limit2">Directed limit to check 'subset' relation against the <paramref name="limit1"/>.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><term><paramref name="limit1"/></term><description>when <paramref name="limit1"/> is a subset of <paramref name="limit2"/>;</description></item>
        /// <item><term><paramref name="limit2"/></term><description>when <paramref name="limit2"/> is a subset of <paramref name="limit1"/>;</description></item>
        /// </list>
        /// </returns>
        /// <exception cref="InvalidOperationException">neither of given limits is a subset of the other one</exception>
        public static DirectedLimit<T> Subset<T>(DirectedLimit<T> limit1, DirectedLimit<T> limit2) where T : IComparable<T>
        {
            if (limit1.IsSubsetOf(limit2))
                return limit1;

            if (limit2.IsSubsetOf(limit1))
                return limit2;

            throw new InvalidOperationException($"Neither limit is a subset of the other one.");
        }

        /// <summary>
        /// Chooses the <see cref="DirectedLimit{T}.IsSupersetOf(DirectedLimit{T})">superset</see> of the two <see cref="DirectedLimit{T}">directed limits</see>.
        /// </summary>
        /// <typeparam name="T">Type of limit point's value (limit's domain).</typeparam>
        /// <param name="limit1">Directed limit to check 'superset' relation against the <paramref name="limit2"/>.</param>
        /// <param name="limit2">Directed limit to check 'superset' relation against the <paramref name="limit1"/>.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><term><paramref name="limit1"/></term><description>when <paramref name="limit1"/> is a superset of <paramref name="limit2"/>;</description></item>
        /// <item><term><paramref name="limit2"/></term><description>when <paramref name="limit2"/> is a superset of <paramref name="limit1"/>;</description></item>
        /// </list>
        /// </returns>
        /// <exception cref="InvalidOperationException">neither of given limits is a superset of the other one</exception>
        public static DirectedLimit<T> Superset<T>(DirectedLimit<T> limit1, DirectedLimit<T> limit2) where T : IComparable<T>
        {
            if (limit1.IsSupersetOf(limit2))
                return limit1;

            if (limit2.IsSupersetOf(limit1))
                return limit2;

            throw new InvalidOperationException($"Neither limit is a superset of the other one.");
        }
    }
}
