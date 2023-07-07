using Idn.DataAccess;

namespace Idn.Domain.Services;

internal interface ITokenService
{
    string Create(User user);
}