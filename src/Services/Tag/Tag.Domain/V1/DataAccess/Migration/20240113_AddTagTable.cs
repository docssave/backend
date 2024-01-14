using FluentMigrator;

namespace Col.DataAccess.Migrations;

[Migration(202305080623)]
public class AddTagTable : Migration
{
    public override void Up()
    {
        Create.Table("Collections")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("Name").AsAnsiString(100).NotNullable()
            .WithColumn("UserId").AsInt64().NotNullable()
            .WithColumn("AddedAtTimespan").AsInt64().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Tag");
    }
}