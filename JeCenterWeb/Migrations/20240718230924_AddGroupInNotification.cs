using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupInNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "UsersBalance");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "CNotifications");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "CNotifications");
        }
    }
}
