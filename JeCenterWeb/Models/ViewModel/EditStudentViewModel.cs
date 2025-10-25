using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class EditStudentViewModel
    {

        [Display(Name = " الاسم ")]
        public string FullName { get; set; }
        [Display(Name = " أسم الطالب / ة")]
        public string? FristName { get; set; }
        [Display(Name = " أسم الأب ")]
        public string? SecondName { get; set; }
        [Display(Name = " أسم الجد ")]
        public string? LastName { get; set; }
        [Display(Name = " أسم العائلة ")]
        public string? FamilyName { get; set; }
        [Display(Name = " العنوان ")]
        public string? Address { get; set; }
        [Display(Name = " الرقم القومي ")]
        public string? NationalID { get; set; }
        [Display(Name = " تاريخ الميلاد ")]
        public DateTime? Birthdate { get; set; }
    
        [Display(Name = " رقم الموبايل ")]
        public string? Mobile { get; set; }
        [Display(Name = " المدرسة ")]
        public string? School { get; set; }
        [Display(Name = " وظيفة ولي الأمر ")]
        public string? ParentJob { get; set; }
        [Display(Name = " رقم ولي الامر ")]
        public string? ParentMobile { get; set; }
    }
}
