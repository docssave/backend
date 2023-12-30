using Idn.Domain.V1.DataAccess;

namespace Idn.Domain.V1.Services;

internal interface ITokenService
{
    string Create(User user);
}