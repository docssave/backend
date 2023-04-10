using System.Reflection;
using FluentMigrator.Runner;
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
            .ScanIn(GetDataAccessAssemblies()).For.Migrations();
    })
    .AddLogging(builder => builder.AddFluentMigratorConsole())
    .BuildServiceProvider(validateScopes: false)
    .CreateScope()
    .ServiceProvider
    .GetRequiredService<IMigrationRunner>()
    .MigrateUp();

static Assembly[] GetDataAccessAssemblies()
{
    var gamingAssemblyNames = Directory
        .GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.DataAccess.dll")
        .Select(filePath =>
        {
            try
            {
                return AssemblyName.GetAssemblyName(filePath);
            }
            catch
            {
                return null;
            }
        })
        .Where(assembly => assembly != null)
        .ToArray();

    var assemblies = new List<Assembly>();

    foreach (var assemblyName in gamingAssemblyNames)
    {
        try
        {
            assemblies.Add(Assembly.Load(assemblyName));
        }
        catch (Exception e)
        {
            throw new Exception($"An exception occurred during loading {assemblyName.Name!}.", e);
        }
    }

    return assemblies.ToArray();
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

