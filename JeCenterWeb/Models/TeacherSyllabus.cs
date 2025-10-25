using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class TeacherSyllabus : MasterModel
    {
        [Key]
        public int TeacherSyllabusId { get; set; }

        [Display(Name = " المدرس ")]
        public int TeacherId { get; set; }

        [Display(Name = " اسم المدرس ")]
        public string TeacherName { get; set; }



        [Display(Name = " اسم المادة / المنهج ")]
        public int SyllabusID { get; set; }
        [Display(Name = " اسم المادة / المنهج ")]
        public string SyllabusName { get; set; }



        [Display(Name = " المرحلة ")]
        public int PhaseId { get; set; }
        [Display(Name = " اسم المرحلة ")]
        public string PhaseName { get; set; }


        [Display(Name = " السنتر ")]
        public int ApplicationCenter { get; set; }

        [Display(Name = " المدرس ")]
        public int ApplicationTeacher { get; set; }

        [Display(Name = " الاستمارة ")]
        public int ApplicationValue { get; set; }



    }
}
