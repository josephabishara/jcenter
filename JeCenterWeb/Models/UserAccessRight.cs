using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeCenterWeb.Models
{
    public class UserAccessRight : MasterModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; } // Assuming ApplicationUser is the class for users

        [Required]
        [ForeignKey("AccessRight")]
        public int AccessId { get; set; }
        public virtual AccessRight AccessRight { get; set; } // Assuming AccessRight is the class for access rights
    }
}
