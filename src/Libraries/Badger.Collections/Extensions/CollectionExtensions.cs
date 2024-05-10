namespace Badger.Collections.Extensions;

public static class CollectionExtensions
{
    public static IReadOnlyList<T> ToReadOnlyList<T>(this IEnumerable<T> source)
    {
        if (source is IReadOnlyList<T> list)
        {
            return list;
        }

        return source.ToList();
    }
    
    public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> source) =>
        source.ToReadOnlyList().AsReadOnlyCollection();

    private static IReadOnlyCollection<T> AsReadOnlyCollection<T>(this IReadOnlyCollection<T> source) =>
        source;
}