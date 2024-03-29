﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Idn.Domain.V1.DataAccess;
using Idn.Domain.V1.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Idn.Domain.V1.Services;

public sealed class TokenService(JwtSecurityTokenHandler tokenHandler, IOptions<TokenOptions> options) : ITokenService
{
    private readonly TokenOptions _options = options.Value;

    public string Create(User user)
    {
        var jwt = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: GetClaimsIdentity(),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.ClientSecret)), SecurityAlgorithms.HmacSha256)
        );

        return tokenHandler.WriteToken(jwt);

        IReadOnlyList<Claim> GetClaimsIdentity()
        {
            return new List<Claim>
            {
                new("id", user.Id.ToString())
            };
        }
    }
}