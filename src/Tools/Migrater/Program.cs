using System.Reflection;
using FluentMigrator.Runner;
using Idn.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.SqlClient;

await CreateDatabaseAsync();

new ServiceCollection()
    .AddFluentMigratorCore()
    .ConfigureRunner(migrationRunner =>
    {
        migrationRunner
            .AddSqlServer2016()
            .WithGlobalConnectionString("Data Source=localhost\\SQL2016;Initial Catalog=DocsSave;Integrated Security=True")
            .ScanIn(GetDataAccessAssemblies().ToArray()).For.Migrations();
    })
    .AddLogging(builder => builder.AddFluentMigratorConsole())
    .BuildServiceProvider(validateScopes: false)
    .CreateScope()
    .ServiceProvider
    .GetRequiredService<IMigrationRunner>()
    .MigrateUp();

static IEnumerable<Assembly> GetDataAccessAssemblies()
{
    yield return typeof(IIdentityRepository).Assembly;
}

static async Task CreateDatabaseAsync()
{
    await using var connection = new SqlConnection("Data Source=localhost\\SQL2016;Integrated Security=True");
    await using var command = connection.CreateCommand();

    await connection.OpenAsync();
    
    command.CommandText =
        @"IF NOT EXISTS(SELECT * FROM SYS.DATABASES WHERE name = 'DocsSave')
            BEGIN
                CREATE DATABASE DocsSave
            END;";

    await command.ExecuteNonQueryAsync();
}

