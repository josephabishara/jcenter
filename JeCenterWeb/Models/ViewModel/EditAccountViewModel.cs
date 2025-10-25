using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class EditAccountViewModel
    {
        public int AccountID { get; set; }
        [Display(Name = "اسم الحساب ")]
        public string AccountName { get; set; }
    }
}
