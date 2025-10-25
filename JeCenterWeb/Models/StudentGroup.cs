using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeCenterWeb.Models
{
    public class StudentGroup :MasterModel
    {
        [Key]
        public int StudentGroupId { get; set; }
        [ForeignKey("CGroups")]
        public int GroupId { get; set; }
        public CGroups? CGroups { get; set; }
        public int StudentId { get; set; }
    }
}
