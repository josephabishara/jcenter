using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class EditStudentLectureValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CenterFee",
                table: "StudentLecture",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CenterValue",
                table: "StudentLecture",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceFee",
                table: "StudentLecture",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeacherFee",
                table: "StudentLecture",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeacherValue",
                table: "StudentLecture",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CenterFee",
                table: "StudentLecture");

            migrationBuilder.DropColumn(
                name: "CenterValue",
                table: "StudentLecture");

            migrationBuilder.DropColumn(
                name: "ServiceFee",
                table: "StudentLecture");

            migrationBuilder.DropColumn(
                name: "TeacherFee",
                table: "StudentLecture");

            migrationBuilder.DropColumn(
                name: "TeacherValue",
                table: "StudentLecture");
        }
    }
}
