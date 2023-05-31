using System.Data;
using FluentMigrator;

namespace Col.DataAccess.Migrations;

[Migration(202305310638)]
public sealed class AddCollectionPatchesTable : Migration
{
    public override void Up()
    {
        Create.Table("CollectionPatches")
            .WithColumn("CollectionId").AsGuid().ForeignKey("Collections", "Id").OnDeleteOrUpdate(Rule.Cascade)
            .WithColumn("Patch").AsAnsiString()
            .WithColumn("UserId").AsInt64()
            .WithColumn("AddedAtTimespan").AsInt64();

        Create.PrimaryKey("CollectionPatches_PrimaryKey")
            .OnTable("CollectionPatches")
            .Columns("CollectionId", "AddedAtTimespan");
    }

    public override void Down()
    {
        Delete.Table("CollectionPatches");
    }
}