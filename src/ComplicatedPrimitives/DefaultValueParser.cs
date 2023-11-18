using System;

namespace ComplicatedPrimitives;

public class DefaultValueParser :
    IParser<byte>,
    IParser<short>,
    IParser<ushort>,
    IParser<int>,
    IParser<uint>,
    IParser<long>,
    IParser<ulong>,
    IParser<float>,
    IParser<double>,
    IParser<decimal>,
    IParser<DateTime>,
    IParser<TimeSpan>
{
    byte IParser<byte>.Parse(ReadOnlySpan<char> str) => byte.Parse(str);
    bool IParser<byte>.TryParse(ReadOnlySpan<char> str, out byte result) => byte.TryParse(str, out result);

    short IParser<short>.Parse(ReadOnlySpan<char> str) => short.Parse(str);
    bool IParser<short>.TryParse(ReadOnlySpan<char> str, out short result) => short.TryParse(str, out result);

    ushort IParser<ushort>.Parse(ReadOnlySpan<char> str) => ushort.Parse(str);
    bool IParser<ushort>.TryParse(ReadOnlySpan<char> str, out ushort result) => ushort.TryParse(str, out result);

    int IParser<int>.Parse(ReadOnlySpan<char> str) => int.Parse(str);
    bool IParser<int>.TryParse(ReadOnlySpan<char> str, out int result) => int.TryParse(str, out result);

    uint IParser<uint>.Parse(ReadOnlySpan<char> str) => uint.Parse(str);
    bool IParser<uint>.TryParse(ReadOnlySpan<char> str, out uint result) => uint.TryParse(str, out result);

    long IParser<long>.Parse(ReadOnlySpan<char> str) => long.Parse(str);
    bool IParser<long>.TryParse(ReadOnlySpan<char> str, out long result) => long.TryParse(str, out result);

    ulong IParser<ulong>.Parse(ReadOnlySpan<char> str) => ulong.Parse(str);
    bool IParser<ulong>.TryParse(ReadOnlySpan<char> str, out ulong result) => ulong.TryParse(str, out result);

    float IParser<float>.Parse(ReadOnlySpan<char> str) => float.Parse(str);
    bool IParser<float>.TryParse(ReadOnlySpan<char> str, out float result) => float.TryParse(str, out result);

    double IParser<double>.Parse(ReadOnlySpan<char> str) => double.Parse(str);
    bool IParser<double>.TryParse(ReadOnlySpan<char> str, out double result) => double.TryParse(str, out result);

    decimal IParser<decimal>.Parse(ReadOnlySpan<char> str) => decimal.Parse(str);
    bool IParser<decimal>.TryParse(ReadOnlySpan<char> str, out decimal result) => decimal.TryParse(str, out result);

    DateTime IParser<DateTime>.Parse(ReadOnlySpan<char> str) => DateTime.Parse(str);
    bool IParser<DateTime>.TryParse(ReadOnlySpan<char> str, out DateTime result) => DateTime.TryParse(str, out result);

    TimeSpan IParser<TimeSpan>.Parse(ReadOnlySpan<char> str) => TimeSpan.Parse(str);
    bool IParser<TimeSpan>.TryParse(ReadOnlySpan<char> str, out TimeSpan result) => TimeSpan.TryParse(str, out result);
}