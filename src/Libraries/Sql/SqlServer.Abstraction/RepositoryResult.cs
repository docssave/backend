namespace SqlServer.Abstraction;

public sealed class RepositoryResult<T> where T : class
{
    public RepositoryResult(T value, Exception? exception)
    {
        Value = value;
        Exception = exception;
    }

    public static RepositoryResult<T> Success(T value) => new(value, null);

    public static RepositoryResult<T> Failed(Exception exception) => new(null, exception);

    public T Value { get; }
    
    public Exception? Exception { get; }

    public bool IsSuccess => Exception == null;
}