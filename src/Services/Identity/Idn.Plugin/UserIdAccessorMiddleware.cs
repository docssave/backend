using System.Security.Claims;
using Idn.Contracts;
using Microsoft.AspNetCore.Http;

namespace Idn.Plugin;

public sealed class UserIdAccessorMiddleware
{
    private readonly RequestDelegate _next;

    public UserIdAccessorMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserIdAccessor userIdAccessor)
    {
        if (context.User.Identity is ClaimsIdentity { IsAuthenticated: true } identity)
        {
            var idClaim = identity.FindFirst("id");
            userIdAccessor.UserId = UserId.TryParse(idClaim?.Value);
        }
        
        await _next(context);
    }
}