using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class CBranch : MasterModel
    {
        [Key]
        public int BranchId { get; set; }
        [Required]
        [Display(Name = " أسم الفرع ")]
        public string BranchName { get; set; }

        [Display(Name = "عدد القاعات")]
        public int RoomsCount { get; set; }
        public ICollection<CRooms>? CRooms { get; set; }
        //  public ICollection<StudentApplications>? StudentApplications { get; set; }
    }
}
