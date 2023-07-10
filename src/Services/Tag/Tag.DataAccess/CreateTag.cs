namespace Tag.DataAccess;

public class CreateTag
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<string> Tags { get; set; }

    public CreateTag(int id, int userId, List<string> tags)
    {
        Id = id;
        UserId = userId;
        Tags = tags;
    }
}