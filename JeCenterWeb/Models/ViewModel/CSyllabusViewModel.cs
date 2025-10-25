using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace JeCenterWeb.Models.ViewModel
{
    public class CSyllabusViewModel
    {
        [Required]
        [Display(Name = " اسم المادة / المنهج ")]
        public string SyllabusName { get; set; }

        [Display(Name = " المرحلة ")]
        public int PhaseID { get; set; }
        [Display(Name = " الصورة ")]
        public IFormFile? imgurl { get; set; }
    }
}
