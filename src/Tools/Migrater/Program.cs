using FluentMigrator.Runner;
using Idn.DataAccess.Migrations;
using Microsoft.Extensions.DependencyInjection;

new ServiceCollection()
    .AddFluentMigratorCore()
    .ConfigureRunner(migrationRunner =>
    {
        migrationRunner
            .AddSqlServer2016()
            .WithGlobalConnectionString("Data Source=localhost\\SQL2016;Initial Catalog=DocSave;Integrated Security=True")
            .ScanIn(typeof(AddUsersTable).Assembly).For.Migrations();
    })
    .AddLogging(loggingBuilder => loggingBuilder.AddFluentMigratorConsole())
    .BuildServiceProvider(validateScopes: false)
    .CreateScope()
    .ServiceProvider
    .GetRequiredService<IMigrationRunner>()
    .MigrateUp();