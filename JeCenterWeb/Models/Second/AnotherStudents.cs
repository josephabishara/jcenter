using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.Second
{
    public class AnotherStudents
    {
        [Key]
        public int StudentID { get; set; }
        public string? FristName { get; set; }
        public string? SecondName { get; set; }
        public string? LastName { get; set; }
        public string? FamilyName { get; set; }
        public string? NationalID { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? StudentEmail { get; set; }
        public string? StudentMobile { get; set; }
        public string? School { get; set; }
        public string? ParentName { get; set; }
        public string? ParentMobile { get; set; }
        public int? City { get; set; }
        public int? Phase { get; set; }
        public string? StudentPassword { get; set; }
        public string? confirmed { get; set; }
        public string? YearCode { get; set; }
        public string? img { get; set; }
        public string? Address { get; set; }
        public string? gender { get; set; }
        public string? parentjob { get; set; }
        public int? create_uid { get; set; }
        public DateTime? create_date { get; set; }
        public int? write_uid { get; set; }
        public DateTime? write_date { get; set; }
        public string? appcenter { get; set; }
        public int? active { get; set; }
        public int? online { get; set; }
        public string? sname { get; set; }
        public string? lname { get; set; }
    }
}
