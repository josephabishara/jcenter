using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeCenterWeb.Models
{
    [Index(nameof(LectureDate))]
    public class CGroupSchedule
    {
        [Key]
        public int GroupscheduleId { get; set; }

        [ForeignKey("CGroups")]
        public int GroupId { get; set; }
        public CGroups? CGroups { get; set; }

        [Display(Name = " رقم المحاضرة ")]
        public int LectureNo { get; set; }
        [Display(Name = " تاريخ محاضرة ")]
        public DateTime LectureDate { get; set; }
        [Display(Name = " كود الشهر ")]
        public string MonthCode { get; set; }
        [Display(Name = " حالة المحاضرة ")]
        public bool Closed { get; set; }
        [Display(Name = " حالة المحاضرة ")]
        public bool Paided { get; set; }
        [Display(Name = "  مٌسدد ")]
        public decimal PaidedValue { get; set; }
        public int? PaidId { get; set; }
        public DateTime? PaidDate { get; set; }
    }
}
