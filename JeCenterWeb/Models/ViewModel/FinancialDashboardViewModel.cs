namespace JeCenterWeb.Models.ViewModel
{
    public class FinancialDashboardViewModel
    {

        public IEnumerable<FinancialDashboardReport>? FinancialDashboardReport { get; set; }

        public decimal TotalTreasury { get; set; }
      //  public decimal TotalCards { get; set; }
        public decimal TotalCharged { get; set; }
        public decimal TotalPayments { get; set; }
        public decimal TeacherCount { get; set; }
        public int StudentCount { get; set; }
        public int GroupsCount { get; set; }
        public int ReviewsCount { get; set; }
        public int ExamsCount { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }

    }
}
