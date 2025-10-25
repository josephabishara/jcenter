using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentApplications_CBranch_CBranchBranchId",
                table: "StudentApplications");

            migrationBuilder.DropIndex(
                name: "IX_StudentApplications_CBranchBranchId",
                table: "StudentApplications");

            migrationBuilder.DropColumn(
                name: "CBranchBranchId",
                table: "StudentApplications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CBranchBranchId",
                table: "StudentApplications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentApplications_CBranchBranchId",
                table: "StudentApplications",
                column: "CBranchBranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentApplications_CBranch_CBranchBranchId",
                table: "StudentApplications",
                column: "CBranchBranchId",
                principalTable: "CBranch",
                principalColumn: "BranchId");
        }
    }
}
