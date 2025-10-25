using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeCenterWeb.Models
{
    public class StudentDiscount : MasterModel
    {
        [Key]
        public int StudentDiscountId { get; set; }

        // 0 = Group , 1 = Review  , 2 = Exam
        [Display(Name = " نوع المحاضرة ")]
        public int LectureType { get; set; }

        [Display(Name = "الطالب")]
        [ForeignKey("FinancialAccount")]
        [Range(1, 1000000)]
        public int StudentId { get; set; }
        public FinancialAccount? FinancialAccount { get; set; }

       
        [Display(Name = "المدرس")]
        [ForeignKey("Teacher")]
        [Range(1, 1000000)]
        public int TeacherId { get; set; }
        public ApplicationUser? Teacher { get; set; }

        

        [Display(Name = "الخصم")]
        public decimal Discount { get; set; }

        [Display(Name = "خصم السنتر")]
        [Range(0, 1000)]
        public decimal CenterDiscount { get; set; }

        [Display(Name = "خصم المدرس")]
        [Range(0, 1000)]
        public decimal teacherDiscount { get; set; }

        [Range(1, 1000)]
        public int physicalyearId { get; set; }

        [Display(Name = "بيان")]
        public string Notes { get; set; }
    }
}
