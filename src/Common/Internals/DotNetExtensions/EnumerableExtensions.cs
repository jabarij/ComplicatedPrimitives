using System.Collections.Generic;
using System.Linq;

namespace DotNetExtensions;

internal static class EnumerableExtensions
{
    public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source) =>
        source ?? Enumerable.Empty<T>();

    public static bool IsNullOrEmpty<T>(this IEnumerable<T> source) =>
        source == null
        || !source.Any();

    public static IEnumerable<(TSource, TSource)> Pairwise<TSource>(this IEnumerable<TSource> source) =>
        source.Zip(source.Skip(1), (e, f) => (e, f));

    public static bool HasOne<T>(this IEnumerable<T> source) =>
        source?.Any() == true
        && source?.Skip(1).Any() == false;

    public static bool HasMany<T>(this IEnumerable<T> source) =>
        source?.Skip(1).Any() == true;
}