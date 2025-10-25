using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeCenterWeb.Models
{
    public class CRooms : MasterModel
    {
        [Key]
        public int RoomId { get; set; }
        [Required]
        [Display(Name = " أسم القاعة ")]
        public string RoomName { get; set; }

        [Required]
        [Display(Name = " سعة القاعة ")]
        public string Capacity { get; set; }

        [Required]
        [Display(Name = " الدور ")]
        public string Floor { get; set; }

        [Display(Name = " الفرع ")]
        [ForeignKey("CBranch")]
        public int BranchId { get; set; }
        public CBranch? CBranch { get; set; }

        public ICollection<CGroups>? CGroups { get; set; }
        public ICollection<ReviewsSchedule>? ReviewsSchedule { get; set; }

        
    }
}
