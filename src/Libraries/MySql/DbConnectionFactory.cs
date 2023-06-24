using System.Data;
using MySql.Data.MySqlClient;
using Sql.Abstractions;

namespace MySql;

internal sealed class DbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;
    private IDbConnection? _dbConnection;

    public DbConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public Task<IDbConnection> CreateAsync()
    {
        if (_dbConnection is not { State: ConnectionState.Open })
        {
            _dbConnection = new MySqlConnection(_connectionString);
        }

        return Task.FromResult(_dbConnection);
    }
}