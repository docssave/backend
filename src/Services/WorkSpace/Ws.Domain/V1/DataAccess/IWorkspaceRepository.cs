using Badger.Sql.Error;
using Idn.Contracts.V1;
using OneOf;
using OneOf.Types;
using Ws.Contracts.V1;

namespace Ws.Domain.V1.DataAccess;

internal interface IWorkspaceRepository
{
    Task<OneOf<IReadOnlyList<Workspace>, UnreachableDatabaseError>> ListAsync(UserId userId);

    Task<OneOf<Success, UnreachableDatabaseError>> RegisterAsync(WorkspaceId id, string name,  UserId userId, DateTimeOffset registeredAt);
}