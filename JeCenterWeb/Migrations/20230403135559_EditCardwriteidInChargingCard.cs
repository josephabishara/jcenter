using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class EditCardwriteidInChargingCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Printed",
                table: "ChargingCard");

            migrationBuilder.AddColumn<bool>(
                name: "Printed",
                table: "Cardwrite",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Printed",
                table: "Cardwrite");

            migrationBuilder.AddColumn<bool>(
                name: "Printed",
                table: "ChargingCard",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
