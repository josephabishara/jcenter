using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class LoginViewModel
    {
        [Key]
        public int LoginId { get; set; }

        [Display(Name = " رقم الموبايل ")]
        [Required(ErrorMessage = "  رقم الموبايل  ")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "  كلمة المرور   ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
    }
}
