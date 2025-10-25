using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeCenterWeb.Models
{
    public class TeachesVideos
    {
        [Key]
        public int TeacheVideoId { get; set; }
        [Display(Name = " المدرس  ")]
        public int TeacherId { get; set; }

        [Display(Name = " اسم الفيديو   ")]
        public string VideoName { get; set; }
        [Display(Name = " اسم الفيديو   ")]
        public string VideoUrl { get; set; }

        [Display(Name = " اسم المادة / المنهج ")]
        [ForeignKey("CSyllabus")]
        public int SyllabusID { get; set; }
        public CSyllabus? CSyllabus { get; set; }

        [Display(Name = " ق المدرس ")]
        public int TeacherFee { get; set; }
        [Display(Name = " ق المركز ")]
        public int CenterFee { get; set; }
       
        [Display(Name = " قيمة الفيديو ")]
        public int LecturePrice { get; set; }
    }
}
