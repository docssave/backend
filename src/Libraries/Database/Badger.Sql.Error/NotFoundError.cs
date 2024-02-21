namespace Badger.Sql.Error;

public class NotFoundError;

public class NotFoundError<T>(T value)
{
    public T Value { get; } = value;
}