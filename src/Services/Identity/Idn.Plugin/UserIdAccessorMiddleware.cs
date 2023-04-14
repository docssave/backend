using System.Security.Claims;
using Idn.Contracts;
using Microsoft.AspNetCore.Http;

namespace Idn.Plugin;

public sealed class UserIdAccessorMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IUserIdAccessor _userIdAccessor;

    public UserIdAccessorMiddleware(RequestDelegate next, IUserIdAccessor userIdAccessor)
    {
        _next = next;
        _userIdAccessor = userIdAccessor;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.User.Identity is ClaimsIdentity { IsAuthenticated: true } identity)
        {
            var idClaim = identity.FindFirst("sub");
            _userIdAccessor.UserId = UserId.TryParse(idClaim?.Value);
        }
        
        await _next(context);
    }
}