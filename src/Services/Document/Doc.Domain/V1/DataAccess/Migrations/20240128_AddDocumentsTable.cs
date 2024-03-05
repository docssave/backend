using FluentMigrator;

namespace Doc.Domain.V1.DataAccess.Migrations;

[Migration(202401280820)]
public sealed class AddDocumentsTable : Migration 
{
    public override void Up()
    {
        Create.Table("Documents")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("CollectionId").AsGuid().NotNullable()
            .WithColumn("Name").AsAnsiString(100).NotNullable()
            .WithColumn("Icon").AsAnsiString(100).NotNullable()
            .WithColumn("Version").AsInt64().NotNullable()
            .WithColumn("RegisteredAtTimespan").AsInt64().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Documents");
    }
}