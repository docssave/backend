using System.Data;

namespace Sql;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateAsync();
}