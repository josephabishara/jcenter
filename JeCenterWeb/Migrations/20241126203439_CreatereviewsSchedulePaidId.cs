using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreatereviewsSchedulePaidId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PaidDate",
                table: "ReviewsSchedule",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaidId",
                table: "ReviewsSchedule",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaidDate",
                table: "CGroupSchedule",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaidId",
                table: "CGroupSchedule",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidDate",
                table: "ReviewsSchedule");

            migrationBuilder.DropColumn(
                name: "PaidId",
                table: "ReviewsSchedule");

            migrationBuilder.DropColumn(
                name: "PaidDate",
                table: "CGroupSchedule");

            migrationBuilder.DropColumn(
                name: "PaidId",
                table: "CGroupSchedule");
        }
    }
}
