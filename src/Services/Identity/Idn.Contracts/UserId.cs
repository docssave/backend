using StronglyTypedIds;

namespace Idn.Contracts;

[StronglyTypedId(StronglyTypedIdBackingType.Long)]
public partial struct UserId
{
    public static UserId? TryParse(string? value) => long.TryParse(value, out var id) ? new UserId(id) : null;
}