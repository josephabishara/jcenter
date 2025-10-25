using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class TeachersPayments
    {
        [Key]
        [Display(Name = "رقم المستند")]
        public int FinancialDocumentId { get; set; }
        [Display(Name = "قيمة")]
        public decimal Value { get; set; }


        [Display(Name = "بيان")]
        public string Notes { get; set; }

        [Display(Name = "المستخدم")]
        public string FullName { get; set; }

        [Display(Name = "تاريخ")]
        public DateTime JournalEntryDate { get; set; }
        [Display(Name = "توقيت")]

        public DateTime? CreatedDate { get; set; }
        [Display(Name = "مستند")]
        public string MovementTypeName { get; set; }
        [Display(Name = "  الفرع ")]
        public int BranchId { get; set; }
        public int GroupscheduleId { get; set; }
    }
}
