using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    public partial class AddStudentGroupModelinDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "TeacherGroups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CreateId",
                table: "TeacherGroups",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TeacherGroups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeleteId",
                table: "TeacherGroups",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Deleted",
                table: "TeacherGroups",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "TeacherGroups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdateId",
                table: "TeacherGroups",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "TeacherGroups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Writed",
                table: "TeacherGroups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StudentGroup",
                columns: table => new
                {
                    StudentGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroup", x => x.StudentGroupId);
                    table.ForeignKey(
                        name: "FK_StudentGroup_CGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "CGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroup_GroupId",
                table: "StudentGroup",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentGroup");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "TeacherGroups");

            migrationBuilder.DropColumn(
                name: "CreateId",
                table: "TeacherGroups");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TeacherGroups");

            migrationBuilder.DropColumn(
                name: "DeleteId",
                table: "TeacherGroups");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "TeacherGroups");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "TeacherGroups");

            migrationBuilder.DropColumn(
                name: "UpdateId",
                table: "TeacherGroups");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "TeacherGroups");

            migrationBuilder.DropColumn(
                name: "Writed",
                table: "TeacherGroups");
        }
    }
}
