namespace JeCenterWeb.Models.ViewModel
{
    public class DashboardViewModel
    {
        public decimal TotalTreasury { get; set; } = 0;
        public decimal TotalCards { get; set; } = 0;
        public decimal TotalCharged { get; set; } = 0;
        public decimal TotalPayments { get; set; } = 0;
        public int TeacherCount { get; set; } = 0;
        public int StudentCount { get; set; } = 0;
        public int GroupsCount { get; set; } = 0;
        public int ReviewsCount { get; set; } = 0;
        public int ExamsCount { get; set; } = 0;
        public IEnumerable<BalanceDashboardViewModel>? Treasury { get; set; }

        public IEnumerable<DashboardGroups>? DashboardGroups { get; set; }
        public IEnumerable<DashboardReviews>? DashboardReviews { get; set; }
        public IEnumerable<DashboardExams>? DashboardExams { get; set; }

        public IEnumerable<TeachersPayments>? TeachersPayments { get; set; }

        public IEnumerable<DashboardCopons>? DashboardCopons { get; set; }
    }
}
