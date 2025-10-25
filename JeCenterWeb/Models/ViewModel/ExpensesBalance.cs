using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class ExpensesBalance
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = " الاسم ")]
        public string FullName { get; set; }
        [Display(Name = " مدين ")]
        public decimal SumDebit { get; set; }
        [Display(Name = " دائن ")]
        public decimal SumCredit { get; set; }
        [Display(Name = " الرصيد ")]
        public decimal Balance { get; set; }
        public int UserTypeId { get; set; }
        public int PhysicalyearId { get; set; }

    }
}
