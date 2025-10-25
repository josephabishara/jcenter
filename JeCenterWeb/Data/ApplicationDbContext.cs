using JeCenterWeb.Models;
using JeCenterWeb.Models.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JeCenterWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }

        // public DbSet<CApplicationsValue> CApplicationsValue { get; set; }
        public DbSet<CBranch> CBranch { get; set; }
        public DbSet<Branchcolor> Branchcolor { get; set; }
        public DbSet<CDepartment> CDepartment { get; set; }
        public DbSet<CGroups> CGroups { get; set; }
        public DbSet<CPhases> CPhases { get; set; }
        public DbSet<CRooms> CRooms { get; set; }
        public DbSet<CSyllabus> CSyllabus { get; set; }
        public DbSet<ChargingCard> ChargingCard { get; set; }
        public DbSet<Cardwrite> Cardwrite { get; set; }
        public DbSet<ElRa3yNews> ElRa3yNews { get; set; }
        public DbSet<CBlogs> CBlogs { get; set; }
        public DbSet<CSlider> CSlider { get; set; }
        public DbSet<FinancialAccount> FinancialAccount { get; set; }
        public DbSet<FinancialDocuments> FinancialDocuments { get; set; }
        public DbSet<FinancialJournalEntryLine> FinancialJournalEntryLine { get; set; }
        public DbSet<MovementType> MovementType { get; set; }
        public DbSet<FinancialDashboardReport> FinancialDashboardReport { get; set; }

        public DbSet<AccessRight> AccessRight { get; set; }
        public DbSet<UserAccessRight> UserAccessRight { get; set; }


        public DbSet<PhysicalYear> PhysicalYear { get; set; }
        public DbSet<ResourceCity> ResourceCity { get; set; }
        public DbSet<ResourceDays> ResourceDays { get; set; }
        public DbSet<ResourceGender> ResourceGender { get; set; }
        public DbSet<ResourceMailbox> ResourceMailbox { get; set; }
        public DbSet<StudentApplications> StudentApplications { get; set; }
        public DbSet<StudentPhaseApplications> StudentPhaseApplications { get; set; }
        public DbSet<StudentGroup> StudentGroup { get; set; }
        public DbSet<TeacherAssistant> TeacherAssistant { get; set; }
        public DbSet<TeacherSyllabus> TeacherSyllabus { get; set; }
        public DbSet<TeacherSyllabusViewModel> TeacherSyllabusViewModel { get; set; }
        public DbSet<WebAboutUS> WebAboutUS { get; set; }
        public DbSet<CGroupSchedule> CGroupSchedule { get; set; }
      
        public DbSet<TeacherGroups> TeacherGroups { get; set; }
        public DbSet<TeachesVideos> TeachesVideos { get; set; }

        public DbSet<ReviewsSchedule> ReviewsSchedule { get; set; }

        public DbSet<StudentDiscount> StudentDiscount { get; set; }
        public DbSet<StudentLecture> StudentLecture { get; set; }
        public DbSet<CloseStudentLectureViewModel> CloseStudentLectureViewModel { get; set; }
      
        public DbSet<StudentComments> StudentComments { get; set; }
        public DbSet<StudentBalancePending> StudentBalancePending { get; set; }

        public DbSet<LectureAbsentViewModel> LectureAbsentViewModel { get; set; }
        public DbSet<UsersBalance> UsersBalance { get; set; }
        public DbSet<TeachersBalance> TeachersBalance { get; set; }
        public DbSet<ExpensesBalance> ExpensesBalance { get; set; }

        public DbSet<RoomGroupReviewsExamsScheduleViewModel> RoomGroupReviewsExamsScheduleViewModel { get; set; }

        public DbSet<StudentBalancePendingViewModel> StudentBalancePendingViewModel { get; set; }
        public DbSet<StudentBalancePendingList> StudentBalancePendingList { get; set; }

        public DbSet<StudentLectureViewModel> StudentLectureViewModel { get; set; }

        public DbSet<StudentsLecturesAndCount> StudentsLecturesAndCount { get; set; }

        public DbSet<DashboardGroups> DashboardGroups { get; set; }
        public DbSet<DashboardReviews> DashboardReviews { get; set; }
        public DbSet<DashboardExams> DashboardExams { get; set; }
        public DbSet<DashboardCopons> DashboardCopons { get; set; }

        public DbSet<FInanceDetailsViewModel> FInanceDetailsViewModel { get; set; }

        public DbSet<FinancialJournalEntryLineViewModel> FinancialJournalEntryLineViewModel { get; set; }
        public DbSet<FinancialJournalEntryViewModel> FinancialJournalEntryViewModel { get; set; }

        public DbSet<TeachersPayments> TeachersPayments { get; set; }
        public DbSet<StudentExamViewModel> StudentExamViewModel { get; set; }
        public DbSet<StudentReviewViewModel> StudentReviewViewModel { get; set; }
        public DbSet<StudentLectureByBranchToday> StudentLectureByBranchToday { get; set; }


        // Notifications

        public DbSet<CNotifications> CNotifications { get; set; }
  
        public DbSet<UsersNotifications> UsersNotifications { get; set; }

        public DbSet<GNotifications> GNotifications { get; set; }
  
        public DbSet<StudentNotifications> StudentNotifications { get; set; }

        public DbSet<TeacherScheduleReport> TeacherScheduleReport { get; set; }
        public DbSet<TeacherScheduleReportMothly> TeacherScheduleReportMothly { get; set; }
    }
}
