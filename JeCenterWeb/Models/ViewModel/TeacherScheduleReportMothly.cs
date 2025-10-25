using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class TeacherScheduleReportMothly
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Teacher ID")]
        public int TeacherId { get; set; }
        [Display(Name = "Teacher Name")]
        public string TeacherName { get; set; }
        [Display(Name = "Lecture Date")]
        public DateTime LectureDate { get; set; }
        public int Count { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }
        public string MonthCode { get; set; }
    }
}
