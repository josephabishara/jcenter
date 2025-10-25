using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class SysLoginLog
    {
        [Key]
        public int SysLoginLogId { get; set; }

        public string? MacAddress { get; set; }
        public string? Mac { get; set; }
        public string? Ip { get; set; }
        public string? Path { get; set; }
        public string? os { get; set; }
        public string? browsr { get; set; }
        public string? device { get; set; }
        public string? user { get; set; }
        public string? password { get; set; }

    }
}
