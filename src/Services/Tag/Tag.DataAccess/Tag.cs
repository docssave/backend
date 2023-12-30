namespace Tag.DataAccess;

public class Tag
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<string> Tags { get; set; }

    public Tag(int id, int userId, List<string> tags)
    {
        Id = id;
        UserId = userId;
        Tags = tags;
    }
}