using Badger.Sql.Error;
using Idn.Contracts.V1;
using OneOf;
using OneOf.Types;
using Ws.Contracts.V1;

namespace Ws.Domain.V1.DataAccess;

internal interface IWorkspaceRepository
{
    Task<OneOf<IReadOnlyList<Workspace>, UnreachableError>> ListAsync(UserId userId);

    Task<OneOf<Success, UnreachableError>> RegisterAsync(WorkspaceId id, string name,  UserId userId, DateTimeOffset registeredAt);
}