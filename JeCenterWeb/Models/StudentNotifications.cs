using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class StudentNotifications : MasterModel
    {
        [Key]
        public int StudentNotificationId { get; set; }


        //[ForeignKey("User")]
        public int StudentId { get; set; }
        //public ApplicationUser? User { get; set; }


        [ForeignKey("GNotifications")]
        public int NotificationId { get; set; }
        public GNotifications? GNotifications { get; set; }

        public bool Seen { get; set; }

      
    }
}
