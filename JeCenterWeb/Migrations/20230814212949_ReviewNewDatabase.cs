using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class ReviewNewDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckCouponViewModel");

            migrationBuilder.DropTable(
                name: "Teachers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckCouponViewModel",
                columns: table => new
                {
                    MyProperty = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CenterFee = table.Column<int>(type: "int", nullable: false),
                    CenterValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LectureName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LecturePrice = table.Column<int>(type: "int", nullable: false),
                    LectureType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Paided = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Printed = table.Column<int>(type: "int", nullable: false),
                    ServiceFee = table.Column<int>(type: "int", nullable: false),
                    StudentIndex = table.Column<int>(type: "int", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Studentmobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherFee = table.Column<int>(type: "int", nullable: false),
                    TeacherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    scheduleId = table.Column<int>(type: "int", nullable: false),
                    teacherDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckCouponViewModel", x => x.MyProperty);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    TeacherID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    City = table.Column<int>(type: "int", nullable: false),
                    EducaAdmin = table.Column<int>(type: "int", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phase = table.Column<int>(type: "int", nullable: false),
                    PhasesID = table.Column<int>(type: "int", nullable: false),
                    Schools = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeacherEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeacherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeacherPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    create_uid = table.Column<int>(type: "int", nullable: false),
                    curriculumID = table.Column<int>(type: "int", nullable: false),
                    img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    write_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    write_uid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.TeacherID);
                });
        }
    }
}
