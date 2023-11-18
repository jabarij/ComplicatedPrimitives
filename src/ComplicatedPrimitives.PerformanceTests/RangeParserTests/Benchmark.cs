using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace ComplicatedPrimitives.PerformanceTests.RangeParserTests;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net80)]
public class Benchmark
{
    private readonly Old.RangeParser<int> _oldRangeParser = new(new Old.DefaultValueParser());
    private readonly RangeParser<int> _newRangeParser = new();
    private readonly IRangeParser<int> _newRangeParserAsInterface = new RangeParser<int>();

    public IEnumerable<object[]> Formats()
    {
        yield return new object[] { "(1;2]" };
        yield return new object[] { "(-2000000000;2000000000]" };
        yield return new object[] { "(-∞;0]" };
        yield return new object[] { "(-oo;0]" };
        yield return new object[] { "[0;∞)" };
        yield return new object[] { "[0;oo)" };
        yield return new object[] { "[0;+∞)" };
        yield return new object[] { "[0;+oo)" };
    }

    [Benchmark]
    [ArgumentsSource(nameof(Formats))]
    public Range<int> OldRangeParser(string format)
    {
        return _oldRangeParser.Parse(format);
    }

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(Formats))]
    public Range<int> NewRangeParser(string format)
    {
        return _newRangeParser.Parse(format.AsSpan());
    }

    [Benchmark]
    [ArgumentsSource(nameof(Formats))]
    public Range<int> NewRangeParserAsInterface(string format)
    {
        return _newRangeParserAsInterface.Parse(format.AsSpan());
    }
}