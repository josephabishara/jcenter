using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreateBranchcolorModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "Branchcolor",
                columns: table => new
                {
                    BranchcolorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    btn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    txt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    txth = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branchcolor", x => x.BranchcolorId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Branchcolor");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "TeachersPayments");

            migrationBuilder.DropColumn(
                name: "ReservationDate",
                table: "StudentLectureByBranchToday");
        }
    }
}
