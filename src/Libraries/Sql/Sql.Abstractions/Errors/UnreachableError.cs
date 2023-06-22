namespace Sql.Abstractions.Errors;

public sealed class UnreachableError
{
    public UnreachableError(string reason)
    {
        Reason = reason;
    }

    public string Reason { get; }
}