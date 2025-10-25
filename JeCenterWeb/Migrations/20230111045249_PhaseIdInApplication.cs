using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    public partial class PhaseIdInApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhaseId",
                table: "StudentApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentApplications_PhaseId",
                table: "StudentApplications",
                column: "PhaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentApplications_CPhases_PhaseId",
                table: "StudentApplications",
                column: "PhaseId",
                principalTable: "CPhases",
                principalColumn: "PhaseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentApplications_CPhases_PhaseId",
                table: "StudentApplications");

            migrationBuilder.DropIndex(
                name: "IX_StudentApplications_PhaseId",
                table: "StudentApplications");

            migrationBuilder.DropColumn(
                name: "PhaseId",
                table: "StudentApplications");
        }
    }
}
