using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class StudentLectureByBranchToday
    {
        [Key]
        public int BranchId { get; set; }
        public int StudentCount { get; set; }
        public DateTime ReservationDate { get; set; }

    }
}
