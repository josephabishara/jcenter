using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreateFinancialJournalEntryNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialJournalEntryLine_MovementType_MovementTypeId",
                table: "FinancialJournalEntryLine");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "FinancialJournalEntryLine");

            migrationBuilder.AlterColumn<int>(
                name: "MovementTypeId",
                table: "FinancialJournalEntryLine",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialJournalEntryLine_MovementType_MovementTypeId",
                table: "FinancialJournalEntryLine",
                column: "MovementTypeId",
                principalTable: "MovementType",
                principalColumn: "MovementTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialJournalEntryLine_MovementType_MovementTypeId",
                table: "FinancialJournalEntryLine");

            migrationBuilder.AlterColumn<int>(
                name: "MovementTypeId",
                table: "FinancialJournalEntryLine",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "FinancialJournalEntryLine",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialJournalEntryLine_MovementType_MovementTypeId",
                table: "FinancialJournalEntryLine",
                column: "MovementTypeId",
                principalTable: "MovementType",
                principalColumn: "MovementTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
