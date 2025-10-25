using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class RoomGroupReviewsExamsScheduleViewModel
    {
        [Key]
        public int Id { get; set; }
        public string? Capacity { get; set; }
        public int RoomId { get; set; }
        public string Titel { get; set; }
        public TimeSpan SessionStart { get; set; }
        public TimeSpan SessionEnd { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = " المرحلة ")]
        public string? PhaseName { get; set; }
        public int TypeId { get; set; }
        public int LectureNo { get; set; }
        public int Exist { get; set; }
    }
}
