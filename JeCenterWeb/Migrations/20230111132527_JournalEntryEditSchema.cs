using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    public partial class JournalEntryEditSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialJournalEntryLine_FinancialAccounts_AccountID",
                table: "FinancialJournalEntryLine");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialJournalEntryLine_FinancialJournalEntry_JournalEntryID",
                table: "FinancialJournalEntryLine");

            migrationBuilder.DropTable(
                name: "FinancialAccounts");

            migrationBuilder.DropTable(
                name: "FinancialJournalEntry");

            migrationBuilder.DropTable(
                name: "FinancialJournals");

            migrationBuilder.DropTable(
                name: "FinancialJournalType");

            migrationBuilder.RenameColumn(
                name: "JournalEntryID",
                table: "FinancialJournalEntryLine",
                newName: "physicalyearId");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialJournalEntryLine_JournalEntryID",
                table: "FinancialJournalEntryLine",
                newName: "IX_FinancialJournalEntryLine_physicalyearId");

            migrationBuilder.AddColumn<DateTime>(
                name: "JournalEntryDate",
                table: "FinancialJournalEntryLine",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MovementTypeId",
                table: "FinancialJournalEntryLine",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "FinancialJournalEntryLine",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialJournalEntryLine_AspNetUsers_AccountID",
                table: "FinancialJournalEntryLine",
                column: "AccountID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialJournalEntryLine_PhysicalYear_physicalyearId",
                table: "FinancialJournalEntryLine",
                column: "physicalyearId",
                principalTable: "PhysicalYear",
                principalColumn: "PhysicalyearId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialJournalEntryLine_AspNetUsers_AccountID",
                table: "FinancialJournalEntryLine");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialJournalEntryLine_PhysicalYear_physicalyearId",
                table: "FinancialJournalEntryLine");

            migrationBuilder.DropColumn(
                name: "JournalEntryDate",
                table: "FinancialJournalEntryLine");

            migrationBuilder.DropColumn(
                name: "MovementTypeId",
                table: "FinancialJournalEntryLine");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "FinancialJournalEntryLine");

            migrationBuilder.RenameColumn(
                name: "physicalyearId",
                table: "FinancialJournalEntryLine",
                newName: "JournalEntryID");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialJournalEntryLine_physicalyearId",
                table: "FinancialJournalEntryLine",
                newName: "IX_FinancialJournalEntryLine_JournalEntryID");

            migrationBuilder.CreateTable(
                name: "FinancialAccounts",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsMaster = table.Column<int>(type: "int", nullable: false),
                    ParentAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentAccountId = table.Column<int>(type: "int", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true)
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
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    JournalTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Writed = table.Column<int>(type: "int", nullable: true)
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
                    JournalTypeId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreditAccount = table.Column<int>(type: "int", nullable: false),
                    DebitAccount = table.Column<int>(type: "int", nullable: false),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    JournalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Writed = table.Column<int>(type: "int", nullable: true)
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
                    JournalId = table.Column<int>(type: "int", nullable: false),
                    PhysicalyearId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    JournalEntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JournalEntryValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Writed = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_FinancialJournalEntry_JournalId",
                table: "FinancialJournalEntry",
                column: "JournalId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialJournalEntry_PhysicalyearId",
                table: "FinancialJournalEntry",
                column: "PhysicalyearId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialJournals_JournalTypeId",
                table: "FinancialJournals",
                column: "JournalTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialJournalEntryLine_FinancialAccounts_AccountID",
                table: "FinancialJournalEntryLine",
                column: "AccountID",
                principalTable: "FinancialAccounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialJournalEntryLine_FinancialJournalEntry_JournalEntryID",
                table: "FinancialJournalEntryLine",
                column: "JournalEntryID",
                principalTable: "FinancialJournalEntry",
                principalColumn: "JournalEntryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
