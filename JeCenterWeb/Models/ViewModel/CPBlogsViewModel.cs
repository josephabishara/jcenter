using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModels;

public class CPBlogsViewModel
{
    [Key]
    public int BlogsId { get; set; }

    [Required]
    [Display(Name = "Upload Image")]
    public IFormFile? BlogImage { get; set; }

    [Required]
    [Display(Name = "Blog Titel")]
    public string BlogTitle { get; set; }

    [Required]
    [Display(Name = "Discretion")]
    public string BlogDiscretion { get; set; }

    [Required]
    [Display(Name = "Article")]
    public string Paragraph { get; set; }

    [Required]
    [Display(Name = "Date")]
    public DateTime BlogDate { get; set; }

 

}
