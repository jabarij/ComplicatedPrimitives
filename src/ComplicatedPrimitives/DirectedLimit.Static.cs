using System;

namespace ComplicatedPrimitives
{
    public static class DirectedLimit
    {
        public static DirectedLimit<T> ProperSubset<T>(DirectedLimit<T> limit1, DirectedLimit<T> limit2) where T : IComparable<T>
        {
            if (limit1.IsProperSubsetOf(limit2))
                return limit1;

            if (limit2.IsProperSubsetOf(limit1))
                return limit2;

            throw new InvalidOperationException($"Neither limit is a proper subset of the other one.");
        }

        public static DirectedLimit<T> ProperSuperset<T>(DirectedLimit<T> limit1, DirectedLimit<T> limit2) where T : IComparable<T>
        {
            if (limit1.IsProperSupersetOf(limit2))
                return limit1;

            if (limit2.IsProperSupersetOf(limit1))
                return limit2;

            throw new InvalidOperationException($"Neither limit is a proper superset of the other one.");
        }

        public static DirectedLimit<T> Subset<T>(DirectedLimit<T> limit1, DirectedLimit<T> limit2) where T : IComparable<T>
        {
            if (limit1.IsSubsetOf(limit2))
                return limit1;

            if (limit2.IsSubsetOf(limit1))
                return limit2;

            throw new InvalidOperationException($"Neither limit is a subset of the other one.");
        }

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
