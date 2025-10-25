using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    public partial class ModifyApplicationBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentApplications_CBranch_BranchId",
                table: "StudentApplications");

            migrationBuilder.DropIndex(
                name: "IX_StudentApplications_BranchId",
                table: "StudentApplications");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "StudentApplications");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "StudentApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentApplications_BranchId",
                table: "StudentApplications",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentApplications_CBranch_BranchId",
                table: "StudentApplications",
                column: "BranchId",
                principalTable: "CBranch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
