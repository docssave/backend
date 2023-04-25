using Common;

namespace TagContracts;

public record TagResponse(string Name, Error? Error) : ResponseBase(Error);