using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class EditRoomGroupReviewsExamsScheduleViewModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomGroupReviewsExamsScheduleViewModel",
                table: "RoomGroupReviewsExamsScheduleViewModel");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RoomGroupReviewsExamsScheduleViewModel");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "RoomGroupReviewsExamsScheduleViewModel",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomGroupReviewsExamsScheduleViewModel",
                table: "RoomGroupReviewsExamsScheduleViewModel",
                column: "RoomId");
        }
    }
}
