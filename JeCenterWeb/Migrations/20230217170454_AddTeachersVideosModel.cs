using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    public partial class AddTeachersVideosModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeachesVideos",
                columns: table => new
                {
                    TeacheVideoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    ReviewsScheduleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SyllabusID = table.Column<int>(type: "int", nullable: false),
                    TeacherFee = table.Column<int>(type: "int", nullable: false),
                    CenterFee = table.Column<int>(type: "int", nullable: false),
                    LecturePrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachesVideos", x => x.TeacheVideoId);
                    table.ForeignKey(
                        name: "FK_TeachesVideos_CSyllabus_SyllabusID",
                        column: x => x.SyllabusID,
                        principalTable: "CSyllabus",
                        principalColumn: "SyllabusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeachesVideos_SyllabusID",
                table: "TeachesVideos",
                column: "SyllabusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeachesVideos");
        }
    }
}
