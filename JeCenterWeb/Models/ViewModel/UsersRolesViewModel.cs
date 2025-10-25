using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class UsersRolesViewModel
    {
        public int UserId { get; set; }
        public int OldRoleId { get; set; }
        public int RoleId { get; set; }
        [Display(Name = " مراجع  ")]
        public bool AdvancedPermission { get; set; } = false;

    }
}
