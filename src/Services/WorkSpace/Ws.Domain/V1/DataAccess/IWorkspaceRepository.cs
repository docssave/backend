using Badger.OneOf.Types;
using Idn.Contracts.V1;
using OneOf;
using OneOf.Types;
using Ws.Contracts.V1;

namespace Ws.Domain.V1.DataAccess;

internal interface IWorkspaceRepository
{
    Task<OneOf<IReadOnlyList<Workspace>, Unreachable<string>>> ListAsync(UserId userId);

    Task<OneOf<Success, Unreachable<string>>> RegisterAsync(WorkspaceId id, string name,  UserId userId, DateTimeOffset registeredAt);
}