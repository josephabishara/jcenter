using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class ResourceCity
    {
        [Key]
        public int CityId { get; set; }
        public string CityName { get; set; }

    }
}
