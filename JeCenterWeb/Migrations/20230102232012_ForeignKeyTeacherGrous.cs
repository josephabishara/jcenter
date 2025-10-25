using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    public partial class ForeignKeyTeacherGrous : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CGroups_TeacherId",
                table: "CGroups",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_CGroups_AspNetUsers_TeacherId",
                table: "CGroups",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CGroups_AspNetUsers_TeacherId",
                table: "CGroups");

            migrationBuilder.DropIndex(
                name: "IX_CGroups_TeacherId",
                table: "CGroups");
        }
    }
}
