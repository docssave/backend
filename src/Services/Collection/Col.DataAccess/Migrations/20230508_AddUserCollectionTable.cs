using FluentMigrator;

namespace Col.DataAccess.Migrations;

[Migration(20230508)]
public class AddUserCollectionTable : Migration
{
    public override void Up()
    {
        Create.Table("UserCollections")
            .WithColumn("UserId").AsInt64()
            .WithColumn("CollectionId").AsGuid();

        Create.PrimaryKey("UserCollections_PrimaryKey")
            .OnTable("UserCollections")
            .Columns("UserId", "CollectionId");
    }

    public override void Down()
    {
        Delete.Table("UserCollections");
    }
}