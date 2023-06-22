using System.Data;

namespace Sql.Abstractions;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateAsync();
}