using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreateFinancialJournalEntryLineFinancialAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FinancialJournalEntryLine_AccountID",
                table: "FinancialJournalEntryLine",
                column: "AccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialJournalEntryLine_FinancialAccount_AccountID",
                table: "FinancialJournalEntryLine",
                column: "AccountID",
                principalTable: "FinancialAccount",
                principalColumn: "FinancialAccountId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialJournalEntryLine_FinancialAccount_AccountID",
                table: "FinancialJournalEntryLine");

            migrationBuilder.DropIndex(
                name: "IX_FinancialJournalEntryLine_AccountID",
                table: "FinancialJournalEntryLine");
        }
    }
}
