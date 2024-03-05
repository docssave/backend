namespace Badger.Sql.Error;

public sealed class UnreachableDatabaseError(string reason)
{
    public string Reason { get; } = reason;
}