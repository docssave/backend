using Idn.Contracts;
using Idn.DataAccess;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Endpoints
{
    public static class TagsEndpoints
    {
        private const string BaseRoute = "api/v1/tags";

        public static void MapTagsEndpoints(this WebApplication application)
        {
            application.MapGet($"{BaseRoute}", GetAsync);
        }

        private static Task<IActionResult> GetAsync([FromServices] IUserAccessor userAccessor)
        {
            string[] tags = TagService.GetTag(userAccessor);
            return Task.FromResult<IActionResult>(Ok(tags)); //tried to write it as return TagService.GetTag(userAccessor) but it doesn't work
        }

        private static IActionResult Ok(string[] tags) // this method IDE generated
        {
            throw new NotImplementedException();
        }
    }
}