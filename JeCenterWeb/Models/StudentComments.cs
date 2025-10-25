using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class StudentComments : MasterModel
    {
        [Key]
        public int StudentCommentId { get; set; }

        [Display(Name = " الطالب /ة ")]
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        public ApplicationUser? Student { get; set; }

        public string? Note { get; set; }
        public int ParentId { get; set; } = 0;

    }
}
