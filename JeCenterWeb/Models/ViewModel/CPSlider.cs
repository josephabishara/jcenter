using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModels;

public class CPSlider
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
    public IFormFile? SliderImg { get; set; }

    [Display(Name = "Url Link")]
    public string? Url { get; set; }

    [Display(Name = "Button Name")]
    public string? BtnName { get; set; }

}
