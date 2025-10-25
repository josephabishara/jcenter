using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class PaymentViewModel
    {
        public int AccountId { get; set; }
        public decimal Value { get; set; }
        public int GroupscheduleId { get; set; }

        public int TreasuryID { get; set; }
        public int MovementTypeId { get; set; }
        public DateTime JournalEntryDate { get; set; }
        public int physicalyearId { get; set; }
       
    }
}
