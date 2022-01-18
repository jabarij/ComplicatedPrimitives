namespace ComplicatedPrimitives
{
    internal static class Parsing
    {
        public static readonly char[] OpenLimitTypeRepresentations = new[] { '(', ')' };
        public static readonly char[] ClosedLimitTypeRepresentations = new[] { '[', ']' };
        
        public static readonly char[] LeftLimitSideRepresentations = new[] { '(', '[' };
        public static readonly char[] RightLimitSideRepresentations = new[] { ')', ']' };
        
        public static readonly string[] InfinityRepresentations = new[] { "oo", "∞" };
    }
}