using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreateStudentReviewViewModelAndExam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentExamViewModel");

            migrationBuilder.DropTable(
                name: "StudentReviewViewModel");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "StudentLectureViewModel");

            migrationBuilder.DropColumn(
                name: "Attached",
                table: "FinancialJournalEntryViewModel");

            migrationBuilder.DropColumn(
                name: "TreasuryID",
                table: "FinancialJournalEntryViewModel");

            migrationBuilder.DropColumn(
                name: "imgurl",
                table: "FinancialJournalEntryViewModel");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "FinancialJournalEntryLineViewModel");
        }
    }
}
