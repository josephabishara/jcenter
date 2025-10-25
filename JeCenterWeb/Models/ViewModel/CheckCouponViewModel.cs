using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class CheckCouponViewModel
    {
    
        [Display(Name = "كود الحصة")]
        public int scheduleId { get; set; }

        // 0 = Group , 1 = Review  , 2 = Exam
        [Display(Name = " نوع المحاضرة ")]
        public string LectureType { get; set; }

        [Display(Name = " اسم المحاضرة ")]
        public string LectureName { get; set; }

        [Display(Name = " اسم الطالب ")]
        public string StudentName { get; set; }
        [Display(Name = " موبايل الطالب ")]
        public string Studentmobile { get; set; }

        [Display(Name = " اسم المدرس ")]
        public string TeacherName { get; set; }



        [Display(Name = " رقم الطالب ")]
        public int StudentIndex { get; set; }

        [Display(Name = " عدد مرات طباعة الكوبون ")]
        public int Printed { get; set; }
      
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

        [Display(Name = "مدفوع")]
        public decimal Paided { get; set; }


    }
}
