using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class StudentDiscountTeacherId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StudentDiscount_TeacherId",
                table: "StudentDiscount",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentDiscount_AspNetUsers_TeacherId",
                table: "StudentDiscount",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentDiscount_AspNetUsers_TeacherId",
                table: "StudentDiscount");

            migrationBuilder.DropIndex(
                name: "IX_StudentDiscount_TeacherId",
                table: "StudentDiscount");
        }
    }
}
