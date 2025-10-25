using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace JeCenterWeb.Models
{
    [Index(nameof(ReviewDate), nameof(ReviewTypeId))]
    public class ReviewsSchedule
    {
        [Key]
        public int ReviewsScheduleId { get; set; }
        [Display(Name = " المدرس  ")]
        public int TeacherId { get; set; }
        [Display(Name = " نوع المحاضرة  ")]
        public int ReviewTypeId { get; set; }
        
        [Display(Name = " اسم المحاضرة / الامتحان  ")]
        public string ReviewsScheduleName { get; set; }

        [Display(Name = " تاريخ  ")]
        public DateTime ReviewDate { get; set; }
        [Display(Name = " كود الشهر ")]
        public string MonthCode { get; set; }
        [Display(Name = " القاعة ")]
        [ForeignKey("CRooms")]
        public int RoomId { get; set; }
        [Display(Name = " القاعة ")]
        public CRooms? CRooms { get; set; }
        [Display(Name = " السنة الدراسية ")]
        [ForeignKey("PhysicalYear")]
        public int physicalyearId { get; set; }
        public PhysicalYear? PhysicalYear { get; set; }
        [Display(Name = " اسم المادة / المنهج ")]
        [ForeignKey("CSyllabus")]
        public int SyllabusID { get; set; }
        public CSyllabus? CSyllabus { get; set; }
        [Display(Name = " ق المدرس ")]
        public int TeacherFee { get; set; }
        [Display(Name = " ق المركز ")]
        public int CenterFee { get; set; }
        [Display(Name = " خ السنتر ")]
        public int ServiceFee { get; set; }
        [Display(Name = " قيمة المحاضرة ")]
        public int LecturePrice { get; set; }
        [Display(Name = " من ")]
        public TimeSpan SessionStart { get; set; }
        [Display(Name = " الى ")]
        public TimeSpan SessionEnd { get; set; }
        public bool Paided { get; set; }
        [Display(Name = "  مٌسدد ")]
        public decimal PaidedValue { get; set; }

        [Display(Name = " حالة المحاضرة ")]
        public bool Closed { get; set; }
        [Display(Name = "  رقم المحاضرة ")]
        public int LectureNo { get; set; }

        [Display(Name = " أسم الفرع ")]
        public int BranchId { get; set; }

        public int? PaidId { get; set; }

        public int CountAttend { get; set; } = 0;
        public DateTime? PaidDate { get; set; }
    }
}
