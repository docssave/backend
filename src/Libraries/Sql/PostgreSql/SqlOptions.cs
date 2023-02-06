namespace PostgreSql;

public sealed class SqlOptions
{
    public static string SectionName => "Sql";

    public string ConnectionString { get; set; } = null!;
}