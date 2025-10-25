using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class EditGroupViewModel
    {
        public int GroupId { get; set; }
        [Display(Name = " من ")]
        public TimeSpan SessionStart { get; set; }
        [Display(Name = " الى ")]
        public TimeSpan SessionEnd { get; set; }
        [Display(Name = " ق المدرس ")]
        public int TeacherFee { get; set; }
        [Display(Name = " ق المركز ")]
        public int CenterFee { get; set; }
        [Display(Name = " خ السنتر ")]
        public int ServiceFee { get; set; }

        [Display(Name = " القاعة ")]
        public int RoomId { get; set; }
        public DateTime date { get; set; }
        [Display(Name = "  رقم المحاضرة ")]
        public int lectureno { get; set; }

        public bool Active { get; set; }

    }
}
