using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class MovementType : MasterModel
    {
        [Key]
        public int MovementTypeId { get; set; }
        public string MovementTypeName { get; set; }
     //   public ICollection<FinancialJournalEntryLine>? FinancialJournalEntryLine { get; set; }

    }
}
