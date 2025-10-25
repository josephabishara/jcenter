using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreatelecturenoInGroupModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CGroups_ResourceGender_ResourceGenderGenderId",
                table: "CGroups");

            migrationBuilder.DropIndex(
                name: "IX_CGroups_ResourceGenderGenderId",
                table: "CGroups");

            migrationBuilder.DropColumn(
                name: "lecture",
                table: "ResourceGender");

            migrationBuilder.DropColumn(
                name: "ResourceGenderGenderId",
                table: "CGroups");

            migrationBuilder.AddColumn<int>(
                name: "lectureno",
                table: "CGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lectureno",
                table: "CGroups");

            migrationBuilder.AddColumn<string>(
                name: "lecture",
                table: "ResourceGender",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
    }
}
