using System.Text;
using Idn.Plugin;
using Microsoft.IdentityModel.Tokens;
using SqlServer;
using WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "Denis",
            ValidateAudience = true,
            ValidAudience = "Audience",
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("c38b6e91-1380-47d6-b8b8-325aee0eab5481eb4361-a72b-4697-8b2d-dfa693001346a576dc63-64fb-4655-a3ef-efaeeb8e2585068098df-688f-42af-87df-eb16717bf98a")),
            ValidateLifetime = false
        };
    });

builder.Services.AddDbConnectionFactory(builder.Configuration.GetConnectionString("HoneyBadgerDb"));
builder.Services.AddIdentityService();

var app = builder.Build();

app.MapUsersEndpoints();
app.MapTagsEndpoints();
app.UseAuthentication();
app.UseMiddleware<UserIdAccessorMiddleware>();

await app.RunAsync();