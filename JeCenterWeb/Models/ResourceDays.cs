using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class ResourceDays
    {
        [Key]
        public int DayOfWeekId { get; set; }
        [Display(Name = "يوم")]
        public string DayOfWeekName { get; set; }
        public ICollection<CGroups>? CGroups { get; set; }
    }
}
