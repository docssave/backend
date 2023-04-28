using FluentMigrator;

namespace Ws.DataAccess.Migrations;

[Migration(202304170830)]
public sealed class AddWorkspacesTable : Migration
{
    public override void Up()
    {
        Create.Table("Workspaces")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Name").AsAnsiString().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Workspaces");
    }
}