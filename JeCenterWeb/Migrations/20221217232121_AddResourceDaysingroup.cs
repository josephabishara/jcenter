using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    public partial class AddResourceDaysingroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TeacherName",
                table: "CGroups",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeekId",
                table: "CGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CGroups_DayOfWeekId",
                table: "CGroups",
                column: "DayOfWeekId");

            migrationBuilder.AddForeignKey(
                name: "FK_CGroups_ResourceDays_DayOfWeekId",
                table: "CGroups",
                column: "DayOfWeekId",
                principalTable: "ResourceDays",
                principalColumn: "DayOfWeekId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CGroups_ResourceDays_DayOfWeekId",
                table: "CGroups");

            migrationBuilder.DropIndex(
                name: "IX_CGroups_DayOfWeekId",
                table: "CGroups");

            migrationBuilder.DropColumn(
                name: "DayOfWeekId",
                table: "CGroups");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherName",
                table: "CGroups",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
