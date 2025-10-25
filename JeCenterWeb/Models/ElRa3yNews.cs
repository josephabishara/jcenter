using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class ElRa3yNews : MasterModel
    {
        [Key]
        public int ElRa3yNewsId { get; set; }
        [Required]
        [Display(Name = "عنوان الخبر ")]
        public string TitelNews { get; set; }
        [Required]
        [Display(Name = " تفاصيل الخبر ")]
        public string ParagraphNews { get; set; }
    }
}
