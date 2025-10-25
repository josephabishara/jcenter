using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddNoteStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "AspNetUsers");
        }
    }
}
