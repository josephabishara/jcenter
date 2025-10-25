using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeCenterWeb.Models
{
    public class FinancialJournalEntryLine : MasterModel
    {
        [Key]
        public int JournalEntryDetilsID { get; set; }

       
        [Display(Name = "رقم المستند")]
        [ForeignKey("FinancialDocuments")]
        public int FinancialDocumentId { get; set; }
        public FinancialDocuments? FinancialDocuments { get; set; }


       
        [Display(Name = "تاريخ")]
        public DateTime JournalEntryDate { get; set; }

       

        [ForeignKey("FinancialAccount")]
        [Display(Name = "الحساب")]
        public int AccountID { get; set; }
        [Display(Name = "الحساب")]
        public FinancialAccount? FinancialAccount { get; set; }


        [Display(Name = "مدين")]
        public decimal Debit { get; set; }
        [Display(Name = "دائن")]
        public decimal Credit { get; set; }

      

        [Display(Name = " السنة الدراسية ")]
        [ForeignKey("PhysicalYear")]
        public int physicalyearId { get; set; }
        [Display(Name = " أسم الفرع ")]
        public int? BranchId { get; set; }
        public PhysicalYear? PhysicalYear { get; set; }

    }
}
