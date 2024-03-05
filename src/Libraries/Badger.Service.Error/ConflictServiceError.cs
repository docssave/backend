namespace Badger.Service.Error;

public sealed class ConflictServiceError(string type): ServiceError(type);