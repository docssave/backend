using Idn.Contracts;

namespace Idn.Plugin.V1;

internal sealed class UserIdAccessor : IUserIdAccessor
{
    public UserId? UserId { get; set; }
}