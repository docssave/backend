using System.Data;

namespace PostgreSql;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateAsync();
}