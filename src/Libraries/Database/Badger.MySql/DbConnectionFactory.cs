using System.Data;
using MySql.Data.MySqlClient;
using Badger.Sql.Abstractions;

namespace Badger.MySql;

internal sealed class DbConnectionFactory(string connectionString) : IDbConnectionFactory
{
    private IDbConnection? _dbConnection;

    public Task<IDbConnection> CreateAsync()
    {
        if (_dbConnection is not { State: ConnectionState.Open })
        {
            _dbConnection = new MySqlConnection(connectionString);
        }

        return Task.FromResult(_dbConnection);
    }
}