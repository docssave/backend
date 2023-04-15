using Idn.DataAccess;

namespace Idn.Domain;

internal interface ITokenService
{
    string Create(User user);
}