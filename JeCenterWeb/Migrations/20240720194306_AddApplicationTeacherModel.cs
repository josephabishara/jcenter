using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationTeacherModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationCenter",
                table: "TeacherSyllabus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationTeacher",
                table: "TeacherSyllabus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationValue",
                table: "TeacherSyllabus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PhaseName",
                table: "TeacherSyllabus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TeacherName",
                table: "TeacherSyllabus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationCenter",
                table: "TeacherSyllabus");

            migrationBuilder.DropColumn(
                name: "ApplicationTeacher",
                table: "TeacherSyllabus");

            migrationBuilder.DropColumn(
                name: "ApplicationValue",
                table: "TeacherSyllabus");

            migrationBuilder.DropColumn(
                name: "PhaseName",
                table: "TeacherSyllabus");

            migrationBuilder.DropColumn(
                name: "TeacherName",
                table: "TeacherSyllabus");
        }
    }
}
