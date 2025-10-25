using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class LectureAbsentViewModel
    {
        [Key]
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
    }
}
