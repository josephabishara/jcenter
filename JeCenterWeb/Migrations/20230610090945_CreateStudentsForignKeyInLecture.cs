using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreateStudentsForignKeyInLecture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StudentLecture_StudentID",
                table: "StudentLecture",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLecture_AspNetUsers_StudentID",
                table: "StudentLecture",
                column: "StudentID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentLecture_AspNetUsers_StudentID",
                table: "StudentLecture");

            migrationBuilder.DropIndex(
                name: "IX_StudentLecture_StudentID",
                table: "StudentLecture");
        }
    }
}
