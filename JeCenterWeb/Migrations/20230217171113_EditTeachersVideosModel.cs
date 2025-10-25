using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    public partial class EditTeachersVideosModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReviewsScheduleName",
                table: "TeachesVideos",
                newName: "VideoUrl");

            migrationBuilder.AddColumn<string>(
                name: "VideoName",
                table: "TeachesVideos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoName",
                table: "TeachesVideos");

            migrationBuilder.RenameColumn(
                name: "VideoUrl",
                table: "TeachesVideos",
                newName: "ReviewsScheduleName");
        }
    }
}
