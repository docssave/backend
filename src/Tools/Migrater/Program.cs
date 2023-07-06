using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

new ServiceCollection()
    .AddFluentMigratorCore()
    .ConfigureRunner(migrationRunner =>
    {
        migrationRunner
            .AddMySql5()
            .WithGlobalConnectionString("server=localhost;port=3306;uid=root;database=DocsSave;")
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

