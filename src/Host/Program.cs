using Badger.Clock;
using Badger.MySql;
using Col.Plugin;
using Idn.Plugin.V1.Extensions;
using Ws.Plugin;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbConnectionFactory(builder.Configuration.GetConnectionString("HoneyBadgerDb"));
builder.Services.AddClock();

builder.Services.AddIdentityService(sectionName => builder.Configuration.GetSection(sectionName));
builder.Services.AddWorkspaceService();
builder.Services.AddCollectionService();

var app = builder.Build();

app.UseAuthentication();
app.UseIdentity();

await app.RunAsync();