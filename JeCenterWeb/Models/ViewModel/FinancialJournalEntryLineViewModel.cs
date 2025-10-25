using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class FinancialJournalEntryLineViewModel
    {
        [Key]
        public int JournalEntryDetilsID { get; set; }

      

        [Display(Name = "رقم المستند")]
        public int FinancialDocumentId { get; set; }

        [Display(Name = "تاريخ")]
        public DateTime JournalEntryDate { get; set; }

        [Display(Name = "الحساب")]
        public int AccountID { get; set; }
        [Display(Name = "المصروف")]
        public string AccountName { get; set; }

        [Display(Name = "مدين")]
        public decimal Debit { get; set; }
        [Display(Name = "دائن")]
        public decimal Credit { get; set; }


        [Display(Name = "بيان")]
        public string Notes { get; set; }

        [Display(Name = "المستخدم")]
        public string FullName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int physicalyearId { get; set; }

        [Display(Name = "نوع العملية")]
        public int MovementTypeId { get; set; }
        [Display(Name = " أسم الفرع ")]
        public int? BranchId { get; set; }
    }
}
