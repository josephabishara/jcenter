using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.Second
{
    public class Clint
    {
        [Key]
        public int ClintID { get; set; }
        public string? ClintEmail { get; set; }
        public string?    ClintName { get; set; }
        public int Department { get; set; }
        public string? ClintPassword { get; set; }
        public bool Admin { get; set; }
        public int head_color { get; set; }
        public int font_color { get; set; }
        public string? Confirmed { get; set; }
        public string? img { get; set; }
        public string? mobile { get; set; }

        public int create_uid { get; set; }
        public DateTime create_date { get; set; }
        public int write_uid { get; set; }
        public DateTime write_date { get; set; }
        public int Treasury { get; set; }
    }
}
