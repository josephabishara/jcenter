using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeCenterWeb.Models
{
    public class StudentLecture : MasterModel
    {
        [Key]
        public int StudentLectureId { get; set; }

        [Display(Name = "كود الحصة")]
        public int scheduleId { get; set; }
        [Display(Name = " تاريخ محاضرة ")]
        public DateTime LectureDate { get; set; }

        // Reservation
        [Display(Name = " تاريخ الحجز")]
        public DateTime ReservationDate { get; set; } = DateTime.Today.Date;

        // 0 = Group , 1 = Review  , 2 = Exam
        [Display(Name = " نوع المحاضرة ")]
        public int LectureType { get; set; }


        [Display(Name = " الطالب /ة ")]
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        public ApplicationUser? Student { get; set; }

        [Display(Name = " رقم الطالب ")]
        public int StudentIndex { get; set; }

        [Display(Name = " عدد مرات طباعة الكوبون ")]
        public int Printed { get; set; }

        [Display(Name = " نوع الطالب ")]
        public int StudentType { get; set; }
        // price 
        
        [Display(Name = " ق المدرس ")]
        public int TeacherFee { get; set; }
        [Display(Name = " ق المركز ")]
        public int CenterFee { get; set; }
        [Display(Name = " خ السنتر ")]
        public int ServiceFee { get; set; }


        [Display(Name = " قيمة المحاضرة ")]
        public int LecturePrice { get; set; }


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

        [Display(Name = "مدقوع")]
        public decimal Paided { get; set; }

        [Display(Name = " أسم الفرع ")]
        public int BranchId { get; set; }
       

    }
}
