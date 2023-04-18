using System;
using Tag.Contracts;

namespace Tag.DataAccess
{
	public class Tag
	{
        public int Id { get; set; } 
        public UserId UserId { get; set; }
        public List<string> TagsList { get; set; }

        public Tag(int id, UserId userId, List<string> tagsList)
        {
            Id = id;
            UserId = userId;
            TagsList = tagsList;
        }       
    }
}

