using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    public partial class CreateUsersBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            create view UsersBalance as
           SELECT JeCenter.AspNetUsers.Id, JeCenter.AspNetUsers.FullName, 
                  ISNULL(SUM(JeCenter.FinancialJournalEntryLine.Debit), 0) AS SumDebit, 
                  ISNULL(SUM(JeCenter.FinancialJournalEntryLine.Credit), 0) AS SumCredit, 
                  ISNULL(SUM(JeCenter.FinancialJournalEntryLine.Debit), 0) - ISNULL(SUM(JeCenter.FinancialJournalEntryLine.Credit), 0) AS Balance, JeCenter.AspNetUsers.UserTypeId
           FROM   JeCenter.AspNetUsers LEFT OUTER JOIN
                  JeCenter.FinancialJournalEntryLine ON JeCenter.AspNetUsers.Id = JeCenter.FinancialJournalEntryLine.AccountID
          GROUP BY JeCenter.AspNetUsers.Id, JeCenter.AspNetUsers.FullName, JeCenter.AspNetUsers.UserTypeId
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Drop view UsersBalance ;");
        }
    }
}
