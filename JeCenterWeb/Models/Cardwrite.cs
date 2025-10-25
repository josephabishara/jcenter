using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class Cardwrite
    {
        [Key]
        public int Cardwriteid { get; set; }
        [Required]
        [Display(Name = " عدد ")]
        public int CardCount { get; set; }

        [Required]
        [Display(Name = " فئة ")]
        public int Cardvalue { get; set; }

        public bool Printed { get; set; }

    }
}
