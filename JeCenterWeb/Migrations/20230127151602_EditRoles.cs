using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    public partial class EditRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
              name: "AspNetRoles",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                  NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                  ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_AspNetRoles", x => x.Id);
              });

            migrationBuilder.CreateTable(
                           name: "AspNetUserRoles",
                           columns: table => new
                           {
                               UserId = table.Column<int>(type: "int", nullable: false),
                               RoleId = table.Column<int>(type: "int", nullable: false)
                           },
                           constraints: table =>
                           {
                               table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                               table.ForeignKey(
                                   name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                                   column: x => x.RoleId,
                                   principalTable: "AspNetRoles",
                                   principalColumn: "Id",
                                   onDelete: ReferentialAction.Cascade);
                               table.ForeignKey(
                                   name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                                   column: x => x.UserId,
                                   principalTable: "AspNetUsers",
                                   principalColumn: "Id",
                                   onDelete: ReferentialAction.Cascade);
                           });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersBalance");
        }
    }
}
