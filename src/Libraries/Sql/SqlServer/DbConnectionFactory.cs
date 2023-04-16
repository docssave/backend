using System.Data;
using Microsoft.Data.SqlClient;
using SqlServer.Abstraction;

namespace SqlServer;

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
            _dbConnection = new SqlConnection(_connectionString);
        }

        return Task.FromResult(_dbConnection);
    }
}