using Badger.Clock;
using Badger.Sql.Abstractions.Errors;
using Fl.Contracts.V1;
using Fl.Domain.V1.DataAccess;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fl.Domain.V1.Handlers;

internal sealed class RegisterFilesHandler(IFileRepository repository, IClock clock) 
    : IRequestHandler<UploadFilesRequest, OneOf<Success, Error<string>>>
{
    public async Task<OneOf<Success, Error<string>>> Handle(UploadFilesRequest request, CancellationToken cancellationToken)
    {
        var result = await repository.RegisterAsync(request.DocumentId, request.Files, clock.Now);

        return result.Match(success => success, ToError);

        OneOf<Success, Error<string>> ToError(UnreachableError unreachableError) => new Error<string>(unreachableError.Reason);
    }
}