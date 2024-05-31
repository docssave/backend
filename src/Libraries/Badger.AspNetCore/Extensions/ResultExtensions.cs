using System.Net;
using Badger.OneOf.Types;
using Microsoft.AspNetCore.Http;
using OneOf.Types;

namespace Badger.AspNetCore.Extensions;

public static class ResultExtensions
{
    public static IResult RetryLate(this IResultExtensions extensions, Unreachable _) => extensions.RetryLate();

    public static IResult RetryLate(this IResultExtensions _) => Results.StatusCode((int)HttpStatusCode.ServiceUnavailable);

    public static IResult Unknown(this IResultExtensions extensions, Unknown _) => extensions.Unknown(); 
    
    public static IResult Unknown(this IResultExtensions _) => Results.StatusCode((int)HttpStatusCode.InternalServerError);
}