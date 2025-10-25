using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class DeletePhaseIdAndGenderIdFromUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CGroups_ResourceGender_GenderId",
                table: "CGroups");

            migrationBuilder.DropIndex(
                name: "IX_CGroups_GenderId",
                table: "CGroups");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "CGroups");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhaseId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "ResourceGenderGenderId",
                table: "CGroups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CGroups_ResourceGenderGenderId",
                table: "CGroups",
                column: "ResourceGenderGenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CGroups_ResourceGender_ResourceGenderGenderId",
                table: "CGroups",
                column: "ResourceGenderGenderId",
                principalTable: "ResourceGender",
                principalColumn: "GenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CGroups_ResourceGender_ResourceGenderGenderId",
                table: "CGroups");

            migrationBuilder.DropIndex(
                name: "IX_CGroups_ResourceGenderGenderId",
                table: "CGroups");

            migrationBuilder.DropColumn(
                name: "ResourceGenderGenderId",
                table: "CGroups");

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "CGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhaseId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CGroups_GenderId",
                table: "CGroups",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CGroups_ResourceGender_GenderId",
                table: "CGroups",
                column: "GenderId",
                principalTable: "ResourceGender",
                principalColumn: "GenderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
