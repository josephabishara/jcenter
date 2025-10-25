using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class StudentLectureViewModel
    {
        [Key]
        [Display(Name = "رقم الكوبون")]
        public int StudentLectureId { get; set; }

        [Display(Name = "كود الحصة")]
        public int scheduleId { get; set; }
        public int StudentID { get; set; }
        [Display(Name = "مدفوع")]
        public decimal Paided { get; set; }
        [Display(Name = " تاريخ محاضرة ")]
        public DateTime LectureDate { get; set; }

        // 0 = Group , 1 = Review  , 2 = Exam
        [Display(Name = " نوع المحاضرة ")]
        public int LectureType { get; set; }

        [Display(Name = " أسم المجموعة ")]
        public string GroupName { get; set; }
        [Display(Name = " السنة الدراسية ")]
        public int physicalyearId { get; set; }
        public int? Deleted { get; set; }

    }
}
