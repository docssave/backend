using System.Data;
using FluentMigrator;

namespace Col.DataAccess.Migrations;

[Migration(202305310645)]
public sealed class AddTagCollectionsTable : Migration
{
    public override void Up()
    {
        Create.Table("TagCollections")
            .WithColumn("TagId").AsInt64()
            .WithColumn("CollectionId").AsGuid().ForeignKey("Collections", "Id").OnDeleteOrUpdate(Rule.Cascade);

        Create.PrimaryKey("TagCollections_PrimaryKey")
            .OnTable("TagCollections")
            .Columns("TagId", "CollectionId");
    }

    public override void Down()
    {
        Delete.Table("TagCollections");
    }
}