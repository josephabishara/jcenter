using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class FinancialJournalEntryViewModel
    {
         
        [Key]

        [Display(Name = "رقم المستند")]
        public int FinancialDocumentId { get; set; }
       
        [Display(Name = "تاريخ")]
        public DateTime JournalEntryDate { get; set; }

        [Display(Name = "الحساب")]
        public int AccountID { get; set; }
        [Display(Name = "الحساب")]
        public string AccountName { get; set; }

        [Display(Name = "قيمة")]
        public decimal Value { get; set; }
        
        [Display(Name = "بيان")]
        public string Notes { get; set; }

        [Display(Name = "المستخدم")]
        public string FullName { get; set; }

        [Display(Name = "نوع العملية")]
        public int MovementTypeId { get; set; }

        [Display(Name = "نوع العملية")]
        public string MovementTypeName { get; set; }
        [Display(Name = "  الفرع ")]
        public string BranchName { get; set; }

        [Display(Name = " الحالة  ")]
        public int Approve { get; set; }

        public string? imgurl { get; set; }
        public bool? Attached { get; set; } = false;
        [Display(Name = "رقم الخزينة")]
        public int TreasuryID { get; set; }

        public int physicalyearId { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
