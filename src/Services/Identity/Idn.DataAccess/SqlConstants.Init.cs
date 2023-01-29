namespace Idn.DataAccess;

internal static partial class SqlConstants
{
    public static string CreateUsersTableQuery =
        "CREATE TABLE IF NOT EXISTS Users ("+
        "    Id BIGINT IS NOT NULL," +
        "    Name VARCHAR(500) IS NOT NULL," +
        "    EncryptedEmail VARCHAR(320) IS NOT NULL,"+
        "    Source VARCHAR(12) IS NOT NULL,"+
        "   PRIMARY KEY (Id)"+
        ");";
}