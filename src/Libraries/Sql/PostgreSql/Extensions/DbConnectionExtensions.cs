using System.Data;

namespace PostgreSql.Extensions;

public static class DbConnectionExtensions
{
    public static async Task<T> TryAsync<T>(this IDbConnectionFactory dbConnectionFactory, Func<IDbConnection, Task<T>> func, Action<Exception> errorAction)
    {
        try
        {
            using var connection = await dbConnectionFactory.CreateAsync();

            return await func(connection);
        }
        catch (Exception e)
        {
            errorAction(e);
        }

        return default;
    }
}