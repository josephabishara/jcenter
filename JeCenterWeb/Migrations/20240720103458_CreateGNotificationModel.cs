using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreateGNotificationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CNotifications_NotificationType_NotificationTypeId",
                table: "CNotifications");

            migrationBuilder.DropTable(
                name: "NotificationType");

            migrationBuilder.DropIndex(
                name: "IX_CNotifications_NotificationTypeId",
                table: "CNotifications");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "CNotifications");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "CNotifications");

            migrationBuilder.DropColumn(
                name: "NotificationTypeId",
                table: "CNotifications");

            migrationBuilder.RenameColumn(
                name: "StudentGroupId",
                table: "UsersNotifications",
                newName: "UsersNotificationId");

            migrationBuilder.CreateTable(
                name: "GNotifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    NotiTitel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationNote = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_GNotifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_GNotifications_CGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "CGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentNotifications",
                columns: table => new
                {
                    StudentNotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    NotificationId = table.Column<int>(type: "int", nullable: false),
                    Seen = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_StudentNotifications", x => x.StudentNotificationId);
                    table.ForeignKey(
                        name: "FK_StudentNotifications_GNotifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "GNotifications",
                        principalColumn: "NotificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GNotifications_GroupId",
                table: "GNotifications",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentNotifications_NotificationId",
                table: "StudentNotifications",
                column: "NotificationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentNotifications");

            migrationBuilder.DropTable(
                name: "GNotifications");

            migrationBuilder.RenameColumn(
                name: "UsersNotificationId",
                table: "UsersNotifications",
                newName: "StudentGroupId");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "CNotifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "CNotifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NotificationTypeId",
                table: "CNotifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NotificationType",
                columns: table => new
                {
                    NotificationTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Writed = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationType", x => x.NotificationTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CNotifications_NotificationTypeId",
                table: "CNotifications",
                column: "NotificationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CNotifications_NotificationType_NotificationTypeId",
                table: "CNotifications",
                column: "NotificationTypeId",
                principalTable: "NotificationType",
                principalColumn: "NotificationTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
