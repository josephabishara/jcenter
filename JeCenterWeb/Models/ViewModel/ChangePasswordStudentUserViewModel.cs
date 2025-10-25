using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class ChangePasswordStudentUserViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = " الاسم ")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = " كلمة المرور  الجديدة ")]
        public string Password { get; set; }
    }
}
