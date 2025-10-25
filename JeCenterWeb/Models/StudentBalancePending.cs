using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class StudentBalancePending
    {
        [Key]
        public int MyProperty { get; set; }

        [ForeignKey("FinancialAccount")]
        [Display(Name = "الحساب")]
        public int AccountID { get; set; }

        [Display(Name = "الحساب")]
        public FinancialAccount? FinancialAccount { get; set; }


        [Display(Name = " المدرس ")]
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public ApplicationUser? Teacher { get; set; }

        public int GroupId { get; set; }

        [Display(Name = "مدين")]
        public decimal Debit { get; set; }
        [Display(Name = "دائن")]
        public decimal Credit { get; set; }
    }
}
