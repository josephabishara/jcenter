using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class DashboardGroups
    {
        [Key]
        public int scheduleId { get; set; }
        public string? TeacherName { get; set; }
        public string? GroupName { get; set; }
        public DateTime LectureDate { get; set; }
        public int LectureType { get; set; }
        public int Students { get;  set; }
        public decimal TeacherValue { get; set;}
        public bool Closed { get; set; }
        public bool Paided { get; set; }
        public string? UserName { get; set; }
        public DateTime? PaidDate { get; set; }
    }
}
