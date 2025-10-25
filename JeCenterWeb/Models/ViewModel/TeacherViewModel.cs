using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class TeacherViewModel
    {
        [Required]
        [Display(Name = " الاسم ")]
        public string FullName { get; set; }
      
        [Required]
        [Display(Name = " رقم الموبايل ")]
        public string Mobile { get; set; }
        [Required]
        [Display(Name = " كلمة المرور  ")]
        public string Password { get; set; }
        [Display(Name = "صورة المدرس")]
        public IFormFile? imgurl { get; set; }

    }
}
