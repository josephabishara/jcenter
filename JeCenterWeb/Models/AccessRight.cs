using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class AccessRight : MasterModel
    {
        [Key]
        public int AccessId { get; set; }

        [Required]
        [Display(Name = "Access Name")]
        public string AccessName { get; set; }


        [Required]
        [Display(Name = "Controller Name")]
        public string ControllerName { get; set; }

        [Required]
        [Display(Name = "Action Name")]
        public string ActionName { get; set; }

      
        [Display(Name = "Menu Icon")]
        public string? MenuIcon { get; set; }

        [Display(Name = "Menu Order")]
        public int? MenuOrder { get; set; }


    }
 
}
