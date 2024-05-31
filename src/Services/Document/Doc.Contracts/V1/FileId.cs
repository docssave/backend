using StronglyTypedIds;

namespace Doc.Contracts.V1;

[StronglyTypedId]
public partial struct FileId
{
    public static bool TryParse(string? value, out FileId id)
    {
        if (Guid.TryParse(value, out var guid))
        {
            id = new FileId(guid);
            return true;
        }

        id = Empty;

        return false;
    }
}