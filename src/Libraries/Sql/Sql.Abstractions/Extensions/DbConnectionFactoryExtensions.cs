using System.Data;
using System.Data.Common;
using OneOf;
using Sql.Abstractions.Errors;

namespace Sql.Abstractions.Extensions;

public static class DbConnectionFactoryExtensions
{
    public static async Task<T> TryAsync<T>(
        this IDbConnectionFactory dbConnectionFactory,
        Func<IDbConnection, Task<T>> sqlFunc,
        Func<Exception, T> exceptionFunc)
    {
        try
        {
            using var connection = await dbConnectionFactory.CreateAsync();

            return await sqlFunc(connection);
        }
        catch (DbException e)
        {
            return exceptionFunc(e);
        }
    }
    
    public static async Task<OneOf<T, UnreachableError>> TryAsync<T>(
        this IDbConnectionFactory dbConnectionFactory,
        Func<IDbConnection, Task<T>> sqlFunc,
        Func<Exception, UnreachableError> exceptionFunc)
    {
        try
        {
            using var connection = await dbConnectionFactory.CreateAsync();

            return await sqlFunc(connection);
        }
        catch (DbException e)
        {
            return exceptionFunc(e);
        }
    }
    
    public static async Task<OneOf<T, TError, UnreachableError>> TryAsync<T, TError>(
        this IDbConnectionFactory dbConnectionFactory,
        Func<IDbConnection, Task<OneOf<T, TError>>> sqlFunc,
        Func<Exception, UnreachableError> exceptionFunc)
    {
        try
        {
            using var connection = await dbConnectionFactory.CreateAsync();

            var result = await sqlFunc(connection);

            return result.Match(OneOf<T, TError, UnreachableError>.FromT0, OneOf<T, TError, UnreachableError>.FromT1);
        }
        catch (DbException e)
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
            connection.Open();
            
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
            finally
            {
                connection.Close();
            }
        }, exceptionFunc);
    
    public static async Task<OneOf<T, UnreachableError>> TryAsync<T>(
        this IDbConnectionFactory dbConnectionFactory,
        Func<IDbConnection, IDbTransaction, Task<T>> sqlFunc,
        Func<Exception, UnreachableError> exceptionFunc) =>
        await dbConnectionFactory.TryAsync(async connection =>
        {
            connection.Open();
            
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
            finally
            {
                connection.Close();
            }
        }, exceptionFunc);
}