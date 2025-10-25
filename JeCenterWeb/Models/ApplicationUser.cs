using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    [Index(nameof(Mobile))]
    [Index(nameof(FullName))]

    public class ApplicationUser : IdentityUser<int>
    {
        [Required]
        [Display(Name = " الاسم ")]
        public string FullName { get; set; }
        [Display(Name = " أسم الطالب / ة")]
        public string? FristName { get; set; }
        [Display(Name = " أسم الأب ")]
        public string? SecondName { get; set; }
        [Display(Name = " أسم الجد ")]
        public string? LastName { get; set; }
        [Display(Name = " أسم العائلة ")]
        public string? FamilyName { get; set; }

        [Display(Name = " العنوان ")]
        public string? Address { get; set; }
        [Display(Name = " الرقم القومي ")]
        public string? NationalID { get; set; }

        [Display(Name = " تاريخ الميلاد ")]
        public DateTime? Birthdate { get; set; }

        [Display(Name = " البريد الإليكتروني ")]
        public string? Email { get; set; }

        [Display(Name = " رقم الموبايل ")]
        public string Mobile { get; set; }

        [Display(Name = " المدرسة ")]
        public string? School { get; set; }
        //[Display(Name = " المرحلة ")]
        //public int PhaseId { get; set; } = 0;
        //[Display(Name = " النوع ")]
        //public int GenderId { get; set; } = 0;

        [Display(Name = " وظيفة ولي الأمر ")]
        public string? ParentJob { get; set; }
        [Display(Name = " رقم ولي الامر ")]
        public string? ParentMobile { get; set; }

        [Display(Name = " ولي الأمر ")]
        public int ParentId { get; set; } = 0;
        ///  Setting
        public int DepartmentId { get; set; } = 0;

        
        // 1 "AdminSettings", 2 "PowerUser", 3 "SuperUser", 4 "User", 5 "Teacher", 6 "Assistant", 7 "Student"
        [Display(Name = "نوع المستخدم")]
        public int UserTypeId { get; set; }

        public bool AdvancedPermission { get; set; } = false;

        [Display(Name = "كلمة المرور")]
        public string Pwd { get; set; }
        public string imgurl { get; set; }
        public int online { get; set; } = 0;
        public bool Active { get; set; } = true;

        public int AccountID { get; set; } = 0;
        public string? Note { get; set; }
        public int? CreateId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public ICollection<StudentApplications>? StudentApplications { get; set; }
        public ICollection<CGroups>? CGroups { get; set; }
        public ICollection<StudentDiscount>? StudentDiscount { get; set; }
        public ICollection<StudentLecture>? StudentLecture { get; set; }

        [Display(Name = " المرحلة ")]
        public int imageint { get; set; } = 0;
    }
}
