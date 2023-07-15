using FluentMigrator;

namespace Ws.DataAccess.Migrations;

[Migration(202304170830)]
public sealed class AddWorkspacesTable : Migration
{
    public override void Up()
    {
        Create.Table("Workspaces")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Name").AsAnsiString().NotNullable()
            .WithColumn("AddedAtTimespan").AsInt64();
    }

    public override void Down()
    {
        Delete.Table("Workspaces");
    }
}