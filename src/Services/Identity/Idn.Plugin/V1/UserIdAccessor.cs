using Idn.Contracts;
using Idn.Contracts.V1;

namespace Idn.Plugin.V1;

internal sealed class UserIdAccessor : IUserIdAccessor
{
    public UserId? UserId { get; set; }
}