using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.Second
{
    public class oldstudent
    {
        [Key]
        public int StudentID { get; set; }
        public string? FristName { get; set; }
        public string? SecondName { get; set; }
        public string? LastName { get; set; }
        public string? FamilyName { get; set; }
        public string? StudentMobile { get; set; }
        public string? StudentPassword { get; set; }
        public DateTime Birthdate { get; set; }
        public string? Address { get; set; }
        public string? School { get; set; }
        public string? NationalID { get; set; }
        public string? parentjob { get; set; }
        public string? ParentMobile { get; set; }

    }
}
