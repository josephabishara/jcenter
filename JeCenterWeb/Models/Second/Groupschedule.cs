using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.Second
{
    public class Groupschedule
    {
        [Key]
        public int GroupscheduleID { get; set; }
        public int GroupnID { get; set; }
        public int Dayofweek { get; set; }
        public int Timefrom { get; set; }
        public int Timeto { get; set; }
        public string Note { get; set; }
        public int create_uid { get; set; }
        public DateTime create_date { get; set; }
        public int write_uid { get; set; }
        public DateTime write_date { get; set; }
        public int roomid { get; set; }
        public int lectureno { get; set; }
        public int countlectuere { get; set; }
        public decimal lecturePrice { get; set; }
        public DateTime GroupDate { get; set; }
        public decimal centerprofit { get; set; }
        public decimal teacherprofit { get; set; }
        public decimal centerservice { get; set; }
    }
}
