using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class UsersNotifications : MasterModel
    {
        [Key]
        public int UsersNotificationId { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }
        public ApplicationUser? User { get; set; }


        [ForeignKey("CNotifications")]
        public int NotificationId { get; set; }
        public CNotifications? CNotifications { get; set; }

        public bool Seen { get; set; }

    }
}
