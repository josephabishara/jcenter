using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class EditStudentUserViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = " الاسم ")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = " البريد الإليكتروني ")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [Display(Name = " رقم الموبايل ")]
        public string Mobile { get; set; }

    }
}
