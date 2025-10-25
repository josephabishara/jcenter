using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class NotificationViewModel
    {
        public int NotificationId { get; set; }

        [Display(Name = " نوع الاشعار ")]
       
        public int NotificationTypeId { get; set; }

        public int GroupId { get; set; }

        [Display(Name = " العنوان ")]
        [Required]
        public string NotiTitel { get; set; }


        [Display(Name = " الاشعار ")]
        [Required]
        public string NotificationNote { get; set; }

        
    }
}
