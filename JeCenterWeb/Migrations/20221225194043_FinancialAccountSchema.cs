using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    public partial class FinancialAccountSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinancialAccounts",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentAccountId = table.Column<int>(type: "int", nullable: true),
                    IsMaster = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_FinancialAccounts", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "FinancialJournalType",
                columns: table => new
                {
                    JournalTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JournalTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_FinancialJournalType", x => x.JournalTypeId);
                });

            migrationBuilder.CreateTable(
                name: "FinancialJournals",
                columns: table => new
                {
                    JournalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JournalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JournalTypeId = table.Column<int>(type: "int", nullable: false),
                    DebitAccount = table.Column<int>(type: "int", nullable: false),
                    CreditAccount = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_FinancialJournals", x => x.JournalId);
                    table.ForeignKey(
                        name: "FK_FinancialJournals_FinancialJournalType_JournalTypeId",
                        column: x => x.JournalTypeId,
                        principalTable: "FinancialJournalType",
                        principalColumn: "JournalTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinancialJournalEntry",
                columns: table => new
                {
                    JournalEntryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JournalEntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JournalEntryValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JournalId = table.Column<int>(type: "int", nullable: false),
                    PhysicalyearId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_FinancialJournalEntry", x => x.JournalEntryID);
                    table.ForeignKey(
                        name: "FK_FinancialJournalEntry_FinancialJournals_JournalId",
                        column: x => x.JournalId,
                        principalTable: "FinancialJournals",
                        principalColumn: "JournalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinancialJournalEntry_PhysicalYear_PhysicalyearId",
                        column: x => x.PhysicalyearId,
                        principalTable: "PhysicalYear",
                        principalColumn: "PhysicalyearId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinancialJournalEntryLine",
                columns: table => new
                {
                    JournalEntryDetilsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JournalEntryID = table.Column<int>(type: "int", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_FinancialJournalEntryLine", x => x.JournalEntryDetilsID);
                    table.ForeignKey(
                        name: "FK_FinancialJournalEntryLine_FinancialAccounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "FinancialAccounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinancialJournalEntryLine_FinancialJournalEntry_JournalEntryID",
                        column: x => x.JournalEntryID,
                        principalTable: "FinancialJournalEntry",
                        principalColumn: "JournalEntryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialJournalEntry_JournalId",
                table: "FinancialJournalEntry",
                column: "JournalId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialJournalEntry_PhysicalyearId",
                table: "FinancialJournalEntry",
                column: "PhysicalyearId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialJournalEntryLine_AccountID",
                table: "FinancialJournalEntryLine",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialJournalEntryLine_JournalEntryID",
                table: "FinancialJournalEntryLine",
                column: "JournalEntryID");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialJournals_JournalTypeId",
                table: "FinancialJournals",
                column: "JournalTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialJournalEntryLine");

            migrationBuilder.DropTable(
                name: "FinancialAccounts");

            migrationBuilder.DropTable(
                name: "FinancialJournalEntry");

            migrationBuilder.DropTable(
                name: "FinancialJournals");

            migrationBuilder.DropTable(
                name: "FinancialJournalType");
        }
    }
}
