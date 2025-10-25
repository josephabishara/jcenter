using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class GNotifications : MasterModel
    {
        [Key]
        public int NotificationId { get; set; }

 


        [Display(Name = " المجموعة ")]
        [ForeignKey("CGroups")]

        public int GroupId { get; set; }
        public CGroups? CGroups { get; set; }


        [Display(Name = " العنوان ")]
        [Required]
        public string NotiTitel { get; set; }


        [Display(Name = " الاشعار ")]
        [Required]
        public string NotificationNote { get; set; }

        public ICollection<StudentNotifications>? StudentNotifications { get; set; }
    }
}
