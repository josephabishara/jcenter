using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace JeCenterWeb.Models.ViewModel
{
    public class CloseStudentLectureViewModel
    {
        [Key]
        public Int64 ItemNo { get; set; }

        [Display(Name = "كود الحصة")]
        public int scheduleId { get; set; }
        [Display(Name = " نوع المحاضرة ")]
        public int LectureType { get; set; }
        [Display(Name = " ق المدرس ")]
        public int TeacherFee { get; set; }
        [Display(Name = " ق المركز ")]
        public int CenterFee { get; set; }
        [Display(Name = " خ السنتر ")]
        public int ServiceFee { get; set; }

        [Display(Name = "خصم")]
        public decimal Discount { get; set; }
        [Display(Name = "خصم السنتر")]
        public decimal CenterDiscount { get; set; }
        [Display(Name = "خصم المدرس")]
        public decimal teacherDiscount { get; set; }

        [Display(Name = "المدرس بعد الخصم")]
        public decimal TeacherValue { get; set; }
        [Display(Name = "المركز بعد الخصم")]
        public decimal CenterValue { get; set; }

        [Display(Name = "مدفوع")]
        public decimal Paided { get; set; }
    }
}
