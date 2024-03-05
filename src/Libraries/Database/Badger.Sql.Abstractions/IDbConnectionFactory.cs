using System.Data;

namespace Badger.Sql.Abstractions;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateAsync();
}