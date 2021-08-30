namespace ComplicatedPrimitives
{
    internal class Constants
    {
        internal const char LeftOpenSign = '>';
        internal const char LeftClosedSign = '≥';
        internal const char RightOpenSign = '<';
        internal const char RightClosedSign = '≤';

        public const string InfinityString = "∞";
        public const string EmptySetString = "Ø";
        public const string ValueSeparatorString = ";";
        public const string LeftOpenLimitTypeString = "(";
        public const string LeftClosedLimitTypeString = "[";
        public const string RightOpenLimitTypeString = ")";
        public const string RightClosedLimitTypeString = "]";

        internal const string LeftInfiniteValueString = LeftOpenLimitTypeString + InfinityString;
        internal const string RightInfiniteValueString = InfinityString + RightOpenLimitTypeString;
        internal const string InfiniteRangeString = LeftInfiniteValueString + ValueSeparatorString + RightInfiniteValueString;
    }
}
