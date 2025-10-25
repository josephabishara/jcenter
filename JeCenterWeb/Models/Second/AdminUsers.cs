using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.Second
{
    public class AdminUsers
    {
        [Key]
        public int UserID { get; set; }
        public string FirsName { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime LastLogout { get; set; }
        public int GroupID { get; set; }
        public int CurrentStatus { get; set; }
        public bool MasterAdmin { get; set; }
        public bool isHRAdmin { get; set; }
        public string Confirmed { get; set; }
        public int create_uid { get; set; }
        public DateTime create_date { get; set; }
        public int write_uid { get; set; }
        public DateTime write_date { get; set; }
    }
}
