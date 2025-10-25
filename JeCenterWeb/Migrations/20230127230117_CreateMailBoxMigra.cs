using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    public partial class CreateMailBoxMigra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResourceMailbox",
                columns: table => new
                {
                    MailboxId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MailFrom = table.Column<int>(type: "int", nullable: false),
                    MailTo = table.Column<int>(type: "int", nullable: false),
                    MailSubject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MailBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MailDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttachUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sent = table.Column<bool>(type: "bit", nullable: false),
                    Recived = table.Column<bool>(type: "bit", nullable: false),
                    SentDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RecivedDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceMailbox", x => x.MailboxId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourceMailbox");
        }
    }
}
