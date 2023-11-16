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
    byte IParser<byte>.Parse(string str) => byte.Parse(str);
    bool IParser<byte>.TryParse(string str, out byte result) => byte.TryParse(str, out result);

    short IParser<short>.Parse(string str) => short.Parse(str);
    bool IParser<short>.TryParse(string str, out short result) => short.TryParse(str, out result);

    ushort IParser<ushort>.Parse(string str) => ushort.Parse(str);
    bool IParser<ushort>.TryParse(string str, out ushort result) => ushort.TryParse(str, out result);

    int IParser<int>.Parse(string str) => int.Parse(str);
    bool IParser<int>.TryParse(string str, out int result) => int.TryParse(str, out result);

    uint IParser<uint>.Parse(string str) => uint.Parse(str);
    bool IParser<uint>.TryParse(string str, out uint result) => uint.TryParse(str, out result);

    long IParser<long>.Parse(string str) => long.Parse(str);
    bool IParser<long>.TryParse(string str, out long result) => long.TryParse(str, out result);

    ulong IParser<ulong>.Parse(string str) => ulong.Parse(str);
    bool IParser<ulong>.TryParse(string str, out ulong result) => ulong.TryParse(str, out result);

    float IParser<float>.Parse(string str) => float.Parse(str);
    bool IParser<float>.TryParse(string str, out float result) => float.TryParse(str, out result);

    double IParser<double>.Parse(string str) => double.Parse(str);
    bool IParser<double>.TryParse(string str, out double result) => double.TryParse(str, out result);

    decimal IParser<decimal>.Parse(string str) => decimal.Parse(str);
    bool IParser<decimal>.TryParse(string str, out decimal result) => decimal.TryParse(str, out result);

    DateTime IParser<DateTime>.Parse(string str) => DateTime.Parse(str);
    bool IParser<DateTime>.TryParse(string str, out DateTime result) => DateTime.TryParse(str, out result);

    TimeSpan IParser<TimeSpan>.Parse(string str) => TimeSpan.Parse(str);
    bool IParser<TimeSpan>.TryParse(string str, out TimeSpan result) => TimeSpan.TryParse(str, out result);
}