using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeCenterWeb.Models
{
    public class StudentApplications : MasterModel
    {
        //  StudentApplicationsRepository
        [Key]
        public int ApplicationId { get; set; }
        //[Display(Name = " الفرع ")]
        //[ForeignKey("CBranch")]
        //public int BranchId { get; set; }
        //public CBranch? CBranch { get; set; }

        [Display(Name = " الطالب /ة ")]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public ApplicationUser? Student { get; set; }

        [Display(Name = " السنة الدراسية ")]
        [ForeignKey("PhysicalYear")]
        public int PhysicalyearId { get; set; }
        public PhysicalYear? PhysicalYear { get; set; }

        [Display(Name = " المرحلة ")]
        [ForeignKey("CPhases")]
        public int PhaseId { get; set; }
        public CPhases? CPhases { get; set; }

        public bool Paided { get; set; }
        public int DocNo { get; set; }
    }
}
