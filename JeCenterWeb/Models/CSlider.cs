using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class CSlider : MasterModel
    {
        [Key]
        [Display(Name = "ID")]
        public int SliderId { get; set; }
        [Display(Name = "Titel")]
        [Required]
        public string Titel { get; set; }

        [Display(Name = "SubTitel")]
        [Required]
        public string SubTitel { get; set; }

        [Display(Name = "Image")]
        [Required]
        public string SliderImg { get; set; }

        [Display(Name = "Url")]
        public string? Url { get; set; }
        [Display(Name = "Button")]
        public string? BtnName { get; set; }

    }
}
