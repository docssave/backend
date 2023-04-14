using System.Data;

namespace SqlServer.Abstraction;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateAsync();
}