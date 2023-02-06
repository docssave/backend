using System.Data;
using Npgsql;

namespace PostgreSql;

public sealed class DbConnectionFactory : IDbConnectionFactory
{
    private readonly NpgsqlConnectionStringBuilder _connectionStringBuilder;

    public DbConnectionFactory(NpgsqlConnectionStringBuilder connectionStringBuilder)
    {
        _connectionStringBuilder = connectionStringBuilder;
    }
    
    public Task<IDbConnection> CreateAsync()
    {
        IDbConnection connection = new NpgsqlConnection(_connectionStringBuilder.ConnectionString);

        return Task.FromResult(connection);
    }
}