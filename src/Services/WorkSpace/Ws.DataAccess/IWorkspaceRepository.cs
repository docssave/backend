﻿using Idn.Contracts;
using Sql.Abstractions;

namespace Ws.DataAccess;

public interface IWorkspaceRepository
{
    Task<RepositoryResult<Workspace?>> GetWorkspaceAsync(UserId userId);

    Task<RepositoryResult<Workspace>> CreateWorkspaceAsync(UserId userId, string name);
}