using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreateFinancialJournalEntryNote3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FinancialDocuments_MovementTypeId",
                table: "FinancialDocuments",
                column: "MovementTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialDocuments_MovementType_MovementTypeId",
                table: "FinancialDocuments",
                column: "MovementTypeId",
                principalTable: "MovementType",
                principalColumn: "MovementTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialDocuments_MovementType_MovementTypeId",
                table: "FinancialDocuments");

            migrationBuilder.DropIndex(
                name: "IX_FinancialDocuments_MovementTypeId",
                table: "FinancialDocuments");
        }
    }
}
