using System.Text;
using Badger.Clock;
using Badger.MySql;
using Col.Plugin;
using Idn.Plugin;
using Idn.Plugin.V1;
using Idn.Plugin.V1.Endpoints;
using Idn.Plugin.V1.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Endpoints;
using Ws.Plugin;

var builder = WebApplication.CreateBuilder(args);

#region JWT
var tokenOptions = builder.Configuration .GetSection(TokenOptions.SectionName);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = tokenOptions["Issuer"],
            ValidateAudience = true,
            ValidAudience = tokenOptions["Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenOptions["ClientSecret"])),
            ValidateLifetime = false
        };
    });

builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection(TokenOptions.SectionName));
#endregion

builder.Services.AddDbConnectionFactory(builder.Configuration.GetConnectionString("HoneyBadgerDb"));
builder.Services.AddIdentityService();
builder.Services.AddWorkspaceService();
builder.Services.AddCollectionService();
builder.Services.AddClock();

var app = builder.Build();

app.MapUsersEndpoints();
app.MapTagsEndpoints();
app.UseAuthentication();
app.UseMiddleware<UserIdAccessorMiddleware>();

await app.RunAsync();