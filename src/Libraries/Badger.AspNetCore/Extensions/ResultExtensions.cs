using System.Net;
using Microsoft.AspNetCore.Http;

namespace Badger.AspNetCore.Extensions;

public static class ResultExtensions
{
    public static IResult RetryLate(this IResultExtensions _) => Results.StatusCode((int)HttpStatusCode.ServiceUnavailable);

    public static IResult Unknown(this IResultExtensions _) => Results.StatusCode((int)HttpStatusCode.InternalServerError);
}