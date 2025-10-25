using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class NotificationType : MasterModel
    {

        [Key]
        public int NotificationTypeId { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
