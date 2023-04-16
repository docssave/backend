using System.Text;
using Idn.Contracts.Options;
using Idn.Plugin;
using Microsoft.IdentityModel.Tokens;
using SqlServer;
using WebApi.Endpoints;

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

var app = builder.Build();

app.MapUsersEndpoints();
app.MapTagsEndpoints();
app.UseAuthentication();
app.UseMiddleware<UserIdAccessorMiddleware>();

await app.RunAsync();