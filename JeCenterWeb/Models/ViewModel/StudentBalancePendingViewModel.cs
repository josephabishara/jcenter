using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class StudentBalancePendingViewModel
    {
        [Key]
        public Int64 Id { get; set; }
        public int AccountID { get; set; }
        public int TeacherId { get; set; }
        [Display(Name = " اسم المدرس ")]
        public string?    FullName { get; set; }
        public int GroupId { get; set; }
        [Display(Name = " أسم المجموعة ")]
        public string? GroupName { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal balance { get; set; }
    }
}
