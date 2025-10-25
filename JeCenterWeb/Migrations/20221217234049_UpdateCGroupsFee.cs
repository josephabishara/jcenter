using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    public partial class UpdateCGroupsFee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CenterFee",
                table: "CGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "CGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceFee",
                table: "CGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SessionEnd",
                table: "CGroups",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SessionStart",
                table: "CGroups",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "TeacherFee",
                table: "CGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CGroups_RoomId",
                table: "CGroups",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_CGroups_CRooms_RoomId",
                table: "CGroups",
                column: "RoomId",
                principalTable: "CRooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CGroups_CRooms_RoomId",
                table: "CGroups");

            migrationBuilder.DropIndex(
                name: "IX_CGroups_RoomId",
                table: "CGroups");

            migrationBuilder.DropColumn(
                name: "CenterFee",
                table: "CGroups");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "CGroups");

            migrationBuilder.DropColumn(
                name: "ServiceFee",
                table: "CGroups");

            migrationBuilder.DropColumn(
                name: "SessionEnd",
                table: "CGroups");

            migrationBuilder.DropColumn(
                name: "SessionStart",
                table: "CGroups");

            migrationBuilder.DropColumn(
                name: "TeacherFee",
                table: "CGroups");
        }
    }
}
