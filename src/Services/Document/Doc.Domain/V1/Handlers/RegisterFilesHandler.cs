﻿using Badger.Clock;
using Badger.OneOf.Types;
using Doc.Contracts.V1;
using Doc.Domain.V1.DataAccess;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace Doc.Domain.V1.Handlers;

internal sealed class RegisterFilesHandler(IFileRepository repository, ILogger<RegisterFilesHandler> logger, IClock clock) 
    : IRequestHandler<UploadFilesRequest, OneOf<Success, Error<string>>>
{
    public async Task<OneOf<Success, Error<string>>> Handle(UploadFilesRequest request, CancellationToken cancellationToken)
    {
        if (request.DocumentId == DocumentId.Empty)
        {
            logger.LogError("DocumentId can not be empty");
            return new Error<string>("Could not register files for empty document.");
        }
        
        var result = await repository.RegisterAsync(request.DocumentId, request.Files, clock.Now);

        return result.MapT1(ToError);

        Error<string> ToError(Unreachable<string> unreachableError)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(IFileRepository), unreachableError.Value);
            return new(unreachableError.Value);
        }
    }
}