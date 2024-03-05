namespace Badger.Sql.Error;

public sealed class ConflictDatabaseError;

public sealed class ConflictDatabaseError<T>(T value)
{
    public T Value { get; } = value;
}