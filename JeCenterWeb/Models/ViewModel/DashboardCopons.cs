using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class DashboardCopons
    {
        [Key]
        public int  Id{ get; set; }
        public string FullName { get; set; }
        public int UserTypeId { get; set; }
        public int CoponsOfAll { get; set; }

        public int CoponsOfGroups { get; set; }
        public int CoponsOfReviews { get; set; }
        public int CoponsOfExams { get; set; }

        public string? imgurl { get; set; }

        public string? BranchName { get; set; }
        

    }
}
