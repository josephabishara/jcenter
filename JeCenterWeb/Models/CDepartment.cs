using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class CDepartment : MasterModel
    {
        [Key]
        public int DepartmentId { get; set; }
        [Required]
        [Display(Name = " أسم الإدارة ")]
        public string DepartmentName { get; set; }
    }
}
