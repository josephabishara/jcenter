using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class StudentsLecturesAndCount
    {
        [Key]
        public int GroupscheduleId { get; set; }
        public int GroupId { get; set; }

        [Display(Name = " رقم المحاضرة ")]
        public int LectureNo { get; set; }
        [Display(Name = " تاريخ محاضرة ")]
        public DateTime LectureDate { get; set; }
        [Display(Name = " كود الشهر ")]
        public string MonthCode { get; set; }
        [Display(Name = " حالة المحاضرة ")]
        public bool Closed { get; set; }
        [Display(Name = " حالة المحاضرة ")]
        public bool Paided { get; set; }
        [Display(Name = "  مٌسدد ")]
        public decimal PaidedValue { get; set; }
        [Display(Name = " أسم القاعة ")]
        public string RoomName { get; set; }

        [Display(Name = " أسم المجموعة ")]
        public string GroupName { get; set; }

        [Display(Name = " عدد الحضور ")]
        public int CountAttend { get; set; }

        [Display(Name = " سعة القاعة ")]
        public string Capacity { get; set; }
        [Display(Name = " اسم المدرس ")]
        public string TeacherName { get; set; }

        [Display(Name = " السنة الدراسية ")]
        public int physicalyearId { get; set; }

        [Display(Name = " المرحلة ")]
        public int PhaseID { get; set; }
        [Display(Name = " اسم المادة / المنهج ")]
        public string SyllabusName { get; set; }
        [Display(Name = " أول محاضرة ")]
        public DateTime FirstDay { get; set; }

        [Display(Name = " من ")]
        public TimeSpan SessionStart { get; set; }
        [Display(Name = " الى ")]
        public TimeSpan SessionEnd { get; set; }
        [Display(Name = " قيمة المحاضرة ")]
        public int LecturePrice { get; set; }
    }
}
