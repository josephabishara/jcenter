using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class SearchActiveByWordViewModel
    {
        public int Id { get; set; }

        [Display(Name = " الاسم ")]
        public string FullName { get; set; }

        [Display(Name = " رقم الموبايل ")]
        public string Mobile { get; set; }
        [Display(Name = "  الرصيد ")]
        public Decimal Balance { get; set; }
        public bool Active { get; set; }
    }
}
