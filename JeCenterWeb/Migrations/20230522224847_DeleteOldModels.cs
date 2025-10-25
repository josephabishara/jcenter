using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class DeleteOldModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Martina");

            migrationBuilder.DropTable(
                name: "oldteacher");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Martina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Martina", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "oldteacher",
                columns: table => new
                {
                    TeacherID = table.Column<double>(type: "float", nullable: false),
                    Account = table.Column<double>(type: "float", nullable: false),
                    Active = table.Column<double>(type: "float", nullable: false),
                    Mobile = table.Column<double>(type: "float", nullable: true),
                    PhasesID = table.Column<double>(type: "float", nullable: false),
                    TeacherEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeacherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    img = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oldteacher", x => x.TeacherID);
                });
        }
    }
}
