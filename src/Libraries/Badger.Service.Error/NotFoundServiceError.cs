namespace Badger.Service.Error;

public sealed class NotFoundServiceError(string type): ServiceError(type);