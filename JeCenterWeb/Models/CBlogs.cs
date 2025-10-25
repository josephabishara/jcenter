using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class CBlogs : MasterModel
    {
        [Key]
        public int BlogsId { get; set; }

        [Required]
        public string BlogImage { get; set; }


        [Required]
        [Display(Name = "Blog Name")]
        public string BlogTitle { get; set; }


        [Display(Name = "Discretion")]
        public string? BlogDiscretion { get; set; }

        public string? Paragraph { get; set; }


        [Display(Name = "Date")]
        public DateTime BlogDate { get; set; } = DateTime.Today;

      
    }
}
