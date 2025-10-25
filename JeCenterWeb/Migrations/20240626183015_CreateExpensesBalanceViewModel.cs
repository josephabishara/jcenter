using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreateExpensesBalanceViewModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpensesBalance");

            migrationBuilder.DropColumn(
                name: "PhaseName",
                table: "RoomGroupReviewsExamsScheduleViewModel");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "FinancialJournalEntryViewModel");

            migrationBuilder.DropColumn(
                name: "physicalyearId",
                table: "FinancialJournalEntryViewModel");

            migrationBuilder.DropColumn(
                name: "physicalyearId",
                table: "FinancialJournalEntryLineViewModel");
        }
    }
}
