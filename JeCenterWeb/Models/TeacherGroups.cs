using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeCenterWeb.Models
{
    public class TeacherGroups : MasterModel
    {
        [Key]
        public int TeacherGroupId { get; set; }
        [ForeignKey("CGroups")]
        public int GroupId { get; set; }
        public CGroups? CGroups { get; set; }

        public int TeacherId { get; set; }
    }
}
