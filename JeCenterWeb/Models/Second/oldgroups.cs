using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.Second
{
    public class oldgroups
    {
        [Key]
        public int GroupID { get; set; }
        public string GroupnName { get; set; }
        public int Active { get; set; }
        public int TeacherID { get; set; }
        public int curriculumID { get; set; }
        public int GroupNo { get; set; }
        public int create_uid { get; set; }
        public DateTime create_date { get; set; }
        public int write_uid { get; set; }
        public DateTime write_date { get; set; }
        public string gender { get; set; }
        public int countlectuere { get; set; }
        public int physicalyearID { get; set; }
    }
}
