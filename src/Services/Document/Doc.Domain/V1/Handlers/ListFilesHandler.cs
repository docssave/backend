﻿using Badger.OneOf.Types;
using Doc.Contracts.V1;
using Doc.Domain.V1.DataAccess;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using File = Doc.Contracts.V1.File;

namespace Doc.Domain.V1.Handlers;

internal sealed class ListFilesHandler(IFileRepository repository, ILogger<ListFilesHandler> logger) 
    : IRequestHandler<ListFilesRequest, OneOf<IReadOnlyList<File>, Error<string>>>
{
    public async Task<OneOf<IReadOnlyList<File>, Error<string>>> Handle(ListFilesRequest request, CancellationToken cancellationToken)
    {
        if (request.DocumentId == DocumentId.Empty)
        {
            logger.LogError("DocumentId can not be empty");

            return new Error<string>("Could not find files for empty document.");
        }

        var result = await repository.ListAsync(request.DocumentId);

        return result.MapT1(ToError);
        
        Error<string> ToError(Unreachable<string> error)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(IFileRepository), error.Value);

            return new(error.Value);
        }
    }
}