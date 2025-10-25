using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreateFinancialJournalEntryViewModel3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
    
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialJournalEntryViewModel",
                table: "FinancialJournalEntryViewModel");

            migrationBuilder.AlterColumn<int>(
                name: "FinancialDocumentId",
                table: "FinancialJournalEntryViewModel",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "JournalEntryDetilsID",
                table: "FinancialJournalEntryViewModel",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialJournalEntryViewModel",
                table: "FinancialJournalEntryViewModel",
                column: "JournalEntryDetilsID");
        }
    }
}
