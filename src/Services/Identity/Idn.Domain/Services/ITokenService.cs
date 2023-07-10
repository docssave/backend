using Idn.DataAccess;
using Idn.Domain.DataAccess;

namespace Idn.Domain.Services;

internal interface ITokenService
{
    string Create(User user);
}