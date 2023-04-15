namespace SqlServer;

public sealed class RepositoryResult<T>
{
    public RepositoryResult(T value, Exception? exception)
    {
        Value = value;
        Exception = exception;
    }

    public T Value { get; }
    
    public Exception? Exception { get; }

    public bool IsSuccess => Exception == null;
}