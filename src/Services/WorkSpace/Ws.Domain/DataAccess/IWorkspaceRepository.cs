﻿using Badger.Sql.Abstractions.Errors;
using Idn.Contracts;
using OneOf;
using OneOf.Types;
using Ws.Contracts;

namespace Ws.Domain.DataAccess;

internal interface IWorkspaceRepository
{
    Task<OneOf<IReadOnlyList<Workspace>, UnreachableError>> ListAsync(UserId userId);

    Task<OneOf<Success, UnreachableError>> RegisterAsync(WorkspaceId id, string name,  UserId userId, DateTimeOffset registeredAt);
}