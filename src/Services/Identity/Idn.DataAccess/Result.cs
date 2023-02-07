namespace Idn.DataAccess;

public sealed class Result<T>
{
    public Result(T value, Exception? exception)
    {
        Value = value;
        Exception = exception;
    }

    public T Value { get; }
    
    public Exception? Exception { get; }

    public bool IsSuccess => Exception == null;
}