using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class StudentBalancePendingList
    {
        [Key]
       public int AccountID { get; set; }
       public string? AccountName { get; set; }
       public int TeacherId { get; set; }
        [Display(Name = " اسم المدرس ")]
        public string? TeacherName { get; set; }
       public int GroupId { get; set; }
        [Display(Name = " المجموعة ")]
        public string GroupName { get; set; }
       public decimal Debit { get; set; }
       public decimal Credit { get; set; }
        [Display(Name = " محجوز ")]
        public decimal Balance { get; set; }
    }
}
