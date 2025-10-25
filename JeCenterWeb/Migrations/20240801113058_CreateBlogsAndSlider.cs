using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreateBlogsAndSlider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ElRa3yNews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CreateId",
                table: "ElRa3yNews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ElRa3yNews",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeleteId",
                table: "ElRa3yNews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Deleted",
                table: "ElRa3yNews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "ElRa3yNews",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdateId",
                table: "ElRa3yNews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "ElRa3yNews",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Writed",
                table: "ElRa3yNews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CBlogs",
                columns: table => new
                {
                    BlogsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogDiscretion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Paragraph = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_CBlogs", x => x.BlogsId);
                });

            migrationBuilder.CreateTable(
                name: "CSlider",
                columns: table => new
                {
                    SliderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubTitel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SliderImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BtnName = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_CSlider", x => x.SliderId);
                });



        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CBlogs");

            migrationBuilder.DropTable(
                name: "CSlider");

            migrationBuilder.DropTable(
                name: "FinancialDashboardReport");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "ElRa3yNews");

            migrationBuilder.DropColumn(
                name: "CreateId",
                table: "ElRa3yNews");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ElRa3yNews");

            migrationBuilder.DropColumn(
                name: "DeleteId",
                table: "ElRa3yNews");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "ElRa3yNews");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "ElRa3yNews");

            migrationBuilder.DropColumn(
                name: "UpdateId",
                table: "ElRa3yNews");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "ElRa3yNews");

            migrationBuilder.DropColumn(
                name: "Writed",
                table: "ElRa3yNews");
        }
    }
}
