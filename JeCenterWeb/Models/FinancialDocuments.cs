using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeCenterWeb.Models
{
    [Index(nameof(JournalEntryDate), nameof(physicalyearId),nameof(TreasuryID), nameof(AccountID))]

    public class FinancialDocuments : MasterModel
    {
        [Key]
        [Display(Name = "رقم المستند")]
        public int FinancialDocumentId { get; set; }

        [Display(Name = "تاريخ")]
        public DateTime JournalEntryDate { get; set; }

        [Display(Name = "بيان")]
        public string Notes { get; set; }

        [Display(Name = "الخزينة")]
        public int TreasuryID { get; set; }

        [Display(Name = "الحساب")]
        public int AccountID { get; set; }

        [Display(Name = "قيمة")]
        public decimal Value { get; set; }

        [Display(Name = "نوع العملية")]
        [ForeignKey("MovementType")]
        public int MovementTypeId { get; set; }
        public MovementType? MovementType { get; set; }

        [Display(Name = " السنة الدراسية ")]
        public int physicalyearId { get; set; }

        public bool Receipted { get; set; }

        public int Approve { get; set; } = 1;

        [Display(Name = "تاريخ الاستلام")]
        public DateTime ReceiptDate { get; set; }

        public int GroupscheduleId { get; set; }

        [Display(Name = " أسم الفرع ")]
        public int BranchId { get; set; }

        public string? imgurl  { get; set; }
        public bool? Attached { get; set; } = false;
        public ICollection<FinancialJournalEntryLine>? FinancialJournalEntryLine { get; set; }

    }
}
