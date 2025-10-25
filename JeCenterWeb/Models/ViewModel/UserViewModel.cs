using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class UserViewModel
    {
        [Key]
        public int RegisterId { get; set; }
        [Display(Name = "Full Name")]
        [Required]
        public string? FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Job Titel")]
        public string JobTitle { get; set; }

        public int Gender { get; set; }

        [Display(Name = "Admin Settings")]
        public bool AdminSettings { get; set; }
        [Display(Name = "Access Rights")]
        public bool AccessRights { get; set; }
        [Display(Name = "Sales Manager")]
        public bool SalesManager { get; set; }
        [Display(Name = "Sales")]
        public bool SalesUser { get; set; }
        [Display(Name = "Purchases Manager")]
        public bool PurchasesManager { get; set; }
        [Display(Name = "Purchases")]
        public bool PurchaseUser { get; set; }
        [Display(Name = "Inventory Manager")]
        public bool InventoryManager { get; set; }
        [Display(Name = "Inventory")]
        public bool InventoryUser { get; set; }
        [Display(Name = "Finance Adviser")]
        public bool FinanceAdviser { get; set; }
        [Display(Name = "FinanceManager")]
        public bool FinanceManager { get; set; }
        [Display(Name = "Finance Accountant")]
        public bool FinanceAccountant { get; set; }
        [Display(Name = "Treasury")]
        public bool FinanceTreasury { get; set; }
        [Display(Name = "HR Manager")]
        public bool HRManager { get; set; }
        [Display(Name = "HR")]
        public bool HRUser { get; set; }
        [Display(Name = "Recruitment")]
        public bool Recruitment { get; set; }
    }
}
