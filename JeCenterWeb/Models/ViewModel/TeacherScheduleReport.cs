using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace JeCenterWeb.Models.ViewModel
{
    public class TeacherScheduleReport
    {
        [Key]
        public Int64 Id { get; set; }
        [Display(Name = "Teacher ID")]
        public int TeacherId { get; set; }
        [Display(Name = "Teacher Name")]
        public string TeacherName { get; set; }
        [Display(Name = "Lecture Date")]
        public DateTime LectureDate { get; set; }
        public int Count { get; set; }
        public int GroupId { get; set; }

        [Display(Name = " أسم المجموعة ")]
        public string GroupName { get; set; }
        public int SyllabusID { get; set; }

        [Display(Name = " اسم المادة / المنهج ")]
        public string SyllabusName { get; set; }

        public int PhaseId { get; set; }

        [Display(Name = " المرحلة ")]
        public string PhaseName { get; set; }

        [Display(Name = " قيمة المحاضرة ")]
        public int LecturePrice { get; set; }

        /*
         TeacherId, 
         TeacherName
         LectureDate,
         LecturePrice  { get; set; } 
         Count, 
         public int    GroupId  { get; set; } 
         public string GroupName  { get; set; } 
         public int  SyllabusID  { get; set; }
         public string SyllabusName  { get; set; }
         public int  PhaseId  { get; set; }
         public string  PhaseName { get; set; }
       */


    }
}
