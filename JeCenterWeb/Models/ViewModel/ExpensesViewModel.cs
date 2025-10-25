using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class ExpensesViewModel
    {
        public int AccountId { get; set; }
        public decimal Value { get; set; }
        public string Note { get; set; }

        public int TreasuryID  { get; set; }
        public int MovementTypeId  { get; set; }
        public DateTime JournalEntryDate  { get; set; }
        public int physicalyearId { get; set; }


        [Display(Name = " أسم الفرع ")]
        public int BranchId { get; set; }
        public IFormFile? imgurl { get; set; }
    }
}
