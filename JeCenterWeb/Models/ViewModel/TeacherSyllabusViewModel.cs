using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class TeacherSyllabusViewModel
    {
        [Key]
        public int TeacherSyllabusId { get; set; }
        public int TeacherId { get; set; }
        [Display(Name = " اسم المادة / المنهج ")]
        public int SyllabusID { get; set; }
      
        [Display(Name = " اسم المادة / المنهج ")]
        public string Name { get; set; }
    }
}
