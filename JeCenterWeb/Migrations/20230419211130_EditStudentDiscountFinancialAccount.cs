using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class EditStudentDiscountFinancialAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StudentDiscount_StudentId",
                table: "StudentDiscount",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentDiscount_FinancialAccount_StudentId",
                table: "StudentDiscount",
                column: "StudentId",
                principalTable: "FinancialAccount",
                principalColumn: "FinancialAccountId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentDiscount_FinancialAccount_StudentId",
                table: "StudentDiscount");

            migrationBuilder.DropIndex(
                name: "IX_StudentDiscount_StudentId",
                table: "StudentDiscount");
        }
    }
}
