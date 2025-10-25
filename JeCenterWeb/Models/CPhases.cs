using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class CPhases : MasterModel
    {
        [Key]
        public int PhaseId { get; set; }
        [Required]
        [Display(Name = " المرحلة ")]
        public string PhaseName { get; set; }
        public string imgurl { get; set; }
        [Required]
        [Display(Name = " سعر الاستمارة ")]
        public int ApplicationValue { get; set; }

        public int Parent { get; set; }
        public ICollection<CSyllabus>? CSyllabus { get; set; }
        public ICollection<CApplicationsValue>? CApplicationsValue { get; set; }
        public ICollection<StudentApplications>? StudentApplications { get; set; }

    }
}
