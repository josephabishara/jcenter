using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class EditTeacherViewModel
    {
        public int id { get; set; }
        [Required]
        [Display(Name = " الاسم ")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = " رقم الموبايل ")]
        public string Mobile { get; set; }
    }
}
