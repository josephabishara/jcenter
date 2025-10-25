using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class FinancialAccount : MasterModel
    {
        [Key]
        public int FinancialAccountId { get; set; }
        [Display(Name = "المصروف")]
        public string AccountName { get; set; }
        public int AccountTypeId { get; set; }

        [Display(Name = "اخفاء")]
        public bool AdvancedPermission { get; set; } = true;
        //  public int UserId { get; set; }
        public ICollection<StudentDiscount>? StudentDiscount { get; set; }
        public ICollection<FinancialJournalEntryLine>? FinancialJournalEntryLine { get; set; }

    }
}
