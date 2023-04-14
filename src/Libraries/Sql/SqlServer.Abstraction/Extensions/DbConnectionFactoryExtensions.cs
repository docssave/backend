using System.Data;

namespace SqlServer.Abstraction.Extensions;

public static class DbConnectionFactoryExtensions
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