using System.Data;

namespace SqlServer.Abstraction.Extensions;

public static class DbConnectionFactoryExtensions
{
    public static async Task<T> TryAsync<T>(this IDbConnectionFactory dbConnectionFactory, Func<IDbConnection, Task<T>> sqlFunc, Func<Exception, T> exceptionFunc)
    {
        try
        {
            using var connection = await dbConnectionFactory.CreateAsync();

            return await sqlFunc(connection);
        }
        catch (Exception e)
        {
            return exceptionFunc(e);
        }
    }
}