using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class StudentDiscountViewModel
    {
        [Key]
        public int StudentDiscountId { get; set; }

        // 0 = Group , 1 = Review  , 2 = Exam
        [Display(Name = " نوع المحاضرة ")]
        public string? LectureType { get; set; }
 
        public int AccountId { get; set; }
        public int StudentId { get; set; }

        [Display(Name = "اسم الطالب ")]
        public string? StudentName { get; set; }

        public int TeacherId { get; set; }
        [Display(Name = "اسم المدرس ")]
        public string? TeacherName { get; set; }

        public int? UserId { get; set; }

        [Display(Name = "اسم المستخدم")]
        public string? UserName { get; set; }

        [Display(Name = "الخصم")]
        public decimal Discount { get; set; }

        [Display(Name = "خصم السنتر")]
       
        public decimal CenterDiscount { get; set; }

        [Display(Name = "خصم المدرس")]
         
        public decimal teacherDiscount { get; set; }

        [Display(Name = "السنة الدراسية")]
        public int physicalyearId { get; set; }

        [Display(Name = "بيان")]
        public string Notes { get; set; }

        public int? UpdateId { get; set; }

        public bool Active { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
