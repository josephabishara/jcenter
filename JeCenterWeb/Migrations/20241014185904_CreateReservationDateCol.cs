using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreateReservationDateCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
     

          



            migrationBuilder.AddColumn<DateTime>(
                name: "ReservationDate",
                table: "StudentLecture",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

         

 

          

  
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentLectureByBranchToday");

            migrationBuilder.DropTable(
                name: "TeachersBalance");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TeachersPayments");

            migrationBuilder.DropColumn(
                name: "JournalEntryDate",
                table: "TeachersPayments");

            migrationBuilder.DropColumn(
                name: "MovementTypeName",
                table: "TeachersPayments");

            migrationBuilder.DropColumn(
                name: "ReservationDate",
                table: "StudentLecture");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "FinancialJournalEntryLineViewModel");

            migrationBuilder.DropColumn(
                name: "MovementTypeId",
                table: "FinancialJournalEntryLineViewModel");
        }
    }
}
