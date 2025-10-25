using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.Second
{
    public class Studentstatement
    {
        [Key]
        public int StudentstatementID { get; set; }
        public int StudentID { get; set; }
        public string? Code { get; set; }
        public int? statetype { get; set; }
        public decimal? Depit { get; set; }
        public decimal? Credit { get; set; }
        public DateTime? satatedate { get; set; }
        public int? lectureID { get; set; }
        public int? create_uid { get; set; }
        public DateTime? create_date { get; set; }
        public int? write_uid { get; set; }
        public DateTime? write_date { get; set; }
        public int? appCreate { get; set; }
        public int? appWrite { get; set; }
    }
}
