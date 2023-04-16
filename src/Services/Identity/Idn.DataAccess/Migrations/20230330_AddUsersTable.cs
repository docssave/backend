using FluentMigrator;

namespace Idn.DataAccess.Migrations;

[Migration(202303300824)]
public sealed class AddUsersTable : Migration
{
    public override void Up()
    {
        Create.Table("Users")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Name").AsAnsiString(500).NotNullable()
            .WithColumn("EncryptedEmail").AsAnsiString(320).NotNullable()
            .WithColumn("Source").AsAnsiString(12).NotNullable()
            .WithColumn("SourceUserId").AsAnsiString(36).NotNullable()
            .WithColumn("RegisteredAt").AsInt64().NotNullable();

        Create.Index("Users_SourceUserId_Index")
            .OnTable("Users")
            .OnColumn("SourceUserId").Unique();
    }

    public override void Down()
    {
        Delete.Index("Users_SourceUserId_Index");
        Delete.Table("Users");
    }
}