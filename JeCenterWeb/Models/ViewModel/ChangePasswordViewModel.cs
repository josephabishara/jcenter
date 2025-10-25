using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class ChangePasswordViewModel
    {

        [Required(ErrorMessage = "  كلمة المرور  القديمة ")]
        [DataType(DataType.Password)]
        [Display(Name = " كلمة المرور القديمة ")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "أكتب كلمة مرور")]
        [MinLength(6, ErrorMessage = "أكتب كلمة مرور أكثر من 6 حروف")]
        [MaxLength(16, ErrorMessage = " كلمة مرور أكثر من  اللازم")]
        [DataType(DataType.Password)]
        [Display(Name = " كلمة المرور الجديدة")]
        public string Password { get; set; }

        [Required(ErrorMessage = "أكتب كلمة مرور")]
        [DataType(DataType.Password)]
        [Display(Name = " تأكيد كلمة المرور ")]
        [Compare("Password", ErrorMessage = " كلمة مرور غير متطابقة")]
        [MaxLength(16)]
        public string ConfirmPassword { get; set; }

    }
}
