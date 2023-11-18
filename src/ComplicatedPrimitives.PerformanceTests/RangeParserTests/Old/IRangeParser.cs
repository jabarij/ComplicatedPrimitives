namespace ComplicatedPrimitives.PerformanceTests.RangeParserTests.Old;

public interface IRangeParser<T> : IParser<Range<T>>
    where T : IComparable<T>
{
}