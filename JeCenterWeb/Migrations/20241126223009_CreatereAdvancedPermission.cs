using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreatereAdvancedPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<bool>(
                name: "AdvancedPermission",
                table: "FinancialAccount",
                type: "bit",
                nullable: true);

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdvancedPermission",
                table: "FinancialAccount");

            migrationBuilder.DropColumn(
                name: "PaidDate",
                table: "DashboardReviews");

            migrationBuilder.DropColumn(
                name: "PaidDate",
                table: "DashboardGroups");

            migrationBuilder.DropColumn(
                name: "PaidDate",
                table: "DashboardExams");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "DashboardReviews",
                newName: "imgurl");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "DashboardGroups",
                newName: "imgurl");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "DashboardExams",
                newName: "imgurl");
        }
    }
}
