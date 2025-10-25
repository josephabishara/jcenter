using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.Second
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public string? UserEmail { get; set; }
        public string? UserName { get; set; }
        public int Department { get; set; }
        public string? UserPassword { get; set; }
        public bool Admin { get; set; }
        public string? Confirmed { get; set; }
        public string? img { get; set; }
        public string? mobile { get; set; }
        public int create_uid { get; set; }
        public DateTime? create_date { get; set; }
        public int write_uid { get; set; }
        public DateTime? write_date { get; set; }
        public int Treasury { get; set; }
    }
}
