using FluentMigrator;

namespace Fl.Domain.V1.DataAccess.Migrations;

[Migration(202401160816)]
public sealed class AddFilesTable : Migration
{
    private const int TwentyMegabytes = 20971520;
    
    public override void Up()
    {
        Create.Table("Files")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("Content").AsBinary(TwentyMegabytes).NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Files");
    }
}