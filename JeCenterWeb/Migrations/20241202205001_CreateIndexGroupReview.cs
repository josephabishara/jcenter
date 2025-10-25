using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreateIndexGroupReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "AdvancedPermission",
                table: "FinancialAccount",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewsSchedule_ReviewDate_ReviewTypeId",
                table: "ReviewsSchedule",
                columns: new[] { "ReviewDate", "ReviewTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialDocuments_JournalEntryDate_physicalyearId_TreasuryID_AccountID",
                table: "FinancialDocuments",
                columns: new[] { "JournalEntryDate", "physicalyearId", "TreasuryID", "AccountID" });

            migrationBuilder.CreateIndex(
                name: "IX_CGroupSchedule_LectureDate",
                table: "CGroupSchedule",
                column: "LectureDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReviewsSchedule_ReviewDate_ReviewTypeId",
                table: "ReviewsSchedule");

            migrationBuilder.DropIndex(
                name: "IX_FinancialDocuments_JournalEntryDate_physicalyearId_TreasuryID_AccountID",
                table: "FinancialDocuments");

            migrationBuilder.DropIndex(
                name: "IX_CGroupSchedule_LectureDate",
                table: "CGroupSchedule");

            migrationBuilder.AlterColumn<bool>(
                name: "AdvancedPermission",
                table: "FinancialAccount",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
