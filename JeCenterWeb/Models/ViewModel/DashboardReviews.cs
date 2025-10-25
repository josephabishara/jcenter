using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class DashboardReviews
    {
        [Key]
        public int ReviewId { get; set; }
        public string? TeacherName { get; set; }
        public string? ReviewName { get; set; }
        public DateTime ReviewDate { get; set; }
        public int LectureType { get; set; }
        public int Students { get; set; }
        public decimal TeacherValue { get; set; }
        public bool Closed { get; set; }
        public bool Paided { get; set; }
        public string? UserName { get; set; }
        public DateTime? PaidDate { get; set; }
    }
}
