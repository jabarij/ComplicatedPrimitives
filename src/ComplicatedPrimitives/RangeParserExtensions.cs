using System;

namespace ComplicatedPrimitives;

public static class RangeParserExtensions
{
    public static Range<T> Parse<T>(this IRangeParser<T> parser, string format)
        where T : IComparable<T>
        => parser.Parse(format.AsSpan());

    public static bool TryParse<T>(this IRangeParser<T> parser, string format, out Range<T> range)
        where T : IComparable<T>
        => parser.TryParse(format.AsSpan(), out range);
}