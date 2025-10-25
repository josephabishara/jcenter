using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    public partial class CreateGroupSchedualandTeacherGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LecturePrice",
                table: "CGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CGroupSchedule",
                columns: table => new
                {
                    GroupscheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    LectureNo = table.Column<int>(type: "int", nullable: false),
                    LectureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MonthCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CGroupSchedule", x => x.GroupscheduleId);
                    table.ForeignKey(
                        name: "FK_CGroupSchedule_CGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "CGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherGroups",
                columns: table => new
                {
                    TeacherGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherGroups", x => x.TeacherGroupId);
                    table.ForeignKey(
                        name: "FK_TeacherGroups_CGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "CGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CGroupSchedule_GroupId",
                table: "CGroupSchedule",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherGroups_GroupId",
                table: "TeacherGroups",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CGroupSchedule");

            migrationBuilder.DropTable(
                name: "TeacherGroups");

            migrationBuilder.DropColumn(
                name: "LecturePrice",
                table: "CGroups");
        }
    }
}
