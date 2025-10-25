namespace JeCenterWeb.Models.ViewModel
{
    public class ReadedNotificationViewModel
    {
        public CNotifications? cNotifications {  get; set; } 
        public IEnumerable<NotificationUserSeen>? NotificationUserSeen { get; set; }

    }
}
