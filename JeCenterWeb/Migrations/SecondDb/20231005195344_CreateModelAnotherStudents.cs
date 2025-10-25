using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations.SecondDb
{
    /// <inheritdoc />
    public partial class CreateModelAnotherStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnotherStudents");

            migrationBuilder.DropTable(
                name: "GroupView5");

            migrationBuilder.DropTable(
                name: "oldstudent");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
