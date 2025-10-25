using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeCenterWeb.Models.ViewModel
{
    public class TeacherGroupsViewModel
    {
        [Display(Name = " أسم المجموعة ")]
        public string? GroupName { get; set; }
        [Required]
        public int GroupNo { get; set; }
       
        [Display(Name = " اسم المدرس ")]
        public string? TeacherName { get; set; }
        [Required]
        [Display(Name = " القاعة ")]
        public int RoomId { get; set; }
        [Required]
        [Display(Name = " ق المدرس ")]
        public int TeacherFee { get; set; }
        [Required]
        [Display(Name = " ق المركز ")]
        public int CenterFee { get; set; }
        [Required]
        [Display(Name = " خ السنتر ")]
        public int ServiceFee { get; set; }
        [Required]
        [Display(Name = " من ")]
        public TimeSpan SessionStart { get; set; }
        [Required]
        [Display(Name = " الى ")]
        public TimeSpan SessionEnd { get; set; }
        [Required]

        [Display(Name = " السنة الدراسية ")]
        [Range(1, 99999)]
        public int PhysicalyearId { get; set; }
        [Required]
        [Display(Name = " يوم ")]
        public int DayOfWeekId { get; set; }
        [Required]
        [Range(1, 99999)]
        [Display(Name = " المادة / المنهج ")]
        public int SyllabusID { get; set; }
        [Required]
        [Display(Name = "  المدرس ")]
        public int TeacherId { get; set; }
        [Required]

        [Display(Name = " أول محاضرة ")]
        public DateTime FirstDay { get; set; }
        [Required]

        [Display(Name = " تاريخ الحلي ")]
        [NotMapped]
        public DateTime? CrrDate { get; set; } = DateTime.Today;
        [Required]
        public int TypeId { get; set; }

        [Display(Name = "  رقم المحاضرة ")]
        public int lectureno { get; set; }

        public bool Active { get; set; }

    }
}
