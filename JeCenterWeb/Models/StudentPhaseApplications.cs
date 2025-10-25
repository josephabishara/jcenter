using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class StudentPhaseApplications : MasterModel
    {
        [Key]
        public int ApplicationId { get; set; }
        

        [Display(Name = " الطالب /ة ")]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public ApplicationUser? Student { get; set; }

        [Display(Name = " السنة الدراسية ")]
        [ForeignKey("PhysicalYear")]
        public int PhysicalyearId { get; set; }
        public PhysicalYear? PhysicalYear { get; set; }

        

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


        [Display(Name = " الاستمارة ")]
        public int ApplicationValue { get; set; }

        public bool Paided { get; set; }
        public int DocNo { get; set; }
    }
}
