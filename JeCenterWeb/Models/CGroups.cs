//using AspNetCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeCenterWeb.Models
{
    public class CGroups : MasterModel
    {
        [Key]
        public int GroupId { get; set; }

        [Required]
        [Display(Name = " أسم المجموعة ")]
        public string GroupName { get; set; }

        [Display(Name = " اسم المدرس ")]
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public ApplicationUser? Teacher { get; set; }

        [Display(Name = " اسم المدرس ")]
        public string TeacherName { get; set; }

        [Display(Name = " اسم المادة / المنهج ")]
        [ForeignKey("CSyllabus")]
        public int SyllabusID { get; set; }
        public CSyllabus? CSyllabus { get; set; }


        public int GroupNo { get; set; }

        [Display(Name = "  رقم المحاضرة ")]
        public int lectureno { get; set; }

        //[Display(Name = " طلبة المجموعة ")]
        //[ForeignKey("ResourceGender")]
        //public int GenderId { get; set; }
        //public ResourceGender? ResourceGender { get; set; }


        [Display(Name = " السنة الدراسية ")]
        [ForeignKey("PhysicalYear")]
        public int physicalyearId { get; set; }
        public PhysicalYear? PhysicalYear { get; set; }


        [Display(Name = " يوم ")]
        [ForeignKey("ResourceDays")]
        public int DayOfWeekId { get; set; }
        [Display(Name = " يوم ")]
        public ResourceDays? ResourceDays { get; set; }
        [Display(Name = " أول محاضرة ")]
        public DateTime FirstDay { get; set; }

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
        [Display(Name = " قيمة المحاضرة ")]
        public int LecturePrice { get; set; }

        [Display(Name = " القاعة ")]
        [ForeignKey("CRooms")]
        public int RoomId { get; set; }
        [Display(Name = " القاعة ")]
        public CRooms? CRooms { get; set; }

        public int OldGroupId { get; set; }
        public int OldGroupSchedualId { get; set; }
        public int CountAttend { get; set; } = 0;

        public ICollection<CGroupSchedule>? CGroupSchedule { get; set; }
        public ICollection<TeacherGroups>? TeacherGroups { get; set; }
        public ICollection<StudentGroup>? StudentGroup { get; set; }


    }
}
