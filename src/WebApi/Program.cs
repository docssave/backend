using PostgreSql;
using PostgreSql.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SqlOptions>(builder.Configuration.GetSection(SqlOptions.SectionName));

builder.Services.AddDbConnectionFactory(builder.Configuration.Get<SqlOptions>()!);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();