using StronglyTypedIds;

namespace Col.Contracts.V1;

[StronglyTypedId]
public partial struct CollectionId
{
    public static bool TryParse(string? value, out CollectionId id)
    {
        if (Guid.TryParse(value, out var guid))
        {
            id = new CollectionId(guid);
            return true;
        }

        id = CollectionId.Empty;

        return false;
    }
}