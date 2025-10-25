using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class PhysicalYear : MasterModel
    {
        [Key]
        public int PhysicalyearId { get; set; }
        [Required]
        [Display(Name = " السنة الدراسية ")]
        public string PhysicalyearName { get; set; }
        [Required]
        [Display(Name = " من تاريخ ")]
        public DateTime FromDate { get; set; }
        [Required]
        [Display(Name = " الى تاريخ ")]
        public DateTime ToDate { get; set; }

        public ICollection<CApplicationsValue>? CApplicationsValue { get; set; }
        public ICollection<StudentApplications>? StudentApplications { get; set; }
        public ICollection<CGroups>? CGroups { get; set; }
        public ICollection<FinancialJournalEntryLine>? FinancialJournalEntryLine { get; set; }
        public ICollection<ReviewsSchedule>? ReviewsSchedule { get; set; }
    }
}
