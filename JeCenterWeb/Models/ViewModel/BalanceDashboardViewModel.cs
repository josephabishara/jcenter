using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class BalanceDashboardViewModel
    {
        public int Id { get; set; }
        [Display(Name = " الاسم ")]
        public string FullName { get; set; }
        [Display(Name = " الاسم ")]
        public string Imgurl { get; set; }

        [Display(Name = " الرصيد ")]
        public decimal Balance { get; set; }
    }
}
