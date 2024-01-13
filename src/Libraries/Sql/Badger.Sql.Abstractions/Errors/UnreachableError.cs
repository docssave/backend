namespace Badger.Sql.Abstractions.Errors;

public sealed class UnreachableError(string reason)
{
    public string Reason { get; } = reason;
}