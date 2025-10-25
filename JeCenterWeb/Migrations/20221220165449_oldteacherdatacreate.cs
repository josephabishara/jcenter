using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    public partial class oldteacherdatacreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "oldteacher",
                columns: table => new
                {
                    TeacherID = table.Column<double>(type: "float", nullable: false),
                    TeacherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<double>(type: "float", nullable: false),
                    TeacherPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhasesID = table.Column<double>(type: "float", nullable: false),
                    img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Account = table.Column<double>(type: "float", nullable: false),
                    Active = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oldteacher", x => x.TeacherID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "oldteacher");
        }
    }
}
