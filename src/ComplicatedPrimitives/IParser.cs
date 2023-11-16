namespace ComplicatedPrimitives;

public interface IParser<T>
{
    T Parse(string str);
    bool TryParse(string str, out T result);
}