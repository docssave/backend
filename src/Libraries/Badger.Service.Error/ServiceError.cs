namespace Badger.Service.Error;

public abstract class ServiceError(string type)
{
    public string Type { get; } = type;
}