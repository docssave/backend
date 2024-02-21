namespace Badger.Sql.Error;

public sealed class UnreachableError(string reason)
{
    public string Reason { get; } = reason;
}