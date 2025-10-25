using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class WebAboutUS : MasterModel
    {
        [Key]
        public int AboutId { get; set; }
        public string Titel { get; set; }
        public string Paragraph_ar { get; set; }
        public string imgurl { get; set; }
    }
}
