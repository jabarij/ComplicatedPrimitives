using System;

namespace ComplicatedPrimitives;

public interface IParser<T>
{
    T Parse(ReadOnlySpan<char> str);
    bool TryParse(ReadOnlySpan<char> str, out T result);
}