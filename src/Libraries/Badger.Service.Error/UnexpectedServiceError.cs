namespace Badger.Service.Error;

public sealed class UnexpectedServiceError(string type) : ServiceError(type);