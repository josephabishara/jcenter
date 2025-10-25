using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    public partial class AddReviewsScheduleRooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "ReviewsSchedule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewsSchedule_RoomId",
                table: "ReviewsSchedule",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewsSchedule_CRooms_RoomId",
                table: "ReviewsSchedule",
                column: "RoomId",
                principalTable: "CRooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewsSchedule_CRooms_RoomId",
                table: "ReviewsSchedule");

            migrationBuilder.DropIndex(
                name: "IX_ReviewsSchedule_RoomId",
                table: "ReviewsSchedule");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "ReviewsSchedule");
        }
    }
}
