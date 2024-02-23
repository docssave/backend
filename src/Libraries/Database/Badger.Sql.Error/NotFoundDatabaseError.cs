namespace Badger.Sql.Error;

public sealed class NotFoundDatabaseError;

public sealed class NotFoundDatabaseError<T>(T value)
{
    public T Value { get; } = value;
}