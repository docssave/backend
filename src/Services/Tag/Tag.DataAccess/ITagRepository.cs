using SqlServer.Abstraction;

namespace Tag.DataAccess;

public interface ITagRepository
{
    Task<RepositoryResult<Tag?>> GetTagListAsync(string sourceTagByUserId);

    Task<RepositoryResult<Tag>> CreateTagAsync(CreateTag createTag);
}