namespace Badger.Sql.Error;

public class ConflictError;

public class ConflictError<T>(T value)
{
    public T Value { get; } = value;
}