using System;

namespace Tag.Extensions;

	public class TagService
	{
		public static string[] GetTag(IUserAccessor userAccessor)
    {
        //here will be call DB by userAccessor.GetUserId()
        string[] tagArray = new string[] { "important", "private", "favorite" };
        return tagArray;
    }
}