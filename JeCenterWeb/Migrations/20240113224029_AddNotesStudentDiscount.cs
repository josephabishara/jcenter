using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddNotesStudentDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "StudentDiscount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DashboardCopons");

            migrationBuilder.DropTable(
                name: "DashboardExams");

            migrationBuilder.DropTable(
                name: "DashboardGroups");

            migrationBuilder.DropTable(
                name: "DashboardReviews");

            migrationBuilder.DropTable(
                name: "FInanceDetailsViewModel");

            migrationBuilder.DropTable(
                name: "FinancialJournalEntryLineViewModel");

            migrationBuilder.DropTable(
                name: "TeachersPayments");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "StudentDiscount");
        }
    }
}
