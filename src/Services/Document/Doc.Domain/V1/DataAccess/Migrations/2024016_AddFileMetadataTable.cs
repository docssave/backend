using FluentMigrator;

namespace Doc.Domain.V1.DataAccess.Migrations;

[Migration(202401160831)]
public sealed class AddFileMetadataTable : Migration
{
    public override void Up()
    {
        Create.Table("FileMetadata")
            .WithColumn("FileId").AsGuid().NotNullable()
            .WithColumn("DocumentId").AsGuid().NotNullable()
            .WithColumn("Size").AsInt64().NotNullable()
            .WithColumn("Type").AsAnsiString(65).NotNullable()
            .WithColumn("RegisteredAtTimestamp").AsInt64().NotNullable();

        Create.PrimaryKey("FileMetadata_PrimaryKey")
            .OnTable("FileMetadata")
            .Columns("FileId", "DocumentId");
    }

    public override void Down()
    {
        Delete.Table("FileMetadata");
    }
}