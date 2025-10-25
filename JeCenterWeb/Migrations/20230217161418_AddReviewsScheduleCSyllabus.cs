using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    public partial class AddReviewsScheduleCSyllabus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SyllabusID",
                table: "ReviewsSchedule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "physicalyearId",
                table: "ReviewsSchedule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewsSchedule_physicalyearId",
                table: "ReviewsSchedule",
                column: "physicalyearId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewsSchedule_SyllabusID",
                table: "ReviewsSchedule",
                column: "SyllabusID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewsSchedule_CSyllabus_SyllabusID",
                table: "ReviewsSchedule",
                column: "SyllabusID",
                principalTable: "CSyllabus",
                principalColumn: "SyllabusID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewsSchedule_PhysicalYear_physicalyearId",
                table: "ReviewsSchedule",
                column: "physicalyearId",
                principalTable: "PhysicalYear",
                principalColumn: "PhysicalyearId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewsSchedule_CSyllabus_SyllabusID",
                table: "ReviewsSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewsSchedule_PhysicalYear_physicalyearId",
                table: "ReviewsSchedule");

            migrationBuilder.DropIndex(
                name: "IX_ReviewsSchedule_physicalyearId",
                table: "ReviewsSchedule");

            migrationBuilder.DropIndex(
                name: "IX_ReviewsSchedule_SyllabusID",
                table: "ReviewsSchedule");

            migrationBuilder.DropColumn(
                name: "SyllabusID",
                table: "ReviewsSchedule");

            migrationBuilder.DropColumn(
                name: "physicalyearId",
                table: "ReviewsSchedule");
        }
    }
}
