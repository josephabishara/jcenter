using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    public partial class CreateFinancialJournalDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FinancialDocumentId",
                table: "FinancialJournalEntryLine",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FinancialDocuments",
                columns: table => new
                {
                    FinancialDocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JournalEntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TreasuryID = table.Column<int>(type: "int", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MovementTypeId = table.Column<int>(type: "int", nullable: false),
                    physicalyearId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialDocuments", x => x.FinancialDocumentId);
                });

            migrationBuilder.CreateTable(
                name: "MovementType",
                columns: table => new
                {
                    MovementTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovementTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovementType", x => x.MovementTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialJournalEntryLine_FinancialDocumentId",
                table: "FinancialJournalEntryLine",
                column: "FinancialDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialJournalEntryLine_MovementTypeId",
                table: "FinancialJournalEntryLine",
                column: "MovementTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialJournalEntryLine_FinancialDocuments_FinancialDocumentId",
                table: "FinancialJournalEntryLine",
                column: "FinancialDocumentId",
                principalTable: "FinancialDocuments",
                principalColumn: "FinancialDocumentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialJournalEntryLine_MovementType_MovementTypeId",
                table: "FinancialJournalEntryLine",
                column: "MovementTypeId",
                principalTable: "MovementType",
                principalColumn: "MovementTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialJournalEntryLine_FinancialDocuments_FinancialDocumentId",
                table: "FinancialJournalEntryLine");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialJournalEntryLine_MovementType_MovementTypeId",
                table: "FinancialJournalEntryLine");

            migrationBuilder.DropTable(
                name: "FinancialDocuments");

            migrationBuilder.DropTable(
                name: "MovementType");

            migrationBuilder.DropIndex(
                name: "IX_FinancialJournalEntryLine_FinancialDocumentId",
                table: "FinancialJournalEntryLine");

            migrationBuilder.DropIndex(
                name: "IX_FinancialJournalEntryLine_MovementTypeId",
                table: "FinancialJournalEntryLine");

            migrationBuilder.DropColumn(
                name: "FinancialDocumentId",
                table: "FinancialJournalEntryLine");
        }
    }
}
