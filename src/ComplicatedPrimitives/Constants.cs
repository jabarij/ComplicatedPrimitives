namespace ComplicatedPrimitives
{
    internal class Constants
    {
        public const char LeftOpenSign = '>';
        public const char LeftClosedSign = '≥';
        public const char RightOpenSign = '<';
        public const char RightClosedSign = '≤';

        public const string UndefinedString = "undefined";
        public const string InfinityString = "∞";
        public const string EmptySetString = "Ø";
        public const string ValueSeparatorString = ";";
        public const string LeftOpenLimitTypeString = "(";
        public const string LeftClosedLimitTypeString = "[";
        public const string RightOpenLimitTypeString = ")";
        public const string RightClosedLimitTypeString = "]";

        public const string LeftInfiniteValueString = LeftOpenLimitTypeString + InfinityString;
        public const string RightInfiniteValueString = InfinityString + RightOpenLimitTypeString;
        public const string InfiniteRangeString = LeftInfiniteValueString + ValueSeparatorString + RightInfiniteValueString;
    }
}
