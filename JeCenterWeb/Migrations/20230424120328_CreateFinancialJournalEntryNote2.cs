using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreateFinancialJournalEntryNote2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialJournalEntryLine_MovementType_MovementTypeId",
                table: "FinancialJournalEntryLine");

            migrationBuilder.DropIndex(
                name: "IX_FinancialJournalEntryLine_MovementTypeId",
                table: "FinancialJournalEntryLine");

            migrationBuilder.DropColumn(
                name: "MovementTypeId",
                table: "FinancialJournalEntryLine");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovementTypeId",
                table: "FinancialJournalEntryLine",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialJournalEntryLine_MovementTypeId",
                table: "FinancialJournalEntryLine",
                column: "MovementTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialJournalEntryLine_MovementType_MovementTypeId",
                table: "FinancialJournalEntryLine",
                column: "MovementTypeId",
                principalTable: "MovementType",
                principalColumn: "MovementTypeId");
        }
    }
}
