using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class PhasesViewModel
    {
        public int PhaseId { get; set; }
        [Required]
        [Display(Name = " المرحلة ")]
        public string PhaseName { get; set; }

        [Display(Name = "صورة المرحلة")]
        public IFormFile? imgurl { get; set; }

        [Required]
        [Display(Name = " سعر الاستمارة ")]
        public int ApplicationValue { get; set; }
    }
}
