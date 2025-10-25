using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class EditReviewsScheduleSessionStartAndEnd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CenterFee",
                table: "ReviewsSchedule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LecturePrice",
                table: "ReviewsSchedule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceFee",
                table: "ReviewsSchedule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SessionEnd",
                table: "ReviewsSchedule",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SessionStart",
                table: "ReviewsSchedule",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "TeacherFee",
                table: "ReviewsSchedule",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CenterFee",
                table: "ReviewsSchedule");

            migrationBuilder.DropColumn(
                name: "LecturePrice",
                table: "ReviewsSchedule");

            migrationBuilder.DropColumn(
                name: "ServiceFee",
                table: "ReviewsSchedule");

            migrationBuilder.DropColumn(
                name: "SessionEnd",
                table: "ReviewsSchedule");

            migrationBuilder.DropColumn(
                name: "SessionStart",
                table: "ReviewsSchedule");

            migrationBuilder.DropColumn(
                name: "TeacherFee",
                table: "ReviewsSchedule");
        }
    }
}
