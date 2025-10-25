using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.Second
{
    public class Teachers
    {
        [Key]
        public int TeacherID { get; set; }
        public string TeacherName { get; set; }
        public DateTime Birthdate { get; set; }
        public string TeacherEmail { get; set; }
        public string Mobile { get; set; }
        public string Mobile2 { get; set; }
        public int Phase { get; set; }
        public string Schools { get; set; }
        public int City { get; set; }
        public int EducaAdmin { get; set; }
        public string TeacherPassword { get; set; }
        public int PhasesID { get; set; }
        public int curriculumID { get; set; }
        public string img { get; set; }
        public int create_uid { get; set; }
        public DateTime create_date { get; set; }
        public int write_uid { get; set; }
        public DateTime write_date { get; set; }
        public int Account { get; set; }
        public bool Active { get; set; }
    }
}
