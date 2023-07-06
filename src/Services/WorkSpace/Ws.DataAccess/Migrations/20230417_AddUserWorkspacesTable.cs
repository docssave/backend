using System.Data;
using FluentMigrator;

namespace Ws.DataAccess.Migrations;

[Migration(202304170833)]
public sealed class AddUserWorkspacesTable : Migration
{
    public override void Up()
    {
        Create.Table("UserWorkspaces")
            .WithColumn("UserId").AsInt64()
            .WithColumn("WorkspaceId").AsGuid().ForeignKey("WorkspaceId_ForeignKey", "Workspaces", "Id").OnDeleteOrUpdate(Rule.Cascade);

        Create.PrimaryKey("UserWorkspaces_PrimaryKey")
            .OnTable("UserWorkspaces")
            .Columns("UserId", "WorkspaceId");
    }

    public override void Down()
    {
        Delete.Table("UserWorkspaces");
    }
}