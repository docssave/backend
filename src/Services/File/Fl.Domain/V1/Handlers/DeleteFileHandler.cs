using Badger.Sql.Abstractions.Errors;
using Fl.Contracts.V1;
using Fl.Domain.V1.DataAccess;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace Fl.Domain.V1.Handlers;

internal sealed class DeleteFileHandler(IFileRepository repository, ILogger<DeleteFileHandler> logger) : IRequestHandler<DeleteFileRequest, OneOf<Success, Error<string>>>
{
    public async Task<OneOf<Success, Error<string>>> Handle(DeleteFileRequest request, CancellationToken cancellationToken)
    {
        if (request.FileId == FileId.Empty)
        {
            logger.LogError("FileId can not be empty");

            return new Error<string>("Could not delete file, because of empty FileId.");
        }

        var result = await repository.DeleteAsync(request.FileId);

        return result.MapT1(ToError);
        
        Error<string> ToError(UnreachableError error)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(IFileRepository), error.Reason);

            return new(error.Reason);
        }
    }
}