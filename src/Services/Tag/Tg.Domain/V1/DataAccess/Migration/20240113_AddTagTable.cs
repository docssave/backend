using FluentMigrator;

namespace Tg.Domain.V1.DataAccess.Migration;

[Migration(202305080623)]
public class AddTagsTable : FluentMigrator.Migration
{
    public override void Up()
    {
        Create.Table("Tags")
            .WithColumn("Value").AsAnsiString(100).NotNullable()
            .WithColumn("UserId").AsInt64().NotNullable();

        Create.PrimaryKey("Tags_PrimaryKey")
            .OnTable("Tags")
            .Columns("Value", "UserId");
    }

    public override void Down()
    {
        Delete.Table("Tags");
    }
}