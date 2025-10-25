using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeCenterWeb.Models
{
    public class CNotifications : MasterModel
    {
        [Key]
        public int NotificationId { get; set; }

        

        [Display(Name = " العنوان ")]
        [Required]
        public string NotiTitel { get; set; }


        [Display(Name = " الاشعار ")]
        [Required]
        public string NotificationNote { get; set; }

        public ICollection<UsersNotifications>? UsersNotifications { get; set; }

    }
}
