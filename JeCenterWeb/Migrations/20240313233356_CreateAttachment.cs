using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreateAttachment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Attached",
                table: "FinancialDocuments",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "imgurl",
                table: "FinancialDocuments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attached",
                table: "FinancialDocuments");

            migrationBuilder.DropColumn(
                name: "imgurl",
                table: "FinancialDocuments");
        }
    }
}
