using System;
using Idn.Contracts;

namespace WebApi.Services
{
	public class TagService
	{
		public static string[] GetTag(IUserAccessor userAccessor)
        {
            //here will be call DB by userAccessor.GetUserId()
            string[] tagArray = new string[] { "important", "private", "favorite" };
            return tagArray;
        }
    }
}

