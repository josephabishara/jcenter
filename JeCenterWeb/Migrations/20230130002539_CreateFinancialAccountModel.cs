using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    public partial class CreateFinancialAccountModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "FinancialDocuments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CreateId",
                table: "FinancialDocuments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "FinancialDocuments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeleteId",
                table: "FinancialDocuments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Deleted",
                table: "FinancialDocuments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "FinancialDocuments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdateId",
                table: "FinancialDocuments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "FinancialDocuments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Writed",
                table: "FinancialDocuments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cardwrite",
                columns: table => new
                {
                    Cardwriteid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardCount = table.Column<int>(type: "int", nullable: false),
                    Cardvalue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cardwrite", x => x.Cardwriteid);
                });

            migrationBuilder.CreateTable(
                name: "ChargingCard",
                columns: table => new
                {
                    ChargingCardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChargingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    Printed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargingCard", x => x.ChargingCardId);
                });

            migrationBuilder.CreateTable(
                name: "FinancialAccount",
                columns: table => new
                {
                    FinancialAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1000000, 1"),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_FinancialAccount", x => x.FinancialAccountId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cardwrite");

            migrationBuilder.DropTable(
                name: "ChargingCard");

            migrationBuilder.DropTable(
                name: "FinancialAccount");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "FinancialDocuments");

            migrationBuilder.DropColumn(
                name: "CreateId",
                table: "FinancialDocuments");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "FinancialDocuments");

            migrationBuilder.DropColumn(
                name: "DeleteId",
                table: "FinancialDocuments");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "FinancialDocuments");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "FinancialDocuments");

            migrationBuilder.DropColumn(
                name: "UpdateId",
                table: "FinancialDocuments");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "FinancialDocuments");

            migrationBuilder.DropColumn(
                name: "Writed",
                table: "FinancialDocuments");
        }
    }
}
