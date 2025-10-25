using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class ChargeValueViewModel
    {
        [Required]
        public int id { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public int BranchId { get; set; }
    }
}
