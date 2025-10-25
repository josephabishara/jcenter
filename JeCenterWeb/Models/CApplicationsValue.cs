using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeCenterWeb.Models
{
    public class CApplicationsValue : MasterModel
    {
        [Key]
        public int ApplicationValueId { get; set; }


        [Display(Name = " المرحلة ")]
        [ForeignKey("CPhases")]
        public int PhaseId { get; set; }
        public CPhases? CPhases { get; set; }

        [Display(Name = " السنة الدراسية ")]
        [ForeignKey("PhysicalYear")]
        public int PhysicalyearId { get; set; }
        public PhysicalYear? PhysicalYear { get; set; }

        [Display(Name = " قيمة الاستمارة ")]
        [Required]
        public int ApplicationValue { get; set; }

    }
}
