using StronglyTypedIds;

namespace Doc.Contracts.V1;

[StronglyTypedId]
public partial struct DocumentId
{
    public static bool TryParse(string? value, out DocumentId id)
    {
        if (Guid.TryParse(value, out var guid))
        {
            id = new DocumentId(guid);
            return true;
        }

        id = DocumentId.Empty;

        return false;
    }
}