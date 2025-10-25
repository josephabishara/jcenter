using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class CreateStudentViewModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = " أسم الطالب / ة")]
        public string FristName { get; set; }
        [Display(Name = " أسم الأب ")]
        [Required(ErrorMessage = "*")]
        public string SecondName { get; set; }
        [Display(Name = " أسم الجد ")]
        [Required(ErrorMessage = "*")]
        public string LastName { get; set; }
        [Display(Name = " أسم العائلة ")]
        public string FamilyName { get; set; }

        [Display(Name = " رقم الموبايل ")]
        [Required(ErrorMessage = " *")]
        public string Mobile { get; set; }

   
        [Display(Name = " كلمة المرور")]
        public string Password { get; set; }

        [Display(Name = " العنوان ")]
        public string? Address { get; set; }
        [Display(Name = " الرقم القومي ")]
        public string? NationalID { get; set; }

        [Display(Name = " تاريخ الميلاد ")]
        [Required(ErrorMessage = "أكتب تاريخ ميلادك  ")]
        public DateTime Birthdate { get; set; }


        [Display(Name = " المدرسة ")]
        public string? School { get; set; }



        [Display(Name = " وظيفة ولي الأمر ")]
        public string? ParentJob { get; set; }
        [Display(Name = " رقم ولي الامر ")]
        public string? ParentMobile { get; set; }
    }
}
