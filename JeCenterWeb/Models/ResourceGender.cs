using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class ResourceGender
    {
        [Key]
        public int GenderId { get; set; }
        public string GenderName { get; set; }
       // public string lecture { get; set; }

      //  public ICollection<CGroups>? CGroups { get; set; }
    }
}
