namespace ComplicatedPrimitives.PerformanceTests.RangeParserTests.Old;

public interface IParser<T>
{
    T Parse(string format);
    bool TryParse(string format, out T result);
}