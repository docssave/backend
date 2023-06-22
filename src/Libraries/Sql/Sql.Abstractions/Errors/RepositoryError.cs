namespace Sql.Abstractions.Errors;

public abstract class RepositoryError
{
    private RepositoryError(string reason)
    {
        Reason = reason;
    }
    
    public string Reason { get; }
    
    public sealed class Unreachable : RepositoryError
    {
        public Unreachable(string reason)
            : base(reason)
        {
        }
    }
}