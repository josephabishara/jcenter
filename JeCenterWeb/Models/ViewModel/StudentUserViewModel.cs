using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class StudentUserViewModel
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

        [Display(Name = "صورة المستخدم")]
        [Required]
        public IFormFile? imgurl { get; set; }

        [Display(Name = "نوع المستخدم")]
        [Required]
        public int UserType { get; set; }

        [Display(Name = " مراجع  ")]
        public bool AdvancedPermission { get; set; } = false;
    }
}
