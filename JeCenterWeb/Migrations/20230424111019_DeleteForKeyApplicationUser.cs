using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class DeleteForKeyApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialJournalEntryLine_AspNetUsers_AccountID",
                table: "FinancialJournalEntryLine");

            migrationBuilder.DropIndex(
                name: "IX_FinancialJournalEntryLine_AccountID",
                table: "FinancialJournalEntryLine");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FinancialJournalEntryLine_AccountID",
                table: "FinancialJournalEntryLine",
                column: "AccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialJournalEntryLine_AspNetUsers_AccountID",
                table: "FinancialJournalEntryLine",
                column: "AccountID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
