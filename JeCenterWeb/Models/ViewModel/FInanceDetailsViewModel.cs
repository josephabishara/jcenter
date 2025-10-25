using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class FInanceDetailsViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = " تاريخ ")]
        public int Date { get; set; }
        [Display(Name = "الحساب")]
        public int AccountID { get; set; }

        [Display(Name = " الاسم ")]
        public string? FullName { get; set; }

        [Display(Name = " بيان ")]
        public string? Notes { get; set; }

        [Display(Name = "مدين")]
        public decimal Debit { get; set; }
        [Display(Name = "دائن")]
        public decimal Credit { get; set; }
    }
}
