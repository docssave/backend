using FluentMigrator;

namespace Col.DataAccess.Migrations;

[Migration(202305080623)]
public class AddCollectionTable : Migration
{
    public override void Up()
    {
        Create.Table("Collections")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("Name").AsAnsiString(100).NotNullable()
            .WithColumn("Icon").AsAnsiString(100).NotNullable()
            .WithColumn("EncryptSide").AsAnsiString(10).NotNullable()
            .WithColumn("Version").AsInt32().NotNullable()
            .WithColumn("AddedAtTimespan").AsInt64().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Collections");
    }
}