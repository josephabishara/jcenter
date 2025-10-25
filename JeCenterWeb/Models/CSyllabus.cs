using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeCenterWeb.Models
{
    public class CSyllabus : MasterModel
    {
        [Key]
        public int SyllabusID { get; set; }
        [Required]
        [Display(Name = " اسم المادة / المنهج ")]
        public string SyllabusName { get; set; }

        [Display(Name = " المرحلة ")]
        [ForeignKey("CPhases")]
        public int PhaseID { get; set; }
        public CPhases? CPhases { get; set; }
        public string imgurl { get; set; }
        public ICollection<CGroups>? CGroups { get; set; }
        public ICollection<ReviewsSchedule>? ReviewsSchedule { get; set; }
    }
}
