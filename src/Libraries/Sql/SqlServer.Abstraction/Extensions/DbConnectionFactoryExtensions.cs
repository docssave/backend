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

    public static async Task<T> TryAsync<T>(
        this IDbConnectionFactory dbConnectionFactory,
        Func<IDbConnection, IDbTransaction, Task<T>> sqlFunc,
        Func<Exception, T> exceptionFunc) =>
        await dbConnectionFactory.TryAsync(async connection =>
        {
            var transaction = connection.BeginTransaction();

            try
            {
                var result = await sqlFunc(connection, transaction);
                
                transaction.Commit();
 
                return result;
            }
            catch (Exception e)
            {
                transaction.Rollback();

                throw e;
            }
        }, exceptionFunc);
}