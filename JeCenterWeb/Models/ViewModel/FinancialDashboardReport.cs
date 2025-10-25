using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class FinancialDashboardReport
    {
        [Key]
        public Int64 Id { get; set; }

        [Display(Name = " نوع المصروف  ")]
        public int MovementTypeId { get; set; }
        [Display(Name = " نوع المصروف  ")]
        public string? MovementTypeName { get; set; }
        [Display(Name = "  الفرع ")]
        public int BranchId { get; set; }
        [Display(Name = " التاريخ  ")]
        public DateTime JournalEntryDate { get; set; }

        [Display(Name = "مدين")]
        public decimal Debit { get; set; }
        [Display(Name = "دائن")]
        public decimal Credit { get; set; }

        [Display(Name = " السنة الدراسية ")]
        public int physicalyearId { get; set; }


    }
}
