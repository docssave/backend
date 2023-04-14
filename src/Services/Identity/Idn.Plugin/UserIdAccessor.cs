using Idn.Contracts;

namespace Idn.Plugin;

internal sealed class UserIdAccessor : IUserIdAccessor
{
    public UserId? UserId { get; set; }
}