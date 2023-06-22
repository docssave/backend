namespace Collections.Extensions;

public static class CollectionExtensions
{
    public static IReadOnlyList<T> ToReadonlyList<T>(this IEnumerable<T> source)
    {
        if (source is IReadOnlyList<T> list)
        {
            return list;
        }

        return source.ToList();
    }
}