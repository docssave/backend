using System.Data.Common;

namespace Tag.Contracts;

public record TagResponse(Tag tag) : ResponseBase<string>;

