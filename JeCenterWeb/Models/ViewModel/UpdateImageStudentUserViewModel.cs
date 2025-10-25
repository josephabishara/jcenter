using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class UpdateImageStudentUserViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = " الاسم ")]
        public string FullName { get; set; }
        [Display(Name = "صورة المستخدم")]
        public IFormFile? imgurl { get; set; }
        [Display(Name = "صورة المستخدم")]
        public string oldimgurl { get; set; }
    }
}
