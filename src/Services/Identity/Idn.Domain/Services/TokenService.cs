using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Idn.Contracts.Options;
using Idn.DataAccess;
using Idn.Domain.DataAccess;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Idn.Domain.Services;

public sealed class TokenService : ITokenService
{
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly TokenOptions _options;

    public TokenService(JwtSecurityTokenHandler tokenHandler, IOptions<TokenOptions> options)
    {
        _tokenHandler = tokenHandler;
        _options = options.Value;
    }
    
    public string Create(User user)
    {
        var jwt = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: GetClaimsIdentity(),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.ClientSecret)), SecurityAlgorithms.HmacSha256)
        );

        return _tokenHandler.WriteToken(jwt);

        IReadOnlyList<Claim> GetClaimsIdentity()
        {
            return new List<Claim>
            {
                new("id", user.Id.ToString())
            };
        }
    }
}