using System;
using System.Collections.Generic;
using System.Text;

namespace ComplicatedPrimitives
{
    internal class DirectedLimitComparer<T> : IComparer<DirectedLimit<T>> where T : IComparable<T>
    {
        public int Compare(DirectedLimit<T> left, DirectedLimit<T> right)
        {
            int compareValue = left.LimitValue.Value.CompareTo(right.LimitValue.Value);
            if (compareValue != 0)
                return compareValue;

            if (left.LimitValue.Type != right.LimitValue.Type)
                return left.Side == LimitSide.Left
                    ? (left.LimitValue.Type == LimitType.Closed ? 1 : -1)
                    : (left.LimitValue.Type == LimitType.Open ? -1 : 1);
            else if (left.LimitValue.Type == LimitType.Open)
                return left.Side == LimitSide.Left ? 1 : -1;

            int compareSide = left.Side.CompareTo(right.Side);
            if (compareSide != 0)
                return compareSide;

            return 0;
        }
    }
}
