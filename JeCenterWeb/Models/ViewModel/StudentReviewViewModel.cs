using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class StudentReviewViewModel
    {
        [Key]
        [Display(Name = "رقم الكوبون")]
        public int StudentLectureId { get; set; }

        [Display(Name = "كود الحصة")]
        public int scheduleId { get; set; }
        public int StudentID { get; set; }
        [Display(Name = "مدفوع")]
        public decimal Paided { get; set; }
        [Display(Name = " تاريخ المراجعة ")]
        public DateTime LectureDate { get; set; }

        // 0 = Group , 1 = Review  , 2 = Exam
        [Display(Name = " نوع المراجعة ")]
        public int LectureType { get; set; }

        [Display(Name = " أسم المراجعة ")]
        public string ReviewsScheduleName { get; set; }
        [Display(Name = " السنة الدراسية ")]
        public int physicalyearId { get; set; }
        public int? Deleted { get; set; }
    }
}
