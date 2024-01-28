using System.Data;
using FluentMigrator;

namespace Doc.Domain.V1.DataAccess.Migrations;

[Migration(202401280830)]
public sealed class AddUserDocumentsTable : Migration 
{
    public override void Up()
    {
        Create.Table("UserDocuments")
            .WithColumn("UserId").AsInt64()
            .WithColumn("DocumentId").AsGuid().ForeignKey("Documents", "Id")
                                                    .OnDeleteOrUpdate(Rule.Cascade);

        Create.PrimaryKey("UserDocuments_PrimaryKey")
            .OnTable("UserDocuments")
            .Columns("UserId", "DocumentId");
    }

    public override void Down()
    {
        Delete.Table("UserDocuments");
    }
}