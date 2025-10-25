using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeCenterWeb.Models.ViewModel
{
    public class SyllabusViewModel
    {
        [Required]
        [Display(Name = " اسم المادة / المنهج ")]
        public string SyllabusName { get; set; }

        [Display(Name = " المرحلة ")]
        [ForeignKey("CPhases")]
        public int PhaseID { get; set; }
        public CPhases? CPhases { get; set; }
        public IFormFile? imgurl { get; set; }
    }
}
