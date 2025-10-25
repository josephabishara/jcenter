using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class ChargingCard
    {
        [Key]
        public int ChargingCardId { get; set; }
        public string CardCode { get; set; }
        public decimal CardValue { get; set; }
        public DateTime ChargingDate { get; set; }
        public int StudentID { get; set; }
        public bool State { get; set; }

        public int Cardwriteid { get; set; }

    }
}
