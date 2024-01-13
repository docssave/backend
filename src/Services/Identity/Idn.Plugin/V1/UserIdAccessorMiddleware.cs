using System.Security.Claims;
using Idn.Contracts.V1;
using Microsoft.AspNetCore.Http;

namespace Idn.Plugin.V1;

public sealed class UserIdAccessorMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, IUserIdAccessor userIdAccessor)
    {
        if (context.User.Identity is ClaimsIdentity { IsAuthenticated: true } identity)
        {
            var idClaim = identity.FindFirst("id");
            userIdAccessor.UserId = UserId.TryParse(idClaim?.Value);
        }
        
        await next(context);
    }
}