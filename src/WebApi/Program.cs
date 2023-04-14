using Encryption.Extensions;
using Idn.Plugin;
using SqlServer;
using WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication();

builder.Services.AddDbConnectionFactory(builder.Configuration.GetConnectionString("HoneyBadgerDb"));
builder.Services.AddEncryptor();
builder.Services.AddIdentityService();

var app = builder.Build();

app.MapUsersEndpoints();
app.MapTagsEndpoints();
app.UseAuthentication();
app.UseMiddleware<UserIdAccessorMiddleware>();

app.Run();