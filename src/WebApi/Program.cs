using Encryption.Extensions;
using Idn.Extensions;
using PostgreSql;
using PostgreSql.Extensions;
using WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

builder.Services.Configure<SqlOptions>(builder.Configuration.GetSection(SqlOptions.SectionName));

builder.Services.AddDbConnectionFactory(builder.Configuration.Get<SqlOptions>()!);
builder.Services.AddEncryptor();
builder.Services.AddIdentityService();

var app = builder.Build();

app.MapUsersEndpoints();
app.MapTagsEndpoints();
app.UseAuthorization();

app.Run();